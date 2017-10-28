using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace AppFlorDeLiz.Modelo
{
    public class Colecao
    {
        [PrimaryKey, AutoIncrement]
        public long? ColecaoId { get; set; }
        public string Nome { get; set; }
        public byte[] Foto { get; set; }

        [OneToMany]
        public List<Sapato> Itens { get; set; }

        public override bool Equals(object obj)
        {
            Colecao colecao = obj as Colecao;
            if (colecao == null)
            {
                return false;
            }

            return (ColecaoId.Equals(colecao.ColecaoId));
        }

        public override int GetHashCode()
        {
            return ColecaoId.GetHashCode();
        }
    }
}
