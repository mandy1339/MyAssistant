using MyAssistant.Controllers;
using MyAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyAssistant
{
    public partial class TodoGroupPage : System.Web.UI.Page
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

            // Load Groups (ListItems) into DropDownListGroup
            LoadGroupsIntoComboBox();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        // Load Groups (ListItems) into DropDownListGroup
        protected void LoadGroupsIntoComboBox()
        {
            // perform SQL Statement to get group names and group ids for this user
            IEnumerable<Group> groups = GroupController.GetGroupsForUser((Session["user"] as User).ID);
            // go through the result set and add all the items,
            foreach (Group group in groups)
                DropDownListGroup.Items.Add(new ListItem() { Text = group.GroupName, Value = group.ID.ToString() });
            // make the first one selected by default
            DropDownListGroup.Items[0].Selected = true;
        }


        // TODO: Refactor to add for group
        // Pass null for userid and pass groupID
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
            if (HiddenField_DueDate.Value.Length > 1)
                dueDate = DateTime.Parse(HiddenField_DueDate.Value);

            // Grab the text
            string description = Txb_AddItem.Text;

            // Grab the priority
            int priority = Int32.Parse(DD_Priority.SelectedValue.ToString());

            // Grab the group
            int groupID = Int32.Parse(DropDownListGroup.SelectedValue);

            TodoItemController.AddTodoItem(description, dueDate, cat, false, priority, null, groupID);
            LoadTodosFromDB();

            Txb_AddItem.Text = "";
            HiddenField_DueDate.Value = "";
        }




        /// <summary>
        /// When clicking button to take ownership of todoItem, 
        /// the item will get its UserID value filled out and will 
        /// move to the view of that user and away from the group view.
        /// The groupID will stay set on the todoItem which allows for later
        /// being able to send an item back to the group view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TakeThisItem_Click(object sender, EventArgs e)
        {
            // get the id of the item
            Button b = ((Button)sender);
            int startIndex = b.ID.IndexOf("_") + 1;
            string idStr = b.ID.Substring(startIndex);
            int id = int.Parse(idStr);

            // the proc will set the update the todo item and set the userid value
            TodoItemController.TakeOwnerShipOfTodoItem(id, (Session["user"] as User).ID);
            LoadTodosFromDB();
        }




        protected void LoadTodosFromSessionCache()
        {
            SetupHeaders();
            IEnumerable<TodoItem> todos = new List<TodoItem>();
            todos = (Session["todoDictionary"] as Dictionary<int, TodoItem>).Values.ToList();
            AddTodoRows(todos);
        }


        // TODO: Refactor
        // if unchecked: first take ownership, then mark as done
        // if checked: remove ownership and mark not done
        protected void TodoCheckBox_Toggle(object sender, EventArgs e)
        {
            // at this point sender ID will have the form DeleteButton_ID# and we need to parse out the ID# and then delete it
            if (!(sender is CheckBox))
                throw new Exception("Need To Click A CheckBox");
            
            CheckBox ib = (CheckBox)sender;
            int startIndex = ib.ID.IndexOf('_') + 1;
            string idStr = ib.ID.Substring(startIndex);
            int id = int.Parse(idStr);
            int userID = (Session["user"] as User).ID;
            TodoItem curItem = TodoItemController.GetTodoItemByID(id);
            if (!curItem.IsComplete)
                TodoItemController.TakeOwnerShipOfTodoItem(id, userID);                
            else
                TodoItemController.RemoveOwnerShipOfTodoItem(id);
            TodoItemController.ToggleCheckBox(id);
            LoadTodosFromDB();
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
            TodoItemController.DeleteTodoItem(id);
            LoadTodosFromDB();
        }

        
        protected void LoadTodosFromDB()
        {
            SetupHeaders();
            IEnumerable<TodoItem> todos = new List<TodoItem>();
            todos = TodoItemController.GetGroupItems((Session["user"] as User).ID);
            Dictionary<int, TodoItem> todoDictionary = new Dictionary<int, TodoItem>();
            foreach (TodoItem item in todos)
                todoDictionary.Add(item.PKey, item);
            Session["todoDictionary"] = todoDictionary;
            AddTodoRows(todos);
        }


        // TODO NEED TO REFACTOR
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
            TableCell col7 = new TableCell() { Text = "Take This Item", ID = "TakeThisItemCol", CssClass = "tableHeader" };
            TableCell col8 = new TableCell() { Text = "Delete", ID = "DeleteCol", CssClass = "tableHeader" };
            headerRow.Cells.Add(col1);
            headerRow.Cells.Add(col2);
            headerRow.Cells.Add(col3);
            headerRow.Cells.Add(col4);
            headerRow.Cells.Add(col5);
            headerRow.Cells.Add(col6);
            headerRow.Cells.Add(col7);
            headerRow.Cells.Add(col8);
            TodoTable1.Rows.Add(headerRow);
        }


        // TODO NEED TO REFACTOR
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
                TableCell cell2 = new TableCell() { Text = i.Description, ID = $"DescriptionCol_{i.PKey}", CssClass = "tableCell" };
                TableCell cell3 = new TableCell() { Text = i.Priority.ToString(), ID = $"PriorityCol_{i.PKey}", CssClass = "tableCell" };
                TableCell cell4 = new TableCell() { Text = i.Category.ToString(), ID = $"CategoryCol_{i.PKey}", CssClass = "tableCell" };
                TableCell cell5 = new TableCell() { Text = i.CreatedDate.Date.ToShortDateString(), ID = $"CreatedDateCol_{i.PKey}", CssClass = "tableCell" };
                TableCell cell6 = new TableCell() { Text = i.DueDate?.Date.ToShortDateString(), ID = $"DueDateCol_{i.PKey}", CssClass = "tableCell" };
                TableCell cell7 = new TableCell() { ID = $"TakeItemCol_{i.PKey}", CssClass = "tableCell" };
                Button takeItembutton = new Button() { ID = $"TakeItemBut_{i.PKey}", CausesValidation = false, Text = "Click To Take This Item", CssClass = "w3-w3-button w3-WhiteBorderButton w3-w3-hide-small" };
                takeItembutton.Click += TakeThisItem_Click;
                cell7.Controls.Add(takeItembutton);
                TableCell cell8 = new TableCell() { ID = $"deleteCol_{i.PKey}", CssClass = "tableCell" };
                ImageButton ib = new ImageButton() { Width = 20, Height = 20, AlternateText = "DeleteButton", ImageUrl = "Images/Red-X.png", ID = $"DeleteButton_{i.PKey}", CausesValidation = false, ImageAlign = ImageAlign.Middle };
                ib.Click += DeleteButton_Click;
                cell8.Controls.Add(ib);
                TableRow tr = new TableRow();
                tr.Cells.Add(cell1);
                tr.Cells.Add(cell2);
                tr.Cells.Add(cell3);
                tr.Cells.Add(cell4);
                tr.Cells.Add(cell5);
                tr.Cells.Add(cell6);
                tr.Cells.Add(cell7);
                tr.Cells.Add(cell8);
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
    }
}