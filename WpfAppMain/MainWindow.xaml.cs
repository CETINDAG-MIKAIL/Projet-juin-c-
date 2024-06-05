using System.Collections.ObjectModel;
using System.Windows;
using EntitiesLibrary;
using Newtonsoft.Json;
using System.IO;
using WpfAppCoachForm;
using WpfAppPlayerForm;
using System.Xml.Serialization;

namespace WpfAppMain
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Player> Players { get; set; }
        public ObservableCollection<Coach> Coachs { get; set; }
        public Team Team { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            string filePath = "team.json";

            if (!File.Exists(filePath))
            {
                CreateTeamJsonFile(filePath);
            }

            Players = new ObservableCollection<Player>();
            Coachs = new ObservableCollection<Coach>();

            // Charger l'équipe à partir du fichier JSON
            Team = Team.LoadFromJson(filePath);

            // Ajouter les joueurs et les coachs de l'équipe à vos listes observables
            foreach (var player in Team.Players)
            {
                Players.Add(player);
            }
            foreach (var coach in Team.Coachs)
            {
                Coachs.Add(coach);
            }
            DataContext = this;
        }

        private void SaveTeamToJson()
        {
            // Mettre à jour les listes de joueurs et de coachs de l'équipe avec les données actuelles
            Team.Players.Clear();
            Team.Coachs.Clear();
            foreach (var player in Players)
            {
                Team.AddPlayer(player);
            }
            foreach (var coach in Coachs)
            {
                Team.AddCoach(coach);
            }

            string filePath = "team.json";
            Team.SaveToJson(filePath);
        }

        private void CreateTeamJsonFile(string filePath)
        {
            // Créer un objet Team vide
            Team team = new Team();

            // Convertir l'objet Team en format JSON
            string jsonData = JsonConvert.SerializeObject(team);

            // Écrire les données JSON dans le fichier
            File.WriteAllText(filePath, jsonData);
        }

        private void ExportTeamToJson(string filePath)
        {
            SaveTeamToJson();
            Team.SaveToJson(filePath);
        }

        private void ImportTeamFromJson(string filePath)
        {
            Team = Team.LoadFromJson(filePath);
            Players.Clear();
            Coachs.Clear();
            foreach (var player in Team.Players)
            {
                Players.Add(player);
            }
            foreach (var coach in Team.Coachs)
            {
                Coachs.Add(coach);
            }
        }

        private void Button_NewPlayer(object sender, RoutedEventArgs e)
        {
            CreatePlayerForm createPlayerForm = new CreatePlayerForm();
            bool? result = createPlayerForm.ShowDialog();

            if (result == true)
            {
                Player newPlayer = createPlayerForm.GetPlayerInfo();
                Players.Add(newPlayer);
            }
        }

        private void Button_NewCoach(object sender, RoutedEventArgs e)
        {
            CreateCoachForm createCoachForm = new CreateCoachForm();
            bool? result = createCoachForm.ShowDialog();

            if (result == true)
            {
                Coach newCoach = createCoachForm.GetCoachInfo();
                Coachs.Add(newCoach);
            }
        }

        private void Button_DeletePlayer(object sender, RoutedEventArgs e)
        {
            if (DataGridPlayers.SelectedItem != null && DataGridPlayers.SelectedItem is Player selectedPlayer)
            {
                // Supprimer le joueur sélectionné de la liste des joueurs
                Players.Remove(selectedPlayer);
                SaveTeamToJson();
            }
        }

        private void Button_DeleteCoach(object sender, RoutedEventArgs e)
        {
            if (DataGridCoachs.SelectedItem != null && DataGridCoachs.SelectedItem is Coach selectedCoach)
            {
                // Supprimer le coach sélectionné de la liste des coachs
                Coachs.Remove(selectedCoach);
                SaveTeamToJson();
            }
        }

        private void ExportPlayerToXml(Player player, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Player));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, player);
            }
        }

        private Player ImportPlayerFromXml(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Player));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (Player)serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Le fichier XML ne correspond pas au format attendu pour un joueur.", "Erreur de désérialisation", MessageBoxButton.OK, MessageBoxImage.Error);
                return new Player();
            }
        }

        private void ExportCoachToXml(Coach coach, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Coach));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, coach);
            }
        }

        private Coach ImportCoachFromXml(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Coach));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (Coach)serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Le fichier XML ne correspond pas au format attendu pour un coach.", "Erreur de désérialisation", MessageBoxButton.OK, MessageBoxImage.Error);
                return new Coach();
            }
        }

        private void Button_ImportPlayer(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Player importedPlayer = ImportPlayerFromXml(openFileDialog.FileName);
                Players.Add(importedPlayer);
                SaveTeamToJson(); // Sauvegarder après import
            }
        }

        private void Button_ExportPlayer(object sender, RoutedEventArgs e)
        {
            if (DataGridPlayers.SelectedItem != null && DataGridPlayers.SelectedItem is Player selectedPlayer)
            {
                // Choisir le fichier de destination
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "XML Files (*.xml)|*.xml"
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    ExportPlayerToXml(selectedPlayer, saveFileDialog.FileName);
                }
            }
        }

        private void Button_ExportCoach(object sender, RoutedEventArgs e)
        {
            if (DataGridCoachs.SelectedItem != null && DataGridCoachs.SelectedItem is Coach selectedCoach)
            {
                // Choisir le fichier de destination
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "XML Files (*.xml)|*.xml"
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    ExportCoachToXml(selectedCoach, saveFileDialog.FileName);
                }
            }
        }

        private void Button_ImportCoach(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Coach importedCoach = ImportCoachFromXml(openFileDialog.FileName);
                Coachs.Add(importedCoach);
                SaveTeamToJson(); // Sauvegarder après import
            }
        }

        private void ImporterEquipe_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                ImportTeamFromJson(openFileDialog.FileName);
                SaveTeamToJson(); // Sauvegarder après import
            }
        }

        private void ExporterEquipe_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                ExportTeamToJson(saveFileDialog.FileName);
            }
        }
    }
}
