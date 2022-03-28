using MaterialDesignThemes.Wpf;
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

using MaterialDesignThemes;
using MaterialDesignColors;

using System.Windows.Media;


namespace Notely_OOD_Project
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        
        public Options()
        {
            InitializeComponent();
            MainWindow main = this.Parent as MainWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            var fontList = Fonts.SystemFontFamilies;
            comboBxFont.ItemsSource = fontList.ToList();

        }
        //private void btnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    if (rbLight.IsChecked == true)
        //    {

        //        //MaterialDesignColors.PrimaryColor = new PrimaryColor(Color.FromRgb(0, 0, 0));




        //    }
        //}

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();//kmm 15/3/22

        private void ToggleBaseColour(bool isDark)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }


        private void cb_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = this.Owner as MainWindow;
            CustomColorTheme md = Application.Current.Resources.MergedDictionaries[0] as CustomColorTheme;
            
            if (md.BaseTheme == BaseTheme.Light)
            {
                md.BaseTheme = BaseTheme.Dark;
                foreach (Note note in main.notes)
                {
                    if (note.Prior == Note.Priority.Critical)
                    {
                        note.ImageLocation = "images/criticalW.png";

                    }
                    else if (note.Prior == Note.Priority.Urgent)
                    {
                        note.ImageLocation = "images/urgentW.png";
                    }

                    else if (note.Prior == Note.Priority.Important)
                    {
                        note.ImageLocation = "images/importantW.png";
                    }

                    else if (note.Prior == Note.Priority.Relaxed)
                    {
                        note.ImageLocation = "images/relaxedW.png";
                    }

                    main.listBxNoteBoard.ItemsSource = null;
                    main.listBxNoteBoard.ItemsSource = main.notes;
                

                }
                
            }
            else
            {
                md.BaseTheme = BaseTheme.Light;
                foreach (Note note in main.notes)
                {
                    if (note.Prior == Note.Priority.Critical)
                    {
                        note.ImageLocation = "images/critical.png";

                    }
                    else if (note.Prior == Note.Priority.Urgent)
                    {
                        note.ImageLocation = "images/urgent.png";
                    }

                    else if (note.Prior == Note.Priority.Important)
                    {
                        note.ImageLocation = "images/important.png";
                    }

                    else if (note.Prior == Note.Priority.Relaxed)
                    {
                        note.ImageLocation = "images/relaxed.png";
                    }
                    

                    main.listBxNoteBoard.ItemsSource = null;
                    main.listBxNoteBoard.ItemsSource = main.notes;

                 

                }
            }

            Application.Current.Resources.MergedDictionaries[0] = md;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = this.Owner as MainWindow;
            FontFamily selectedFont = comboBxFont.SelectedItem as FontFamily ;

           // MessageBox.Show(selectedFont);

            if (selectedFont != null)
            {
                main.FontFamily = new FontFamily(selectedFont.ToString());
            }
            

           

            this.Close();
        }
    }
}
