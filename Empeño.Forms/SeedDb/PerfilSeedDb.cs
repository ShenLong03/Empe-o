using Empeño.Common.Entities;
using Empeño.Forms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empeño.Forms.SeedDb
{
    public class PerfilSeedDb : IDisposable
    {
        public static DataContext _context = new DataContext();

        public PerfilSeedDb()
        {

        }

        public static void CheckPerfiles()
        {
            try
            {
                if (!_context.Perfil.Any())
                {
                    _context.Perfil.Add(new Perfil { Nombre = "SuperUsuario" });
                    _context.Perfil.Add(new Perfil { Nombre = "Administrador" });
                    _context.Perfil.Add(new Perfil { Nombre = "Supervisor" });
                    _context.Perfil.Add(new Perfil { Nombre = "Empleado" });
                 
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
