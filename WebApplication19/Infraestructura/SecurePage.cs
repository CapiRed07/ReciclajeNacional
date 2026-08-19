using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication19.logica
{
    public class SecurePage
    {
        //Propiedad que permite a cada página definir quién tiene acceso a ella.
        //Por defecto, cualquier usuario logueado puede acceder (""), pero se puede personalizar en cada página que herede de SecurePage.
        public string RolRequerido { get; set; } = ""; // Por defecto, cualquier usuario logueado puede acceder.

        //Esta clase se encarga de ser un checkeo general que las demas páginas pueden heredar para verificar si el usuario está logueado y tiene permisos para ver la página.
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //Se asegura de que el cache del navegador no almacene la página para que no se pueda acceder a ella después de cerrar sesión.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Expires = -1;

            if (HttpContext.Current.Session == null ||
                Session["IsLoggedIn"] == null ||
                !(bool)Session["IsLoggedIn"])
            {
                Response.Clear();
                Response.Redirect("~/Login.aspx", true);
                return;
            }
            
            //Validacion del rol guardado en la sesion duranto el login
            if (!string.IsNullOrEmpty(RolRequerido))
            {
                //Extrae el rol del usuario de la sesión
                string rolUsuario = Session["Rol"] != null ? Session["Rol"].ToString() : string.Empty;

                //Verifica si el rol del usuario coincide con el rol requerido
                if (rolUsuario != RolRequerido)
                {
                    Response.Clear();
                    Response.Redirect("~/Unauthorized.aspx", true);
                    return;
                }
            }
        }
    }
}