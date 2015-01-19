using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;

namespace Inscription.WebUI
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e) { } // Method 

        protected void AddEtudiantButton_Click(object sender, EventArgs e)
        {

            // Service client 
            var InscriptionService = new InscriptionService.InscriptionServiceClient();

            // Vérifie que les chaînes passées sont bien des nombres
            try
            {
                Int32.Parse(AnneeTextBox.Text);
                Int32.Parse(IdEtudiantTextBox.Text);
                Int32.Parse(IdMatiereTextBox.Text);
            }
            catch (FormatException)
            {
                // TODO : Changer ça en faisant apparaître un label sur le site
                System.Diagnostics.Debug.WriteLine("Erreur de format");
                return;
            }

            // Create Contact object based on info in Text fields 
            var couple = new InscriptionService.SuitMatiere
            {
                Annee = AnneeTextBox.Text,
                IdEtudiant = Int32.Parse(IdEtudiantTextBox.Text),
                IdMatiere = Int32.Parse(IdMatiereTextBox.Text)
            };

            // Send the New Contact to the service for insertion in DB 
            try
            {
                InscriptionService.addMS(couple);
            }
            catch (Exception exception)
            {
                //test
                if (exception.Message.StartsWith("Violation of PRIMARY KEY constraint 'primaryKey'."))
                {
                    // TODO : Changer ça en faisant apparaître un label sur le site
                    System.Diagnostics.Debug.WriteLine("Erreur à l'insertion - Clé Primaire dupliquée");
                    return;
                }
            }

            // Update the interface 
            CoupleGridView.DataBind();
            AnneeTextBox.Text = "";
            IdEtudiantTextBox.Text = "";
            IdMatiereTextBox.Text = "";

        } // Method

        protected void DeleteEtudiantButton_Click(object sender, EventArgs e)
        {

            // Service client 
            var InscriptionService = new InscriptionService.InscriptionServiceClient();

            // Vérifie que les chaînes passées sont bien des nombres
            try
            {
                Int32.Parse(DeleteEtudiantTextBox.Text);
                Int32.Parse(DeleteMatiereTextBox.Text);
            }
            catch (FormatException)
            {
                // TODO : Changer ça en faisant apparaître un label sur le site
                System.Diagnostics.Debug.WriteLine("Erreur de format");
                return;
            }

            int IdEtudiant = Int32.Parse(DeleteEtudiantTextBox.Text);
            int IdMatiere = Int32.Parse(DeleteMatiereTextBox.Text);

            InscriptionService.deleteMS(IdEtudiant,IdMatiere);

            // Update the interface 
            CoupleGridView.DataBind();
            DeleteEtudiantTextBox.Text = "";
            DeleteMatiereTextBox.Text = "";

        } // Method

        protected void DeleteByEtudiantButton_Click(object sender, EventArgs e)
        {

            // Service client 
            var InscriptionService = new InscriptionService.InscriptionServiceClient();

            // Vérifie que les chaînes passées sont bien des nombres
            try
            {
                Int32.Parse(DeleteByEtudiantTextBox.Text);
            }
            catch (FormatException)
            {
                // TODO : Changer ça en faisant apparaître un label sur le site
                System.Diagnostics.Debug.WriteLine("Erreur de format");
                return;
            }

            int IdEtudiant = Int32.Parse(DeleteByEtudiantTextBox.Text);

            InscriptionService.deleteMSByEtudiant(IdEtudiant);

            // Update the interface 
            CoupleGridView.DataBind();
            DeleteByEtudiantTextBox.Text = "";

        } // Method

        protected void DeleteByMatiereButton_Click(object sender, EventArgs e)
        {

            // Service client 
            var InscriptionService = new InscriptionService.InscriptionServiceClient();

            // Vérifie que les chaînes passées sont bien des nombres
            try
            {
                Int32.Parse(DeleteByMatiereTextBox.Text);
            }
            catch (FormatException)
            {
                // TODO : Changer ça en faisant apparaître un label sur le site
                System.Diagnostics.Debug.WriteLine("Erreur de format");
                return;
            }

            int IdMatiere = Int32.Parse(DeleteByMatiereTextBox.Text);

            InscriptionService.deleteMSByMatiere(IdMatiere);

            // Update the interface 
            CoupleGridView.DataBind();
            DeleteByMatiereTextBox.Text = "";

        } // Method

        //////////////////

        protected void AddMultipleButton_Click(object sender, EventArgs e)
        {

            // Service client 
            var InscriptionService = new InscriptionService.InscriptionServiceClient();

            if (MultipleFileUpload.HasFile)
            {
                var file = MultipleFileUpload.PostedFile;

                // Lit le fichier
                string filename = Path.GetFileName(file.FileName);
                System.Diagnostics.Debug.WriteLine(filename);

                // ATTENTION : Cela ne marche que sous OS Windows
                MultipleFileUpload.SaveAs(@"C:\Users\Public\Downloads\" + filename);

                string line;
                string pattern = @"^\d{4}[\-]\d+[\-]\d+$";
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                Regex splitter = new Regex(@"\-");

                // Read the file and display it line by line.
                System.IO.StreamReader fileRead = new System.IO.StreamReader(@"C:\Users\Public\Downloads\" + filename);

                while ((line = fileRead.ReadLine()) != null)
                {
                    System.Diagnostics.Debug.WriteLine(line);
                    // Faire une comparaison des lignes du fichier par exemple avec une regex
                    
                    Match match = rgx.Match(line);

                    if (match.Success)
                    {
                        // TODO Si la ligne est valide, l'ajouter à la base via un service déjà construit (addMS)
                        System.Diagnostics.Debug.WriteLine("Ligne correcte du fichier trouvée");

                        var donnees = splitter.Split(line);
                        var year = donnees[0];
                        var idEtud = donnees[1];
                        var idMat = donnees[2];

                        System.Diagnostics.Debug.WriteLine("Année="+year+" Etudiant="+idEtud+" Matiere="+idMat);

                        if (1900 <= Int32.Parse(year) && Int32.Parse(year) <= 2050)
                        {
                            System.Diagnostics.Debug.WriteLine("Ajout d'une ligne");

                            // Create Contact object based on info in Text fields 
                            var couple = new InscriptionService.SuitMatiere
                            {
                                Annee = year,
                                IdEtudiant = Int32.Parse(idEtud),
                                IdMatiere = Int32.Parse(idMat)
                            };

                            // Send the New Contact to the service for insertion in DB 
                            try
                            {
                                InscriptionService.addMS(couple);
                            }
                            catch (Exception exception)
                            {
                                //test
                                if (exception.Message.StartsWith("Violation of PRIMARY KEY constraint 'primaryKey'."))
                                {
                                    // TODO : Changer ça en faisant apparaître un label sur le site
                                    System.Diagnostics.Debug.WriteLine("Erreur à l'insertion - Clé Primaire dupliquée");
                                    return;
                                }
                            }

                            // Update the interface 
                            CoupleGridView.DataBind();
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Année incorrecte");
                        }

                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Ligne incorrecte trouvée et ignorée");
                    }
                    
                }

                fileRead.Close();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Pas de fichier sélectionné");
            }

        } // Method

    } // Class
}