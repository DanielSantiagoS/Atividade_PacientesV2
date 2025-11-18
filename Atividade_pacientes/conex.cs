using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Atividade_pacientes
{
    internal class conex
    {
        //ondetudoacontece
        private string stringconx = "server=localhost;database=filadeespera;uid=root;password='';";

        public MySqlConnection conx()
        {
            MySqlConnection conexao = new MySqlConnection(stringconx);
            conexao.Open();
            return conexao;
        }
    }
}
    

