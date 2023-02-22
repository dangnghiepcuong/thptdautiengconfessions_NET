namespace THPTDTCFS.Models
{
    public class DataContext
    {
        private string connectionString;
        public DataContext()
        {
            connectionString = "Data source=localhost; Initial Catalog=Contacts; Integrated Security=True; Database=thptdtcfs_voucher_event";
        }

        public string ConnectionString { get { return connectionString; } set { connectionString = value; } }
    }
}
