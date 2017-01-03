using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsciiImageConverter;

namespace ascii_art_console
{
    class Program
    {
        static void Main(string[] args)
        {
            string scale;
            int charH, charW;
            // TODO: the rest of the program
            if(args.Length == 0)
            {
                Console.WriteLine("Please enter filename");
                return;
            }
            try
            {
                using (var bmp = new Bitmap(args[0]))
                {
                    string outName = Path.ChangeExtension(args[0], ".txt");

                    // TODO: parse args here
                    scale = "@%#*+=-:. ";
                    charH = 9;
                    charW = 5;

                    string output = AsciiConverter.ConvertAscii(args[0], scale, charH, charW);

                    using (var sw = new StreamWriter(outName))
                    {
                        sw.WriteLine(output);
                        Console.WriteLine("Written to '" + outName + "' successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }
    }
}
