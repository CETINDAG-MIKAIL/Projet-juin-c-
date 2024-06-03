using System.Collections.Specialized;
using System.ComponentModel;

namespace EntitiesLibrary
{
    public class Person : INotifyPropertyChanged, INotifyCollectionChanged
    {
        private int _id { get; set; }
        private string? _firstName = null;
        public string FirstName
        {
            get { return   _firstName; }
            set
            {
                _firstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
            }
        }
        private string? _lastName = null;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;        
            }
            set
            {
                _birthDate = value;
            }
        }
        public int Age
        {
            get
            {
                // Calcul de l'âge en soustrayant l'année de naissance de l'année actuelle
                int age = DateTime.Today.Year - BirthDate.Year;

                // Réduire l'âge si l'anniversaire n'est pas encore passé cette année
                if (BirthDate > DateTime.Today.AddYears(-age))
                    age--;

                return age;
            }
        }

        private string? _position = null;

        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private string? _photoPath = null;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public string PhotoPath
        {
            get { return _photoPath; }
            set { _photoPath= value; }
        }
        public Person() 
        { 
            _id = -1;
            _firstName = "Default";
            _lastName = "Default";
            _birthDate = DateTime.Now;
            _position = "Default";
            _photoPath = "Default";
        }
        public Person(int id, string firstName, string lastName, DateTime birthDate, string position, string photoPath)
        {
            _id = id;
            FirstName = firstName;
            LastName = lastName;
            _birthDate = birthDate;
            Position = position;
            PhotoPath = photoPath;
        }
    }
}
