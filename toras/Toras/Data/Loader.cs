using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toras.Utilities;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;

namespace Toras.Data
{
    public static class Loader
    {
        private static UserData userData = new UserData();

        public static void Init()
        {           
            // Setup default user data or load from file
            if (UserFirstSession())
            {
                userData.SetDefaultData();
                Serialize();
            } else
            {
                Deserialize();
            }
        }

        /* Deletes existing data.bin file (if exists) and replaces it with new user data. */
        public static void Serialize()
        {
            if (!UserFirstSession())
            {
                Debug.Trace($"Original String: { userData.FtpPassword }");
                userData.encrypted = Cipher.Encrypt(userData.FtpPassword, userData.IV);
                userData.FtpPassword = ""; // Wipe password
                Debug.Trace($"Encrypted String: { Encoding.ASCII.GetString(userData.encrypted) }");
            }

            File.Delete(FileManager.DataPath); // Delete data.bin
            Stream stream = File.OpenWrite(FileManager.DataPath);

            // Format userData as binary
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, userData);

            stream.Flush();
            stream.Close();
            stream.Dispose();
        }

        /* Loads user data from data.bin */
        public static void Deserialize()
        {
            // Create stream to add UserData to
            FileStream fstream = File.Open(FileManager.DataPath, FileMode.Open);

            // Format UserData as binary
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                object obj = formatter.Deserialize(fstream);
                userData = (UserData)obj;
            }
            catch (Exception e)
            {
                Debug.Trace(e.ToString());
            }

            Debug.Trace($"Encrypted String: { Encoding.ASCII.GetString(userData.encrypted) }");
            userData.FtpPassword = Cipher.Decrypt(userData.encrypted, userData.IV);
            Debug.Trace($"Decrypted String: {userData.FtpPassword}");

            fstream.Flush();
            fstream.Close();
            fstream.Dispose();
        }

        public static UserData GetUserData()
        {
            return userData;
        }

        /* Checks whether data.txt exists. 
         * @return true, if data.txt not found
         * @return false, if data.txt found */
        private static bool UserFirstSession()
        {
            return FileManager.FileExists(FileManager.DataPath);
        }

    }
}
