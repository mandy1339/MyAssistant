<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MyAssistant.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Todo Login</title>
    <link rel="stylesheet" href ="CSS/StyleSheet1.css" type="text/css" />
    <link href="CSS/StyleSheet1.css" type="text/css" rel="stylesheet" />
    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <!-- JavaScript Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="center LoginDiv">
            <h1>Productivity Assistant</h1>
            <br />
            <label class="LoginLabel">User Name</label>
            <asp:TextBox ID="Txb_UserName" runat="server"></asp:TextBox>
            <br />
            <label class="LoginLabel">Password</label> 
            <asp:TextBox ID="Txb_Password" runat="server" type="password"></asp:TextBox>
            <br />
            <br />
            
            <asp:Button ID="Btn_Login" CssClass="LoginButton btn btn-success" runat="server" Text="Login" Height="32px" Width="87px" OnClick="Btn_Login_Click" />
            <br />
            <asp:Label ID="Lbl_Result" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
