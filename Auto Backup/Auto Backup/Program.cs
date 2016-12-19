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
                DriveInfo[] Laufwerke = DriveInfo.GetDrives();

                string sZielpfad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Backup " + DateTime.Now.ToShortDateString();

                Thread.Sleep(200);
                foreach (DriveInfo dInfo in Laufwerke)
                {
                    //End Program if Target exist
                    if (File.Exists(sZielpfad + ".zip"))
                    {
                        bCheck = false;
                    }

                    //Check if Drive is Removable
                    else if (dInfo.DriveType == DriveType.Removable)
                    {

                        Thread.Sleep(200);
                        //SourcePath declaring
                        DirectoryInfo dPfad = dInfo.RootDirectory;
                        string sQuelle = Path.GetFullPath(dPfad.ToString());

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
