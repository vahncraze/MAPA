using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CadastroPescador.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using CadastroPescador.Enums;
using CadastroPescador.Services;
using CadastroPescador.Extensions;
using CadastroPescador.GoogleReCAPTCHA;
using Newtonsoft.Json;
using CadastroPescador.DTO;

namespace CadastroPescador.Controllers
{
    public class HomeController : Controller
    {

        private readonly IECM _ecm;
        private readonly IConfiguration _configuration;
        private readonly ReCAPTCHAServices _reCAPTCHAServices;

        public HomeController(IECM ecm, IConfiguration configuration, ReCAPTCHAServices reCAPTCHAServices)
        {
            _ecm = ecm;
            _configuration = configuration;
            _reCAPTCHAServices = reCAPTCHAServices;
        }

        public IActionResult Index()
        {

            List<SelectListItem> listaGrupo = GerarLista();
            string habilitarPescadorAmador = _configuration["HabilitarPescadorAmador"];
            if (habilitarPescadorAmador == "false")
            {
                SelectListItem remover = new SelectListItem();
                foreach (var item in listaGrupo)
                {
                    if (item.Value == "8")
                    {
                        remover = item;
                    }
                }
                listaGrupo.Remove(remover);
            }
            ViewBag.GrupoLista = listaGrupo;
            ViewBag.EmConstrucaoLista = GerarListaSimNao();
            ViewBag.TipoPessaoLista = GerarListaTipoPessoa();
            ViewBag.Formulario = GerarListaFormulario();
            ViewBag.Api = _configuration["API"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UsuarioExternoModel usuario)
        {
            try
            {
                //var recaptcha = await _reCAPTCHAServices.Check(usuario.Token);

                //if (recaptcha.score <= 0.5)
                //{
                //    return View("Error");
                //}
                DTO.TokenResponse resultadoToken = _ecm.ObterToken();
                var existeUsuario = _ecm.ExisteUsuario(usuario);

                if (existeUsuario.Count > 0)
                {
                    var documentoLogin = new DocumentoLoginModel();
                    existeUsuario.ForEach(x => documentoLogin = x.ToDocumentoLoginModel());
                    return View("VerificaUsuario", documentoLogin);
                }

                bool criarUsuario = _ecm.CriarUsuario(usuario);
                if (criarUsuario)
                {
                    var documento = _ecm.CriarDocumento(usuario);
                    return View("Sucesso", usuario);
                }

                return View("ErrorGeneric");
            }
            catch (Exception ex)
            {
                _ecm.SalvarLog(ex.Message);
                return RedirectToAction("ErrorApi", new { msgEx = ex.Message });
            }


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ErrorApi(string msgEx)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, msg = msgEx });
        }

        private static List<SelectListItem> GerarLista()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Selecione", Selected = true });
            foreach (var item in (int[])Enum.GetValues(typeof(GruposTrabalho)))
            {
                var grupo = new SelectListItem();
                grupo.Value = item.ToString();

                if (item == 1)
                    grupo.Text = "Entidade Colaboradora";
                if (item == 2)
                    grupo.Text = "Pescador Profissional";
                if (item == 3)
                    grupo.Text = "Embarcação";
                if (item == 4)
                    grupo.Text = "Público Interno";
                if (item == 5)
                    grupo.Text = "INSS";
                if (item == 6)
                    grupo.Text = "Agente Validador";
                if (item == 7)
                    grupo.Text = "Empresa Rastreadora";
                if (item == 8)
                    grupo.Text = "Pescador Amador";
                if (item == 9)
                    grupo.Text = "SisTainha";

                list.Add(grupo);
            }

            return list;
        }

        private static List<SelectListItem> GerarListaSimNao()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Selecione", Selected = true });
            foreach (var item in (int[])Enum.GetValues(typeof(EmConstrucao)))
            {
                var grupo = new SelectListItem();
                grupo.Value = item.ToString();

                if (item == 1)
                    grupo.Text = "Sim";
                if (item == 2)
                    grupo.Text = "Não";

                list.Add(grupo);
            }

            return list;
        }

        private static List<SelectListItem> GerarListaTipoPessoa()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Selecione", Selected = true });
            foreach (var item in (int[])Enum.GetValues(typeof(TipoPessoa)))
            {
                var grupo = new SelectListItem();
                grupo.Value = item.ToString();

                if (item == 1)
                    grupo.Text = "Pessoa Jurídica";
                if (item == 2)
                    grupo.Text = "Pessoa Física";

                list.Add(grupo);
            }

            return list;
        }

        private static List<SelectListItem> GerarListaFormulario()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Selecione", Selected = true });
            foreach (var item in (int[])Enum.GetValues(typeof(FormularioTainha)))
            {
                var formulario = new SelectListItem();
                formulario.Value = item.ToString();

                if (item == 1)
                    formulario.Text = "Cerco Traineira";
                if (item == 2)
                    formulario.Text = "Emalhe Anilhado";
                if (item == 3)
                    formulario.Text = "Empresa Pesqueira";

                list.Add(formulario);
            }

            return list;
        }

        [HttpPost]
        public IActionResult RetornaCheckUsuarioDTO(UsuarioExternoModel documento)
        {
            return Json(new List<object>() {
                documento.ChecarUsuario(),
                documento.ChecarGrupo()
            });
        }

        [HttpPost]
        public async Task<IActionResult> RetornaUsuarioDTO(UsuarioExternoModel usuario)
        {
            var homeController = this;
            int[] numArray = new int[1] { 25 };
            string str = JsonConvert.SerializeObject(new
            {
                Nome = usuario.RetornarNome(),
                Login = usuario.Login,
                Email = usuario.Email,
                CpfCnpj = usuario.RetornaCpfCnpj(),
                Grupo = numArray,
                Observacao = usuario.RetornarObservacao()
            }); ;
            return homeController.Json(str);
        }

        [HttpPost]
        public async Task<IActionResult> RetornaDocumentoDTO(UsuarioExternoModel documento)
        {
            return Json(documento.ToDocumentoDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Sucesso(UsuarioExternoModel usuario)
        {
            return View("Sucesso", usuario);
        }

        [HttpPost]
        public async Task<IActionResult> AvisoUsuarioExistente(DocumentoDTO usuario)
        {
            return RedirectToAction("VerificaUsuario", usuario.ToDocumentoLoginModel());
        }

        [HttpGet]
        public IActionResult VerificaUsuario(DocumentoLoginModel usuario)
        {
            return View(usuario);
        }

        [HttpGet]
        public IActionResult RetornarRgp2020()
        {
            string listaRgp2020 = _configuration["ListaRgp2020"];
            return Ok(listaRgp2020.Split(','));
        }
    }
}
