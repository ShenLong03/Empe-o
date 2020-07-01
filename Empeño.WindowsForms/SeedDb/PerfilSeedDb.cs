using Empeño.CommonEF.Entities;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empeño.WindowsForms.SeedDb
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
