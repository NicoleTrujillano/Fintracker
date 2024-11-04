using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Models
{
    public class ItensVenda
    {
        public int IdVenda { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        // Construtor da classe ItensVenda
        public ItensVenda(int idVenda, int idProduto, int quantidade, decimal preco)
        {
            this.IdVenda = idVenda;
            this.IdProduto = idProduto;
            this.Quantidade = quantidade;
            this.Preco = preco;
        }
        public ItensVenda() { }
    }
}
