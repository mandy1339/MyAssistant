<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MyAssistant.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Productivity Assistant</h1>
            <br />
            User Name <asp:TextBox ID="Txb_UserName" runat="server"></asp:TextBox>
            <br />
            Password &nbsp; <asp:TextBox ID="Txb_Password" runat="server" type="password"></asp:TextBox>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btn_Login" runat="server" Text="Login" Height="32px" Width="87px" OnClick="Btn_Login_Click" />
            <br />
            <asp:Label ID="Lbl_Result" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
