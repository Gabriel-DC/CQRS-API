using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Infra.StoreContext.DataContext
{
    public sealed class StoreDbContext : IDisposable
    {        
        public SqlConnection Connection { get; set; }

        public StoreDbContext()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }
        public void Dispose()
        {
            if(Connection.State != ConnectionState.Closed)
                Connection.Close();

            GC.SuppressFinalize(this);
        }
    }
}
