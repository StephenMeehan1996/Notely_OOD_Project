using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notely_OOD_Project;
using System.Data.Entity;

namespace DataManager
{
    class Program
    {
        static void Main(string[] args)
        {
            NoteData db = new NoteData();

            
            using (db)
            {
                // creates first test note for database 

                Note n1 = new Note("Complete .Net project", Note.Priority.Important, DateTime.Now.AddMonths(2) ,"Complete Project", "Test");

                db.notes.Add(n1);

                db.SaveChanges();

            }

        }
    }
}
