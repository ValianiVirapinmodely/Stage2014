using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNet.WebServices.DataModel {
    
    public class EMail {
        public String Address;
        public EMailType Type;
    }

    public enum EMailType { Personal, Profesional }
}