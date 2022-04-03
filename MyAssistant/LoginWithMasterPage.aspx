<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpages/TodoItemsMasterPage.Master" AutoEventWireup="true" CodeBehind="LoginWithMasterPage.aspx.cs" Inherits="MyAssistant.LoginWithMasterPage" %>

<%--HEAD--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Javascript/DefaultJSHandlers.js" type="text/javascript"></script>
    <link href="CSS/StyleSheet2.css" type="text/css" rel="stylesheet" />
    <style>
        body,h1,h2,h3,h4,h5,h6 {font-family: "Raleway", sans-serif}
    </style>
</asp:Content>

<%--MAIN CONTENT--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center w3-container w3-padding-large w3-grey"">
        <h1><b>Login</b></h1>
        <br />
        <label class="LoginLabel">User Name</label>
        <asp:TextBox ID="Txb_UserName" runat="server"></asp:TextBox>
        <br />
        <label class="LoginLabel">Password</label> 
        <asp:TextBox ID="Txb_Password" runat="server" type="password"></asp:TextBox>
        <br />
        <br />
            
        <asp:Button ID="Btn_Login" CssClass="LoginButton w3-white w3-hover-black" runat="server" Text="Login" Height="32px" Width="87px" OnClick="Btn_Login_Click" />
        <br />
        <asp:Label ID="Lbl_Result" runat="server" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
