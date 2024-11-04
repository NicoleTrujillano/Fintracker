using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Models
{
    public class Cliente
    {
        public int id_Cliente;
        public string Nome;
        public string Data_de_Cadastro;
        public string CNPJ;
        public string Endereco;
        public string Bairro;
        public string Cidade;
        public string Estado;
        public string CEP;
        public string Telefone;
        public string Email;
        public string Status;

        public Cliente(String nome, string Data_de_Cadastro, String cnpj, String endereco, String bairro, String cidade, String estado, String cep, String telefone, String email, String status)
        {
            this.Nome = nome;
            this.CNPJ= cnpj;
            this.Data_de_Cadastro = Data_de_Cadastro;
            this.Endereco = endereco;
            this.Bairro = bairro;
            this.Cidade = cidade;
            this.Estado = estado;
            this.CEP = cep;
            this.Telefone = telefone;
            this.Email = email;
            this.Status= status;
        }
        public Cliente(int id, String nome)
        {
            this.id_Cliente = id;
            this.Nome = nome;
        }
        public Cliente()
        {
            this.Status = "ativo";
        }
    }
}
