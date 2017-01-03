using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AsciiImageConverter;

namespace ascii_art
{
    public partial class Form1 : Form
    {
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
                filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            }

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
                    int charW, charH;
                    
                    if (int.TryParse(txtBxCharH.Text, out charH) == false)
                    {
                        MessageBox.Show("The text boxes must contain numbers.");
                    }
                    if (int.TryParse(txtBxCharW.Text, out charW) == false)
                    {
                        MessageBox.Show("The text boxes must contain numbers.");
                    }

                    StreamWriter sw;

                    // get the output string
                    output = AsciiConverter.ConvertAscii(stream, txtBxScale.Text, charH, charW);
                    sw = new StreamWriter(stream);
                    // write output;
                    sw.WriteLine(output);

                    // close stream and stream writer
                    sw.Close();
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

        private void txtBxScale_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
