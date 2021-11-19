using ASPtareaNuñez.Models;
using ASPtareaNuñez.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASPtareaNuñez.Controllers
{
    public class HomeController : Controller
    {
        BD baseD = new BD();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public List<cliente> getcliente()
        {
            List<cliente> listaClientes = new List<cliente>();
            List<invoice> listaFacturas = new List<invoice>();
            List<invoice_detail> detalleFactura = new List<invoice_detail>();

            cliente clientes = new cliente();

            clientes.ID = 1; 
            clientes.primerNombre = "Bryan";
            clientes.segundoNombre = "Steven";
            clientes.documento_ID = 1005279055;

            listaClientes.Add(clientes);

            invoice factura = new invoice();

            factura.ID = 1;
            factura.Id_Cliente = 1;
            factura.Cod = 1;

            listaFacturas.Add(factura);

            invoice_detail detalle = new invoice_detail();

            detalle.ID = 1;
            detalle.Id_invoce = 1;
            detalle.descripcion = "compra de telefono movil";
            detalle.valor = 20;

            detalleFactura.Add(detalle);


            clientes.listaFactura = listaFacturas;
            factura.detallesFactura = detalleFactura;
            return listaClientes;
        }

        public string insertarLista([FromBody] cliente Clientes)
        {
            string sql = "insert into cliente (ID,primerNombre,segundoNombre,documento_ID) values("+ Clientes.ID+",'"+ Clientes.primerNombre+"','"+Clientes.segundoNombre+"',"+Clientes.documento_ID+");";

            string resultado = baseD.ejecutarSQL(sql);
            return resultado;
        }

        public string insertarFactura([FromBody] invoice factura)
        {
            string sql = "insert into invoice(Id_Cliente,Cod) values("+factura.Id_Cliente+","+factura.Cod+");";

            sql += "select @@identity as ID;" + Environment.NewLine;

            foreach(invoice_detail item in factura.detallesFactura)
            {
                sql += "insert into invoice_detail (Id_invoce,descripcion,valor) values (@@identity,'"+item.descripcion+"',"+item.valor+");";
            }
            string resultado = baseD.ejecutarSQL(sql);

            return resultado;
        }

        public List<cliente> todosClientes()
        {
            List<cliente> listaClientes = new List<cliente>();

            DataTable tabla = baseD.ejecutarSQL1($"select * from cliente");

            listaClientes = (from DataRow datos in tabla.Rows select new cliente()
            {
                ID = Convert.ToInt32(datos["ID"]),
                primerNombre = Convert.ToString(datos["primerNombre"]),
                segundoNombre = Convert.ToString(datos["segundoNombre"]),
                documento_ID = Convert.ToInt32(datos["documento_ID"])

            }).ToList();
            return listaClientes;
        }

        public List<cliente> mostrarCliente(int id)
        {
            List<cliente> listaClientes = new List<cliente>();

            DataTable tabla = baseD.ejecutarSQL1($"select * from cliente where ID = {id}");

            listaClientes = (from DataRow datos in tabla.Rows
                             select new cliente()
                             {
                                 ID = Convert.ToInt32(datos["ID"]),
                                 primerNombre = Convert.ToString(datos["primerNombre"]),
                                 segundoNombre = Convert.ToString(datos["segundoNombre"]),
                                 documento_ID = Convert.ToInt32(datos["documento_ID"])

                             }).ToList();
            return listaClientes;
        }

        public List<invoice> MostrarFacturas(int id)
        {
            List<invoice> listaFacturas = new List<invoice>();
            List<invoice_detail> detallesFactura = new List<invoice_detail>();

            invoice_detail detalles = new invoice_detail();

            DataTable factura = baseD.ejecutarSQL1($"select * from invoice where ID = {id}");
            DataTable detalle = baseD.ejecutarSQL1($"select * from invoice_detail");


            detallesFactura = (from DataRow datos in detalle.Rows
                             select new invoice_detail()
                             {
                                 ID = Convert.ToInt32(datos["ID"]),
                                 Id_invoce = Convert.ToInt32(datos["Id_invoce"]),
                                 descripcion = Convert.ToString(datos["descripcion"]),
                                 valor = Convert.ToInt32(datos["valor"])
                             }).ToList();

            listaFacturas = (from DataRow datos in factura.Rows
                             select new invoice()
                             {
                                 ID = Convert.ToInt32(datos["ID"]),
                                 Id_Cliente = Convert.ToInt32(datos["Id_Cliente"]),
                                 Cod = Convert.ToInt32(datos["Cod"]),
                                 detallesFactura = detallesFactura
                             }).ToList();

            return listaFacturas;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
