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
        protected static String databaseServer = "a8487r8k2p.database.windows.net";
        protected static String databaseName = "MasterProgramDB";
        protected static String username = "projet.cloud@a8487r8k2p";
        protected static String password = "Spurious7899|";
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

                var select = "SELECT diplome.Intitule" + "\n";
                var from = "FROM formation.Diplome diplome" + "\n";
                var where = "WHERE diplome.IdDiplome = '{0}'";

                var diplome = new Diplome();

                using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
                {
                    dbConnection.Open();

                    using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                    {
                        ues = getUEs(IdDiplome, sqlCommand);

                        sqlCommand.CommandText = String.Format(select + from + where, IdDiplome);

                        SqlDataReader reader = sqlCommand.ExecuteReader();

                       

                        reader.Read();

                        String id = IdDiplome;
                        String intitule;
                        if (!reader.IsDBNull(1))
                        {
                            intitule = (String)reader["Intitule"];
                        }
                        else
                        {
                            intitule = null;
                        }
                        

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
                var diplomes = new List<Diplome>();
                List<UniteEnseignement> ues = null;

                using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
                {
                    dbConnection.Open();

                    using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = "SELECT * FROM formation.Diplome";
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        String intitule = null; 

                        while (reader.Read())
                        {

                            var id = (String)reader["IdDiplome"];

                            if (!reader.IsDBNull(1))
                            {
                                intitule = (String)reader["Intitule"];
                            }
                            else
                            {
                                intitule = null;
                            }
                            var diplome = new Diplome
                            {
                                IdDiplome = (String)reader["IdDiplome"],
                                Intitule = intitule,
                                UEs = ues
                            };
                            if (diplome!=null) {
                            diplomes.Add(diplome);
                            }
                        }// fin While
                        reader.Close();

                        foreach (Diplome diplome in diplomes)
                        {
                            var id = diplome.IdDiplome;
                            diplome.UEs = getUEs(id, sqlCommand);
                        }
                    }//fin SqlCommand
                    dbConnection.Close();
                }
                return diplomes;
            }//fin getMatiere ()


            public UniteEnseignement getUE(String IdUE)
            {
                var select = "SELECT ue.IdUE, ue.Intitule, ue.Descriptif, ue.Semestre " + "\n";
                var from = "FROM formation.UE ue " + "\n";
                var where = "WHERE ue.IdUE = '{0}'";

                var ue = new UniteEnseignement();
                List<Diplome> diplomes = null;

                using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
                {
                    dbConnection.Open();

                    using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                    {
                        diplomes = getDiplomes(IdUE, sqlCommand);
                        sqlCommand.CommandText = String.Format(select + from + where, IdUE);

                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        String intitule = null;
                        String descriptif = null;
                        int semestre = -1;
                        reader.Read();

                        String id = (String)reader["IdUE"];

                        if (!reader.IsDBNull(1))
                        {
                            intitule = (String)reader["Intitule"];
                        }

                        if (!reader.IsDBNull(2))
                        {
                            descriptif = (String)reader["Descriptif"];
                        }

                        if (!reader.IsDBNull(3))
                        {
                            semestre = (int)reader["Semestre"];
                        }

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
                        String intitule = null;
                        String descriptif = null;
                        int semestre = -1; 

                        while (reader.Read())
                        {
                            var id = (String)reader["IdUE"];

                            if (!reader.IsDBNull(1))
                            {
                                intitule = (String)reader["Intitule"];
                            }
                            else
                            {
                                intitule = null; 
                            }

                            if (!reader.IsDBNull(2))
                            {
                                descriptif = (String)reader["Descriptif"];
                            }
                            else
                            {
                                descriptif = null;
                            }

                            if (!reader.IsDBNull(3))
                            {
                                semestre = (int)reader["Semestre"];
                            }
                            else
                            {
                                semestre = -1;
                            }

                            var ue = new UniteEnseignement
                            {
                                IdUE = (String)reader["IdUE"],
                                Intitule = intitule,
                                Descriptif = descriptif,
                                Semestre = semestre,
                                Diplomes = diplomes
                            };

                            ues.Add(ue);
                        }// fin While
                        reader.Close();
                        foreach (UniteEnseignement ue in ues)
                        {
                            var id = ue.IdUE;
                            ue.Diplomes = getDiplomes(id, sqlCommand);
                        }
                    }//fin SqlCommand
                    dbConnection.Close();
                }
                return ues;
            }//fin getAllUEs()


            public Module getModule(String IdModule)
            {
                var select = "SELECT module.Intitule, module.ModuleChoix, module.IdUE" + "\n";
                var from = "FROM formation.MODULES module " + "\n";
                var where = "WHERE module.IdModules = '{0}' ";

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
                        String intitule = null;

                        if (!reader.IsDBNull(1))
                        {
                            intitule = (String)reader["Intitule"];
                        }

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
                        String intitule = null;

                        while (reader.Read())
                        {
                            var id = (String)reader["IdModule"];

                            if (!reader.IsDBNull(1))
                            {
                                intitule = (String)reader["Intitule"];
                            }
                            else
                            {
                                intitule = null;
                            }

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
                        reader.Close();
                    }//fin SqlCommand
                    dbConnection.Close();
                }
                return modules;
            }//fin getAllModules()


            public Matiere getMatiere(String IdMatiere)
            {
                var select = "SELECT matiere.IdMatiere, matiere.Intitule" + "\n";
                var from = "FROM formation.Matiere matiere " + "\n";
                var where = "WHERE matiere.IdMatiere = '{0}'";

                var matiere = new Matiere();

                using (var dbConnection = new SqlConnection(MasterProgramDbConnectionString))
                {
                    dbConnection.Open();

                    using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                    {
                        
                        sqlCommand.CommandText = String.Format(select + from + where, IdMatiere);

                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        reader.Read();

                        String intitule = null;
                        int nbCM = -1;
                        int nbTD = -1; 
                        int nbTP = -1; 
                  
                        if (!reader.IsDBNull(1))
                        {
                            intitule = (String)reader["Intitule"];
                        }

                        if (!reader.IsDBNull(2))
                        {
                            nbCM = (int)reader["nbCM"];
                        }

                        if (!reader.IsDBNull(3))
                        {
                            nbTD = (int)reader["nbTD"];
                        }

                        if (!reader.IsDBNull(4))
                        {
                            nbTP = (int)reader["nbTP"];
                        }


                        String id = (String)reader["IdMatiere"];
                        String idModule = (String)reader["IdModules"];

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
                        String intitule = null; 

                        while (reader.Read())
                        {

                            if (!reader.IsDBNull(1))
                            {
                                intitule = (String)reader["Intitule"];
                            }
                            else 
                            {
                                intitule = null; 
                            }
                            var matiere = new Matiere
                            {
                                IdMatiere = (String)reader["IdMatiere"],
                                Intitule = intitule
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
                var from = "FROM formation.UE ue ";
                var where = "WHERE ue.IdUE = '{0}'";

                var IdUEs = new List<String>();
                IdUEs = getIdUEs(IdDiplome, sqlCommand);

                var UEs = new List<UniteEnseignement>();

                String intitule = null; 
                String descriptif = null; 
                int semestre = -1; 

                foreach (String id in IdUEs)
                {
                    sqlCommand.CommandText = String.Format(select + from + where, id);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();

                    if (!reader.IsDBNull(1))
                    {
                        intitule = (String)reader["Intitule"];
                    }
                    else
                    {
                        intitule = null; 
                    }

                    if (!reader.IsDBNull(2))
                    {
                        descriptif = (String)reader["Intitule"];
                    }
                    else 
                    {
                        descriptif = null; 
                    }

                    if (!reader.IsDBNull(3))
                    {
                        semestre = (int)reader["Intitule"];
                    }
                    else 
                    {
                        semestre = -1; 
                    }

                    var ue = new UniteEnseignement
                    {
                        IdUE = id,
                        Intitule = intitule,
                        Descriptif =descriptif,
                        Semestre = semestre,
                        Diplomes = null
                    };
                    reader.Close();
                    if (ue != null)
                    {
                        UEs.Add(ue);
                    }
                }//fin foreach
                return UEs;
            }//fin getUEs()

            protected List<String> getIdUEs(String IdDiplome, SqlCommand sqlCommand)
            {
                var select = "SELECT constitution.IdUE ";
                var from = "FROM formation.CONSTITUTION_DIPLOME constitution ";
                var where = "WHERE constitution.IdDiplome = '{0}'";

                sqlCommand.CommandText = String.Format(select + from + where, IdDiplome);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                var IdUEs = new List<String>();

                while (reader.Read())
                {
                    var IdUE = (String)reader["IdUE"];
                    IdUEs.Add(IdUE);
                }
                reader.Close();
                return IdUEs;

            }//fin getIdUEs()

            protected List<Diplome> getDiplomes(String IdUE, SqlCommand sqlCommand)
            {
                var select = "SELECT diplome.Intitule ";
                var from = "FROM formation.DIPLOME diplome ";
                var where = "WHERE diplome.IdDiplome = '{0}' ";

                var IdDiplomes = getIdDiplomes(IdUE, sqlCommand);
                var Diplomes = new List<Diplome>();
                String intitule = null; 

                foreach (String id in IdDiplomes)
                {
                    sqlCommand.CommandText = String.Format(select + from + where, id);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();

                    
                    if (!reader.IsDBNull(1))
                    {
                        intitule = (String)reader["Intitule"];
                    }
                    else
                    {
                        intitule = null; 
                    }

                    var diplome = new Diplome
                    {
                        IdDiplome = id,
                        Intitule = intitule,
                        UEs = null
                    };

                    reader.Close();

                    Diplomes.Add(diplome);
                }
                return Diplomes;

            }//fin getDiplomes()

            protected List<String> getIdDiplomes(String IdUE, SqlCommand sqlCommand)
            {
                var select = "SELECT constitution.IdDiplome ";
                var from = "FROM formation.CONSTITUTION_DIPLOME constitution ";
                var where = "WHERE constitution.IdUE = '{0}'";

                sqlCommand.CommandText = String.Format(select + from + where, IdUE);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                var IdDiplomes = new List<String>();
                while (reader.Read())
                {
                    var IdDiplome = (String)reader["IdDiplome"];
                    IdDiplomes.Add(IdDiplome);
                }
                reader.Close();
                return IdDiplomes;

            }//fin getIdDiplome()

            protected UniteEnseignement getUE(String IdModule, SqlCommand sqlCommand)
            {
                var select = "SELECT ue.Intitule, ue.Descriptif, ue.Semestre ";
                var from = "FROM formation.UE ue ";
                var where = "WHERE ue.IdUE = '{0}'";

                var id = getIdUE(IdModule, sqlCommand);
                var diplomes = getDiplomes(id, sqlCommand);

                sqlCommand.CommandText = String.Format(select + from + where, id);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                reader.Read();

                String intitule = null; 
                String descriptif = null; 
                int semestre = -1; 

                if (!reader.IsDBNull(1))
                {
                    intitule = (String)reader["Intitule"];
                }
                if (!reader.IsDBNull(2))
                {
                    descriptif = (String)reader["Descriptif"];
                } 
                if (!reader.IsDBNull(3))
                {
                    semestre = (int)reader["Semestre"];
                } 
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

            }//fin get UE

            protected String getIdUE(String IdModule, SqlCommand sqlCommand)
            {
                var select = "SELECT module.IdUE, ue ";
                var from = "FROM formation.MODULES module ";
                var where = "WHERE module.IdModules = '{0}'";
                sqlCommand.CommandText = String.Format(select + from + where, IdModule);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                reader.Read();
                String id = (String)reader["IdUE"];
                reader.Close();

                return id;
            }//fin get IdUE

            protected Module getModule (String IdMatiere,SqlCommand sqlCommand)
            {
                var select = "SELECT module.Intitule, module.ModuleChoix ";
                var from = "FROM formation.MODULES module ";
                var where = "WHERE module.IdModules = '{0}'";

                var id = getIdModule(IdMatiere, sqlCommand);
                var ue = getUE(id, sqlCommand);

                sqlCommand.CommandText = String.Format(select + from + where, id);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                reader.Read();

                String intitule = null; 
                if (!reader.IsDBNull(1))
                {
                    intitule = (String)reader["Intitule"];
                } 
                
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
            }//fin get Module

            protected String getIdModule (String IdMatiere, SqlCommand sqlCommand)
            {
                var select = "SELECT matiere.IdModules ";
                var from = "FROM formation.MATIERE matiere ";
                var where = "WHERE matiere.IdMatiere= '{0}'";
                sqlCommand.CommandText = String.Format(select + from + where, IdMatiere);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                reader.Read();
                String id = (String)reader["IdModules"];
                reader.Close();

                return id;
            }//fin get IdModule


        #endregion
    }//fin classe
}//fin namespace
