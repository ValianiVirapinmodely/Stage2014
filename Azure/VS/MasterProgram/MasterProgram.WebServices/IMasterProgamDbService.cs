using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MasterProgram.WebServices
{
    using MasterProgram.WebServices.DataModel;

    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IMasterProgamDbService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IMasterProgamDbService
    {
        [OperationContract]
        Diplome getDiplome(String IdDiplome);

        [OperationContract]
        UniteEnseignement getUE(String IdUE);

        [OperationContract]
        Module getModule(String IdModule);

        [OperationContract]
        Matiere getMatiere(String IdMatiere);

        [OperationContract]
        List<Diplome> getAllDiplomes();

        [OperationContract]
        List<UniteEnseignement> getAllUEs();

        [OperationContract]
        List<Module> getAllModules();

        [OperationContract]
        List<Matiere> getAllMatieres();

    }
}
