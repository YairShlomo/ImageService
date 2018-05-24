using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ImageServiceGUI
{
    class Debug_program
    {
        private string path;
        public Debug_program()
        {
            path = "C:/Users/gal/Pictures/debug/debug.txt";
        }
        public void write(string message)
        {
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(message);

                }
            }
            else
            {
               // File.Delete(path);
                
                using (StreamWriter file = new StreamWriter(path, true))
                    file.WriteLine(message);
            }
        }
    }
}
