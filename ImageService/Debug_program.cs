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
        /// <summary>
        /// Initializes a new instance of the <see cref="Debug_program"/> class.
        /// </summary>
        public Debug_program()
        {
            path = "C:/Users/gal/Pictures/debug/debug.txt";
        }
        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void write(string message)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path,true))
                file.WriteLine(message);
        }
    }
}
