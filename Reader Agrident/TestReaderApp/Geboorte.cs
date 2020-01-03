using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReaderApp
{
    class Geboorte : Schaap
    {
        //string[] kolommen = { "", "moeder", "diernrMdr", "datum", "verloop", "geborenen", "levend", "registratie", "lam_1", "diernrLam_1", "geslacht_1", "gewicht_1", "status_1", "lam_2", "diernrLam_2", "geslacht_2", "gewicht_2", "status_2", "lam_3", "diernrLam_3", "geslacht_3", "gewicht_3", "status_3", "lam_4", "diernrLam_4", "geslacht_4", "gewicht_4", "status_4", "lam_5", "diernrLam_5", "geslacht_5", "gewicht_5", "status_5", "lam_6", "diernrLam_6", "geslacht_6", "gewicht_6", "status_6", "lam_7", "diernrLam_7", "geslacht_7", "gewicht_7", "status_7", };

     
     
        public string diernrMdr { get; set; }
        public DateTime datum { get; set; }
        public string verloop { get; set; }
        public string geborenen { get; set; }
        public string levend { get; set; }
        public string registratie { get; set; }

        public List<Lam> Lammeren { get; set; } = new List<Lam>();

    }
}
