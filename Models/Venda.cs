using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinTracker.Models
{
    public class Venda
    {
        public int IdVenda { get; set; }
        public int IdCliente { get; set; }
        public String nomeCliente { get; set; }
        public string Metodo { get; set; }
        public string Status { get; set; }
        public int Parcelas { get; set; }
        public List<String> produtos { get; set; }
        public int QuantidadeProdutos { get; set; }
        public DateTime DataTransacao { get; set; }
        public string data { get; set; }
        public decimal TotalPreco { get; set; }

        public Venda(int idVenda, string nomeCliente, string metodo, string status, int parcelas,
                 List<string> produtos, int quantidadeProdutos, DateTime dataTransacao, decimal totalPreco)
        {
            idVenda = idVenda;
            nomeCliente = nomeCliente;
            metodo = metodo;
            this.Status = status;
            this.Parcelas = parcelas;
            this.produtos = produtos ?? new List<string>(); // Inicializa com uma lista vazia se null
            this.QuantidadeProdutos = quantidadeProdutos;
            this.DataTransacao = dataTransacao;
            this.TotalPreco = totalPreco;
        }
        public Venda()
        {

        }
    }

    

}
