using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Data.SqlClient;

namespace MasterProgram.WebServices
{
    using MasterProgram.WebServices.DataModel;
    using System.IO;

    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "MasterProgamDbService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez MasterProgamDbService.svc ou MasterProgamDbService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class MasterProgamDbService : IMasterProgamDbService
    {
        protected static String databaseServer = "xgpgwhhd2b.database.windows.net,1433";
        protected static String databaseName = "MasterProgramDB";
        protected static String username = "valiani.virapinmodely@xgpgwhhd2b";
        protected static String password = "Spurious7899";
        protected static String MasterProgramDbConnectionString;

        public MasterProgamDbService()
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = databaseServer,
                InitialCatalog = databaseName,
                Encrypt = true,
                TrustServerCertificate = false,
                UserID = username,
                Password = password
            };
            MasterProgramDbConnectionString = connectionString.ToString();

        }//fin constructeur

        #region Service Methods

        public Diplome getDiplome(String IdDiplome)
        {

            List<UniteEnseignement> ues = null;

            var select = "SELECT diplome.IdDiplome, diplome.Intitule" + "\n";
            var from = "FROM formation.Diplome diplome" + "\n";
            var where = "WHERE diplome.IdDiplome = {0}";

            var diplome = new Diplome();

            using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
            {
                dbConnection.Open();

                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    sqlCommand.CommandText = String.Format(select + from + where, IdDiplome);

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    ues = getUEs(IdDiplome, sqlCommand);

                    reader.Read();

                    String id = (String)reader["IdDiplome"];
                    String intitule = (String)reader["Intitule"];

                    reader.Close();

                    diplome.IdDiplome = id;
                    diplome.Intitule = intitule;
                    diplome.UEs = ues;

                }//fin SqlCommand

                dbConnection.Close();
            }//fin dbConnection

            return diplome;
        }//fin getDiplome()


        public List<Diplome> getAllDiplomes()
        {
            List<Diplome> diplomes = null;
            List<UniteEnseignement> ues = null;

            using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
            {
                dbConnection.Open();

                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM formation.Diplome";
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var id = (String)reader["IdDiplome"];
                        ues = getUEs(id, sqlCommand);
                        var diplome = new Diplome
                        {
                            IdDiplome = (String)reader["IdDiplome"],
                            Intitule = (String)reader["Intitule"],
                            UEs = ues
                        };
                        diplomes.Add(diplome);
                    }// fin While
                }//fin SqlCommand
                dbConnection.Close();
            }
            return diplomes;
        }//fin getMatiere ()


        public UniteEnseignement getUE(int IdUE)
        {
            var select = "SELECT ue.IdUE, ue.Intitule, ue.Descriptif, ue.Semestre " + "\n";
            var from = "FROM formation.UE ue " + "\n";
            var where = "WHERE ue.IdUE = {0}";

            var ue = new UniteEnseignement();
            List<Diplome> diplomes = null;

            using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
            {
                dbConnection.Open();

                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    sqlCommand.CommandText = String.Format(select + from + where, IdUE);

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    diplomes = getDiplomes(IdUE, sqlCommand);

                    reader.Read();

                    int id = (int)reader["IdUE"];
                    String intitule = (String)reader["Intitule"];
                    String descriptif = (String)reader["Descriptif"];
                    int semestre = (int)reader["Semestre"];

                    reader.Close();

                    ue.IdUE = id;
                    ue.Intitule = intitule;
                    ue.Descriptif = descriptif;
                    ue.Semestre = semestre;
                    ue.Diplomes = diplomes;

                }//fin SqlCommand

                dbConnection.Close();
            }//fin dbConnection

            return ue;
        }//fin getUE()


        public List<UniteEnseignement> getAllUEs()
        {

            List<UniteEnseignement> ues = null;
            List<Diplome> diplomes = null;

            using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
            {
                dbConnection.Open();

                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM formation.UE";
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    ues = new List<UniteEnseignement>();

                    while (reader.Read())
                    {
                        var id = (int)reader["IdUE"];
                        diplomes = getDiplomes(id, sqlCommand);
                        var ue = new UniteEnseignement
                        {
                            IdUE = (int)reader["IdUE"],
                            Intitule = (String)reader["Intitule"],
                            Descriptif = (String)reader["Descriptif"],
                            Semestre = (int)reader["Semestre"],
                            Diplomes = diplomes
                        };
                        ues.Add(ue);
                    }// fin While
                }//fin SqlCommand
                dbConnection.Close();
            }
            return ues;
        }//fin getAllUEs()


        public Module getModule(int IdModule)
        {
            var select = "SELECT module.Intitule, module.ModuleChoix, module.IdUE" + "\n";
            var from = "FROM formation.MODULE module " + "\n";
            var where = "WHERE module.IdModule = {0} ";

            var module = new Module();

            using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
            {
                dbConnection.Open();

                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    var ue = getUE(IdModule, sqlCommand);
                    sqlCommand.CommandText = String.Format(select + from + where, IdModule);

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();

                    String intitule = (String)reader["Intitule"];
                    bool moduleChoix = (bool)reader["ModuleChoix"];

                    reader.Close();

                    module.IdModule = IdModule;
                    module.Intitule = intitule;
                    module.ModuleChoix = moduleChoix;
                    module.UE = ue;

                }//fin SqlCommand

                dbConnection.Close();
            }//fin dbConnection

            return module;
        }//fin getModule()


        public List<Module> getAllModules()
        {
            List<Module> modules = null;

            using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
            {
                dbConnection.Open();

                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM formation.MODULES";
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    modules = new List<Module>();

                    while (reader.Read())
                    {
                        var id = (int)reader["IdModule"];
                        var intitule = (String)reader["Intitule"];
                        var moduleChoix = (bool)reader["ModuleChoix"];
                        var ue = getUE(id, sqlCommand);

                        var module = new Module
                        {
                            IdModule = id,
                            Intitule = intitule,
                            ModuleChoix = moduleChoix,
                            UE = ue
                        };

                        modules.Add(module);
                    }// fin While
                }//fin SqlCommand
                dbConnection.Close();
            }
            return modules;
        }//fin getAllModules()


        public Matiere getMatiere(int IdMatiere)
        {
            var select = "SELECT matiere.IdMatiere, matiere.Intitule, matiere.NbCM, matiere.NbTD, matiere.NbTD" + "\n";
            var from = "FROM formation.Matiere matiere " + "\n";
            var where = "WHERE matiere.IdMatiere = {0}";

            var matiere = new Matiere();

            using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
            {
                dbConnection.Open();

                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    sqlCommand.CommandText = String.Format(select + from + where, IdMatiere);

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();

                    int id = (int)reader["IdMatiere"];
                    String intitule = (String)reader["Intitule"];
                    int nbCM = (int)reader["NbCM"];
                    int nbTD = (int)reader["NbTD"];
                    int nbTP = (int)reader["NbTP"];
                    int idModule = (int)reader["ModuleAssocie"];
                    reader.Close();

                    matiere.IdMatiere = id;
                    matiere.Intitule = intitule;
                    matiere.NbCM = nbCM;
                    matiere.NbTD = nbTD;
                    matiere.NbTP = nbTP;
                    matiere.ModuleAssocie = getModule(id);

                }//fin SqlCommand

                dbConnection.Close();
            }//fin dbConnection

            

            return matiere;
        }//fin getMatiere ()


        public List<Matiere> getAllMatieres()
        {
            List<Matiere> matieres = null;

            using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
            {
                dbConnection.Open();

                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM formation.MATIERE";
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    matieres = new List<Matiere>();

                    while (reader.Read())
                    {
                        var matiere = new Matiere
                        {
                            IdMatiere = (int)reader["IdMatiere"],
                            Intitule = (String)reader["Intitule"]
                        };
                        matieres.Add(matiere);
                    }// fin While
                }//fin SqlCommand
                dbConnection.Close();
            }
            return matieres;
        }//fin getMatiere ()

        #endregion

        #region Helper Methods
        protected List<UniteEnseignement> getUEs(String IdDiplome, SqlCommand sqlCommand)
        {
            var select = "SELECT ue.Intitule, ue.Descriptif, ue.Semestre ";
            var from = "FROM formation.UE ue";
            var where = "WHERE ue.IdUE = {0}";

            var IdUEs = new List<int>();
            IdUEs = getIdUEs(IdDiplome, sqlCommand);

            var UEs = new List<UniteEnseignement>();

            foreach (int id in IdUEs)
            {
                sqlCommand.CommandText = String.Format(select + from + where, id);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                reader.Read();

                var ue = new UniteEnseignement
                {
                    IdUE = id,
                    Intitule = (String)reader["Intitule"],
                    Descriptif = (String)reader["Descriptif"],
                    Semestre = (int)reader["Semestre"],
                    Diplomes = null
                };

                UEs.Add(ue);
            }
            return UEs;
        }//fin getUEs

        protected List<int> getIdUEs(String IdDiplome, SqlCommand sqlCommand)
        {
            var select = "SELECT constitution.IdUE";
            var from = "FROM formation.CONSTITUTION_DIPLOME constitution";
            var where = "WHERE constitution.IdDiplome = {0}";

            sqlCommand.CommandText = String.Format(select + from + where, IdDiplome);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            var IdUEs = new List<int>();

            while (reader.Read())
            {
                var IdUE = (int)reader["IdUE"];
                IdUEs.Add(IdUE);
            }
            return IdUEs;

        }//fin getIdUEs()

        protected List<Diplome> getDiplomes(int IdUE, SqlCommand sqlCommand)
        {
            var select = "SELECT diplome.Intitule";
            var from = "FROM formation.DIPLOME diplome";
            var where = "WHERE diplome.IdDiplome = {0} ";

            var IdDiplomes = getIdDiplomes(IdUE, sqlCommand);
            var Diplomes = new List<Diplome>();

            foreach (String id in IdDiplomes)
            {
                sqlCommand.CommandText = String.Format(select + from + where, id);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                reader.Read();

                var diplome = new Diplome
                {
                    IdDiplome = id,
                    Intitule = (String)reader["Intitule"],
                    UEs = null
                };

                Diplomes.Add(diplome);
            }
            return Diplomes;

        }//fin getDiplomes

        protected List<String> getIdDiplomes(int IdUE, SqlCommand sqlCommand)
        {
            var select = "SELECT constitution.IdDiplome";
            var from = "FROM formation.CONSTITUTION_DIPLOME constitution";
            var where = "WHERE constitution.IdUE = {0}";

            sqlCommand.CommandText = String.Format(select + from + where, IdUE);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            var IdDiplomes = new List<String>();
            while (reader.Read())
            {
                var IdDiplome = (String)reader["IdDiplome"];
                IdDiplomes.Add(IdDiplome);
            }
            return IdDiplomes;

        }//fin getIdDiplome

        protected UniteEnseignement getUE(int IdModule, SqlCommand sqlCommand)
        {
            var select = "SELECT ue.Intitule, ue.Descriptif, ue.Semestre";
            var from = "FROM formation.UE ue";
            var where = "WHERE ue.IdUE = {0}";

            var id = getIdUE(IdModule, sqlCommand);
            var diplomes = getDiplomes(id, sqlCommand);

            sqlCommand.CommandText = String.Format(select + from + where, id);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();

            String intitule = (String)reader["Intitule"];
            String descriptif = (String)reader["Descriptif"];
            int semestre = (int)reader["Semestre"];


            reader.Close();
            var ue = new UniteEnseignement
            {
                IdUE = id,
                Intitule = intitule,
                Descriptif = descriptif,
                Semestre = semestre,
                Diplomes = diplomes
            };

            return ue;

        }

        protected int getIdUE(int IdModule, SqlCommand sqlCommand)
        {
            var select = "SELECT module.IdUE, ue";
            var from = "FROM formation.MODULE module";
            var where = "WHERE module.IdModule = {0}";
            sqlCommand.CommandText = String.Format(select + from + where, IdModule);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            int id = (int)reader["IdUE"];
            reader.Close();

            return id;
        }

        protected Module getModule (int IdMatiere,SqlCommand sqlCommand)
        {
            var select = "SELECT module.Intitule, module.ModuleChoix";
            var from = "FROM formation.MODULE module";
            var where = "WHERE module.IdModule = {0}";

            var id = getIdModule(IdMatiere, sqlCommand);
            var ue = getUE(id, sqlCommand);

            sqlCommand.CommandText = String.Format(select + from + where, id);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();

            String intitule = (String)reader["Intitule"];
            bool moduleChoix = (bool)reader["ModuleChoix"];

            reader.Close();
            var module = new Module
            {
                IdModule = id,
                Intitule = intitule,
                ModuleChoix = moduleChoix,
                UE = ue
            };

            return module; 
        }

        protected int getIdModule (int IdMatiere, SqlCommand sqlCommand)
        {
            var select = "SELECT matiere.ModuleAssocie";
            var from = "FROM formation.MATIERE matiere";
            var where = "WHERE Matiere.ModuleAssocie= {0}";
            sqlCommand.CommandText = String.Format(select + from + where, IdMatiere);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            int id = (int)reader["ModuleAssocie"];
            reader.Close();

            return id;
        }

        #endregion
    }//fin classe
}//fin namespace
