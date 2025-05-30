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
        public string NameTitular { get; set; }
        public double Saldo { get; set; }
        public int Senha { get; set; }
        
    }
}
