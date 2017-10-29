using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFlorDeLiz.Infraestructure;
using AppFlorDeLiz.Modelo;
using SQLite.Net;
using Xamarin.Forms;

namespace AppFlorDeLiz.Dal
{
    public class SapatoDAL
    {
        private SQLiteConnection sqlConnection;

        public SapatoDAL()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Sapato>();
        }

        public void Add(Sapato sapato)
        {
            sqlConnection.Insert(sapato);
        }

        public void DeleteById(long id)
        {
            sqlConnection.Delete<Sapato>(id);
        }

        public void Update(Sapato sapato)
        {
            sqlConnection.Update(sapato);
        }
    }
}
