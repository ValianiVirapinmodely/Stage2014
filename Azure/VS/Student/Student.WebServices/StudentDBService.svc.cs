using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace Student.WebServices
{
    using Student.WebServices.DataModel;
    using System.IO;
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "StudentDBService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez StudentDBService.svc ou StudentDBService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class StudentDBService : IStudentDBService
    {
        protected static String databaseServer = "xgpgwhhd2b.database.windows.net,1433";
        protected static String databaseName = "StudentDB";
        protected static String username = "valiani.virapinmodely@xgpgwhhd2b";
        protected static String password = "Spurious7899";

        protected static String StudentDbConnectionString;


        public StudentDBService()
        {
            var connexionString = new SqlConnectionStringBuilder
            {
                DataSource = databaseServer,
                InitialCatalog = databaseName,
                Encrypt = true,
                TrustServerCertificate = false,
                UserID = username,
                Password = password
            };

            StudentDbConnectionString = connexionString.ToString();
        }//fin constructeur 

        //getStudent
        public Etudiant getEtudiant(int IdEtudiant)
        {
            var select = "SELECT etudiant.IdEtudiant, etudiant.Erasmus, etudiant.DernierDiplomeObtenu, etudiant.OrigineDiplome, etudiant.Natinoalite" + "\n"; 
            var from = "FROM student.ETUDIANT etudiant" + "\n";
            var where = "etudiant.IdEtudiant = {0}";

            var etudiant = new Etudiant();

            using (var dbConnection = new SqlConnection(StudentDbConnectionString))
            {
                dbConnection.Open();
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    sqlCommand.CommandText = String.Format(select + from + where, IdEtudiant);

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();

                    int id = (int)reader["IdEtudiant"];
                    bool erasmus = (bool)reader["Erasmus"];
                    String dernierDiplomeObtenu = (String)reader["DernierDiplomeObtenu"];
                    String origineDiplome = (String)reader["OrigineDiplome"];
                    String nationalite = (String)reader["Nationalite"];

                    reader.Close();

                    etudiant.IdEtudiant = id;
                    etudiant.Erasmus = erasmus;
                    etudiant.DernierDiplomeObtenu = dernierDiplomeObtenu;
                    etudiant.OrigineDiplome = origineDiplome;
                    etudiant.Nationalite = nationalite;
                }

                dbConnection.Close();
            }
            return etudiant;
        }//fin getEtudiant


        //getAllStudent
        public List<Etudiant> getAllEtudiants()
        {
            List<Etudiant> etudiants = null;

            using (var dbConnection = new SqlConnection(StudentDbConnectionString))
            {
                dbConnection.Open();
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM student.ETUDIANT";
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    etudiants = new List<Etudiant>();
                    while (reader.Read())
                    {
                        var etudiant = new Etudiant
                        {
                            IdEtudiant = (int)reader["IdEtudiant"],
                            Erasmus = (bool)reader["Erasmus"], 
                            DernierDiplomeObtenu = (String)reader["DernierDiplomeObtenu"],
                            OrigineDiplome = (String)reader["OrigineDiplome"],
                            Nationalite = (String)reader["Nationalite"]
                        };
                    }
                }
                dbConnection.Close();
            }
            return etudiants;
        }//fin getAllEtudiants
    }
}
