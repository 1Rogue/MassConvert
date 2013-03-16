using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Project1
{
    class Class1
    {

        static string ProgramVersion = "1.0.0";


        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            string dir = Directory.GetCurrentDirectory();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string[] files = Directory.GetFiles(@dir, "*.blp");
            int factor = 50;
            StartUp();
            if (files.Length != 0)
            {
                Console.WriteLine(string.Concat("There are ", (int)files.Length, " files in this directory"));
                StringBuilder builder = new StringBuilder();
                for (int w=0; w<files.Length/factor; w++) {
                    for (int i = w*factor; i < w * factor + factor - 1; i++)
                    {
                        builder.Append(string.Concat(files.GetValue(i), " "));
                    }
                    builder.Append(files.GetValue(w * factor + factor - 1));
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "%APPDATA%/MassConverter/BLPConverter.exe " + builder.ToString()
                        }
                    };
                    process.Start();
                    process.WaitForExit();
                    builder.Clear();
                }
            }
            else
            {
                Console.WriteLine("No Files found within current directory");
            }
            Exit(0);
        }

        static void StartUp()
        {
            string cv = GetCurrentVersion();
            if (!ProgramVersion.Equals(cv))
            {
                Console.WriteLine("MassConvert is out of date! Newest version: " + cv);
                Console.WriteLine("You can download at http://bit.ly/YhE3sR");
                Exit(0);
            }
            if (!Directory.Exists("%APPDATA%/MassConverter"))
            {
                Directory.CreateDirectory("%APPDATA%/MassConverter");
            }
            if (!File.Exists("%APPDATA%/MassConverter/BLPConverter.exe"))
            {
                Console.WriteLine("No Converter File supplied. Downloading a new converter.");
                GetNewConverter();
            }
        }

        static String GetCurrentVersion()
        {
            System.Net.WebClient client = new System.Net.WebClient();
            Stream stream = client.OpenRead("https://raw.github.com/1Rogue/MassConvert/master/version.txt");
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        static void GetNewConverter()
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.DownloadFile("https://github.com/1Rogue/MassConvert/blob/master/Resources/BLPConverter.exe?raw=true", @"%APPDATA%/MassConverter/BLPConverter.exe");
            Console.WriteLine("Converter Downloaded.");
        }

        static void Exit(int code)
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Environment.Exit(code);
        }
    }
}
