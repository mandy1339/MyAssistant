using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyAssistant.Models
{
    public class TodoItem
    {
        public int PKey { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public char Category { get; set; }

        public bool IsComplete { get; set; }

        public Byte Priority { get; set; }

        public int? UserID { get; set; }
        public int? GroupID { get; set; }
        
    }
}