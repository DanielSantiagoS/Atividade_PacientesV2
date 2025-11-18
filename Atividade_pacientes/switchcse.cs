using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_pacientes
{
    internal class switchcse
    {

        paciente pac = new paciente();
        public void cch()
        {
            bool sair = false;
            while (!sair)
            {
                Console.WriteLine("Fila de EsperaL\n");
                Console.WriteLine("MENU\n");
                Console.WriteLine("1 - Adicionar paciente");
                Console.WriteLine("2 - Listar pacientes");
                Console.WriteLine("3 - Alterar paciente");
                Console.WriteLine("4 - Atender paciente");
                Console.WriteLine("q - Sair\n");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        pac.Add();
                        break;
                    case "2":
                        pac.List();
                        break;
                    case "3":
                        pac.Alt();
                        break;
                    case "4":
                        pac.andarfila();
                        break;
                    case "q":
                        Console.WriteLine("Saindo");
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        cch();
                        break;
                }
            }
        }
    }
}

   