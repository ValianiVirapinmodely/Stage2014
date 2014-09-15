<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenAuthProviders.ascx.cs" Inherits="MyNet.WebUI.Account.OpenAuthProviders" %>
<%@ Import Namespace="Microsoft.AspNet.Membership.OpenAuth" %>
<fieldset class="open-auth-providers">
    <legend>Connexion à l'aide d'un autre service</legend>
    
    <asp:ListView runat="server" ID="providersList" ViewStateMode="Disabled">
        <ItemTemplate>
            <button type="submit" name="provider" value="<%# HttpUtility.HtmlAttributeEncode(Item<ProviderDetails>().ProviderName) %>"
                title="Connexion à l'aide de votre <%# HttpUtility.HtmlAttributeEncode(Item<ProviderDetails>().ProviderDisplayName) %> compte.">
                <%# HttpUtility.HtmlEncode(Item<ProviderDetails>().ProviderDisplayName) %>
            </button>
        </ItemTemplate>
    
        <EmptyDataTemplate>
            <div class="message-info">
                <p>Aucun service d'authentification externe n'est configuré. Consultez <a href="http://go.microsoft.com/fwlink/?LinkId=252803">cet article</a> pour plus de détails sur la configuration de cette application ASP.NET afin qu'elle prenne en charge la connexion par l'intermédiaire de services externes.</p>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</fieldset>