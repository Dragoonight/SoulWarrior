using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    public struct SettingsData
    {
        public static readonly string FileName = "settings.conf";
        public static readonly Data DefaultData = new Data
        {
            MoveUp = Keys.W,
            MoveDown = Keys.S,
            MoveLeft = Keys.A,
            MoveRight = Keys.D,
            Attack = Keys.Space
        };

        [Serializable]
        public struct Data
        {
            public Keys MoveUp, MoveDown, MoveLeft, MoveRight, Attack;
        }
    }

    public static class Save
    {
        public static SettingsData.Data _currentSettingsData;

        public static void Initialize()
        {
            // Settings
            //If the file does not already exist
            if (!File.Exists(SettingsData.FileName))
            {
                
                
                DoSave(SettingsData.DefaultData, SettingsData.FileName);
            }
            else
            {
                _currentSettingsData = LoadData<SettingsData.Data>(SettingsData.FileName);
            }
        }

        /// <summary>
        /// Opens file and saves data
        /// </summary>
        /// <param name="data">data to write</param>
        /// <param name="filename">name of file to write to</param>
        private static void DoSave<T1>(T1 data, string filename)
        {
            // Open or create file
            FileStream stream = File.Open(filename, FileMode.OpenOrCreate);
            try
            {
                // Make to XML and try to open filestream
                XmlSerializer serializer = new XmlSerializer(typeof(T1));
                serializer.Serialize(stream, data);
            }
            finally
            {
                // Close file
                stream.Close();
            }
        }


        private static T1 LoadData<T1>(string fileName)
        {
            T1 data;

            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T1));
                data = (T1)serializer.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }

            return data;
        }


    }
}
