
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Atividade_pacientes
{
    internal class paciente
    {
        conex cx = new conex();
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public bool Preferencial { get; set; }

        public void Add()
        {
            Console.Write("Nome: ");
            Nome = Console.ReadLine();

            Console.Write("Idade: ");
            Idade = int.Parse(Console.ReadLine());

            Console.Write("preferencial (s/n)? ");
            Preferencial = Console.ReadLine().ToLower() == "s";

            Console.WriteLine($"Nome: {Nome} | Idade: {Idade} | Preferencial: {Preferencial}\n");

            using (MySqlConnection cn = cx.conx())
            {
                string sql = "INSERT INTO pacientes (nome, idade, preferencial) VALUES (@nm, @ida, @pre)";
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@nm", Nome); //2
                cmd.Parameters.AddWithValue("@ida", Idade); //3
                cmd.Parameters.AddWithValue("@pre", Preferencial); //4
                cmd.ExecuteNonQuery();
            }
        }


        public void List()
        {

            using (MySqlConnection cn = cx.conx())
            {
                string sql = "SELECT * FROM pacientes ORDER BY preferencial DESC, horario ASC";
                MySqlCommand cmd = new MySqlCommand(sql, cn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows) //espaço data leitor banco linha 6
                    {
                        Console.WriteLine("Nenhum paciente na fila.");
                        return;
                    }

                    Console.WriteLine("\nLISTA DE PACIENTES NA FILA:");
                    int pos = 1;

                    while (dr.Read())
                    {
                        Console.WriteLine($"{pos} - ID {dr["id"]}: {dr["nome"]} | Idade: {dr["idade"]} | Preferencial: {dr["preferencial"]}");
                        pos++;
                    }
                }
            }
        }

        public void Alt()
        {
            List();
            using (MySqlConnection cn = cx.conx())
            {
                Console.WriteLine("Digite o ID do paciente");
                int novoId = int.Parse(Console.ReadLine());


                Console.WriteLine("novo nome:");
                string nnm = Console.ReadLine();
                Console.WriteLine("Digite a nova idade:");
                int nida = int.Parse(Console.ReadLine());
                Console.WriteLine("preferencial (s/n)?");
                bool npre = Console.ReadLine().ToLower() == "s";

                using (MySqlConnection cnn = cx.conx())
                {
                    string sql = "UPDATE pacientes SET nome = @n, idade = @i, preferencial = @p WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, cnn);
                    cmd.Parameters.AddWithValue("@nm", nnm);
                    cmd.Parameters.AddWithValue("@ida", nida);
                    cmd.Parameters.AddWithValue("@pre", npre);
                    cmd.Parameters.AddWithValue("@id", novoId); //1 auto increment
                    cmd.ExecuteNonQuery();

                    if (cmd.ExecuteNonQuery() > 0)
                        Console.WriteLine("Paciente atualizado\n");
                    else
                        Console.WriteLine("erro\n");
                }

            }
        }

        public void andarfila()
        {
            using (MySqlConnection cn = cx.conx())
            {
                string sql = "SELECT * FROM pacientes ORDER BY preferencial DESC, horario ASC LIMIT 1"; // sql linha 10
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Console.WriteLine($"Atendendo: {dr["nome"]} | Idade: {dr["idade"]} | Preferencial: {dr["preferencial"]}\n");
                        dr.Close();
                        string deleteSql = "DELETE FROM pacientes WHERE id = @id"; //sql linha 13
                        MySqlCommand deleteCmd = new MySqlCommand(deleteSql, cn);
                        deleteCmd.Parameters.AddWithValue("@id", dr["id"]);
                        deleteCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        Console.WriteLine("Nenhum paciente na fila.\n");
                    }


                }

            }
        }
    }
} //checar as querys no banco!!!! order by e delete