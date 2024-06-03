using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLibrary
{
    public class Coach : Person
    {
        private string _role;

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public Coach() : base() { Role = "default"; }
        public Coach(int id, string firstName, string lastName, DateTime birthDate, string position, string photoPath, string role) : base(id, firstName, lastName, birthDate, position, photoPath){ Role = role; }
    }
}
