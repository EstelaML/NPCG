namespace preguntaods.Persistencia
{
    internal class SingletonConexion
    {
        private static SingletonConexion instance;
        private const string supabaseURL = "https://ilsulfckdfhvgljvvmhb.supabase.co";
        private const string supabaseKEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imlsc3VsZmNrZGZodmdsanZ2bWhiIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzY5ODI1MTksImV4cCI6MTk5MjU1ODUxOX0.9Uab8BACftEFrG90mMNJ_b6XTk9biLGn8IkyS3oIIoE";
        
        public Supabase.Client cliente;
        public Supabase.Gotrue.User usuario;
        private SingletonConexion()
        {
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            cliente = new Supabase.Client(supabaseURL, supabaseKEY, options);
            cliente.InitializeAsync();
        }

        public static SingletonConexion GetInstance()
        {
            instance ??= new SingletonConexion();

            return instance;
        }


    }
}
