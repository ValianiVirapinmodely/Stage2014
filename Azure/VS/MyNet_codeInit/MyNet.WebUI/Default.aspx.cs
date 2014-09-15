using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyNet.WebUI
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e) { } // Method 

        protected void AddContactButton_Click(object sender, EventArgs e)
        {

            // Service client 
            var myNetDbService = new MyNetDbService.MyNetDbServiceClient();

            // Create Contact object based on info in Text fields 
            var contact = new MyNetDbService.Contact
            {
                Firstname = FirstnameTextBox.Text,
                Lastname = LastnameTextBox.Text
            };

            // Send the New Contact to the service for insertion in DB 
            myNetDbService.addContact(contact);

            // Update the interface 
            ContactsGridView.DataBind();
            FirstnameTextBox.Text = "";
            LastnameTextBox.Text = "";

        } // Method 

    } // Class 
}