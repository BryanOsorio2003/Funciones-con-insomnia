using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPtareaNuñez.Models.Entities
{
    public class invoice_detail
    {
        public int ID { get; set; }
        public int Id_invoce { get; set; }
        public string descripcion { get; set; }
        public int valor { get; set; }

    }
}
