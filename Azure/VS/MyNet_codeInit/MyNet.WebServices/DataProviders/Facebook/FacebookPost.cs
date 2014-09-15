using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.WebServices.DataProviders.Facebook
{

    public class FacebookPost
    {

        #region Fields Requiring access_token
        public String id;
        public FacebookUser from;
        public FacebookUser to;
        public String message;
        public String type;
        public DateTime created_time;
        public String picture;
        public string source; // url of video embedded within the post
        #endregion

        #region Fields Requiring read_stream
        public String place;
        public String story;
        #endregion

    } // Class
}
