using System.Collections.ObjectModel;
using System.Windows;
using EntitiesLibrary;
using Newtonsoft.Json;
using System.IO;
using WpfAppCoachForm;
using WpfAppPlayerForm;

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

        private void CreateTeamJsonFile(string filePath)
        {
            // Créer un objet Team vide
            Team team = new Team();

            // Convertir l'objet Team en format JSON
            string jsonData = JsonConvert.SerializeObject(team);

            // Écrire les données JSON dans le fichier
            File.WriteAllText(filePath, jsonData);
        }
        
    }
}
