using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLibrary
{
    public class Player : Person
    {
        public int NumberJersey { get; set; }

        public Player() : base() {  NumberJersey = -1;  }
        public Player(int numberJersey): base() {  NumberJersey = numberJersey;}
        public Player(int id, string firstName, string lastName, DateTime birthDate, string position, string photoPath, int numberJersey) : base(id,firstName,lastName, birthDate, position, photoPath) { NumberJersey = numberJersey; }
    }
}
