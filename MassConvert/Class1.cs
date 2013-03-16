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
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            string dir = Directory.GetCurrentDirectory();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string[] files = Directory.GetFiles(@dir, "*.blp");
            string converter = null;
            int factor = 50;
            bool length = (int)files.Length == 0;
            
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
                            FileName = "<converter> " + builder.ToString()
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
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
