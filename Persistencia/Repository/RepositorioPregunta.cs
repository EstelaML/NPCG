﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Microsoft.EntityFrameworkCore;
using preguntaods.Entities;
using preguntaods.Persistencia;
using preguntaods.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    public class RepositorioPregunta : Repository<RetoPregunta>
    {
        public async Task<IEnumerable<RetoPregunta>> GetByDificultad(string dificultad)
        {
            var a = SingletonConexion.GetInstance();
            var response = await a.cliente
                .From<RetoPregunta>()
                .Where(Xamarin => Xamarin.Dificultad == dificultad).Get();

            return response.Models.AsEnumerable();
        }
    }       
}