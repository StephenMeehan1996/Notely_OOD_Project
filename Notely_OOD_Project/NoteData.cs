using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Notely_OOD_Project
{
    public class NoteData : DbContext
    {
        public NoteData() : base("myNoteData") { }
        
         public DbSet<Note> notes { get; set; }
        
    }
}
