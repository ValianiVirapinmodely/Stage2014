using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace MyNet.WebServices.DataProviders.Facebook
{

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class FacebookClient
    {

        private static readonly String FacebookApiBaseAddress = "https://graph.facebook.com";

        public FacebookUser getUser(String username)
        {

            /// URI to User Profile 
            String uri = String.Format(FacebookApiBaseAddress + "/{0}", username);

            // Execute Http GET operation and wait for response
            var request = (HttpWebRequest)WebRequest.Create(uri);
            var response = (HttpWebResponse)request.GetResponse();

            // Extract data (in JSON format) from response 
            var data = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Parse data 
            FacebookUser user = JsonConvert.DeserializeObject<FacebookUser>(data);
            return user;

        } // Method

        public FacebookUser getUser(String username, String accessToken)
        {

            /// URI to User Profile 
            String uri = String.Format(FacebookApiBaseAddress + "/{0}?access_token={1}", username, accessToken);

            // Execute Http GET operation and wait for response
            var request = (HttpWebRequest)WebRequest.Create(uri);
            var response = (HttpWebResponse)request.GetResponse();

            // Extract data (in JSON format) from response 
            var data = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Parse data 
            FacebookUser user = JsonConvert.DeserializeObject<FacebookUser>(data);
            return user;

        } // Method
        /*
        public List<FacebookUser> getUserFriends(String username, String accessToken)
        {

            // Required 
            if (accessToken == null) return null;

            /// URI to User Friends 
            String uri = String.Format(FacebookApiBaseAddress + "/{0}/friends?access_token={1}", username, accessToken);

            // Execute Http GET operation and wait for response
            var request = (HttpWebRequest)WebRequest.Create(uri);
            var response = (HttpWebResponse)request.GetResponse();

            // Extract and Parse data (in JSON format) from response 
            var data = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var jsonFragment = JObject.Parse(data);

            // Fragment Structure { data : [ {id, name} ] }
            // See http://developers.facebook.com/docs/reference/api/user/#friends 
            var friends = (JArray)jsonFragment["data"];

            List<FacebookUser> userFriends = new List<FacebookUser>();

            foreach (var friend in friends)
            {
                var id = (String)friend["id"];
                userFriends.Add(getUser(id,accessToken));
            }

            return userFriends;

        } // Method
        */
        public List<FacebookPost> gerUserPosts(String username, String accessToken)
        {

            // Required 
            if (accessToken == null)
                return null;

            /// URI to User' Wall
            String uri = String.Format(FacebookApiBaseAddress + "/{0}/posts?access_token={1}", username, accessToken);

            // Execute Http GET operation and wait for response
            var request = (HttpWebRequest)WebRequest.Create(uri);
            var response = (HttpWebResponse)request.GetResponse();

            // Extract and Parse data (in JSON format) from response 
            var data = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var jsonFragment = JObject.Parse(data);

            // Fragment Structure { data : [ Post ] }
            // See http://developers.facebook.com/docs/reference/api/post/
            var posts = (JArray)jsonFragment["data"];

            List<FacebookPost> userPosts = new List<FacebookPost>();

            // Deserialize each entry in the wall into a .NET object
            foreach (var post in posts)
            {

                var p = post.ToString();
                FacebookPost wallEntry = JsonConvert.DeserializeObject<FacebookPost>(p);
                userPosts.Add(wallEntry);
            }

            return userPosts;
        } // Method

        public List<FacebookUser> getUserFriends( String username, String accessToken ) {

            // Required 
            if ( accessToken == null )
                return null;

            /// User Friends URI
            String uri = String.Format( FacebookApiBaseAddress + "/{0}/friends?access_token={1}", username, accessToken );

            // GET list of friends ( basic info ) 
            var request = (HttpWebRequest) WebRequest.Create( uri );
            var response = (HttpWebResponse) request.GetResponse();

            // Parse Response into .NET object representing JSON Type
            var data = new StreamReader( response.GetResponseStream() ).ReadToEnd();
            var jsonFragment = JObject.Parse( data );

            // User Friends are contained in an array called "data"
            // See Facebook Documentation for more information
            // http://developers.facebook.com/docs/reference/api/user/#friends 
            var friends = (JArray) jsonFragment["data"];

            //
            // Build list of URIs pointing to Friends Profiles 
            //
            List<String> friendsProfileURIs = new List<String>();

            foreach ( var friend in friends ) {

                var id = (String) friend["id"];
                var friendProfile = String.Format( FacebookApiBaseAddress + "/{0}", id );
                friendsProfileURIs.Add( friendProfile );

            }

            //
            // Retrieve Friends Profiles by Dispatching Multiple Threads
            // Remark: 
            //       Facebook limits MAX Number of Request per Minute 
            //       Dispatcher uses Best Effort Strategie
            //

            List<FacebookUser> userFriends = new List<FacebookUser>();
            List<Task> tasks = new List<Task>();
            const int numberOfUsersToRetrievePerThread = 75;

            // Threads Dispatching based on number "numberOfUsersToRetrievePerThread"
            for ( int i = 0; i < friendsProfileURIs.Count; i++ ) {

                // There are enough friends for New Thread
                if ( i + numberOfUsersToRetrievePerThread < friendsProfileURIs.Count ) {

                    // Copy Friends Profiles to local variable
                    var j = 0;
                    String[] profiles = new String[numberOfUsersToRetrievePerThread];

                    for ( j = 0; j < numberOfUsersToRetrievePerThread; j++ )
                        profiles[j] = friendsProfileURIs[i + j];

                    // Thread Logic
                    var task = new Task( () => {

                        foreach ( var profile in profiles ) {

                            var req = (HttpWebRequest) WebRequest.Create( profile );

                            try {

                                using ( var resp = req.GetResponse() ) {
                                    var f = new StreamReader( resp.GetResponseStream() ).ReadToEnd();
                                    userFriends.Add( JsonConvert.DeserializeObject<FacebookUser>( f ) );
                                }

                                // Best Effort: Ignore Exceptions
                            } catch ( Exception ex ) { }

                        }

                    } ); // Lambda

                    // Star Thread
                    tasks.Add( task );
                    task.Start();

                    i += numberOfUsersToRetrievePerThread;

                    // Friends Retrieved by Main Thread 
                } else {

                    var req = (HttpWebRequest) WebRequest.Create( friendsProfileURIs[i] );

                    try {

                        using ( var resp = req.GetResponse() ) {
                            var f = new StreamReader( resp.GetResponseStream() ).ReadToEnd();
                            userFriends.Add( JsonConvert.DeserializeObject<FacebookUser>( f ) );
                        }

                        // Best Effort: Ignore Exceptions
                    } catch ( Exception ex ) { }

                } // Else

            }

            // Wait completition of all threads
            Task.WaitAll( tasks.ToArray() );

            return userFriends;

        } // Method


    } // Class

    class FacebookRequest
    {

        private static readonly String FacebookApiBaseAddress = "https://graph.facebook.com";

        private HttpWebRequest request;

        // HttpUtility.UrlEncode
        public FacebookRequest(String facebookResource)
        {
            var uri = FacebookApiBaseAddress + facebookResource;
            request = (HttpWebRequest)WebRequest.Create(uri);
        }

        public FacebookRequest(String facebookResource, String parameters)
        {

            String uri = null;

            if (parameters == null)
                uri = FacebookApiBaseAddress + facebookResource;
            else
                uri = FacebookApiBaseAddress + facebookResource + "?" + parameters;

            request = (HttpWebRequest)WebRequest.Create(uri);
        }

        public HttpWebResponse send()
        {
            return (HttpWebResponse)request.GetResponse();
        }


    } // class

    class FacebookResponse
    {

        private HttpWebResponse response;

        public String data;

        public FacebookResponse(HttpWebResponse response)
        {
            this.response = response;
            this.data = extractDataFromResponse(response);
        }

        private String extractDataFromResponse(HttpWebResponse response)
        {
            var reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }

    } // class

}
