using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inscription.WebUI
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e) { } // Method 

        protected void AddEtudiantButton_Click(object sender, EventArgs e)
        {

            // Service client 
            var InscriptionService = new InscriptionService.InscriptionServiceClient();

            // Vérifie que l'année contient bien 4 caractères
            if (AnneeTextBox.Text.Length != 4)
            {
                // TODO : Changer ça en faisant apparaître un label sur le site
                Console.Write("Erreur d'année");
                return;
            }

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
                Console.Write("Erreur de format");
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
                    Console.Write("Erreur à l'insertion - Clé Primaire dupliquée");
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
                Console.Write("Erreur de format");
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
                Console.Write("Erreur de format");
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
                Console.Write("Erreur de format");
                return;
            }

            int IdMatiere = Int32.Parse(DeleteByMatiereTextBox.Text);

            InscriptionService.deleteMSByMatiere(IdMatiere);

            // Update the interface 
            CoupleGridView.DataBind();
            DeleteByMatiereTextBox.Text = "";

        } // Method

    } // Class
}