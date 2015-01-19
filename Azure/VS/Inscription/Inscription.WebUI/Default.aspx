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
 
                Année <asp:TextBox style="display:inline-block" ID="AnneeTextBox" runat="server"/> <br />
                <asp:CompareValidator ID="CV1" runat="server" ControlToValidate="AnneeTextBox" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Veuillez saisir un entier" Display="Dynamic" ValidationGroup="add"/>
                <asp:RequiredFieldValidator ID="RFV1" runat="server" ControlToValidate="AnneeTextBox" ErrorMessage="L'année doit être renseignée" Display="Dynamic" ValidationGroup="add"/>
                <asp:RangeValidator ID="RV1" runat="server" ControlToValidate="AnneeTextBox" MinimumValue="1900" MaximumValue="2050" ErrorMessage="L'année doit être comprise entre 1900 et 2050" Display="Dynamic" ValidationGroup="add"/> <br />

                IdEtudiant <asp:TextBox ID="IdEtudiantTextBox" runat="server"/> <br />
                <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="IdEtudiantTextBox" ErrorMessage="L'id de l'étudiant doit être renseigné" Display="Dynamic" ValidationGroup="add"/>
                <asp:CompareValidator ID="CV2" runat="server" ControlToValidate="IdEtudiantTextBox" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Veuillez saisir un entier" Display="Dynamic" ValidationGroup="add"/> <br />

                IdMatière <asp:TextBox ID="IdMatiereTextBox" runat="server"/> <br />
                <asp:RequiredFieldValidator ID="RFV3" runat="server" ControlToValidate="IdMatiereTextBox" ErrorMessage="L'id de matière doit être renseigné" Display="Dynamic" ValidationGroup="add"/>
                <asp:CompareValidator ID="CV3" runat="server" ControlToValidate="IdMatiereTextBox" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Veuillez saisir un entier" Display="Dynamic" ValidationGroup="add"/> <br />

                <asp:Button ID="AddEtudiantButton" runat="server" Text="Add" OnClick="AddEtudiantButton_Click" ValidationGroup="add"/> 

            </td>

            <td>
                <h2>Delete Participation of a Student to a Lesson</h2>

                IdEtudiant <asp:TextBox ID="DeleteEtudiantTextBox" runat="server"/> <br /> 
                <asp:RequiredFieldValidator ID="RFV4" runat="server" ControlToValidate="DeleteEtudiantTextBox" ErrorMessage="L'id de l'étudiant doit être renseigné" Display="Dynamic" ValidationGroup="del1"/>
                <asp:CompareValidator ID="CV4" runat="server" ControlToValidate="DeleteEtudiantTextBox" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Veuillez saisir un entier" Display="Dynamic" ValidationGroup="del1"/> <br />

                IdMatière <asp:TextBox ID="DeleteMatiereTextBox" runat="server"/> <br /> 
                <asp:RequiredFieldValidator ID="RFV5" runat="server" ControlToValidate="DeleteMatiereTextBox" ErrorMessage="L'id de la matière doit être renseigné" Display="Dynamic" ValidationGroup="del1"/>
                <asp:CompareValidator ID="CV5" runat="server" ControlToValidate="DeleteMatiereTextBox" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Veuillez saisir un entier" Display="Dynamic" ValidationGroup="del1"/> <br />

                <asp:Button ID="DeleteEtudiantButton" runat="server" Text="Delete" OnClick="DeleteEtudiantButton_Click" ValidationGroup="del1"/>

                <h2>Delete Participation of a Student to all Lessons</h2>

                IdEtudiant <asp:TextBox ID="DeleteByEtudiantTextBox" runat="server" /> <br />
                <asp:RequiredFieldValidator ID="RFV6" runat="server" ControlToValidate="DeleteByEtudiantTextBox" ErrorMessage="L'id de l'étudiant doit être renseigné" Display="Dynamic" ValidationGroup="del2"/>
                <asp:CompareValidator ID="CV6" runat="server" ControlToValidate="DeleteByEtudiantTextBox" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Veuillez saisir un entier" Display="Dynamic" ValidationGroup="del2"/> <br />

                <asp:Button ID="DeleteByEtudiantButton" runat="server" Text="Delete" OnClick="DeleteByEtudiantButton_Click" ValidationGroup="del2"/>               

                <h2>Delete a Lesson</h2>

                IdMatière <asp:TextBox ID="DeleteByMatiereTextBox" runat="server"/> <br /> 
                <asp:RequiredFieldValidator ID="RFV7" runat="server" ControlToValidate="DeleteByMatiereTextBox" ErrorMessage="L'id de la matière doit être renseigné" Display="Dynamic" ValidationGroup="del3"/>
                <asp:CompareValidator ID="CV7" runat="server" ControlToValidate="DeleteByMatiereTextBox" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Veuillez saisir un entier" Display="Dynamic" ValidationGroup="del3"/> <br />

                <asp:Button ID="DeleteByMatiereButton" runat="server" Text="Delete" OnClick="DeleteByMatiereButton_Click" ValidationGroup="del3"/>
            </td>
        </tr>
    </table>
    
    <hr />

    <table>
        <tr>
            <td>
                <h1>Insertion of multiple lines</h1>
                <br />
                <h2>Load a file containing the couples to add</h2>
                <asp:FileUpload ID="MultipleFileUpload" runat="server" />
                <asp:Button ID="AddMultipleButton" runat="server" Text="Add Lines" OnClick="AddMultipleButton_Click" />
            </td>
        </tr>
    </table>
</asp:Content>