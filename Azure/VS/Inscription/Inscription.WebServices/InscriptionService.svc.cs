using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace Inscription.WebServices
{
    using Inscription.WebServices.DataModel;

    public class InscriptionService : IInscriptionService
    {
        static String databaseServer = "a8487r8k2p.database.windows.net,1433";
        static String databaseName = "InscriptionDB";
        static String username = "projet.cloud@a8487r8k2p";
        static String password = "Spurious7899|";

        static String InscriptionConnectionString;

        public InscriptionService( )
        {
            // Create DB connection string
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = databaseServer,
                InitialCatalog = databaseName,
                Encrypt = true,
                TrustServerCertificate = false,
                UserID = username,
                Password = password
            };

            InscriptionConnectionString = connectionString.ToString();
        } // Constructor

        // Add Matiere Suivie
        public void addMS(SuitMatiere MS)
        {
            using (var dbConnection = new SqlConnection(InscriptionConnectionString))
            {
                dbConnection.Open();

                // Execute SQL Statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    var annee = MS.Annee;
                    var idEtudiant = MS.IdEtudiant;
                    var idMatiere = MS.IdMatiere;

                    var sqlStatementTemplate = "INSERT INTO inscription.SuitMatiere ( Annee, IdEtudiant, IdMatiere ) VALUES ( N'{0}', N'{1}', N'{2}')";
                    var sqlStatement = String.Format( sqlStatementTemplate, annee, idEtudiant, idMatiere );

                    // Insert into DB
                    sqlCommand.CommandText = sqlStatement;
                    sqlCommand.ExecuteNonQuery();
                } // SQL Command

                dbConnection.Close();
            } // dbConnection
        } // Method

        // Get Matiere Suivie
        public List<SuitMatiere> getMS( )
        {
            List<SuitMatiere> matieresSuivies = null;

            // Connect to DB
            using (var dbConnection = new SqlConnection(InscriptionConnectionString))
            {
                dbConnection.Open();

                // Execute SQL Statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    // Select ALL matieres suivies
                    sqlCommand.CommandText = "SELECT * FROM inscription.SuitMatiere";
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    // crée la liste des matières
                    matieresSuivies = new List<SuitMatiere>();

                    while (reader.Read())
                    {
                        var matiere = new SuitMatiere
                        {
                            Annee = (String) reader["Annee"],
                            IdEtudiant = (int) reader["IdEtudiant"],
                            IdMatiere = (int) reader["IdMatiere"]
                        };

                        matieresSuivies.Add(matiere);
                    } // While reader

                } // SQL Command

                dbConnection.Close();

            } // dbConnection

            return matieresSuivies;

        } // Method

        public List<SuitMatiere> getMSByEtudiant(int idEtudiant)
        {
            List<SuitMatiere> matieresSuivies = null;

            // Connect to DB
            using (var dbConnection = new SqlConnection(InscriptionConnectionString))
            {
                dbConnection.Open();

                // Execute SQL Statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    // Select ALL matieres suivies
                    var sqlStatementTemplate = "SELECT * FROM inscription.SuitMatiere WHERE IdEtudiant = N'{0}'";
                    var sqlStatement = String.Format(sqlStatementTemplate, idEtudiant);

                    sqlCommand.CommandText = sqlStatement;
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    // crée la liste des matières
                    matieresSuivies = new List<SuitMatiere>();

                    while (reader.Read())
                    {
                        var matiere = new SuitMatiere
                        {
                            Annee = (String)reader["Annee"],
                            IdEtudiant = (int)reader["IdEtudiant"],
                            IdMatiere = (int)reader["IdMatiere"]
                        };

                        matieresSuivies.Add(matiere);
                    } // While reader

                } // SQL Command

                dbConnection.Close();

            } // dbConnection

            return matieresSuivies;

        } // Method

        public List<SuitMatiere> getMSByMatiere(int idMatiere)
        {
            List<SuitMatiere> matieresSuivies = null;

            // Connect to DB
            using (var dbConnection = new SqlConnection(InscriptionConnectionString))
            {
                dbConnection.Open();

                // Execute SQL Statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    // Select ALL matieres suivies
                    var sqlStatementTemplate = "SELECT * FROM inscription.SuitMatiere WHERE IdMatiere = N'{0}'";
                    var sqlStatement = String.Format(sqlStatementTemplate, idMatiere);

                    sqlCommand.CommandText = sqlStatement;
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    // crée la liste des matières
                    matieresSuivies = new List<SuitMatiere>();

                    while (reader.Read())
                    {
                        var matiere = new SuitMatiere
                        {
                            Annee = (String)reader["Annee"],
                            IdEtudiant = (int)reader["IdEtudiant"],
                            IdMatiere = (int)reader["IdMatiere"]
                        };

                        matieresSuivies.Add(matiere);
                    } // While reader

                } // SQL Command

                dbConnection.Close();

            } // dbConnection

            return matieresSuivies;

        } // Method

        public void deleteMS(int idEtudiant, int idMatiere)
        {
            // Connect to DB
            using (var dbConnection = new SqlConnection(InscriptionConnectionString))
            {
                dbConnection.Open();

                // Execute SQL Statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    // Select ALL matieres suivies
                    var sqlStatementTemplate = "DELETE FROM inscription.SuitMatiere WHERE IdEtudiant = N'{0}' AND IdMatiere = N'{1}'";
                    var sqlStatement = String.Format(sqlStatementTemplate, idEtudiant, idMatiere);
                    
                    sqlCommand.CommandText = sqlStatement;
                    sqlCommand.ExecuteNonQuery();
                } // SQL Command

                dbConnection.Close();
            }

        } // Method

        public void deleteMSByEtudiant(int idEtudiant)
        {
            // Connect to DB
            using (var dbConnection = new SqlConnection(InscriptionConnectionString))
            {
                dbConnection.Open();

                // Execute SQL Statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    // Select ALL matieres suivies
                    var sqlStatementTemplate = "DELETE FROM inscription.SuitMatiere WHERE IdEtudiant = N'{0}'";
                    var sqlStatement = String.Format(sqlStatementTemplate, idEtudiant);

                    sqlCommand.CommandText = sqlStatement;
                    sqlCommand.ExecuteNonQuery();
                } // SQL Command

                dbConnection.Close();
            }

        } // Method

        public void deleteMSByMatiere(int idMatiere)
        {
            // Connect to DB
            using (var dbConnection = new SqlConnection(InscriptionConnectionString))
            {
                dbConnection.Open();

                // Execute SQL Statements
                using (SqlCommand sqlCommand = dbConnection.CreateCommand())
                {
                    // Select ALL matieres suivies
                    var sqlStatementTemplate = "DELETE FROM inscription.SuitMatiere WHERE IdMatiere = N'{0}'";
                    var sqlStatement = String.Format(sqlStatementTemplate, idMatiere);

                    sqlCommand.CommandText = sqlStatement;
                    sqlCommand.ExecuteNonQuery();
                } // SQL Command

                dbConnection.Close();
            }

        } // Method

    } // Class
}
