using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNet.WebServices.DataModel {

    public class SocialNetworkBasicInfo {
        public String WebSite; 
        public String SocialNetworkID;
        public List<Address> Addresses;
        public List<SocialNetworkLanguage> Languages;
        public List<EMail> EMails;
    }

}