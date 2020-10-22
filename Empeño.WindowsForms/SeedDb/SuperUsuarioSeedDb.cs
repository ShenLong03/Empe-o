using Empeño.CommonEF.Entities;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empeño.WindowsForms.SeedDb
{
    public class SuperUsuarioSeedDb : IDisposable
    {
        public static DataContext _context = new DataContext();

        public SuperUsuarioSeedDb()
        {

        }

        public static void CheckSuperUsuario()
        {
            try
            {
                var superUsuario = _context.User.Where(u => u.Usuario == "Admin").FirstOrDefault();
                if (superUsuario==null)
                {
                    _context.Empleados.Add(new Empleado
                    {
                        Activo=true,
                        Usuario="Admin",
                        Correo="djarquin@amarusystems.com",
                        IsDelete=false,
                        Nombre="Administrador",
                        Telefono="89241262"
                    });
                    _context.User.Add(new User
                    {
                        Activo = true,
                        Codigo = "1973",
                        Password = "AdminAdmin2020",
                        PerfilId = 1,
                        Usuario = "Admin",
                    });
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
