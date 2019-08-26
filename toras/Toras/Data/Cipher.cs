using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.IO;
using Toras.Utilities;

namespace Toras.Data
{
    public static class Cipher
    {
        private static string macAddress = GetMacAddress()+GetMacAddress();
        private static byte[] key = Encoding.ASCII.GetBytes(macAddress);

        public static byte[] Encrypt(string raw, byte[] IV)
        {
            byte[] encrypted;

            using (AesManaged aes = new AesManaged())
            {
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                ICryptoTransform encryptor = aes.CreateEncryptor(key, IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        sw.Write(raw);
                        encrypted = ms.ToArray();
                        
                    }
                }
            }
            return encrypted;
        }

        public static string Decrypt(byte[] cipherText, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

        /* Returns the MAC address of the NIC */
        public static string GetMacAddress()
        {
            string macAddress = NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
            return macAddress;
        }

    }
}
