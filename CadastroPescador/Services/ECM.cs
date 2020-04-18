using CadastroPescador.Models;
using CadastroPescador.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CadastroPescador.DTO;
using System.IO;

namespace CadastroPescador.Services
{
    public class ECM : IECM
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        protected string token { get; set; }
        private string diretorioLog = @"C:\Osas\Ellus\ECM\Temp\";
        private string nomeArquivoLog = "_LogCadastroExterno.txt";


        public ECM(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        public TokenResponse ObterToken()
        {
            var api = _configuration["API"];
            try
            {
                SalvarLog("");
                var responseConverted = new TokenResponse();

                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(new
                {
                    usuario = "apiLogin",
                    senha = "apiLogin"
                });

                SalvarLog("Solicitando Token");
                var response = client.PostAsync(api + "/auth/token", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SalvarLog("Token solicitado com sucesso.");
                    var jsonString = response.Result.Content.ReadAsStringAsync();
                    responseConverted = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(jsonString.Result);
                    token = responseConverted.token;
                }
                return responseConverted;
            }
            catch (Exception e)
            {
                throw new Exception($"Falha ao obter Token : {e.Message}");
            }
        }

        public bool CriarUsuario(UsuarioExternoModel usuario)
        {
            SalvarLog("Início criação de usuário para o CPF/CNPJ " + usuario.RetornaCpfCnpj());
            var api = _configuration["API"];
            var client = _clientFactory.CreateClient();
            var grupo = new int[] { 25 };
            var obs = $"Lotação: {usuario.Lotacao} | Matricula: {usuario.Matricula}";
            var data = JsonConvert.SerializeObject(new
            {
                Nome = usuario.Nome,
                Login = usuario.Login,
                Email = usuario.Email,
                CpfCnpj = usuario.RetornaCpfCnpj(),
                Grupo = grupo,
                Observacao = obs

            });
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            SalvarLog("Executando api usuarios.");
            var response = client.PostAsync($"{api}/v2/usuarios/", new StringContent(data, Encoding.UTF8, "application/json"));
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = response.Result.Content.ReadAsStringAsync();
                var responseConverted = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(jsonString.Result);
                SalvarLog("Api usuarios executada. Resultado: " + response.Result.StatusCode);
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<DocumentoDTO> ExisteUsuario(UsuarioExternoModel usuario)
        {
            SalvarLog("Início Verificação de usuário existente.");
            var api = _configuration["API"];
            var client = _clientFactory.CreateClient();
            var camposPesquisar = new List<object>();
            camposPesquisar.Add(usuario.ChecarUsuario());
            camposPesquisar.Add(usuario.ChecarGrupo());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            SalvarLog("Executando Api usuario existente.");
            var response = client.PostAsync($"{api}/v2/documentos/pesquisar/18/quantidade/1/retornarAnexos/false", new StringContent(JsonConvert.SerializeObject(camposPesquisar), Encoding.UTF8, "application/json"));

            SalvarLog("Api usuario existente executada. Resultado: " + response.Result.StatusCode);
            var result = new List<DocumentoDTO>();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = response.Result.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<List<DocumentoDTO>>(jsonString.Result);
                SalvarLog("Api usuarios executada. Resultado: " + response.Result.StatusCode);
                return result;
            }
            else
            {
                if (response.Result.StatusCode != System.Net.HttpStatusCode.NotFound)//se diferente notfound significa que deu erro.
                {
                    SalvarLog("Api retornou " + response.Result.StatusCode);
                    throw new Exception("Erro ao realizar cadastro. Status: " + response.Result.StatusCode);
                }

            }

            return result;
        }
        public void IniciarProcesso(int processoId, int documentoId)
        {
            SalvarLog("Início iniciar Processo.");
            var api = _configuration["API"];
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            SalvarLog("Executando api processos.");
            var response = client.PostAsync($"{api}/v2/processos/{processoId}/documentos/{documentoId}/iniciar", new StringContent("", Encoding.UTF8, "application/json"));

            if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                SalvarLog("Api executada. Resultado: " + response.Result.StatusCode);
                var jsonString = response.Result.Content.ReadAsStringAsync();
                var responseConverted = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(jsonString.Result);
                SalvarLog("Api iniciar processo executada. Resultado: " + response.Result.StatusCode);
            }
            else
            {
                SalvarLog("Api executada com erro. Resultado: " + response.Result.StatusCode);
            }
        }

        public bool CriarDocumento(UsuarioExternoModel usuario)
        {
            SalvarLog("Início criar Documento.");
            var api = _configuration["API"];
            var client = _clientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(usuario.ToDocumentoDTO());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            SalvarLog("Executando api documentos.");
            var response = client.PostAsync($"{api}/v2/documentos?pastaId={159975}&tipoDocumentoId={18}", new StringContent(data, Encoding.UTF8, "application/json"));
            SalvarLog("Api executada. Resultado: " + response.Result.StatusCode);
            if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                DocumentoVersao result;
                var jsonString = response.Result.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<DocumentoVersao>(jsonString.Result);
                var documentoId = 0;
                var processoId = 36;

                documentoId = result.DocumentoId;

                IniciarProcesso(processoId, documentoId);
                return true;
            }
            else
            {
                SalvarLog("Api retornou" + response.Result.StatusCode);
                throw new Exception("Erro ao realizar cadastro. Status: " + response.Result.StatusCode);
            }
        }

        public void SalvarLog(string mensagem)
        {
            StreamWriter arquivoLog;
            string tempNomeArquivo = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + RetornaDiaFormatado(DateTime.Now.Day) + nomeArquivoLog;
            if (!Directory.Exists(diretorioLog))
            {
                Directory.CreateDirectory(diretorioLog);
            }

            arquivoLog = new StreamWriter(diretorioLog + tempNomeArquivo, true);

            try
            {
                if (mensagem == "")
                {
                    arquivoLog.WriteLine("");
                    arquivoLog.WriteLine("---------------------------------------------------------------------------------");
                    arquivoLog.WriteLine("");
                }
                else
                {
                    arquivoLog.WriteLine(DateTime.Now + " - " + mensagem);
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                arquivoLog.Flush();
                arquivoLog.Close();
            }

        }
        private string RetornaDiaFormatado(int dia)
        {
            if (dia < 10)
            {
                return "0" + dia.ToString();
            }
            else
            {
                return dia.ToString();
            }
        }
    }
}
