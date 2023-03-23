namespace RazorHotelDB.Services
{
    public abstract class Connection
    {
        protected String connectionString;
        public IConfiguration Configuration { get; }

        public Connection()
        {
            connectionString = Secret.ConnectionString;
        }
        public Connection(string constring)
        {
            connectionString = constring;
        }
        public Connection(IConfiguration configuration)
        {
            connectionString = Secret.ConnectionString;


            //Used for testing exception handling
            //connectionString = Secret.ErrorconnectionString;
            Configuration = configuration;
        }
    }
}
