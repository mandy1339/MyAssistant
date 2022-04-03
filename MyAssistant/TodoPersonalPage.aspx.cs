using MyAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyAssistant
{
    public partial class TodoPersonalPage : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //#if !DEBUG
            if (Session["isLoggedIn"] == null)
                Response.Redirect("LoginWithMasterPage.aspx");
            //#endif

            // Walk up the master page chain and tickle the getter on each one
            MasterPage master = this.Master;
            while (master != null) master = master.Master;
            if (!IsPostBack)
            {
                LoadTodosFromDB();
            }
            else
            {
                if (Session["todoDictionary"] != null)
                    LoadTodosFromSessionCache();
                else
                    LoadTodosFromDB();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{

            //}
            //Lb_Message.Text = "";
            //if (Session["user"] != null)
            //    Lbl_UserName.Text = $"Logged In As: {(Session["user"] as User).UserName}";
        }

        protected void Btn_AddItem_Click(object sender, EventArgs e)
        {
            // Validate that category was selected
            bool catWasSelected = false;
            foreach (RadioButton b in this.Form.Controls.OfType<ContentPlaceHolder>()
                                        .Where(x => x.ID.Equals("ContentPlaceHolder1"))
                                        .First().Controls.OfType<RadioButton>().ToList()
                                        .Where(c => c.ValidationGroup == "CategoryValidationGroup"))
            {
                if (b.Checked)
                    catWasSelected = true;
            }
            this.Form.Controls.OfType<ContentPlaceHolder>().Where(x => x.ID.Equals("ContentPlaceHolder1")).First().Controls.OfType<RadioButton>().ToList();
            if (catWasSelected != true)
            {
                Lb_Message.Text = "You must select a category";
                return;
            }

            // Grab the category
            char cat = this.Form.Controls.OfType<ContentPlaceHolder>()
                                        .Where(x => x.ID.Equals("ContentPlaceHolder1"))
                                        .First().Controls.OfType<RadioButton>().ToList()
                                        .Where(c => c.ValidationGroup == "CategoryValidationGroup")
                                        .ToList().Where(c => c.GroupName == "Category" && c.Checked == true)
                                        .First()
                                        .Text[0];

            // Grab the due date if set
            DateTime? dueDate = null;
            if (Txb_DueDate.Text.Length > 1)
                dueDate = DateTime.Parse(Txb_DueDate.Text);

            // Grab the text
            string description = Txb_AddItem.Text;

            // Grab the priority
            int priority = Int32.Parse(DD_Priority.SelectedValue.ToString());

            TodoItems.AddTodoItem(description, dueDate, cat, false, priority);
            LoadTodosFromDB();

            Txb_AddItem.Text = "";
            Txb_DueDate.Text = "";
        }


        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            // at this point sender ID will have the form DeleteButton_ID# and we need to parse out the ID# and then delete it
            if (!(sender is ImageButton))
                throw new Exception("Need To Click An Image Button");

            ImageButton ib = (ImageButton)sender;
            int startIndex = ib.ID.IndexOf('_') + 1;
            string idStr = ib.ID.Substring(startIndex);
            int id = int.Parse(idStr);
            TodoItems.DeleteTodoItem(id);
            LoadTodosFromDB();
        }

        protected void TodoCheckBox_Toggle(object sender, EventArgs e)
        {
            // at this point sender ID will have the form DeleteButton_ID# and we need to parse out the ID# and then delete it
            if (!(sender is CheckBox))
                throw new Exception("Need To Click A CheckBox");

            CheckBox ib = (CheckBox)sender;
            int startIndex = ib.ID.IndexOf('_') + 1;
            string idStr = ib.ID.Substring(startIndex);
            int id = int.Parse(idStr);
            TodoItems.ToggleCheckBox(id);
            LoadTodosFromDB();
        }


        protected void LoadTodosFromDB()
        {
            SetupHeaders();
            IEnumerable<TodoItem> todos = new List<TodoItem>();
            todos = TodoItems.GetPersonalTodoItems();
            Dictionary<int, TodoItem> todoDictionary = new Dictionary<int, TodoItem>();
            foreach (TodoItem item in todos)
                todoDictionary.Add(item.PKey, item);
            Session["todoDictionary"] = todoDictionary;
            AddTodoRows(todos);
        }

        protected void LoadTodosFromSessionCache()
        {
            SetupHeaders();
            IEnumerable<TodoItem> todos = new List<TodoItem>();
            todos = (Session["todoDictionary"] as Dictionary<int, TodoItem>).Values.ToList();
            AddTodoRows(todos);
        }




        protected void SetupHeaders()
        {
            if (TodoTable1.Controls != null)
                TodoTable1.Controls.Clear();
            TableRow headerRow = new TableRow();
            TableCell col1 = new TableCell() { Text = "Done", ID = "isDoneCol", CssClass = "tableHeader" };
            TableCell col2 = new TableCell() { Text = "Description", ID = "DescriptionCol", CssClass = "tableHeader" };
            TableCell col3 = new TableCell() { Text = "Priority", ID = "PriorityCol", CssClass = "tableHeader" };
            TableCell col4 = new TableCell() { Text = "Category", ID = "CategoryCol", CssClass = "tableHeader" };
            TableCell col5 = new TableCell() { Text = "Created Date", ID = "CreatedDateCol", CssClass = "tableHeader" };
            TableCell col6 = new TableCell() { Text = "Due Date", ID = "DueDateCol", CssClass = "tableHeader" };
            TableCell col7 = new TableCell() { Text = "Delete", ID = "DeleteCol", CssClass = "tableHeader" };
            headerRow.Cells.Add(col1);
            headerRow.Cells.Add(col2);
            headerRow.Cells.Add(col3);
            headerRow.Cells.Add(col4);
            headerRow.Cells.Add(col5);
            headerRow.Cells.Add(col6);
            headerRow.Cells.Add(col7);
            TodoTable1.Rows.Add(headerRow);
        }



        protected void AddTodoRows(IEnumerable<TodoItem> todos)
        {
            foreach (TodoItem i in todos)
            {
                CheckBox cb = new CheckBox() { ID = $"cb_{i.PKey}", AutoPostBack = true };
                if (i.IsComplete == true)
                    cb.Checked = true;
                else
                    cb.Checked = false;
                cb.CheckedChanged += TodoCheckBox_Toggle;
                TableCell cell1 = new TableCell() { ID = $"isDoneCol{ i.PKey }", };
                cell1.Controls.Add(cb);
                TableCell cell2 = new TableCell() { Text = i.Description, ID = $"DescriptionCol{i.PKey}", CssClass = "tableCell" };
                TableCell cell3 = new TableCell() { Text = i.Priority.ToString(), ID = $"PriorityCol{i.PKey}", CssClass = "tableCell" };
                TableCell cell4 = new TableCell() { Text = i.Category.ToString(), ID = $"CategoryCol{i.PKey}", CssClass = "tableCell" };
                TableCell cell5 = new TableCell() { Text = i.CreatedDate.Date.ToShortDateString(), ID = $"CreatedDateCol{i.PKey}", CssClass = "tableCell" };
                TableCell cell6 = new TableCell() { Text = i.DueDate?.Date.ToShortDateString(), ID = $"DueDateCol{i.PKey}", CssClass = "tableCell" };
                TableCell cell7 = new TableCell() { ID = $"deleteCol{i.PKey}", CssClass = "tableCell" };
                ImageButton ib = new ImageButton() { Width = 20, Height = 20, AlternateText = "DeleteButton", ImageUrl = "Images/Red-X.png", ID = $"DeleteButton_{i.PKey}", CausesValidation = false, ImageAlign = ImageAlign.Middle };
                ib.Click += DeleteButton_Click;
                cell7.Controls.Add(ib);
                TableRow tr = new TableRow();
                tr.Cells.Add(cell1);
                tr.Cells.Add(cell2);
                tr.Cells.Add(cell3);
                tr.Cells.Add(cell4);
                tr.Cells.Add(cell5);
                tr.Cells.Add(cell6);
                tr.Cells.Add(cell7);
                if (i.Category == 'P')
                    tr.BackColor = System.Drawing.Color.LightBlue;
                if (i.Category == 'W')
                    tr.BackColor = System.Drawing.Color.Plum;
                if (i.Category == 'S')
                    tr.BackColor = System.Drawing.Color.Moccasin;
                TodoTable1.Rows.Add(tr);
            }
        }




        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            foreach (TodoItem item in (Session["todoDictionary"] as Dictionary<int, TodoItem>).Values)
            {
                if (item.DueDate.HasValue && item.DueDate.Value.Date == e.Day.Date)
                {
                    e.Cell.Controls.Add(new Label { Text = $"{item.Description}", BorderStyle = BorderStyle.Groove });
                }
            }
        }

        protected void CalendarDueDatePicker_SelectionChanged(object sender, EventArgs e)
        {
            Txb_DueDate.Text = CalendarDueDatePicker.SelectedDate.ToShortDateString();
        }
    }
}