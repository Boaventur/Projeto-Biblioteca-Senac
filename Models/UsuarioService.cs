using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.usuarios.ToList();
            }

        }

        public Usuario Listar(int id)
        {

            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.usuarios.Find(id);
            }
        }
        public void Inserir(Usuario usuario)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.usuarios.Add(usuario);
                bc.SaveChanges();
            }
        }

        public void Editar(Usuario usuario)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuarios = bc.usuarios.Find(usuario.id);

                if (usuarios.senha != usuario.senha)
                {
                    usuario.senha = Criptografia.TextoCriptografado(usuario.senha);
                }
                else
                {
                    usuarios.senha = usuario.senha;
                }
                usuarios.Nome = usuario.Nome;
                usuarios.login = usuario.login;
                usuarios.tipo = usuario.tipo;

                bc.SaveChanges();
            }
        }
        public void Excluir(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {

                bc.usuarios.Remove(bc.usuarios.Find(id));
                bc.SaveChanges();
            }
        }

    }
}