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
using Newtonsoft.Json; //JSON
using System.IO; //FILE

namespace WpfAppPlayerForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CreatePlayerForm : Window
    {
        private string selectedImagePath; // Déclarer une variable pour stocker le chemin d'accès de l'image sélectionnée
        public CreatePlayerForm()
        {
            InitializeComponent();
        }
        public Player GetPlayerInfo()
        {
            Player player = new Player();

            // Récupérer les informations depuis les contrôles de votre formulaire
            player.FirstName = TextBoxFirstName.Text;
            player.LastName = TextBoxLastName.Text;
            player.Position = (ComboBoxPosition.SelectedItem as ComboBoxItem)?.Content?.ToString(); //  il est possible que le type de l'objet retourné soit simplement un objet générique plutôt qu'un ComboBoxItem spécifique.//Pour résoudre cela, vous pouvez essayer de convertir explicitement l'élément sélectionné en un ComboBoxItem dans votre code-behind.
            player.PhotoPath = selectedImagePath;
            player.BirthDate = DatePickerBirthDate.SelectedDate ?? DateTime.MinValue; // renvoie la date selectionner sinon une date par default
            if (int.TryParse(TextBoxNumberJersey.Text, out int numberJersey))
            {
                player.NumberJersey = numberJersey;
            }
            else
            {
                // Gérer le cas où la conversion échoue, par exemple en affectant une valeur par défaut
                player.NumberJersey = 0;
                // Ou en affichant un message à l'utilisateur pour lui indiquer que la valeur n'est pas valide
                MessageBox.Show("Le numéro de maillot doit être un nombre entier.");
            }

            // etc., pour les autres propriétés du joueur

            return player;
        }

        private void Button_ClickSavePlayer(object sender, RoutedEventArgs e)
        {
            Player newPlayer = GetPlayerInfo();
            this.DialogResult = true;

            // Charger l'équipe existante à partir du fichier JSON
            string filePath = "team.json";
            Team team = Team.LoadFromJson(filePath);

            // Ajouter le nouveau joueur à l'équipe
            team.AddPlayer(newPlayer);

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