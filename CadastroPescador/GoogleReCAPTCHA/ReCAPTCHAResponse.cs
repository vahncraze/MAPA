using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.GoogleReCAPTCHA
{
    public class ReCAPTCHAResponse
    {
        public bool success { get; set; }
        public float score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
        public Dictionary<string, string> error { get; set; }
    }
}
