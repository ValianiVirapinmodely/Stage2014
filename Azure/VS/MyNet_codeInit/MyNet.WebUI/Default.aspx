<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="Default.aspx.cs" Inherits="MyNet.WebUI._Default" %> 

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent"> 
 
    <h2>List of Contacts</h2>       
    <asp:GridView ID="ContactsGridView" runat="server"  
        DataSourceID="MyNetDbServiceClient" 
        AutoGenerateColumns="False"> 
        <Columns> 
            <asp:BoundField DataField="Id" HeaderText="Id" /> 
            <asp:BoundField DataField="Firstname" HeaderText="Firstname"/> 
            <asp:BoundField DataField="Lastname" HeaderText="Lastname"/> 
        </Columns> 
    </asp:GridView> 
 
    <asp:ObjectDataSource ID="MyNetDbServiceClient" runat="server" 
        TypeName="MyNet.WebUI.MyNetDbService.MyNetDbServiceClient" 
        SelectMethod="getContacts" > 
    </asp:ObjectDataSource> 
 
<h2>Add New Contact</h2> 
    Firstname <asp:TextBox ID="FirstnameTextBox" runat="server"/> <br/> 
    Lastname <asp:TextBox ID="LastnameTextBox" runat="server"/> <br/> 
    <asp:Button ID="AddContactButton" runat="server" Text="Add" OnClick="AddContactButton_Click"/> 
   
</asp:Content>