<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyAssistant.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MyAssistant</title>
    <script src="Javascript/DefaultJSHandlers.js" type="text/javascript"></script>
    <link href="CSS/StyleSheet1.css" type="text/css" rel="stylesheet" />
    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <!-- JavaScript Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="float:right">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </div>
            <header id="header1">
                <div style="float:right">
                    <asp:Label ID="Lbl_UserName" runat="server" Text="Label"></asp:Label>
                </div>
                <h1>Productivity Assistant</h1>
            </header>
            <asp:RequiredFieldValidator ID="DescriptionValidator" runat="server" ControlToValidate="Txb_AddItem" ErrorMessage="Invalid Description" BackColor="#FA7A7A"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="DueDateValidator" runat="server" ControlToValidate="Txb_DueDate" ErrorMessage="Invalid Date" MaximumValue="2090/12/31" MinimumValue="2020/01/01" Type="Date" BackColor="#FF7D5E"></asp:RangeValidator>
            <button id="ButtonShowTodoForm" type="button" class="btn btn-success" onclick="toggleShowTodoForm()">Add New | +</button> 
            <br />
            <div id="AddTodoForm" class="center WhiteOnBlack Hidden"  >
                
                    <asp:TextBox ID="Txb_AddItem" runat="server" placeholder="Todo Item Description" Width="808px" BackColor="#E5E5E5"></asp:TextBox>
                    <br />
                    <asp:RadioButton ID="Rd_Work" runat="server" GroupName="Category" Text="Work" ValidationGroup="CategoryValidationGroup" BackColor="Plum" />
                    <asp:RadioButton ID="Rd_Personal" runat="server" GroupName="Category" Text="Personal" ValidationGroup="CategoryValidationGroup" BackColor="LightBlue"/>
                    <asp:RadioButton ID="Rd_School" runat="server" GroupName="Category" Text="School" ValidationGroup="CategoryValidationGroup" BackColor="Moccasin" />
                    &nbsp;<br />
                    <asp:TextBox ID="Txb_DueDate" runat="server" onclick="showDueDatePickerCalendar()" placeholder="Due Date" ToolTip="Enter Due Date If There Is One" BackColor="#E5E5E5" Width="196px"></asp:TextBox>
                
                <asp:Calendar ID="CalendarDueDatePicker" hidden="true" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="CalendarDueDatePicker_SelectionChanged" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom"  />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>                
                    <br />
&nbsp;Priority
                <asp:DropDownList ID="DD_Priority" runat="server" Width="46px">
                    <asp:ListItem Selected="True">4</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Lb_Message" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Button ID="Btn_AddItem" runat="server" Text="Add New" OnClick="Btn_AddItem_Click"/>
            </div>
        </div>
        <div class="center">
            <h3>Todo Items</h3>
            <asp:Table ID="TodoTable1" runat="server" class="centerFill">
                <asp:TableRow runat="server" class="tableHeader">
                    <asp:TableCell ID="isDoneCol" runat="server">isDone?</asp:TableCell>
                    <asp:TableCell ID="descCol" runat="server">Description</asp:TableCell>
                    <asp:TableCell ID="priorityCol" runat="server">Priority</asp:TableCell>
                    <asp:TableCell ID="CategoryCol" runat="server">Category</asp:TableCell>
                    <asp:TableCell ID="CreatedDateCol" runat="server">Created Date</asp:TableCell>
                    <asp:TableCell ID="DueDateCol" runat="server">DueDate</asp:TableCell>
                    <asp:TableCell ID="DeleteCol" runat="server">
                        <asp:ImageButton ID="Btn_Delete" Height="30" Width="30" AlternateText ="DeleteImage" ImageUrl="Images/trash-can.png" runat="server" OnClick="DeleteButton_Click"/></asp:TableCell>
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
    </form>
</body>
</html>
