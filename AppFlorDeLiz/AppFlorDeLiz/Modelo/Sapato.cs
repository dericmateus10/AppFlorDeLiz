using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace AppFlorDeLiz.Modelo
{
    public class Sapato
    {
        [PrimaryKey, AutoIncrement]
        public long? SapatoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public byte[] Foto { get; set; }

        [ForeignKey(typeof(Colecao))]
        public long? ColecaoId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Colecao Colecao { get; set; }
    }
}
