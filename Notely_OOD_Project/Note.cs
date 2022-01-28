using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notely_OOD_Project
{
    class Note
    {
        public string Title { get; set; }
        public DateTime CompleationDate { get; set; }
        //public enum Priority { get; set; }
        public string Content { get; set; }

     public enum Priority
    {
        Relaxed,
        Important, 
        Urgent,
        Top

    }
        public Note(string title, DateTime compleationDate, string content )
        {
            this.Title = title;
            this.CompleationDate = compleationDate;
            this.Content = content;

        }

        public Note()
        {

        }

        public override string ToString()
        {
            return string.Format($"Title: {Title} - {CompleationDate} | Content: {Content}");
        }
    }
}
