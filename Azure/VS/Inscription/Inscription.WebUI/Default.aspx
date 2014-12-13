<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Inscription.WebUI._Default" %> 

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">   
    
    <table>
        <tr>
            <td>
                <h2>List of Students and Lessons</h2>

                <asp:GridView ID="CoupleGridView" runat="server"  
                    DataSourceID="InscriptionServiceClient" 
                    AutoGenerateColumns="False" > 
                    <Columns> 
                        <asp:BoundField DataField="Annee" HeaderText="Année" />
                        <asp:BoundField DataField="IdEtudiant" HeaderText="IdEtudiant" /> 
                        <asp:BoundField DataField="IdMatiere" HeaderText="IdMatière" /> 
                    </Columns> 
                </asp:GridView> 
 
                <asp:ObjectDataSource ID="InscriptionServiceClient" runat="server" 
                    TypeName="Inscription.WebUI.InscriptionService.InscriptionServiceClient" 
                    SelectMethod="getMS" > 
                </asp:ObjectDataSource> 
 
                <h2>Add New Student to a Lesson</h2> 
 
                Année <asp:TextBox ID="AnneeTextBox" runat="server"/> <br />
                IdEtudiant <asp:TextBox ID="IdEtudiantTextBox" runat="server"/> <br /> 
                IdMatière <asp:TextBox ID="IdMatiereTextBox" runat="server"/> <br /> 
                <asp:Button ID="AddEtudiantButton" runat="server" Text="Add" OnClick="AddEtudiantButton_Click" /> 
            </td>

            <td>
                <h2>Delete Participation of a Student to a Lesson</h2>

                IdEtudiant <asp:TextBox ID="DeleteEtudiantTextBox" runat="server"/> <br /> 
                IdMatière <asp:TextBox ID="DeleteMatiereTextBox" runat="server"/> <br /> 
                <asp:Button ID="DeleteEtudiantButton" runat="server" Text="Delete" OnClick="DeleteEtudiantButton_Click" />

                <h2>Delete Participation of a Student to all Lessons</h2>

                IdEtudiant <asp:TextBox ID="DeleteByEtudiantTextBox" runat="server" /> <br /> 
                <asp:Button ID="DeleteByEtudiantButton" runat="server" Text="Delete" OnClick="DeleteByEtudiantButton_Click" />

                <h2>Delete a Lesson</h2>

                IdMatière <asp:TextBox ID="DeleteByMatiereTextBox" runat="server"/> <br /> 
                <asp:Button ID="DeleteByMatiereButton" runat="server" Text="Delete" OnClick="DeleteByMatiereButton_Click" />
            </td>
        </tr>
    </table> 
</asp:Content>