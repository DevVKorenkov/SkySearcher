using SkySearcher.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkySearcher.Model
{
    class EncryptClass
    {
        public bool StartEncript(string enteredPass)
        {
            string pass = string.Empty;

            pass = File.Exists(@"Resources\pass") ? EncryptWord(File.ReadAllText(@"Resources\pass")) : throw new Exception("Файл пароля не найден");

            return enteredPass == pass;
        }

        private string EncryptWord(string word)
        {
            ushort key = 0x0128;

            var ch = word.ToArray();

            string newStr = "";
            foreach (var c in ch)
            {
                newStr += Encrypt(c, key);
            }

            return newStr;
        }

        private char Encrypt(char ch, ushort sKey)
        {
            ch = (char)(ch ^ sKey);
            return ch;
        }
    }
}
