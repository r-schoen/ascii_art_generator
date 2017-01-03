using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asciiImageConverter
{
    public class Class1
    {

        public static string ConvertAscii(Stream imageStream, 
                                    string greyscale = "@%#*+=-:. ",
                                    int charH = 9,
                                    int charW = 5)
        {
            string output = "";
            // TODO: some stuff
            Bitmap bit = new Bitmap(imageStream);

            output = "";
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
            return output;
        }
    }
}
