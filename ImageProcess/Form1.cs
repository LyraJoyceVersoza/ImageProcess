using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap loaded,processed;


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            processed.Save(saveFileDialog1.FileName);
        }

        private void invertColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, Color.FromArgb(255-pixel.R,255-pixel.G, 255-pixel.B));
                }
            }

            pictureBox2.Image = processed;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            int grey;
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    grey = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    processed.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            }

            pictureBox2.Image = processed;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            Color gray;
            Byte graydata;

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    graydata = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    gray = Color.FromArgb(graydata, graydata, graydata);
                    loaded.SetPixel(x, y, gray);
                }
            }

            int[] histdata = new int[256]; 
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    histdata[pixel.R]++; 
                }
            }

            processed = new Bitmap(256, 800);
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 800; y++)
                {
                    processed.SetPixel(x, y, Color.White);
                }
            }


            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < Math.Min(histdata[x] / 5, processed.Height - 1); y++)
                {
                    processed.SetPixel(x, (processed.Height - 1) - y, Color.Black);
                }
            }

            pictureBox2.Image = processed;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            int sepR,sepG,sepB;
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);

                    int R = pixel.R;
                    int G = pixel.G;
                    int B = pixel.B;

                    sepR = (int)(0.393 * R + 0.769 * G + 0.189 * B);
                    sepG = (int)(0.349 * R + 0.686 * G + 0.168 * B);
                    sepB = (int)(0.272 * R + 0.534 * G + 0.131 * B);

                    if (sepR > 255)
                    {
                        R = 255;
                    } else
                    {
                        R = sepR;
                    }

                    if (sepG > 255)
                    {
                        G = 255;
                    } else
                    {
                        G = sepG;
                    }

                    if (sepB > 255)
                    {
                        B = 255;
                    } else
                    {
                        B = sepB;
                    }

                    processed.SetPixel(x, y,Color.FromArgb(R,G,B));
                }
            }
            pictureBox2.Image = processed;
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void goToSubtractOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
            this.Hide();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);

            for(int x = 0; x < loaded.Width; x++)
            {
                for(int y = 0; y < loaded.Height; y++) {
                    pixel = loaded.GetPixel(x,y);
                    processed.SetPixel(x,y,pixel);
                }
            }

            pictureBox2.Image = processed;
        }
    }
}
