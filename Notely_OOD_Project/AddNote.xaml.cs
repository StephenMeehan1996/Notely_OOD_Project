using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Notely_OOD_Project
{
    /// <summary>
    /// Interaction logic for AddNote.xaml
    /// </summary>
    public partial class AddNote : Window
    {
        
        public AddNote()
        {
            InitializeComponent();

            // Links addNote back to main window as parent. // 
            MainWindow main = this.Parent as MainWindow;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] priorities = { "Relaxed", "Important", "Urgent", "Critical" };
            comboPriorityAdd.ItemsSource = priorities;
            

        }

        private void btnCreateNote_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = this.Owner as MainWindow;
    

            Note addNote = new Note(txtBTitleAdd.Text, GetPriority(), datePickerAdd.SelectedDate.GetValueOrDefault(), txtBContentAdd.Text, GetImageLocation(GetPriority()));

            // add to list// 
            main.notes.Add(addNote);

            NoteData db = new NoteData();

            using (db)
            {
                db.notes.Add(addNote);
                db.SaveChanges();

            }
            //main.listBxNoteBoard.ItemsSource = null;
            //main.listBxNoteBoard.ItemsSource = main.notes;

            this.Close();
        }

        // move methods below to class // 

        private Note.Priority GetPriority()
        {
            Note.Priority selected = Note.Priority.Critical;

            switch (comboPriorityAdd.SelectedItem)
            {
                case "Relaxed":
                    selected = Note.Priority.Relaxed;
                    break;
                case "Important":
                    selected = Note.Priority.Important;
                    break;
                case "Urgent":
                    selected = Note.Priority.Urgent;
                    break;
                case "Critical":
                    selected = Note.Priority.Critical;
                    break;
                default:
                    break;
            }

            return selected;
        }

        public string GetImageLocation(Note.Priority hold)
        {
            string image = "hold";

            if (hold == Note.Priority.Relaxed)
            {
                image = "images/relaxedW.png";

            }
            else if (hold == Note.Priority.Important)
            {
                image = "images/importantW.png";
            }
            else if (hold == Note.Priority.Urgent)
            {
                image = "images/urgentW.png";
            }
            else if (hold == Note.Priority.Critical)
            {
                image = "images/criticalW.png";
            }

            return image;

        }
    }
}
