using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFlorDeLiz.Infraestructure;
using AppFlorDeLiz.Modelo;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;

namespace AppFlorDeLiz.Dal
{
    public class ColecaoDAL
    {
        private SQLiteConnection sqlConnection;

        public ColecaoDAL()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Colecao>();
        }

        public IEnumerable<Colecao> GetAll()
        {
            return (from t in sqlConnection.Table<Colecao>() select t).OrderBy(i => i.Nome).ToList();
        }

        public IEnumerable<Colecao> GetAllWithChildren()
        {
            return sqlConnection.GetAllWithChildren<Colecao>().OrderBy(i => i.Nome).ToList();
        }

        public Colecao GetItemById(long id)
        {
            return sqlConnection.Table<Colecao>().FirstOrDefault(t => t.ColecaoId == id);
        }

        public void DeleteById(long id)
        {
            sqlConnection.Delete<Colecao>(id);
        }

        public void Add(Colecao colecao)
        {
            sqlConnection.Insert(colecao);
        }

        public void Update(Colecao colecao)
        {
            sqlConnection.Update(colecao);
        }
    }
}
