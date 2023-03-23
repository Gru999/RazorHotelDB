namespace RazorHotelDB.Services
{
    public static class Secret
    {

		private static string _connectionString = "Data Source=mssql4.unoeuro.com;Initial Catalog=grubak_com_db_storage;User ID=grubak_com;Password=EebFz4t5fwyxD2pB6cdn;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

		public static string ConnectionString
		{
			get { return _connectionString; }
			
		}
	}
}
