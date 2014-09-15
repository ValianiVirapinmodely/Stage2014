using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Data.SqlClient;

namespace MyNet.WebServices {

    using MyNet.WebServices.DataModel;
    using MyNet.WebServices.DataProviders.Facebook;
using System.IO;

    public class MyNetDbService : IMyNetDbService {

        protected static String databaseServer = "xgpgwhhd2b.database.windows.net,1433";
        protected static String databaseName = "MyNetDB";
        protected static String username = "valiani.virapinmodely@xgpgwhhd2b";
        protected static String password = "Spurious7899";

        protected static String MyNetDbConnectionString;

        public static List<Language> mynetLanguages;
        public static bool init = false;

        protected static int nextContactID;
        protected static int nextAddressID;


        public MyNetDbService()
        {

            // Create DB Connection String
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = databaseServer,
                InitialCatalog = databaseName,
                Encrypt = true,
                TrustServerCertificate = false,
                UserID = username,
                Password = password
            };

            MyNetDbConnectionString = connectionString.ToString();

            ///
            // Inititialization Phase
            //     Get the languages residing in Table mynet.Language (used in the Federation)
            //     Calculate the ContactID and the AddressID used when inserting Contacts
            // Note: Executed during the creation of the First Service Instance
            ///

            if (init == false)
            {

                mynetLanguages = getLanguages();
                nextContactID = getNextContactId();
                nextAddressID = getNextAddressId();

                init = true;
            }


        } // Constructor


        #region Service Methods

        /// Get Contact from DB using the ContactID
        public Contact getContact(int contactID)
        {

            var select = "SELECT contact.ID, contact.Firstname, contact.Lastname, contact.Society, basicInfo.WebSite, basicInfo.SocialNetworkID " + "\n";

            var from = "FROM   mynet.Contact contact, " + "\n" +
                                "mynet.SocialNetworkBasicInfo basicInfo " + "\n";

            var where = "WHERE  contact.ID = {0} AND " + "\n" +
                                "basicInfo.ContactID = {0}";

            Contact contact;

            // Connect to MyNetDB
            using (var dbConnection = new SqlConnection(MyNetDbConnectionString))
            {

                dbConnection.Open();

                // Execute SQL statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {

                    // Connect to Federation Member
                    sqlCommand.CommandText = "USE FEDERATION MyNetFederation(ContactLanguageID=0) WITH RESET, FILTERING=OFF";
                    sqlCommand.ExecuteNonQuery();

                    // Get Contact information (including its BasicInfo)
                    sqlCommand.CommandText = String.Format(
                        select + from + where,
                        contactID
                    );

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();

                    // Values for constructing the Contact object
                    int id = (int)reader["ID"];
                    String firstname = (String)reader["Firstname"];
                    String lastname = (String)reader["Lastname"];
                    String socialNetworkID = (String)reader["SocialNetworkID"];
                    String webSite = (String)reader["WebSite"];
                    String society = null;

                    // Note : Society may be NULL
                    if (reader["Society"].GetType() != typeof(DBNull))
                        society = (String)reader["Society"];

                    reader.Close();

                    // Construction of the Contact object
                    contact = new Contact()
                    {
                        Id = id,
                        Firstname = firstname,
                        Lastname = lastname,
                        Society = society,
                        SocialNetworkBasicInfo = new SocialNetworkBasicInfo
                        {
                            SocialNetworkID = socialNetworkID,
                            WebSite = webSite,
                            EMails = getContactEmails(socialNetworkID, sqlCommand),
                            Languages = getContactLanguages(socialNetworkID, sqlCommand),
                            Addresses = getContactAddresses(socialNetworkID, sqlCommand)
                        },
                        Contacts = getContactsOfAContact(id, sqlCommand)
                    };

                } // SqlCommand                

                dbConnection.Close();

            } // dbConnection

            return contact;

        } // Method

        /// Insert Friends of a Facebook User into the Federated version of MyNetDB
        public void addFacebookUser(string facebookUser, string accessToken)
        {

            // Import Facebook User Profile and Friends
            FacebookClient facebook = new FacebookClient();
            var fbUser = facebook.getUser(facebookUser, accessToken);
            List<FacebookUser> fbUserFriends = facebook.getUserFriends(facebookUser, accessToken);

            ///
            // Prepare Contact object with Facebook User information
            ///

            // Contact object
            var fbUserContact = Utility.Convert.facebookUserToContact(fbUser);

            // Convert user friends to Contact objects
            var fbUserContacts = new List<Contact>(fbUserFriends.Count);
            foreach (var friend in fbUserFriends)
                fbUserContacts.Add(Utility.Convert.facebookUserToContact(friend));

            // Associate Friends to the Contact object
            fbUserContact.Contacts = fbUserContacts;

            // Insert Contacts into the Federation
            addContact(fbUserContact);

        } // Method

        /// Remove All Contacts
        public void removeContacts()
        {

            // Connect to MyNetDB
            using (var dbConnection = new SqlConnection(MyNetDbConnectionString))
            {

                dbConnection.Open();

                // Execute SQL statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {

                    var removeStatements = "DELETE mynet.speaksLanguage " +
                        //"DELETE mynet.Language " +
                                            "DELETE mynet.hasEmail	" +
                                            "DELETE mynet.Email " +
                                            "DELETE mynet.hasAddress " +
                                            "DELETE mynet.Address " +
                                            "DELETE mynet.SocialNetworkBasicInfo " +
                                            "DELETE mynet.Post " +
                                            "DELETE mynet.Content	" +
                                            "DELETE mynet.IsContactOf	" +
                                            "DELETE mynet.Contact ";

                    // Connect to Federation Member
                    sqlCommand.CommandText = "USE FEDERATION MyNetFederation(ContactLanguageID=0) WITH RESET, FILTERING=OFF";
                    sqlCommand.ExecuteNonQuery();

                    // Remove Contact from DB
                    sqlCommand.CommandText = removeStatements;
                    sqlCommand.ExecuteNonQuery();

                } // SqlCommand                

                dbConnection.Close();

            } // dbConnection

        } // Method

        #endregion


        #region Helper Methods

        /// Add Contact to Federation MyNetDB
        public void addContact(Contact contact)
        {

            // Connect to MyNetDB
            using (var dbConnection = new SqlConnection(MyNetDbConnectionString))
            {

                dbConnection.Open();

                // Execute SQL statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {


                    ///
                    // Assign Contacts IDs and AddressIDs
                    ///

                    // Contact ID
                    contact.Id = nextContactID++;

                    // Contact Friends IDs
                    if (contact.Contacts != null)
                    {

                        foreach (var c in contact.Contacts)
                            c.Id = nextContactID++;

                    }

                    // Contact Addresses IDs
                    if (contact.SocialNetworkBasicInfo.Addresses != null)
                    {

                        foreach (var a in contact.SocialNetworkBasicInfo.Addresses)
                            a.Id = nextAddressID++;

                    }


                    // Connect to Federation Member
                    sqlCommand.CommandText = "USE FEDERATION MyNetFederation(ContactLanguageID=0) WITH RESET, FILTERING=OFF";
                    sqlCommand.ExecuteNonQuery();



                    // Step 1: Is the contact already in SocialNetworkBasicInfo ?
                    sqlCommand.CommandText = String.Format("SELECT * FROM mynet.SocialNetworkBasicInfo WHERE SocialNetworkID='{0}'", contact.SocialNetworkBasicInfo.SocialNetworkID);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();

                    // Yes: Insert Contact (and all its associated objects) into the DB
                    if (!reader.HasRows)
                    {

                        reader.Close();

                        sqlCommand.CommandText = Utility.Convert.ContactToSqL(contact);
                        string commands = sqlCommand.CommandText;
                        StringReader sreader = new StringReader(commands);
                        string line;
                        while ((line = sreader.ReadLine()) != null) {
                            if (line == "") continue;
                            sqlCommand.CommandText = line;
                            sqlCommand.ExecuteNonQuery();
                        }
                        //sqlCommand.ExecuteNonQuery();
                    }
                    else
                    {

                        reader.Close();

                        sqlCommand.CommandText = Utility.Convert.BasicContactToSqL(contact);
                        sqlCommand.ExecuteNonQuery();

                    }





                } // SqlCommand                

                dbConnection.Close();

            } // dbConnection

        } // Method


        /// Get Contact from DB. Used by getContact() method 
        protected Contact getContact(int contactID, SqlCommand sqlCommand)
        {

            var select = "SELECT contact.ID, contact.Firstname, contact.Lastname, contact.Society, basicInfo.WebSite, basicInfo.SocialNetworkID ";

            var from = "FROM   mynet.Contact contact, " +
                                "mynet.SocialNetworkBasicInfo basicInfo ";

            var where = "WHERE  contact.ID = {0} AND " +
                                "basicInfo.ContactID = {0}";


            // Get Contact information (including its BasicInfo)
            sqlCommand.CommandText = String.Format(
                select + from + where,
                contactID
            );

            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();

            // Values for constructing the Contact object
            int id = (int)reader["ID"];
            String firstname = (String)reader["Firstname"];
            String lastname = (String)reader["Lastname"];
            String socialNetworkID = (String)reader["SocialNetworkID"];
            String webSite = (String)reader["WebSite"];
            String society = null;

            // Note : Society may be NULL
            if (reader["Society"].GetType() != typeof(DBNull))
                society = (String)reader["Society"];

            reader.Close();

            // Construction of the Contact object
            var contact = new Contact()
            {
                Id = id,
                Firstname = firstname,
                Lastname = lastname,
                Society = society,
                SocialNetworkBasicInfo = new SocialNetworkBasicInfo
                {
                    SocialNetworkID = socialNetworkID,
                    WebSite = webSite,
                    EMails = getContactEmails(socialNetworkID, sqlCommand),
                    Languages = getContactLanguages(socialNetworkID, sqlCommand),
                    Addresses = getContactAddresses(socialNetworkID, sqlCommand)
                },
                Contacts = getContactsOfAContact(id, sqlCommand)
            };

            return contact;

        } // Method


        /// Get Contacts of a Contact from DB
        protected List<Contact> getContactsOfAContact(int contactID, SqlCommand sqlCommand)
        {

            var select = "SELECT friend.ID, friend.Firstname, friend.Lastname, friend.Society, basicInfo.WebSite, basicInfo.SocialNetworkID ";

            var from = "FROM   mynet.Contact contact, " + "\n" +
                                "mynet.Contact friend, " + "\n" +
                                "mynet.IsContactOf isContactOf, " + "\n" +
                                "mynet.SocialNetworkBasicInfo basicInfo " + "\n";

            var where = "WHERE  contact.ID = {0} AND" + "\n" +
                                "contact.ID = isContactOf.ContactID_A AND" + "\n" +
                                "friend.ID  = isContactOf.ContactID_B AND" + "\n" +
                                "friend.ID  = basicInfo.ContactID";


            // Get Contacts from DB
            sqlCommand.CommandText = String.Format(
                select + from + where,
                contactID
            );

            SqlDataReader reader = sqlCommand.ExecuteReader();

            // Convert query results to Contact Objects
            var contacts = new List<Contact>();

            while (reader.Read())
            {

                // Values for constructing the Contact object
                int id = (int)reader["ID"];
                String firstname = (String)reader["Firstname"];
                String lastname = (String)reader["Lastname"];
                String socialNetworkID = (String)reader["SocialNetworkID"];
                String webSite = (String)reader["WebSite"];
                String society = null;

                // Note : Society may be NULL
                if (reader["Society"].GetType() != typeof(DBNull))
                    society = (String)reader["Society"];


                // Add Contact object to the list of contacts
                contacts.Add(
                    new Contact()
                    {
                        Id = id,
                        Firstname = firstname,
                        Lastname = lastname,
                        Society = society,
                        SocialNetworkBasicInfo = new SocialNetworkBasicInfo
                        {
                            SocialNetworkID = socialNetworkID,
                            WebSite = webSite
                        }
                    }
                );

            } // While

            reader.Close();

            return contacts;

        } // Method


        /// Get Contact Emails from DB 
        protected List<EMail> getContactEmails(String socialNetworkID, SqlCommand sqlCommand)
        {

            var select = "SELECT Email.EmailAddress, Email.EmailType ";

            var from = "FROM   mynet.SocialNetworkBasicInfo AS basicInfo, " +
                                "mynet.hasEmail AS hasEmail, " +
                                "mynet.Email AS Email ";

            var where = "WHERE  basicInfo.SocialNetworkID = '{0}' AND " +
                                "basicInfo.SocialNetworkID = hasEmail.SocialNetworkID AND " +
                                "hasEmail.EmailAddress = Email.EmailAddress ";

            // Get Emails from DB
            sqlCommand.CommandText = String.Format(
                select + from + where,
                socialNetworkID
            );

            SqlDataReader reader = sqlCommand.ExecuteReader();

            // Convert query results to Email Objects
            var emails = new List<EMail>();

            while (reader.Read())
            {

                // Email Information
                var eType = (String)reader["Emailtype"];
                var address = (String)reader["EmailAddress"];

                // Determine Email Type
                EMailType emailType;

                if (eType.CompareTo("Personal") == 0)
                    emailType = EMailType.Personal;
                else
                    emailType = EMailType.Profesional;

                // Add Email object to the list of emails
                emails.Add(
                    new EMail
                    {
                        Address = address,
                        Type = emailType
                    }
                );

            } // While

            reader.Close();

            return emails;

        } // Method


        /// Get Contact Addresses from DB 
        protected List<Address> getContactAddresses(String socialNetworkID, SqlCommand sqlCommand)
        {

            var select = "SELECT address.ID, address.Street, address.Number, address.City, address.ZipCode ";

            var from = "FROM   mynet.SocialNetworkBasicInfo AS basicInfo, " +
                                "mynet.hasAddress AS hasAddress, " +
                                "mynet.Address AS address ";

            var where = "WHERE  basicInfo.SocialNetworkID = '{0}' AND " +
                                "basicInfo.SocialNetworkID = hasAddress.SocialNetworkID AND " +
                                "hasAddress.AddressID = address.ID ";


            // Get Addresses from DB
            sqlCommand.CommandText = String.Format(
                select + from + where,
                socialNetworkID
            );

            SqlDataReader reader = sqlCommand.ExecuteReader();


            // Convert query results to Address Objects
            var addresses = new List<Address>();

            while (reader.Read())
            {

                addresses.Add(
                    new Address
                    {
                        Id = (int)reader["ID"],
                        Number = (int)reader["Number"],
                        Street = (String)reader["Street"],
                        City = (String)reader["City"],
                        ZipCode = (int)reader["ZipCode"]
                    }
                );

            } // While

            reader.Close();

            return addresses;

        } // Method


        /// Get Contact Addresses from DB
        protected List<SocialNetworkLanguage> getContactLanguages(String socialNetworkID, SqlCommand sqlCommand)
        {

            var select = "SELECT speaks.SocialNetworkLanguageID, speaks.SocialNetworkLanguageName ";

            var from = "FROM   mynet.SocialNetworkBasicInfo AS basicInfo, " +
                                "mynet.speaksLanguage AS speaks ";

            var where = "WHERE  basicInfo.SocialNetworkID = '{0}' AND " +
                                "basicInfo.SocialNetworkID = speaks.ContactSocialNetworkID ";


            // Get Languages from DB
            sqlCommand.CommandText = String.Format(
                select + from + where,
                socialNetworkID
            );

            SqlDataReader reader = sqlCommand.ExecuteReader();


            // Convert query results to SocialNetworkLanguage Objects
            var languages = new List<SocialNetworkLanguage>();

            while (reader.Read())
            {

                languages.Add(
                    new SocialNetworkLanguage
                    {
                        Id = (long)reader["SocialNetworkLanguageID"],
                        Name = (String)reader["SocialNetworkLanguageName"]
                    }
                );

            } // While

            reader.Close();

            return languages;

        } // Method


        #endregion


        #region Methods used during Init phase

        /// Determine the next ContactID to be used when inserting a Contact
        protected int getNextContactId()
        {

            var contactID = 0;

            // Connect to MyNetDB
            using (var dbConnection = new SqlConnection(MyNetDbConnectionString))
            {

                dbConnection.Open();

                // Execute SQL statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {

                    // Connect to Federation Member
                    sqlCommand.CommandText = "USE FEDERATION MyNetFederation(ContactLanguageID=0) WITH RESET, FILTERING=OFF";
                    sqlCommand.ExecuteNonQuery();

                    // Query: Get the maximum ID in mynet.Contact Table
                    sqlCommand.CommandText = "SELECT max(ID) FROM mynet.Contact";
                    var nextID = sqlCommand.ExecuteScalar();

                    // Assing 0 to ContactID when table mynet.Contact has no contacts
                    if (!DBNull.Value.Equals(nextID))
                        contactID = Convert.ToInt32(nextID);
                    else
                        contactID = 0;

                } // SqlCommand                

                dbConnection.Close();

            } // dbConnection

            return contactID + 1;

        } // Method

        /// Determine the next AddressID to be used when inserting a Contact
        protected int getNextAddressId()
        {

            var addressID = 0;

            // Connect to MyNetDB
            using (var dbConnection = new SqlConnection(MyNetDbConnectionString))
            {

                dbConnection.Open();

                // Execute SQL statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {

                    // Connect to Federation Member
                    sqlCommand.CommandText = "USE FEDERATION MyNetFederation(ContactLanguageID=0) WITH RESET, FILTERING=OFF";
                    sqlCommand.ExecuteNonQuery();

                    // Query: Get the maximum ID in mynet.Address Relation
                    sqlCommand.CommandText = "SELECT max(ID) FROM mynet.Address";
                    var nextID = sqlCommand.ExecuteScalar();

                    // Assing 0 to ContactID when table mynet.Contact has no contacts
                    if (!DBNull.Value.Equals(nextID))
                        addressID = Convert.ToInt32(nextID);
                    else
                        addressID = 0;

                } // SqlCommand                

                dbConnection.Close();

            } // dbConnection

            return addressID + 1;

        } // Method

        /// Get the languages residing in mynet.Language Table
        protected List<Language> getLanguages()
        {

            var languages = new List<Language>();

            // Connect to MyNetDB
            using (var dbConnection = new SqlConnection(MyNetDbConnectionString))
            {

                dbConnection.Open();

                // Execute SQL statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {

                    // Connect to Federation Member
                    sqlCommand.CommandText = "USE FEDERATION MyNetFederation(ContactLanguageID=0) WITH RESET, FILTERING=OFF";
                    sqlCommand.ExecuteNonQuery();

                    // Get all languages in the DB
                    sqlCommand.CommandText = "SELECT * FROM mynet.Language";
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    // Create Language object for every language in MyNetDB
                    while (reader.Read())
                    {

                        var language = new Language
                        {
                            Id = (int)reader["LanguageID"],
                            Name = (String)reader["LanguageName"]
                        };

                        languages.Add(language);

                    }

                } // SqlCommand                

                dbConnection.Close();

            } // dbConnection

            return languages;

        } // Method

        #endregion


    } // Class

}