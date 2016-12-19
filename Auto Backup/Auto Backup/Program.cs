using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Auto_Backup_Artur
{
    class Program
    {
        static void Main(string[] args)
        {
            bool bCheck = true;
            do
            {
                //Getting all drives in Laufwerke
                DriveInfo[] Laufwerke = DriveInfo.GetDrives();

                //Declaring Target Path
                string sZielpfad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Backup " + DateTime.Now.ToShortDateString();

                //Sleep to reduce cpu usage
                Thread.Sleep(1000);

                //Going through all drives
                foreach (DriveInfo dInfo in Laufwerke)
                {
                    //End Program if Target Directory exist
                    if (File.Exists(sZielpfad + ".zip"))
                    {
                        bCheck = false;
                    }

                    //Check if Drive is Removable
                    else if (dInfo.DriveType == DriveType.Removable)
                    {
                        //Sleep to Reduce cpu usage
                        Thread.Sleep(1000);

                        //SourcePath declaring
                        DirectoryInfo dPfad = dInfo.RootDirectory;
                        string sQuelle = Path.GetFullPath(dPfad.ToString());

                        Thread.Sleep(500);

                        //Getting all Info's
                        DirectoryInfo dOrdnerInfo = new DirectoryInfo(sQuelle);
                        FileInfo[] fDateien = dOrdnerInfo.GetFiles();

                        //Create Zip File
                        ZipFile.CreateFromDirectory(sQuelle, sZielpfad + ".zip", CompressionLevel.Optimal, true);
                    }
                }
            } while (bCheck == true);
        }
    }
}
