﻿using FinTracker.Interfaces;
using FinTracker.Models;
using FinTracker.TelasPrincipais;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FinTracker.BD
{
    public class ClienteRepository
    {
        private string _connectionString;

        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ClienteRepository()
        {
            
        }

        public void AddCliente(object cliente)
        {
            Cliente c = (Cliente)cliente;
            MySqlConnection conn = MetodosDB.conexao();
            string query = "INSERT INTO cliente (Nome, Data_de_Cadastro, CNPJ, Endereco, Bairro, Cidade, Estado, CEP, Telefone, Email, Status) " +
                           "VALUES (@Nome, @DataCadastro, @CNPJ, @Endereco, @Bairro, @Cidade, @Estado, @CEP, @Telefone, @Email, @Status)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nome", c.Nome);
            cmd.Parameters.AddWithValue("@DataCadastro", c.Data_de_Cadastro);
            cmd.Parameters.AddWithValue("@CNPJ", c.CNPJ);
            cmd.Parameters.AddWithValue("@Endereco", c.Endereco);
            cmd.Parameters.AddWithValue("@Bairro", c.Bairro);
            cmd.Parameters.AddWithValue("@Cidade", c.Cidade);
            cmd.Parameters.AddWithValue("@Estado", c.Estado);
            cmd.Parameters.AddWithValue("@CEP", c.CEP);
            cmd.Parameters.AddWithValue("@Telefone", c.Telefone);
            cmd.Parameters.AddWithValue("@Email", c.Email);
            cmd.Parameters.AddWithValue("@Status", c.Status);
            cmd.ExecuteNonQuery();
        }

        public DataTable GetClientes()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Cliente", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void UpdateCliente(object cliente)
        {
            Cliente c = (Cliente)cliente;
            MySqlConnection conn = MetodosDB.conexao();
            string query = "UPDATE Cliente SET Nome = @Nome, Data_de_Cadastro = @DataCadastro, CNPJ = @CNPJ, Endereco = @Endereco, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, CEP = @CEP, Telefone = @Telefone, Email = @Email, Status = @Status WHERE id_Cliente = @idCliente";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idCliente", c.id_Cliente);
            cmd.Parameters.AddWithValue("@Nome", c.Nome);
            cmd.Parameters.AddWithValue("@DataCadastro", c.Data_de_Cadastro);
            cmd.Parameters.AddWithValue("@CNPJ", c.CNPJ);
            cmd.Parameters.AddWithValue("@Endereco", c.Endereco);
            cmd.Parameters.AddWithValue("@Bairro", c.Bairro);
            cmd.Parameters.AddWithValue("@Cidade", c.Cidade);
            cmd.Parameters.AddWithValue("@Estado", c.Estado);
            cmd.Parameters.AddWithValue("@CEP", c.CEP);
            cmd.Parameters.AddWithValue("@Telefone", c.Telefone);
            cmd.Parameters.AddWithValue("@Email", c.Email);
            cmd.Parameters.AddWithValue("@Status", c.Status);
            cmd.ExecuteNonQuery();
            
        }

        public void DeleteCliente(int idCliente)
        {
            MySqlConnection conn = MetodosDB.conexao();
            string query = "DELETE FROM Cliente WHERE id_Cliente = @idCliente";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idCliente", idCliente);
            cmd.ExecuteNonQuery();
        }

        public void DeleteListaCliente(List<int> idClientes)
        {
            foreach (int id in idClientes)
                DeleteCliente(id);
        }

        public DataTable pesquisarEmTudo(string strBusca)
        {
            MySqlConnection conn = MetodosDB.conexao();
            string query = "select * from cliente " +
                $"WHERE id_Cliente = '{strBusca}' or Nome LIKE '%{strBusca}%' or CNPJ LIKE '%{strBusca}%' or Endereco LIKE '%{strBusca}%' or Bairro LIKE '%{strBusca}%' or " +
                $"Cidade LIKE '%{strBusca}%' or Estado LIKE '%{strBusca}%' or CEP LIKE '%{strBusca}%' or Telefone LIKE '%{strBusca}%' or Email LIKE '%{strBusca}%' or " +
                $"Status LIKE '%{strBusca}%' or Data_de_Cadastro like '%{strBusca}%'";

            try
            {

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
            return null;
        }
    }
}
