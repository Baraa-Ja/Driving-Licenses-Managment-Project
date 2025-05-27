using Driver_License_Business_Layer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace Driving_Licenses_Managment_Presentation_Layer.Global
{
    internal static class clsGlobal
    {
        public static clsUsers CurrentUser;
        /// <summary>
        /// This Function Will Take Care of Exeptions Handeling on SystemLogging
        /// </summary>
        /// <param name="Source"> Your Project Or Application Name</param>
        /// <param name="Message"> You Log Message</param>
        /// <param name="Type"> Your Log Type</param>
        public static void ExeptionsEventLog(string Message, EventLogEntryType Type, string Source = "DLVDApp")
        {
            if(!EventLog.SourceExists(Source))
            {
                EventLog.CreateEventSource(Source, "Application");
            }

            EventLog.WriteEntry(Source, Message, Type);
        }

        /// <summary>
        /// this Will Take Care of Hashing And Encryption of data
        /// </summary>
        /// <param name="input"> Your Data Tht You Want To Hash</param>
        /// <returns></returns>
        static string ComputeHash(string input)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static string key = "1234567890123456";
        /// <summary>
        /// This Function is used to encrypt text
        /// </summary>
        /// <param name="plainText"> Your Text To Encrypt</param>
        /// <param name="key"> Your key That Will Encrypt Your Text</param>
        /// <returns></returns>
        static string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES encryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create an encryptor
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


                // Encrypt the data
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }


                    // Return the encrypted data as a Base64-encoded string
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }


        static string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES decryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create a decryptor
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);


                // Decrypt the data
                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                {
                    // Read the decrypted data from the StreamReader
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            //string HashedPassword = ComputeHash(Password);

            try
            {
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
                ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);
                return false;
            }

        }

        public static bool RememberUsernameAndPasswordUsingRegistry(string Username, string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DLVL_Project";

            string UserNameValue = "Username";
            string UserNameData = Username;

            string PasswordValue = "Password";
            string PasswordData = Encrypt(Password, key);
            //string PasswordData = ComputeHash(Password);

            try
            {
                Registry.SetValue(KeyPath, UserNameValue, UserNameData, RegistryValueKind.String);
                Registry.SetValue(KeyPath, PasswordValue, PasswordData, RegistryValueKind.String);

                return true;
            }

            catch (Exception ex)
            {
                ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);
                MessageBox.Show("Error");
                return false;
            }

        }
        public static bool GetStoredCredentialUsingRegistry(ref string Username, ref string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DLVL_Project";

            string UserNameValue = "Username";

            string PasswordValue = "Password";

            try
            {
               string UsernameDataVlaue = Registry.GetValue(KeyPath, UserNameValue, null) as string;
               string PasswordDataValue = Registry.GetValue(KeyPath, PasswordValue, null) as string;

                if(UserNameValue != null && PasswordValue != null)
                {
                    Username = UsernameDataVlaue;
                    Password = Decrypt(PasswordDataValue, key);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);
                MessageBox.Show("Error: " + ex);
                return false;
            }
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }
    }
}
