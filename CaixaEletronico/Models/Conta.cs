using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico.Models
{
    internal class Conta
    {
        public Guid Id { get; set; }
        public int NumeroConta { get; set; }
        public string NomeTitular { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public double Saldo { get; set; }
        
    }
}
