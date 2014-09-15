using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Student.WebServices
{
    using Student.WebServices.DataModel;
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IStudentDBService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IStudentDBService
    {
        [OperationContract]
        Etudiant getEtudiant(int IdEtudiant);

        [OperationContract]
        List<Etudiant> getAllEtudiants();
    }
}
