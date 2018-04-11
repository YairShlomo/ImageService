using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ImageService
{
    class Debug_program
    {
        private string path;
        public Debug_program()
        {
<<<<<<< HEAD
            path = "C:/Users/gal/Pictures/debug/debug.txt";
=======
            path = "C:/Users/yair144/Pictures/debug/debug.txt";
>>>>>>> edda2540f28b883c0488e1fef78ebb6ee5f6a94d
        }
        public void write(string message)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path,true))
                file.WriteLine(message);
        }
    }
}
