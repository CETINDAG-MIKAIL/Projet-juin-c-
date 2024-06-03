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
using Microsoft.Win32;
using Newtonsoft.Json;

namespace WpfAppCoachForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CreateCoachForm : Window
    {
        private string selectedImagePath; // Déclarer une variable pour stocker le chemin d'accès de l'image sélectionnée

        public CreateCoachForm()
        {
            InitializeComponent();
        }
        public Coach GetCoachInfo()
        {
            Coach coach = new Coach();

            // Récupérer les informations depuis les contrôles de votre formulaire
            coach.FirstName = TextBoxFirstName.Text;
            coach.LastName = TextBoxLastName.Text;
            coach.Role = (ComboBoxRole.SelectedItem as ComboBoxItem)?.Content?.ToString(); //  il est possible que le type de l'objet retourné soit simplement un objet générique plutôt qu'un ComboBoxItem spécifique.//Pour résoudre cela, vous pouvez essayer de convertir explicitement l'élément sélectionné en un ComboBoxItem dans votre code-behind.
            coach.PhotoPath = selectedImagePath;
            coach.BirthDate = DatePickerBirthDate.SelectedDate ?? DateTime.MinValue; // renvoie la date selectionner sinon une date par default
         

            // etc., pour les autres propriétés du joueur

            return coach;
        }
        private void Button_ClickSaveCoach(object sender, RoutedEventArgs e)
        {
            Coach newCoach = GetCoachInfo();
            this.DialogResult = true;

            // Charger l'équipe existante à partir du fichier JSON
            string filePath = "team.json";
            Team team = Team.LoadFromJson(filePath);

            // Ajouter le nouveau joueur à l'équipe
            team.AddCoach(newCoach);

            // Sauvegarder l'équipe mise à jour dans le fichier JSON
            team.SaveToJson(filePath);



        }
        private void Button_ClickButtonUpload(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // Obtenez le chemin du fichier sélectionné et chargez l'image
                selectedImagePath = openFileDialog.FileName;
                // Affichez l'image dans votre interface utilisateur, par exemple dans une Image ou un contrôle ImageBrush
                // Par exemple, si vous avez une Image nommée ImagePreview dans votre XAML :
                ImagePreview.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }
    }

}