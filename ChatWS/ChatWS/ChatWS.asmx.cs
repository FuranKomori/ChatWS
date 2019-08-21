using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ChatWS
{
    /// <summary>
    /// Descripción breve de ChatWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ChatWS : System.Web.Services.WebService
    {
        [WebMethod(Description = "Obtiene el mensaje final")]
        public List<MensajeDTO> ObtenerMensajes(string Receptor, string Emisor)
        {
            List<MensajeDTO> final = new List<MensajeDTO>();
            ChatDBEntities ctx = new ChatDBEntities();
            var listaTemp = (from p in ctx.Mensajes
                             where (p.Emisor.Equals(Emisor) /*|| p.Receptor.Equals(Receptor)*/)
                             orderby p.Fecha
                             select p).ToList();
            foreach (var item in listaTemp)
            {
                final.Add(new MensajeDTO()
                {
                    Receptor = item.Receptor,
                    Emisor = item.Emisor,
                    Mensaje = item.Texto
                });
            }
            return final;

        }

        [WebMethod(Description ="Obtiene el mensaje final")]
        public void GuardaMensaje(MensajeDTO msg)
        {
            
            ChatDBEntities ctx = new ChatDBEntities();
            Mensajes nuevo_mensaje = new Mensajes()
            {
                Receptor = msg.Receptor,
                Emisor = msg.Emisor,
                Texto = msg.Mensaje,
                Fecha = DateTime.Now
            };
            ctx.Mensajes.Add(nuevo_mensaje);
            ctx.SaveChanges();
        }
    }
}
