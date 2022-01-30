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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notely_OOD_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Note> notes = new List<Note>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Note note1 = new Note("Study for Maths Test", Note.Priority.Important, new DateTime(2022, 01, 31), "Study for Exam");
            Note note2 = new Note("Go for Run", Note.Priority.Relaxed, new DateTime(2022, 02, 02), "Go For Run in the woods");
            Note note3 = new Note("Complete Web Dev Project", Note.Priority.Critical, new DateTime(2022, 02, 17), "Complete and upload build to Git");
            Note note4 = new Note("Complete OOD LabSheet", Note.Priority.Important, new DateTime(2022, 02, 05), "Complete and upload this weeks labsheet");

            notes.Add(note1);
            notes.Add(note2);
            notes.Add(note3);
            notes.Add(note4);


            listBxNoteBoard.ItemsSource = notes;

        }
    }
}
