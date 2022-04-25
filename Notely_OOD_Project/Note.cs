using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notely_OOD_Project
{
  
    public class Note 
    {
        public int id { get; set; }
        public string Title { get; set; }
        public Priority Prior { get; set; }
        public DateTime CompleationDate { get; set; }
        public string Content { get; set; }

        public string ImageLocation { get; set; }

     public enum Priority
    {
        Relaxed,
        Important, 
        Urgent,
        Critical

    }
       
    public Note(string title, Priority prior, DateTime compleationDate, string content, string imageLocation)
        {
            this.Title = title;
            this.Prior = prior;
            this.CompleationDate = compleationDate;
            this.Content = content;
            this.ImageLocation = imageLocation;

        }

        public Note()
        {

        }
     
        public override string ToString()
        {
            return string.Format($"Title: {Title} - {CompleationDate.ToShortDateString()} | Content: {Content}");
        }

    }
}
