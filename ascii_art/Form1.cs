using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ascii_art
{
    public partial class Form1 : Form
    {
        private Bitmap bit;
        private string output;
        private string filename;
        public Form1()
        {
            InitializeComponent();
            ResetTextBoxes();
        }

        private void ResetTextBoxes()
        {
            txtBxCharH.Text = "9";
            txtBxCharW.Text = "5";
            SetScaleShort();
        }

        private void SetScaleShort()
        {
            txtBxScale.Text = "@%#*+=-:. ";
        }
        private void SetScaleLong()
        {
            txtBxScale.Text = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\" ^`'. ";
        }
        


        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenImage();
        }
        private void OpenImage()
        {
            openFileDialog1.Filter = "Supported Files (*.bmp, *.jpg, *.png, *.tiff)|*.bmp;*.jpg;*.png;*.tiff";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(openFileDialog1.FileName);
                filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

                if (ext == ".bmp" || ext == ".jpg" || ext == ".png" || ext == ".tiff")
                {
                    bit = new Bitmap(openFileDialog1.FileName);
                }
            }

        }

        private bool bmpToText()
        {
            int charH, charW;
            if (int.TryParse(txtBxCharH.Text, out charH) == false)
            {
                MessageBox.Show("The text boxes must contain numbers.");
                return false;
            }
            if (int.TryParse(txtBxCharW.Text, out charW) == false)
            {
                MessageBox.Show("The text boxes must contain numbers.");
                return false;
            }
            output = "";
            string greyscale = txtBxScale.Text;
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
            return true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void exportImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportAscii();
        }

        private void ExportAscii()
        {
            Stream stream;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FileName = filename + ".txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter sw;
                    if (bmpToText()) // call text converter
                    {
                        sw = new StreamWriter(stream);
                        sw.WriteLine(output);
                        sw.Close();
                    }
                    stream.Close();
                }
            }
            MessageBox.Show("Successfully Exported!");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenImage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExportAscii();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTextBoxes();
        }

        private void btnScaleShort_Click(object sender, EventArgs e)
        {
            SetScaleShort();
        }

        private void btnScaleLong_Click(object sender, EventArgs e)
        {
            SetScaleLong();
        }
    }
}
