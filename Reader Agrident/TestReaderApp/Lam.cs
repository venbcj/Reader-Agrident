using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReaderApp
{
    class Lam : Schaap
    {

        public bool isEmpty()
        {
            if (!String.IsNullOrEmpty(Levensnummer) || !String.IsNullOrEmpty(diernrLam))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string diernrLam { get; set; }
        public string geslacht { get; set; }
        public Decimal gewicht { get; set; }
        public string status { get; set; }
    }
}
