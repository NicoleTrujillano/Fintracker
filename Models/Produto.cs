using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }          // Chave primária, identificador do produto
        public string Nome { get; set; }            // Nome do produto
        public string Categoria { get; set; }       // Categoria do produto
        public string Descricao { get; set; }       // Descrição do produto
        public int Quantidade { get; set; }         // Quantidade em estoque
        public decimal ValorUnitario { get; set; }  // Valor unitário do produto
        public decimal ValorTotal
        {
            get
            {
                return Quantidade * ValorUnitario;
            }
        }  // Valor total (calculado como Quantidade * ValorUnitario)

        // Construtor vazio
        public Produto() { }

        // Construtor com parâmetros
        public Produto(int idProduto, string nome, string categoria, string descricao, int quantidade, decimal valorUnitario)
        {
            IdProduto = idProduto;
            Nome = nome;
            Categoria = categoria;
            Descricao = descricao;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }
        public Produto(int idProduto, string nome, int quantidade, decimal valorUnitario)
        {
            IdProduto = idProduto;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }
    }
}
