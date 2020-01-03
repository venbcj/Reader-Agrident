using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReaderApp
{
    class Schaap
    {
        private string _levensnummer;
        public string Levensnummer
        {
            get { return _levensnummer; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _levensnummer = value.Substring(value.Length - 12, 12);
                }
                else { _levensnummer = null; }

            }
        }
    }
}
