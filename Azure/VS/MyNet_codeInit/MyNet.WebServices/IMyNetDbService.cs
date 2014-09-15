using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace MyNet.WebServices {

    using MyNet.WebServices.DataModel;

    [ServiceContract]
    public interface IMyNetDbService {

        [OperationContract]
        Contact getContact( int ContactID );

        [OperationContract]
        void removeContacts();

        [OperationContract]
        void addFacebookUser( String facebookUser, String accessToken );

    } // Interface
}
