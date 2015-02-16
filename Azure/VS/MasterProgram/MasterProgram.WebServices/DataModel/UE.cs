using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterProgram.WebServices.DataModel
{
    public class UniteEnseignement
    {
        public String IdUE;
        public String Intitule;
        public String Descriptif;
        public int Semestre;
        public List<Diplome> Diplomes;
    }
}