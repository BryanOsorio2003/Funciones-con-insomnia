using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPtareaNuñez.Models.Entities
{
    public class invoice
    {
        public int ID { get; set; }
        public int Id_Cliente { get; set; }
        public int Cod { get; set; }

        public List<invoice_detail> detallesFactura { get; set; }
    }
}
