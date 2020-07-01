//using Empeño.Common.Entities;
using Empeño.Forms.Data;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empeño.Forms.SeedDb
{
    public class ClienteSeedDb : IDisposable
    {
        public static DataContext _context = new DataContext();

        public ClienteSeedDb()
        {

        }

        public static void CheckClientes()
        {
            try
            {
                if (!_context.Clientes.Any())
                {
                    for (int i = 0; i < 10; i++)
                    {
                    //    _context.Clientes.Add(new Cliente
                    //    {
                    //        Activo=true,
                    //        Comentario=string.Empty,
                    //        Identificacion=i.ToString(),
                    //        Correo=$"usuario{i}@gmail.com",
                    //        Direccion="Costa Rica",
                    //        Nombre=$"Usuario{i}",
                    //        Telefono="50689241262"
                    //    });
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
