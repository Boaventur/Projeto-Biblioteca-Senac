using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/login");
            }
        }
    
    public static bool verificaloginsenha(string login, string senha, Controller controller)
    {

        verificaseUsuarioAdminExiste();

        senha = Criptografia.TextoCriptografado(senha);

        using(BibliotecaContext bc = new BibliotecaContext())
        {
            IQueryable<Usuario> usuarioencontrado = bc.usuarios.Where(u => u.login == login && u.senha == senha);

            List<Usuario> listaUsuarios = usuarioencontrado.ToList();

            if(listaUsuarios.Count == 0 )
            {
                return false;
            }
        else
        {
            controller.HttpContext.Session.SetString("login", listaUsuarios[0].login);
            controller.HttpContext.Session.SetInt32("tipo", listaUsuarios[0].tipo);
            return true;
        }
        }
    }
    
     public static void verificaseUsuarioeAdmin( Controller controller)
     {
        if(controller.HttpContext.Session.GetInt32("tipo") == Usuario.PADRAO )
        {
            controller.Request.HttpContext.Response.Redirect("/Usuario/Permissao");
        }

     }
    
    public  static void verificaseUsuarioAdminExiste()
    {
        using(BibliotecaContext bc = new BibliotecaContext())
        {
        IQueryable<Usuario> userAdmin = bc.usuarios.Where(u => u.login == "admin");
        if(userAdmin.ToList().Count == 0)
        {
            Usuario novoAdmin = new Usuario();
            novoAdmin.Nome = "Administrador";
            novoAdmin.login= "admin";
            novoAdmin.senha = Criptografia.TextoCriptografado("123");
            novoAdmin.tipo =  Usuario.ADMIN;

            bc.usuarios.Add(novoAdmin);
            bc.SaveChanges();
        }
        
        
        }
    }
    
    }
}