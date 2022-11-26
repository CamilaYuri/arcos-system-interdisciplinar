using arcos_system.Dados;
using arcos_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arcos_system.Dados
{
    public class DBInitializer
    {
        public static void Initialize(Context context)
        {
           
                if (context.Database.EnsureCreated())
                {
                    //Criar usuário admin
                    context.Usuarios.Add(
                        new Usuario
                        {
                            Nome = "Administrador",
                            NomeLogin = "admin",
                            Senha = "123",
                            Tipo = "adm",    
                        });
                context.Usuarios.Add(
                       new Usuario
                       {
                           Nome = "Usuario",
                           NomeLogin = "usuario",
                           Senha = "123",
                           Tipo = "user",
                       });



                context.SaveChanges();
                }
            }

        
    }
}
