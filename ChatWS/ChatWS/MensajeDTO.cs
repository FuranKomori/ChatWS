using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatWS
{
    public class MensajeDTO
    {
        public string Receptor { get; set; }
        public string Emisor { get; set; }

        public string Mensaje { get; set; }
    }
}