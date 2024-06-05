using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace EntitiesLibrary
{
    public class MyAppParam
    {
        // Clé de registre pour stocker les paramètres de l'application
        private const string RegistryKey = @"Software\MyApp";

        // Propriétés pour les paramètres de l'application
        public string FilePath { get; set; }
        public double MainWindowWidth { get; set; }
        public double MainWindowHeight { get; set; }

        // Méthode pour charger les paramètres depuis la registry
        public void LoadRegistryParameters()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKey))
            {
                if (key != null)
                {
                    FilePath = key.GetValue("FilePath") as string;
                    MainWindowWidth = Convert.ToDouble(key.GetValue("MainWindowWidth", 800));
                    MainWindowHeight = Convert.ToDouble(key.GetValue("MainWindowHeight", 600));
                }
            }
        }

        // Méthode pour enregistrer les paramètres dans la registry
        public void SaveRegistryParameters()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKey))
            {
                if (key != null)
                {
                    key.SetValue("FilePath", FilePath);
                    key.SetValue("MainWindowWidth", MainWindowWidth);
                    key.SetValue("MainWindowHeight", MainWindowHeight);
                }
            }
        }
    }
}

