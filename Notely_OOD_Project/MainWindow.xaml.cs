using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Data.Entity;
using Newtonsoft.Json;



namespace Notely_OOD_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
       
        int styleControl = 0; // controls card or list view
        int sortControl = 0; // controls sort order


        internal List<Note> notes = new List<Note>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            GetDataFromDB();
            DisableEdit();

            string[] priorities = { "Relaxed", "Important", "Urgent", "Critical" };
            comboPriority.ItemsSource = priorities;

            string[] displaypriorities = { "All","Relaxed", "Important", "Urgent", "Critical" };
            comboDisplay.ItemsSource = displaypriorities;
            comboDisplay.SelectedItem = "All";

            listBxNoteBoard.ItemsSource = notes;
        }

        private void GetDataFromDB()
        {
            // pulls notes from DB

            NoteData db = new NoteData();
            var query = from n in db.notes
                        select n;

            notes = query.ToList();

        }
        //sets source for image
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



        #region RandomListCreation

        // two methods to create random test days when problem starts// 
        // adds the random objects into list declared at top//
        private void CreateRandomNotes()
        {
            // Method not in use as Project now uses Database to store Data // 

            string[] t = { "Make Bed", "Study for Maths Test", "Tidy the House", "Go for a walk", "Complete Web Dev Project", "Write In Journal", "Prepare Dinner", "Do the Weekly Shop" };
            string[] c = { "Get Up five minutes early to make bed", "Continue studying Q1-3 for exam", "Spend 30 minutes cleaning the house", "Take 1 hour out, to go for walk", "Upload build of project to Git - Finish report", "Write weekly update in Journal", "Prepare Dinner for the next 3 days", "complete shopping for the week" };

            List<string> titles = new List<string>(t);
            List<string> content = new List<string>(c);

            Random ran = new Random();

            for (int i = 0; i < 8; i++)
            {
               
                int index = ran.Next(0, titles.Count);
                int numDays = ran.Next(0, 20);
                int priorIndex = ran.Next(0, 3);
                Note.Priority holder = PriorityOffDays(numDays);

                notes.Add(new Note(titles[index], holder, DateTime.Now.AddDays(numDays), content[index], GetImageLocation(holder)));

                titles.Remove(titles[index]);
                content.Remove(content[index]);

            }
        }

        // conditional statement assigns Priority based on how many there is to complete note// 
        private Note.Priority PriorityOffDays(int index)
        {
            // Again not is use after connecting project to DB
            Note.Priority p = Note.Priority.Relaxed;

            if (index <= 5)
            {
                p = Note.Priority.Critical;

            }

            else if (index > 5 && index <= 10)
            {
                p = Note.Priority.Urgent;
            }

            else if (index > 10 && index <= 15)
            {
                p = Note.Priority.Important;
            }

            else
            {
                p = Note.Priority.Relaxed;
            }

            return p;
        }



        #endregion

        #region Methods to interact with UI
        private void listBxNoteBoard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Note selectedNote = listBxNoteBoard.SelectedItem as Note;

            if (selectedNote != null)
            {
                DisplayDetails(selectedNote);

            }
            else
            {
                DisableEdit();
                clearDetails();
            }
        }

        // displays selected notes details at side bar 
        private void DisplayDetails(Note selectedNote)
        {
            EnableEdit();
            txtBTitle.Text = selectedNote.Title;
            comboPriority.SelectedItem = selectedNote.Prior.ToString();
            datePicker.SelectedDate = selectedNote.CompleationDate;
            txtBContent.Text = selectedNote.Content;
        }

       
        // gets priority
        private Note.Priority GetPriority()
        {
            // method to return Enum Priority to edit notes //
            Note.Priority selected = Note.Priority.Critical;

            switch (comboPriority.SelectedItem)
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

        

        private void comboDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Note> filteredList = new List<Note>();

          // filters list based on combo box

            string selected = comboDisplay.SelectedItem as string;

            if (selected != "All")
            {
                btnSort.Visibility = Visibility.Collapsed;
            }

            if (selected == "All")
            {
                foreach (Note note in notes)
                {
                    filteredList.Add(note);
                }
                listBxNoteBoard.ItemsSource = filteredList;
                btnSort.Visibility = Visibility.Visible;

            }

            else if (selected == "Relaxed")
            {
                foreach (Note note in notes)
                {
                    if (note.Prior == Note.Priority.Relaxed)
                    {
                        filteredList.Add(note);
                        listBxNoteBoard.ItemsSource = null;
                        listBxNoteBoard.ItemsSource = filteredList;

                    }

                }
            }

            else if (selected == "Important")
            {
                foreach (Note note in notes)
                {
                    if (note.Prior == Note.Priority.Important)
                    {
                        filteredList.Add(note);
                        listBxNoteBoard.ItemsSource = null;
                        listBxNoteBoard.ItemsSource = filteredList;
                    }

                }
            }
            else if (selected == "Urgent")
            {
                foreach (Note note in notes)
                {
                    if (note.Prior == Note.Priority.Urgent)
                    {
                        filteredList.Add(note);
                        listBxNoteBoard.ItemsSource = null;
                        listBxNoteBoard.ItemsSource = filteredList;

                    }

                }
            }
            else if (selected == "Critical")
            {
                foreach (Note note in notes)
                {
                    if (note.Prior == Note.Priority.Critical)
                    {
                        filteredList.Add(note);
                        listBxNoteBoard.ItemsSource = null;
                        listBxNoteBoard.ItemsSource = filteredList;

                    }

                }
            }
             if (filteredList.Count == 0)
            {
               
                listBxNoteBoard.ItemsSource = null;
                
            }

            // calls create card passing filtered list, cards are then rendered from that list// 
            // add note works similar , calling if stylecontrol = 0, rendering out the new note that was added to the list// 
         
         
        }
        #endregion

        #region Methods to interact with notes
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Note selectedNote = listBxNoteBoard.SelectedItem as Note;

            if (selectedNote != null)
            {
                selectedNote.Title = txtBTitle.Text;
                selectedNote.CompleationDate = datePicker.SelectedDate.GetValueOrDefault();
                selectedNote.Prior = GetPriority();
                selectedNote.ImageLocation = GetImageLocation(selectedNote.Prior);
                selectedNote.Content = txtBContent.Text;

                //queries database
                NoteData db = new NoteData();
                var query = from n in db.notes
                            where n.id.Equals(selectedNote.id)
                            select n;
               

                // converts query to single element for deletion// 
                var result = query.FirstOrDefault();

                // deletes old version of the note and add new one// 
                db.notes.Remove(result);
                db.notes.Add(selectedNote);
                db.SaveChanges();

                comboDisplay.SelectedItem = "All";
                listBxNoteBoard.ItemsSource = null;
                listBxNoteBoard.ItemsSource = notes;

            }

        }
        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            // brings up add screen 
            AddNote add = new AddNote();
            add.Owner = this;
            add.ShowDialog();

            listBxNoteBoard.ItemsSource = null;
            listBxNoteBoard.ItemsSource = notes;

            if (styleControl == 1)
            {
                // if on card view, changes back to list view// 
                RenderCards();
            }

        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
          // uses linq to control sort
            if (sortControl == 0)
            {
                var query = notes
                    .Select(n => n)
                    .OrderBy(n => n.Title);

                listBxNoteBoard.ItemsSource = null;
                listBxNoteBoard.ItemsSource = query;
                notes = query.ToList();
                txtBkSort.Text = "Sorted by: Aplbetical";
             
                sortControl = 1;
            }
            else if (sortControl == 1)
            {
                var query = notes
                  .Select(n => n)
                  .OrderBy(n => n.CompleationDate);

                listBxNoteBoard.ItemsSource = null;
                listBxNoteBoard.ItemsSource = query;
                notes = query.ToList();
                txtBkSort.Text = "Sorted by: Date";
                
                sortControl = 2;
            }

            else if (sortControl == 2)
            {
                var query = notes
                  .Select(n => n)
                  .OrderByDescending(n => n.Prior) ;


                listBxNoteBoard.ItemsSource = null;
                listBxNoteBoard.ItemsSource = query;
                notes = query.ToList();
                txtBkSort.Text = "Sorted by: Priority";
            
                sortControl = 0;

            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Note selected = listBxNoteBoard.SelectedItem as Note;

            if (selected != null)
            {
                NoteData db = new NoteData();
                var query = from n in db.notes
                            where n.id.Equals(selected.id)
                            select n;

                // converts query to single element for deletion// 
               var result = query.FirstOrDefault();

                // remove from DB
                db.notes.Remove(result);
                db.SaveChanges();

             // removes from notes list
                notes.Remove(selected);
                listBxNoteBoard.ItemsSource = null;
                listBxNoteBoard.ItemsSource = notes;
            }
        }

        private void btnPrintReport_Click(object sender, RoutedEventArgs e)
        {
            string path = @"c:\College_Work\Year2\sem2\OOD\Project\Notely_OOD_Project\NoteReport.txt";

            // Create a file to write to.
            // objects are converted to JSON before being written to file//
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("--------------------");
                sw.WriteLine($"   Note Report - {DateTime.Now.ToShortDateString()}");
                sw.WriteLine("--------------------");
                sw.WriteLine("      ------");
                
                foreach (Note note in notes)
                {
                    sw.WriteLine(JsonConvert.SerializeObject(note));
                }
                sw.WriteLine("--------------------");

            }

            MessageBox.Show("Report Successful, File created in main folder");


        }

        private void btnChangeView_Click(object sender, RoutedEventArgs e)
        {

            RenderCards();

        }

      
        #endregion

        #region Method to render Cards
        private void RenderCards()
        {
            // 0 is listbox
            // 1 is cards

         
            if (styleControl == 0)
            {
               

                btnChangeView.Content = "List View";
                // hides controls that are needed in ListView 
                HideElements();

                
                WrapPanel noteBoard = new WrapPanel
                {
                    Name = "noteBoard",
                    Background = new SolidColorBrush(Color.FromRgb(245, 245, 245)),
                    Orientation = Orientation.Horizontal,
                


            };

                 // assigns note board as child of scrollViewer
                ScrollViewer myScrollViewer = new ScrollViewer();
                myScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
               
                myScrollViewer.Content = noteBoard;

                mainGrid.Children.Add(myScrollViewer);
                Grid.SetColumn(myScrollViewer, 0);
                Grid.SetRow(myScrollViewer, 3);
                Grid.SetColumnSpan(myScrollViewer, 4);
                

                Random ran = new Random();
           
                // loop to create and assign elements 
                foreach (Note note in notes)
                {

                    Border noteBorder = new Border
                    {
                        BorderBrush = new SolidColorBrush(Color.FromRgb(26, 26, 64)),
                        BorderThickness = new Thickness(1),
                    };

                    TextBlock title = new TextBlock
                    {
                        Text = note.Title.ToString(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Padding = new Thickness(10),
                        Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0))
                    };

                    TextBlock date = new TextBlock
                    {
                        Text = note.CompleationDate.ToShortDateString(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Padding = new Thickness(10),
                         Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0))
                    };

                    TextBlock content = new TextBlock
                    {
                        
                        Text = note.Content.ToString(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Padding = new Thickness(10),
                         Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0))
                    };

                    Grid noteGrid = new Grid
                    {
                        Margin = new Thickness(10),
                    };

                    // selects one of 4 random numbers 
                    switch (ran.Next(0, 4))
                {
                    case 0:
                        noteGrid.Background = new SolidColorBrush(Color.FromRgb(241, 197, 197));
                       break;
                    case 1:
                            noteGrid.Background = new SolidColorBrush(Color.FromRgb(250, 240, 175));
                        break;
                    case 2:
                            noteGrid.Background = new SolidColorBrush(Color.FromRgb(229, 237, 183));
                       break;
                    case 3:
                            noteGrid.Background = new SolidColorBrush(Color.FromRgb(139, 205, 205));
                       break;

                        default:
                        break;
                }

               

                StackPanel allign = new StackPanel
                    {
                        // stack panel used to allign elements// 
                    };

                    // assigns children
                    noteGrid.Children.Add(noteBorder);
                    noteGrid.Children.Add(allign);
                    allign.Children.Add(title);
                    allign.Children.Add(date);
                    allign.Children.Add(content);

                    noteBoard.Children.Add(noteGrid);
                    styleControl = 1;

                }

            }

            else if (styleControl == 1)
            {
                btnChangeView.Content = "Card View";
                RevealElements();

               // cycles through main window children, deletes scrollviewer(card note board)

                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(mainGrid); i++)
                {
                    Visual childVisual = (Visual)VisualTreeHelper.GetChild(mainGrid, i);
                    if (childVisual is ScrollViewer)
                    {
                        mainGrid.Children.Remove((UIElement)childVisual);
                    }
                }

               
                
                styleControl = 0;

            }
        }
        #endregion

        #region BackGround Methods
        // backround methods // 
        private void DisableEdit()
        {
            txtBTitle.IsEnabled = false;
            comboPriority.IsEnabled = false;
            datePicker.IsEnabled = false;
            
            txtBContent.IsEnabled = false;

        }
        private void EnableEdit()
        {
            txtBTitle.IsEnabled = true;
            comboPriority.IsEnabled = true;
            datePicker.IsEnabled = true;
            
            txtBContent.IsEnabled = true;


        }

        private void clearDetails()
        {
            txtBTitle.Text = null;
            comboPriority.SelectedItem = null;
            datePicker.SelectedDate = null;
           
            txtBContent.Text = null;

        }

        private void HideElements()
        {
            borderDetails.Visibility = Visibility.Collapsed;
            //mainGrid.Children.Remove(borderDetails);
            txtBxDisplay.Visibility = Visibility.Hidden;
            comboDisplay.Visibility = Visibility.Hidden;
            //btnSort.Visibility = Visibility.Hidden;
           // borderDetails.Visibility = Visibility.Collapsed;
        }

        private void RevealElements()
        {

            borderDetails.Visibility = Visibility.Visible;
            txtBxDisplay.Visibility = Visibility.Visible;
            comboDisplay.Visibility = Visibility.Visible;
            btnSort.Visibility = Visibility.Visible;
        }
        

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            if (aw > 1000)
            {
                StackPanelDetails.HorizontalAlignment = HorizontalAlignment.Center;
                stackBtn.HorizontalAlignment = HorizontalAlignment.Center;
                tbTitle.HorizontalAlignment = HorizontalAlignment.Center;

            }
            if (aw < 1000)
            {
                StackPanelDetails.HorizontalAlignment = HorizontalAlignment.Right;
                stackBtn.HorizontalAlignment = HorizontalAlignment.Right;
                tbTitle.HorizontalAlignment = HorizontalAlignment.Right;

            }
        }


       

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {

            Options options = new Options();
            options.Owner = this;
            options.ShowDialog();





        }
        #endregion



    }
}
    

