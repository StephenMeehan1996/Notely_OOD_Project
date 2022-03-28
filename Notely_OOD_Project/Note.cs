using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notely_OOD_Project
{
  
    public class Note : IComparable
    {
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
            // use of short date string can be changed//
            // in the details tab the time can be displayed if its added by the user// 
          
            return string.Format($"Title: {Title} - {CompleationDate.ToShortDateString()} | Content: {Content}");
        }

     

        // dont need // 
        public int CompareTo(object obj)
        {
            Note otherNote = obj as Note;
            return this.Prior.CompareTo(otherNote.Prior);
        }

        public void testc()
        {

        }
    }
}
