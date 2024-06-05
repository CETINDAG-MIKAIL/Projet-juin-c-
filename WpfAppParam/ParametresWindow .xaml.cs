using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EntitiesLibrary;

namespace WpfAppParam
{
    /// <summary>
    /// Interaction logic for ParametresWindow.xaml
    /// </summary>
    public partial class ParametresWindow : Window
    {
        public Size DesiredMainWindowSize { get; private set; }
        private MyAppParam appParams;



        public ParametresWindow(MyAppParam appParams)
        {
            InitializeComponent();
            this.appParams = appParams;

            // Charger les paramètres actuels dans les contrôles
            txtFolderPath.Text = appParams.FilePath;
            txtWidth.Text = appParams.MainWindowWidth.ToString();
            txtHeight.Text = appParams.MainWindowHeight.ToString();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Tous les fichiers (*.*)|*.*";

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                txtFolderPath.Text = System.IO.Path.GetDirectoryName(dialog.FileName);
            }
        }

     
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Enregistrer les paramètres
            appParams.FilePath = txtFolderPath.Text;
            appParams.MainWindowWidth = double.Parse(txtWidth.Text);
            appParams.MainWindowHeight = double.Parse(txtHeight.Text);

            // Sauvegarder dans la registry
            appParams.SaveRegistryParameters();

            // Fermer la fenêtre modale
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            // Annuler les modifications et fermer la fenêtre modale
            this.Close();
        }
    }
}