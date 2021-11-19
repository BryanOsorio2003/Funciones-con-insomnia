using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPtareaNuñez.Models.Entities
{
    public class cliente
    {
        public int ID { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public int documento_ID { get; set; }

        public List<invoice> listaFactura { get; set; }
    }
}
