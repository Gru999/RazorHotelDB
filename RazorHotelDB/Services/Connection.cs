namespace RazorHotelDB.Services
{
    public abstract class Connection
    {
        protected String connectionString;
        public IConfiguration Configuration { get; }

        public Connection(IConfiguration configuration)
        {
            connectionString = Secret.ConnectionString;
            Configuration = configuration;
            //connectionString = Configuration["ConnectionStrings:SimplyStorage"];
        }
    }
}
