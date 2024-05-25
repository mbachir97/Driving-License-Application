using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Global
{
    internal class clsGlobal
    {


        private static bool DeleteValueforKey()
        {
            string valueName1 = "UserName";
            string ValueName2 = "PassWord";
            string keyPath = @"SOFTWARE\DVLD";


            try
            {
                // Open the registry key in read/write mode with explicit registry view
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(keyPath, true))
                    {
                        if (key != null)
                        {
                            // Delete the specified value
                            key.DeleteValue(valueName1);
                            key.DeleteValue(ValueName2);

                            return true;    
                        }
                        else
                        {
                            MessageBox.Show(" we can not Delete Key this Value is not Found");
                            return false;
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {

                MessageBox.Show("UnauthorizedAccessException: Run the program with administrative privileges."
);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;   
            }
        }
        public static bool RememberUserNameAndPassWord(string UserName,string PassWord)
        {
            //try
            //{
            //    string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

            //    string FilePath = CurrentDirectory + "\\data.txt";

            //    if (UserName == "" && File.Exists(FilePath))
            //    {
            //        File.Delete(FilePath);
            //        return true;
            //    }

            //    string DataToSave = UserName + "#//#" + PassWord;

            //    using (StreamWriter sw = new StreamWriter(FilePath) )
            //    {
            //        sw.WriteLine(DataToSave);   
            //        return true;
            //    }
            //}
            //catch (Exception ex) {

            //    MessageBox.Show($"An Error Acure {ex.Message}");
            //    return false;   
            //}


            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD";
            string valueName1 = "UserName";
            string valueName2 = "PassWord";

            // Specify the registry key path and value name

            if (UserName == "")
            {
                return DeleteValueforKey();
            }



            try
            {
                // Write the value to the Registry
                Registry.SetValue(keyPath, valueName1, UserName, RegistryValueKind.String);
                Registry.SetValue(keyPath, valueName2, PassWord, RegistryValueKind.String);

                return true;    

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error Acure {ex.Message}");

                return false;
            }




        }


        public static bool GetStoredCredential(ref string UserName,ref string PassWord)
        {
            //try
            //{
            //    string CurrentDirectory =System.IO.Directory.GetCurrentDirectory();

            //    string filepath = CurrentDirectory + "\\data.txt";

            //    if (File.Exists(filepath))
            //    {
            //        using(StreamReader sw = new StreamReader(filepath) )
            //        {
            //            string line;
            //            while((line = sw.ReadLine()) != null)
            //            {
            //                Console.WriteLine(line);
            //                string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
            //                UserName = result[0];   
            //                PassWord = result[1];   

            //            }

            //            return true;


            //        }   
            //    }

            //    else
            //        return false;

            //}
            //catch (Exception ex) {

            //    MessageBox.Show($"An Error Acure {ex.Message}");
            //    return false;


            //}


            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD"; ;
            string valueName1 = "UserName";
            string valueName2 = "PassWord";


            try
            {
                // Read the value from the Registry
                string value = Registry.GetValue(keyPath, valueName1, null) as string;

                string value1 = Registry.GetValue(keyPath, valueName2, null) as string;

                if (value != null && value1 != null)
                {
                   UserName = value;    
                   PassWord = value1; 
                    return true;    
                }
                else
                {
                    //MessageBox.Show("Value is not Found ");
                    return false;   
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;   
            }

        }


        


    }
}
