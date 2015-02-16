using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterProgram.WebServices.DataModel
{
    public class Matiere
    {
        public String IdMatiere;
        public String Intitule;
        public int NbCM;
        public int NbTD;
        public int NbTP;
        public Module ModuleAssocie;
    }
}