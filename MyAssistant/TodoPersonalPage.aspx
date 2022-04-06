<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpages/TodoItemsMasterPage.Master" AutoEventWireup="true" CodeBehind="TodoPersonalPage.aspx.cs" Inherits="MyAssistant.TodoPersonalPage" %>
<%--head--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Javascript/DefaultJSHandlers.js" type="text/javascript"></script>
    <link href="CSS/StyleSheet2.css" type="text/css" rel="stylesheet" />
</asp:Content>



<%--content--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>        
        <header id="header1">         
            <h1>Productivity Assistant</h1>
        </header>
        <asp:RequiredFieldValidator ID="DescriptionValidator" runat="server" ControlToValidate="Txb_AddItem" ErrorMessage="Invalid Description" BackColor="#FA7A7A"></asp:RequiredFieldValidator>
        <%--<asp:RangeValidator ID="DueDateValidator" runat="server" ControlToValidate="Txb_DueDate" ErrorMessage="Invalid Date" MaximumValue="2090/12/31" MinimumValue="2020/01/01" Type="Date" BackColor="#FF7D5E"></asp:RangeValidator>--%>
        <br />
        <div id="AddTodoForm" class="center WhiteOnBlack Hidden"  >
                
            <label class="InputLabel">Description *</label>
                <asp:TextBox ID="Txb_AddItem" runat="server" placeholder="Todo Item Description" Width="482px" BackColor="#E5E5E5"></asp:TextBox>
                <br />
            <label class="InputLabel">Category *</label>
                <asp:RadioButton ID="Rd_Work" runat="server" GroupName="Category" Text="Work" ValidationGroup="CategoryValidationGroup" BackColor="Plum" />
                <asp:RadioButton ID="Rd_Personal" runat="server" GroupName="Category" Text="Personal" ValidationGroup="CategoryValidationGroup" BackColor="LightBlue"/>
                <asp:RadioButton ID="Rd_School" runat="server" GroupName="Category" Text="School" ValidationGroup="CategoryValidationGroup" BackColor="Moccasin" />
                <br />
            <label class="InputLabel">Due Date</label>
            <input id="DueDatePicker" type="date" oninput="datePickerInputJSHandler(this)" style ="background-color:#E5E5E5"/>
            <asp:HiddenField ClientIDMode="Static" ID="HiddenField_DueDate" runat="server" Value ="" />
                <br />
            <label class="InputLabel">Priority *</label>
            <asp:DropDownList ID="DD_Priority" runat="server" Width="46px">
                <asp:ListItem Selected="True">4</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Lb_Message" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Btn_AddItem" CssClass="AddButton w3-button w3-white w3-hide-small" runat="server" Text="Add" OnClick="Btn_AddItem_Click"/>
        </div>
    </div>
    <div class="center" id="TodoSectionDiv">
        <h3>Todo Items - Personal</h3>
            
        <button id="ButtonShowTodoForm" type="button" class="w3-button w3-black w3-hide-small" onclick="toggleShowTodoForm()">Add New | +</button> 
        <asp:Table ID="TodoTable1" runat="server" class="centerFill">
            <asp:TableRow runat="server" class="tableHeader">
                <asp:TableCell ID="isDoneCol" runat="server">isDone?</asp:TableCell>
                <asp:TableCell ID="descCol" runat="server">Description</asp:TableCell>
                <asp:TableCell ID="priorityCol" runat="server">Priority</asp:TableCell>
                <asp:TableCell ID="CategoryCol" runat="server">Category</asp:TableCell>
                <asp:TableCell ID="CreatedDateCol" runat="server">Created Date</asp:TableCell>
                <asp:TableCell ID="DueDateCol" runat="server">DueDate</asp:TableCell>
                <asp:TableCell ID="DeleteCol" runat="server">
                    <asp:ImageButton ID="Btn_Delete" Height="30" Width="30" AlternateText ="DeleteImage" ImageUrl="Images/Red-X.png" runat="server" OnClick="DeleteButton_Click"/></asp:TableCell>
            </asp:TableRow>
        </asp:Table>            
    </div> 
    <div class="center">
        <asp:Calendar ID="Calendar1" runat="server" class="centerFill" BackColor="White" BorderColor="Black" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="500px" NextPrevFormat="FullMonth" OnDayRender="Calendar1_DayRender" DayNameFormat="Shortest" TitleFormat="Month">
            <DayHeaderStyle Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" BackColor="#CCCCCC" />
            <DayStyle Width="14%" />
            <NextPrevStyle Font-Size="8pt" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
            <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
            <TodayDayStyle BackColor="#CCCC99" />
        </asp:Calendar>
    </div>
</asp:Content>
