using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.WebServices.DataProviders.Facebook
{

    public class FacebookUser
    {

        #region Public Fields
        public String id;
        public String username;
        public String name;
        public String first_name;
        public String last_name;
        public String gender;
        public String locale;
        #endregion

        #region Required Access Token
        public List<FacebookLanguage> languages;    // require "user_likes" token
        public String website;                      // require "user_website" or "friends_website" token
        public String email;                        // require "email" token
        public FacebookLocation location;           // require "user_location" or "friends_location" token 
        #endregion

    } // End of Class

    public class FacebookLanguage
    {
        #region Public Fields
        public String id;
        public String name;
        #endregion
    }

    public class FacebookLocation
    {
        #region Public Fields
        public String id;
        public String name;
        #endregion
    }
}
