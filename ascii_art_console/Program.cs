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
                    string outName;

                    // argument syntax: /charw=5
                    var parsedArgs = args
                                    .Where(s => s[0] == '/') // gets items that start with /
                                    .Select(s => s.Substring(1).Split('=')) // splits into substring array using = as delimitor
                                    .ToDictionary(s => s[0], y => y[1]); // turns into dictionary

                    #region Set Defaults
                    if (!parsedArgs.ContainsKey("charw"))
                        charW = 5;
                    else
                    {
                        if(!int.TryParse(parsedArgs["charw"], out charW))
                        {
                            Console.WriteLine("Error getting char width");
                            return;
                        }
                    }
                    if (!parsedArgs.ContainsKey("charh"))
                        charH = 9;
                    else
                    {
                        if(!int.TryParse(parsedArgs["charh"], out charH))
                        {
                            Console.WriteLine("Error getting char height");
                            return;
                        }
                    }
                    if (!parsedArgs.ContainsKey("scale"))
                        scale = "@%#*+=-:. ";
                    else
                    {
                        scale = parsedArgs["scale"].Trim('"');
                    }
                    if (!parsedArgs.ContainsKey("out"))
                        outName = Path.ChangeExtension(args[0], ".txt");
                    else
                    {
                        outName = parsedArgs["out"];
                    }
                    #endregion
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
