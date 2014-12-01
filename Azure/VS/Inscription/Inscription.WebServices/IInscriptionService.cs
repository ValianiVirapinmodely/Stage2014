using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Inscription.WebServices
{
    using Inscription.WebServices.DataModel;
    
    [ServiceContract]
    public interface IInscriptionService
    {
        [OperationContract]
        void addMS( SuitMatiere MS );

        [OperationContract]
        List<SuitMatiere> getMS( );
    }
}
