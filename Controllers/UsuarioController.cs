using System.IO;
using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace Biblioteca.Controllers
{
  public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
             Autenticacao.verificaseUsuarioeAdmin(this);
            return View();
        }
   
       [HttpPost]  
       public IActionResult Cadastro(Usuario user)
        {
            user.senha = Criptografia.TextoCriptografado(user.senha);
            new UsuarioService().Inserir(user);
            return RedirectToAction("ListaUsuario");
        }
        public IActionResult ListaUsuario()
        {
             Autenticacao.CheckLogin(this);
             Autenticacao.verificaseUsuarioeAdmin(this);
            return View(new UsuarioService().Listar());
       }  
        public IActionResult EditarUsuario(int id)
        {
            Autenticacao.CheckLogin(this);
             Autenticacao.verificaseUsuarioeAdmin(this);
            Usuario u = new UsuarioService().Listar(id);
            return View(u);
        }
        [HttpPost]
        public IActionResult EditarUsuario(Usuario UserEditado)
        {
            Autenticacao.CheckLogin(this);
             Autenticacao.verificaseUsuarioeAdmin(this);
            new UsuarioService().Editar(UserEditado);
            return RedirectToAction("ListaUsuario");
        }
        public IActionResult ExcluirUsuario(int id)
        {
           Autenticacao.CheckLogin(this);
             Autenticacao.verificaseUsuarioeAdmin(this);
             new UsuarioService().Excluir(id);
            return RedirectToAction("ListaUsuario");
        }
        public IActionResult Permissao()
        {
           Autenticacao.CheckLogin(this);
            return View();
        }
        
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Home");
        }

    }  
}

    
