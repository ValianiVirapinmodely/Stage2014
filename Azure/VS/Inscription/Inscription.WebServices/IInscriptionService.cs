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
        List<SuitMatiere> getMS();

        [OperationContract]
        List<SuitMatiere> getMSByEtudiant( int idEtudiant );

        [OperationContract]
        List<SuitMatiere> getMSByMatiere( int idMatiere );

        [OperationContract]
        void deleteMS( int idEtudiant, int idMatiere );

        [OperationContract]
        void deleteMSByEtudiant( int idEtudiant );

        [OperationContract]
        void deleteMSByMatiere( int idMatiere );
    }
}
