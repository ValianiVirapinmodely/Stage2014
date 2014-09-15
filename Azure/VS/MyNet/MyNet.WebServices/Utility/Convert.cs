using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace MyNet.WebServices.Utility {

    using MyNet.WebServices.DataModel;
    using MyNet.WebServices.DataProviders.Facebook;


    public class Convert {

        /// Transforms a FacebookUser to a Contact object
        public static Contact facebookUserToContact( FacebookUser facebookUser ) {

            // Contact object with partial information of a Facebook User 
            var contact = new Contact {
                Firstname = facebookUser.first_name,
                Lastname = facebookUser.last_name,
                SocialNetworkBasicInfo = new SocialNetworkBasicInfo {
                    SocialNetworkID = facebookUser.id,
                    WebSite = facebookUser.website,
                    Languages = new List<SocialNetworkLanguage>(),
                    EMails = new List<EMail>(),
                    Addresses = new List<Address>()
                },
                Contacts = new List<Contact>()
            };


            // User specified his/her spoken Languages ?
            if ( facebookUser.languages != null ) {

                // Add Facebook Languages to the Contact object
                foreach ( var fbLanguage in facebookUser.languages ) {

                    // Convert FacebookLanguage to SocialNetworkLanguage
                    contact.SocialNetworkBasicInfo.Languages.Add(
                        new SocialNetworkLanguage {
                            Id = long.Parse( fbLanguage.id ),
                            Name = fbLanguage.name 
                        }
                    );

                } // For

            } // If


            // Assume Facebook User has 1 Personal mail (maximum)
            contact.SocialNetworkBasicInfo.EMails.Add(
                new EMail {
                    Address = facebookUser.email,
                    Type = EMailType.Personal
                }
            );

            ///
            // Assume that Facebook Users do not specify their Addresses
            ///


            return contact;

        } // Method


        /// Transforms a Contact object to SQL statements targeting MyNetDB
        public static String ContactToSqL( Contact contact ) {

            var sqlStatements = new StringBuilder();

            // INSERT for Contact table
            sqlStatements.AppendLine( 
                buildSqlInsertStatementForTable_Contact( contact ) 
            );

            // INSERT for IsContactOf table
            sqlStatements.AppendLine(
                buildSqlInsertStatementForTable_IsContactOf( contact )
            );

            // INSERT for SocialNetworkBasicInfo table
            sqlStatements.AppendLine(
                buildSqlInsertStatementForTable_SocialNetworkBasicInfo( contact )
            );


            // INSERT for EMail and hasEmail tables
            List<EMail> emails = contact.SocialNetworkBasicInfo.EMails;

            if ( emails != null && emails.Count > 0 ) {

                sqlStatements.AppendLine(
                    buildSqlInsertStatementForTable_Email( contact )
                );

                sqlStatements.AppendLine(
                    buildSqlInsertStatementForTable_hasEmail( contact )
                );

            }


            // INSERT for Address and hasAddress tables
            List<Address> addresses = contact.SocialNetworkBasicInfo.Addresses;

            if ( addresses != null && addresses.Count > 0 ) {

                sqlStatements.AppendLine(
                    buildSqlInsertStatementForTable_Address( contact )
                );

                sqlStatements.AppendLine(
                    buildSqlInsertStatementForTable_hasAddress( contact )
                );

            }


            // INSERT for Language and speaksLanguage tables
            List<SocialNetworkLanguage> languages = contact.SocialNetworkBasicInfo.Languages;

            if ( languages != null && languages.Count > 0 ) {
                /*
                sqlStatements.AppendLine(
                    buildSqlInsertStatementForTable_Language( contact )
                );
                */
                sqlStatements.AppendLine(
                    buildSqlInsertStatementForTable_speaksLanguage( contact )
                );

            }


            return sqlStatements.ToString();
           
        }


        /// 
        public static String BasicContactToSqL( Contact contact ) {

            var sqlStatements = new StringBuilder();

            // INSERT for Contact table
            sqlStatements.AppendLine(
                buildSqlInsertStatementForTable_Contact( contact )
            );

            // INSERT for IsContactOf table
            sqlStatements.AppendLine(
                buildSqlInsertStatementForTable_IsContactOf( contact )
            );

            return sqlStatements.ToString();

        }


        #region Helper Methods

        /// INSERT INTO mynet.Contact ( ID, Firstname, Lastname, Society ) 
        private static String buildSqlInsertStatementForTable_Contact( Contact contact ) {

            var insertTemplate = "INSERT INTO mynet.Contact ( ID, Firstname, Lastname, Society ) " +
                                 "VALUES {0}";

            var values = new StringBuilder();
            var comma = ",";


            // INSERT containing Contact information
            values.Append( 
                buildValuesForTable_Contact( contact )    
            );

            ///
            // Set of INSERTs containing Contact' Friends 
            ///

            // At least one friend ?
            if ( contact.Contacts != null ) {

                var contacts = contact.Contacts;

                // Convert friends to SQL
                // Recall: insert values are separated using commas
                for ( var i = 0; i < contacts.Count; i++ ) {

                    if( i == 0 )
                        values.AppendLine( comma );

                    if ( i != contacts.Count - 1 )

                        values.AppendLine(
                            buildValuesForTable_Contact( contacts[i] ) + comma
                        );

                    else
                        values.AppendLine(
                            buildValuesForTable_Contact( contacts[i] )
                        );

                } // For


            } // If

            // Insert statement
            return String.Format( insertTemplate, values.ToString() );

        } // Method


        /// INSERT INTO mynet.IsContactOf ( ContactID_A, ContactID_B )
        private static String buildSqlInsertStatementForTable_IsContactOf( Contact contact ) {

            var insertTemplate = "INSERT INTO mynet.IsContactOf ( ContactID_A, ContactID_B ) " +
                                 "VALUES {0}";
            
            var values = new StringBuilder();
            var comma = ",";

            // Contact has a friend ? 
            if ( contact.Contacts != null ) {

                var contacts = contact.Contacts;
                var contactA = contact;

                // Prepare values
                for ( var i = 0; i < contacts.Count; i++ ) {

                    var contactB = contacts[i];

                    if ( i == contacts.Count - 1 )

                        values.AppendLine(
                            buildValuesForTable_IsContactOf( contactA, contactB )
                        );

                    else
                        values.AppendLine(
                            buildValuesForTable_IsContactOf( contactA, contactB ) + comma
                        );

                } // For
                
            } // If

            // Insert statement
            return String.Format( insertTemplate, values );
        } // Method


        /// INSERT INTO mynet.SocialNetworkBasicInfo ( ContactID, SocialNetworkID, WebSite )
        private static String buildSqlInsertStatementForTable_SocialNetworkBasicInfo( Contact contact ) {

            var insertTemplate = "INSERT INTO mynet.SocialNetworkBasicInfo ( ContactID, SocialNetworkID, WebSite ) " +
                                 "VALUES {0}";

            var values = new StringBuilder();
            var comma = ",";

            // Contact info
            values.Append(
                buildValuesForTable_SocialNetworkBasicInfo( contact )
            );


            if( contact.Contacts != null ) {
            
                List<Contact> contacts = contact.Contacts;
                values.AppendLine( comma );

                // Info about their friends
                for ( int i = 0; i < contacts.Count; i++ ) {

                    if ( i == contacts.Count - 1 )
                        values.AppendLine(
                            buildValuesForTable_SocialNetworkBasicInfo( contacts[i] )
                        );

                    else
                        values.AppendLine(
                            buildValuesForTable_SocialNetworkBasicInfo( contacts[i] )
                            + comma
                        );

                } // For

            } // If

            // Insert statement
            return String.Format( insertTemplate, values );

        } // Method


        /// INSERT INTO mynet.Email ( Email, EmailType ) 
        private static String buildSqlInsertStatementForTable_Email( Contact contact ) {

            var insertTemplate = "INSERT INTO mynet.Email ( EmailAddress, EmailType ) " +
                                 "VALUES {0}";

            var valuesTemplate = "( N'{0}', N'{1}' )";
            var comma = ",";

            var values = new StringBuilder();

            List<EMail> emails = contact.SocialNetworkBasicInfo.EMails;

            for ( var i = 0; i < emails.Count; i++ ) {

                var email = emails[i];

                if ( i == emails.Count - 1 )
                    values.AppendLine(
                        String.Format( valuesTemplate, email.Address, email.Type )
                    );

                else
                    values.AppendLine(
                        String.Format( valuesTemplate, email.Address, email.Type ) + comma
                    );

            } // For
            
            // Insert statement
            return String.Format( insertTemplate, values );
        
        } // Method


        /// INSERT INTO mynet.hasEmail ( SocialNetworkID, Email )
        protected static String buildSqlInsertStatementForTable_hasEmail( Contact contact ) {

            var insertTemplate = "INSERT INTO mynet.hasEmail ( SocialNetworkID, EmailAddress ) " +
                                 "VALUES {0}";

            var valuesTemplate = "( N'{0}', N'{1}' )";
            var comma = ",";

            var values = new StringBuilder();

            List<EMail> emails = contact.SocialNetworkBasicInfo.EMails;
            String socialNetworkID = contact.SocialNetworkBasicInfo.SocialNetworkID;

            for ( var i = 0; i < emails.Count; i++ ) {

                if ( i == emails.Count - 1 ) {

                    values.AppendLine(
                        String.Format( valuesTemplate, socialNetworkID, emails[i].Address )
                    );

                } else {

                    values.AppendLine(
                        String.Format( valuesTemplate, socialNetworkID, emails[i].Address ) + comma
                    );
                    
                }
                    
            } // For 

            // Insert statement
            return String.Format( insertTemplate, values );

        } // Method


        /// INSERT INTO mynet.Address ( ID, Street, Number, City, ZipCode ) 
        private static String buildSqlInsertStatementForTable_Address( Contact contact ) {

            var insertTemplate = "INSERT INTO mynet.Address ( ID, Street, Number, City, ZipCode ) " +
                                 "VALUES {0}";

            var valuesTemplate = "( {0}, N'{1}', {2}, N'{3}', {4} )";

            var values = new StringBuilder();
            var comma = ",";

            List<Address> addresses = contact.SocialNetworkBasicInfo.Addresses;

            for ( var i = 0; i < addresses.Count; i++ ) {

                Address address = addresses[i];

                if ( i == addresses.Count - 1 ) {

                    values.AppendLine(
                        String.Format( valuesTemplate, 
                            address.Id,
                            address.Street,
                            address.Number,
                            address.City,
                            address.ZipCode
                        )
                    );

                } else {

                    values.AppendLine(
                        String.Format( valuesTemplate,
                            address.Id,
                            address.Street,
                            address.Number,
                            address.City,
                            address.ZipCode
                        ) + comma
                    );

                }

            } // For 

            // Insert statement
            return String.Format( insertTemplate, values );

        } // Method


        /// INSERT INTO mynet.hasAddress ( AddressID, SocialNetworkID )
        private static String buildSqlInsertStatementForTable_hasAddress( Contact contact ) {

            var insertTemplate = "INSERT INTO mynet.hasAddress ( AddressID, SocialNetworkID ) " +
                                 "VALUES {0}";

            var valuesTemplate = "( {0}, N'{1}' )";

            var values = new StringBuilder();
            var comma = ",";

            List<Address> addresses = contact.SocialNetworkBasicInfo.Addresses;

            for ( var i = 0; i < addresses.Count; i++ ) {

                Address address = addresses[i];

                if ( i == addresses.Count - 1 ) {

                    values.AppendLine(
                        String.Format( valuesTemplate,
                            address.Id,
                            contact.SocialNetworkBasicInfo.SocialNetworkID
                        )
                    );

                } else {

                    values.AppendLine(
                        String.Format( valuesTemplate,
                            address.Id,
                            contact.SocialNetworkBasicInfo.SocialNetworkID
                        ) + comma
                    );

                }

            } // For 

            // Insert statement
            return String.Format( insertTemplate, values );

        } // Method


        /// INSERT INTO mynet.Language ( LanguageID, LanguageName )
        private static String buildSqlInsertStatementForTable_Language( Contact contact ) {

            var insertTemplate = "INSERT INTO mynet.Language ( LanguageID, LanguageName ) " +
                                 "VALUES {0}";

            var valuesTemplate = "( {0}, N'{1}' )";

            var values = new StringBuilder();
            var comma = ",";

            List<SocialNetworkLanguage> languages = contact.SocialNetworkBasicInfo.Languages;

            for ( var i = 0; i < languages.Count; i++ ) {

                SocialNetworkLanguage language = languages[i];

                if ( i == languages.Count - 1 ) {

                    values.AppendLine(
                        String.Format( valuesTemplate,
                            language.Id,
                            language.Name
                        )
                    );

                } else {

                    values.AppendLine(
                        String.Format( valuesTemplate,
                            language.Id,
                            language.Name
                        ) + comma
                    );

                }

            } // For 

            // Insert statement
            return String.Format( insertTemplate, values );

        } // Method


        /// INSERT INTO mynet.speaksLanguage ( LanguageID, ContactSocialNetworkID, SocialNetworkLanguageID, SocialNetworkLanguageName )
        private static String buildSqlInsertStatementForTable_speaksLanguage( Contact contact ) {

            var insertTemplate = "INSERT INTO mynet.speaksLanguage ( LanguageID, ContactSocialNetworkID, SocialNetworkLanguageID, SocialNetworkLanguageName ) " +
                                 "VALUES {0}";

            var valuesTemplate = "( {0}, N'{1}', {2}, N'{3}' )";

            var values = new StringBuilder();
            var comma = ",";

            // Facebook languages spoken by Contact
            List<SocialNetworkLanguage> contactLanguages = contact.SocialNetworkBasicInfo.Languages;

            for ( var i = 0; i < contactLanguages.Count; i++ ) {

                SocialNetworkLanguage socialNetworkLanguage = contactLanguages[i];
                
                ///
                // Find the Facebook languages counterpart in MyNet
                ///
                Language mynetLanguage = MyNetDbService.mynetLanguages.Find(
                    delegate( Language language ) {
                        return language.Name.CompareTo( socialNetworkLanguage.Name ) == 0;
                    }
                );

                // Language ID to be used
                int mynetLanguageId;

                if ( mynetLanguage != null )
                    mynetLanguageId = mynetLanguage.Id;
                else
                    mynetLanguageId = 4;

                

                if ( i == contactLanguages.Count - 1 ) {

                    values.AppendLine(
                        String.Format( valuesTemplate,
                            mynetLanguageId,
                            contact.SocialNetworkBasicInfo.SocialNetworkID,
                            socialNetworkLanguage.Id,
                            socialNetworkLanguage.Name
                        )
                    );

                } else {

                    values.AppendLine(
                        String.Format( valuesTemplate,
                            mynetLanguageId,
                            contact.SocialNetworkBasicInfo.SocialNetworkID,
                            socialNetworkLanguage.Id,
                            socialNetworkLanguage.Name
                        ) + comma
                    );

                }

            } // For 

            // Insert statement
            return String.Format( insertTemplate, values );

        } // Method


        /// VALUES (  ContactID_A, ContactID_B )
        private static String buildValuesForTable_IsContactOf( Contact contact_A, Contact contact_B ) {

            var valuesTemplate = "( {0}, {1} )";
            var values = "";

            values = String.Format( valuesTemplate, 
                contact_A.Id, 
                contact_B.Id
            );

            return values;

        }


        /// VALUES (  ID, Firstname, Lastname, Society )
        private static String buildValuesForTable_Contact( Contact contact ) {

            var valuesTemplate = "( {0}, N'{1}', N'{2}', N'{3}' )";
            var values = "";

            values = String.Format( valuesTemplate,
                contact.Id,
                contact.Firstname.Replace( "'", "''" ), // Firstname and Lastname may contain apostrophes '
                contact.Lastname.Replace( "'", "''" ),  // They are replaced with double apostrophes '' before insertion in DB
                contact.Society
            );

            return values;
        }


        /// VALUES ( ContactID, SocialNetworkID, WebSite )
        private static String buildValuesForTable_SocialNetworkBasicInfo( Contact contact ) {

            var valuesTemplate = "( {0}, N'{1}', N'{2}' )";
            var values = "";

            values = String.Format( valuesTemplate,
                contact.Id,
                contact.SocialNetworkBasicInfo.SocialNetworkID,
                contact.SocialNetworkBasicInfo.WebSite
            );

            return values;

        } // Method

        #endregion 


    } /// Class

}