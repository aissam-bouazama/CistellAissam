﻿using CistellAissam.Models;
using System.Text.Json;

namespace CistellAissam.Utils
{
    public class SessionUtils
    {
        public static List<Cistella> ObtenerProductosSession(HttpContext httpcontext)
        {
            if (httpcontext.Session.GetString("productescistella") != null)
            {
                List<Cistella> lista = JsonSerializer.Deserialize<List<Cistella>>(httpcontext.Session.GetString("productescistella"));
                return lista;
            }
            else
            {
                return null;
            }

        }
        public static void GuardarProductosSession(HttpContext httpcontext, List<Cistella> lista)
        {
            httpcontext.Session.SetString("productescistella", JsonSerializer.Serialize(lista));
        }
        public static void IncrementarContadorCesta(HttpContext httpcontext)
        {
            var quantitatcista = 1;
            if (httpcontext.Session.GetInt32("Contador") != null)
            {
                quantitatcista = (int)httpcontext.Session.GetInt32("Contador") + 1;
            }
            ActualizarContadorCesta(httpcontext, quantitatcista);
        }
        public static void ActualizarContadorCesta(HttpContext httpcontext, int contador)
        {
            httpcontext.Session.SetInt32("Contador", contador);
        }


        public static UsuariLogin ObtenerUsuariAuth(HttpContext httpcontext)
        {
            var user = httpcontext.Session.GetString("usuarisession");


            if (user != null)
            {
                var userauth = JsonSerializer.Deserialize<UsuariLogin>(user);
                return userauth;
            }
            return null;
        }

        public static void AfegirUsuariSessio(HttpContext httpcontext, UsuariLogin usuari)
        {
            httpcontext.Session.SetString("usuarisession", JsonSerializer.Serialize(usuari));
            httpcontext.Session.Remove(usuari.email);
        }
    }
}
