﻿using preguntaods.Entities;

namespace preguntaods.Persistencia
{
    internal class ConexionBD
    {
        private static ConexionBD _instance;
        private const string SupabaseUrl = "https://ilsulfckdfhvgljvvmhb.supabase.co";
        private const string SupabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imlsc3VsZmNrZGZodmdsanZ2bWhiIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzY5ODI1MTksImV4cCI6MTk5MjU1ODUxOX0.9Uab8BACftEFrG90mMNJ_b6XTk9biLGn8IkyS3oIIoE";

        public Supabase.Client Cliente;
        public Supabase.Gotrue.User UsuarioBD;
        public Usuario UsuarioApp;

        private ConexionBD()
        {
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            Cliente = new Supabase.Client(SupabaseUrl, SupabaseKey, options);
            Cliente.InitializeAsync();
        }

        public static ConexionBD GetInstance()
        {
            return _instance ??= new ConexionBD();
        }
    }
}