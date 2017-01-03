using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiImageConverter
{
    public class AsciiConverter
    {

        /// <summary>
        /// Takes in a stream, and outputs a string for output to file.
        /// </summary>
        /// <param name="imageStream"></param>
        /// <param name="greyscale"></param>
        /// <param name="charH"></param>
        /// <param name="charW"></param>
        /// <returns></returns>
        public static string ConvertAscii(string inputImageStream, 
                                    string greyscale = "@%#*+=-:. ",
                                    int charH = 9,
                                    int charW = 5)
        {
            string output = "";

            // create bitmap of input stream
            using (Bitmap bit = new Bitmap(inputImageStream))
            {
                //begin outer loop. searches in 5x7 blocks
                for (int yo = 0; yo < bit.Height; yo += charH)
                {
                    if (yo + charH < bit.Height) //to avoid going over boundaries
                    {
                        for (int xo = 0; xo < bit.Width; xo += charW)
                        {
                            if (xo + charW < bit.Width)
                            {
                                //begin inner loop. searches every pixel in a single block.
                                int avgTotal = 0;
                                double scaleNumber;
                                for (int yi = 0; yi < charH; yi++)
                                {
                                    for (int xi = 0; xi < charW; xi++)
                                    {
                                        int x = xi + xo;
                                        int y = yi + yo;
                                        Color newColor = bit.GetPixel(x, y);
                                        int avg = (newColor.R + newColor.G + newColor.B) / 3;
                                        avgTotal += avg;

                                    }
                                }
                                double avgFinal = avgTotal / (charH * charW);
                                double avgFinalPercent = avgFinal / 255;
                                scaleNumber = avgFinalPercent * greyscale.Length;
                                output += greyscale[(int)scaleNumber];
                            }
                        }
                        output += "\r\n";
                    }
                }
            }
            return output;
        }
    }
}
