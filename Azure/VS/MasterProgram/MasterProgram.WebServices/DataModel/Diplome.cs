using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterProgram.WebServices.DataModel
{
    public class Diplome
    {
        public String IdDiplome;
        public String Intitule;
        public List<UniteEnseignement> UEs;
    }
}