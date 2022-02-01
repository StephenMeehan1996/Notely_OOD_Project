﻿using System;
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
       public List<Note> notes = new List<Note>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Note note1 = new Note("Study for Maths Test", Note.Priority.Important, new DateTime(2022, 01, 31), "Study for Exam");
            Note note2 = new Note("Go for Run", Note.Priority.Relaxed, new DateTime(2022, 02, 02), "Go For Run in the woods");
            Note note3 = new Note("Complete Web Dev Project", Note.Priority.Critical, new DateTime(2022, 02, 17), "Complete and upload build to Git");
            Note note4 = new Note("Complete OOD LabSheet", Note.Priority.Urgent, new DateTime(2022, 02, 05), "Complete and upload this weeks labsheet");

            notes.Add(note1);
            notes.Add(note2);
            notes.Add(note3);
            notes.Add(note4);

            DisableEdit();



            string[] priorities = { "Relaxed", "Important", "Urgent", "Critical" };
            comboPriority.ItemsSource = priorities;

            string[] displaypriorities = { "All","Relaxed", "Important", "Urgent", "Critical" };
            comboDisplay.ItemsSource = displaypriorities;
            comboDisplay.SelectedItem = "All";

            listBxNoteBoard.ItemsSource = notes;

        }

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

        private Note.Priority GetPriority()
        {
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

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            AddNote add = new AddNote();
             add.Owner = this;
            add.ShowDialog();


            listBxNoteBoard.ItemsSource = null;
            listBxNoteBoard.ItemsSource = notes;


            

            //AddNote.



        }

        private void comboDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Note> filteredList = new List<Note>();

            string selected = comboDisplay.SelectedItem as string;

            if (selected != "All")
            {
                btnSort.Visibility = Visibility.Collapsed;
            }

            if (selected == "All")
            {
                listBxNoteBoard.ItemsSource = notes;
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
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            notes.Sort();
            notes.Reverse();
            listBxNoteBoard.ItemsSource = null;
            listBxNoteBoard.ItemsSource = notes;
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
    }
}
