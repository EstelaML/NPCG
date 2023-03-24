using Postgrest;
using Supabase;
using System;


namespace preguntaods.Persistencia
{
    internal class SingletonConexion
    {
        private static SingletonConexion instance;
        private const string supabaseURL = "https://ilsulfckdfhvgljvvmhb.supabase.co";
        private const string supabaseKEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imlsc3VsZmNrZGZodmdsanZ2bWhiIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTY3Njk4MjUxOSwiZXhwIjoxOTkyNTU4NTE5fQ.5jq8QpYDhX2TeuiZZR9_DE41w28fgnpLpr127UkYKTA";
        
        public Supabase.Client cliente;
        private  SingletonConexion()
        {
            var url = Environment.GetEnvironmentVariable(supabaseURL);
            var key = Environment.GetEnvironmentVariable(supabaseKEY);

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            cliente = new Supabase.Client(url, key, options);
            cliente.InitializeAsync();
        }

        public static SingletonConexion getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonConexion();
            }

            return instance;
        }


    }
}
