﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFlorDeLiz.Modelo;
using AppFlorDeLiz.Infraestructure;
using SQLite.Net;
using Xamarin.Forms;

namespace AppFlorDeLiz.Dal
{
    public class ClienteDAL
    {
        private SQLiteConnection sqlConnection;

        public ClienteDAL()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Cliente>();
        }

        public void Add(Cliente cliente)
        {
            sqlConnection.Insert(cliente);
        }

        public void DeleteById(long id)
        {
            sqlConnection.Delete<Cliente>(id);
        }

        public void Update(Cliente cliente)
        {
            sqlConnection.Update(cliente);
        }

        public IEnumerable<Cliente> GetAll()
        {
            return (from t in sqlConnection.Table<Cliente>() select t).OrderBy(i => i.Nome).ToList();
        }
    }
}
