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


namespace Notely_OOD_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    // if all is selected sort button appears, else is inviable// 
    // I Comparable sort by priority// 
    // write out to file// 
    
    public partial class MainWindow : Window
    {
       
        int styleControl = 0;
        int sortControl = 0;


        private readonly PaletteHelper _paletteHelper = new PaletteHelper();//kmm 15/3/22

        internal List<Note> notes = new List<Note>();
        public MainWindow()
        {
            InitializeComponent();
            //Button but = new Button();
            //but.Content = "test";
            //mainGrid.Children.Add(but);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Note note1 = new Note("Study for Maths Test", Note.Priority.Important, new DateTime(2022, 01, 31), "Study for Exam");
            //Note note2 = new Note("Go for Run", Note.Priority.Relaxed, new DateTime(2022, 02, 02), "Go For Run in the woods");
            //Note note3 = new Note("Complete Web Dev Project", Note.Priority.Critical, new DateTime(2022, 02, 17), "Complete and upload build to Git");
            //Note note4 = new Note("Complete OOD LabSheet", Note.Priority.Urgent, new DateTime(2022, 02, 05), "Complete and upload this weeks labsheet");
            

            CreateRandomNotes();
            // returns list of random notes // 
            //notes.Add(note1);
            //notes.Add(note2);
            //notes.Add(note3);
            //notes.Add(note4);

            DisableEdit();



            string[] priorities = { "Relaxed", "Important", "Urgent", "Critical" };
            comboPriority.ItemsSource = priorities;

            string[] displaypriorities = { "All","Relaxed", "Important", "Urgent", "Critical" };
            comboDisplay.ItemsSource = displaypriorities;
            comboDisplay.SelectedItem = "All";

            listBxNoteBoard.ItemsSource = notes;

            

        }

        #region RandomListCreation

        // two methods to create random test days when problem starts// 
        // adds the random objects into list declared at top//
        private void CreateRandomNotes()
        {
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

                notes.Add(new Note(titles[index], PriorityOffDays(numDays), DateTime.Now.AddDays(numDays), content[index]));

                titles.Remove(titles[index]);
                content.Remove(content[index]);

            }
        }

        // conditional statement assigns Priority based on how many there is to complete note// 
        private Note.Priority PriorityOffDays( int index)
        {
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

        private void DisplayDetails(Note selectedNote)
        {
            EnableEdit();
            txtBTitle.Text = selectedNote.Title;
            comboPriority.SelectedItem = selectedNote.Prior.ToString();
            datePicker.SelectedDate = selectedNote.CompleationDate;

            txtBTime.Text = selectedNote.CompleationDate.TimeOfDay.ToString();
            txtBContent.Text = selectedNote.Content;

            
        }

       

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

            // bug if there is no object of that priority type list does not appear empty // 

            string selected = comboDisplay.SelectedItem as string;

            if (selected != "All")
            {
                btnSort.Visibility = Visibility.Collapsed;
            }

            if (selected == "All")
            {
                // notes added to filtered list here , because filtered list.Count 
                // is used to contol the item source if there are no notes of that 
                // Priority created 

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
                //MessageBox.Show(filteredList.Count.ToString());
                listBxNoteBoard.ItemsSource = null;
                
            }

         
            // calls create card passing filtered list, cards are then rendered from that list// 
            // add note works similar , calling if stylecontrol = 0, rendering out the new note that was added to the list// 
            // adding event listeners for clicks on the notes?? 
         
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
                selectedNote.Content = txtBContent.Text;

                comboDisplay.SelectedItem = "All";
                listBxNoteBoard.ItemsSource = null;
                listBxNoteBoard.ItemsSource = notes;

            }


        }
        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            AddNote add = new AddNote();
            add.Owner = this;
            add.ShowDialog();


            listBxNoteBoard.ItemsSource = null;
            listBxNoteBoard.ItemsSource = notes;

            if (styleControl == 0)
            {
                // if on card view, changes back to list view// 
                RenderCards();
            }

        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            //notes.Sort();
            //notes.Reverse();
            //listBxNoteBoard.ItemsSource = null;
            //listBxNoteBoard.ItemsSource = notes;
           // List<Note> test = new List<Note>();
            //// refactor method //////
            //dynamic query = null;

            // Needs to be refactored here, alot of repeated code// 
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

            //if (styleControl == 1)
            //{
               
            //    RenderCards();
            //}





        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Note selected = listBxNoteBoard.SelectedItem as Note;

            if (selected != null)
            {
                notes.Remove(selected);
                listBxNoteBoard.ItemsSource = null;
                listBxNoteBoard.ItemsSource = notes;
            }
        }

        private void btnPrintReport_Click(object sender, RoutedEventArgs e)
        {
            string path = @"c:\College_Work\Year2\sem2\OOD\Project\Notely_OOD_Project\NoteReport.txt";


            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("--------------------");
                sw.WriteLine($"   Note Report - {DateTime.Now.ToShortDateString()}");
                sw.WriteLine("--------------------");
                sw.WriteLine("      ------");
                
                foreach (Note note in notes)
                {
                    sw.WriteLine(note);
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
                // mainGrid.Children.Remove(listBxNoteBoard);

                btnChangeView.Content = "List View";
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
                Grid.SetRow(myScrollViewer, 2);
                Grid.SetColumnSpan(myScrollViewer, 4);
                //Grid.SetRowSpan(myScrollViewer, 4);

                Random ran = new Random();
           
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
                     
                        //Background = new SolidColorBrush(Color.FromRgb(245, 245, 0))
                        // Background = new SolidColorBrush(Color.FromRgb((byte)ran.Next(220, 255), (byte)ran.Next(100, 255), (byte)ran.Next(1, 10)))
                    };


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

                // btnAddNote.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnAddNote_Click));

                StackPanel allign = new StackPanel
                    {
                        // stack panel used to allign elements// 
                    };


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

                //ListBox list = new ListBox
                //{
                //    Name = "listBxNoteBoard"

                //};

                //listBxNoteBoard.ItemsSource = notes;

                //mainGrid.Children.Add(listBxNoteBoard);
                
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
            txtBTime.IsEnabled = false;
            txtBContent.IsEnabled = false;

        }
        private void EnableEdit()
        {
            txtBTitle.IsEnabled = true;
            comboPriority.IsEnabled = true;
            datePicker.IsEnabled = true;
            txtBTime.IsEnabled = true;
            txtBContent.IsEnabled = true;


        }

        private void clearDetails()
        {
            txtBTitle.Text = null;
            comboPriority.SelectedItem = null;
            datePicker.SelectedDate = null;
            txtBTime.Text = null;
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

            }
        }


        #endregion

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {

            Options options = new Options();
            options.Owner = this;
            options.ShowDialog();





        }


        private void ToggleBaseColour(bool isDark)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

            CustomColorTheme md = Application.Current.Resources.MergedDictionaries[0] as CustomColorTheme;

            if (md.BaseTheme == BaseTheme.Light)
            {
                md.BaseTheme = BaseTheme.Dark;
            }
            else
            {
                md.BaseTheme = BaseTheme.Light;
            }

            Application.Current.Resources.MergedDictionaries[0] = md;

        }
    }
}
    

