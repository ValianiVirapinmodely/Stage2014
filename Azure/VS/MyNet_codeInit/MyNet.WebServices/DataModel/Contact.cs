using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNet.WebServices.DataModel {
    
    public class Contact {
        public int Id;
        public String Firstname;
        public String Lastname;
        public String Society;
        public SocialNetworkBasicInfo SocialNetworkBasicInfo;
        public List<Contact> Contacts;
    } 

}