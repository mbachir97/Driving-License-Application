using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Global
{
    internal class clsUtil
    {
      static  string GenerateGuide()
        {
            Guid guid = Guid.NewGuid(); 

            return guid.ToString(); 
        }

        private static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            
                if (!Directory.Exists(FolderPath))
                {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex) {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
                  
                }

                return true;
           

           
        }

       private static string RePlaceFileWithNewGuid(string soursefile)
        {
            FileInfo fileInfo = new FileInfo(soursefile);   
            string exten = fileInfo.Extension;

            return GenerateGuide() +  exten;
        }

         public static bool CopyImageToProjectImagesFolder(ref string soursefile)
        {
            string FolderPath = @"C:\DVLD-People-Images\";

            if (!CreateFolderIfDoesNotExist(FolderPath))
            {
                return false;
            }

            string DestinationFile = FolderPath + RePlaceFileWithNewGuid(soursefile);

            try
            {
                File.Copy(soursefile, DestinationFile, true); 
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "warning and error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;   
            }
            soursefile = DestinationFile;
            return true;
        }


       public static string ComputeHash(string input)
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

        public static void Log(string SourseNane,string LogName,string Mesg,
            EventLogEntryType EntryType)
        {
            if(!EventLog.SourceExists(SourseNane)) 
            {
                EventLog.CreateEventSource(SourseNane, LogName);
            }

            EventLog.WriteEntry(SourseNane, Mesg, EntryType);
        }


    }

}
