using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace EntitiesLibrary
{
    public class Team
    {
        public List<Player> Players { get; private set; }
        public List<Coach> Coachs { get; private set; }

        public Team()
        {
            Players = new List<Player>();
            Coachs = new List<Coach>();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void AddCoach(Coach coach)
        {
            Coachs.Add(coach);
        }

        public void SaveToJson(string filePath)
        {
            var jsonData = JsonConvert.SerializeObject(this);
            File.WriteAllText(filePath, jsonData);
        }

        public static Team LoadFromJson(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Team>(jsonData);
        }
    }
}
