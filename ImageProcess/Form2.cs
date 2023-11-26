using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ImageProcess
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        Bitmap imageB, imageA, colorgreen, resultImage;
        Device[] devcs = DeviceManager.GetAllDevices();
        int camstart = 0;

        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = imageB;
        }

        private void btnLoadBG_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            devcs[0].Sendmessage();
            IDataObject clipboardData = Clipboard.GetDataObject();
            Image clipboardImage;

            clipboardImage = (Image)clipboardData.GetData(DataFormats.Bitmap);
            Bitmap bmpresult = (Bitmap)clipboardImage;


            SubtractWebCam(ref bmpresult, ref imageA);


        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            camstart = 1;
            devcs[0].ShowWindow(pictureBox1);
        }

        private void stopToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            camstart = 0;
            devcs[0].Stop();

            timer1.Stop();
        }


        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog2.FileName);
            pictureBox2.Image = imageA;
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            if (camstart == 1)
            {
                timer1.Start();
            } else
            {
                SubtractImg();
            }

        }

        private void SubtractImg()
        {
            Color mygreen = Color.FromArgb(0, 255, 0);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 20;


            resultImage = new Bitmap(imageB.Width, imageB.Height);


            for (int x = 0; x < imageB.Width; x++)
            {
                for (int y = 0; y < imageB.Height; y++)
                {
                    Color pixel = imageB.GetPixel(x, y);
                    Color backpixel = imageA.GetPixel(x, y);

                    int grey = (pixel.R + pixel.G + pixel.B) / 3;

                    int subtractvalue = Math.Abs(grey - greygreen);

                    Console.WriteLine("Subtractvalue = " + subtractvalue);

                    if (subtractvalue > threshold)
                    {
                        resultImage.SetPixel(x, y, pixel);
                    }
                    else
                    {
                        resultImage.SetPixel(x, y, backpixel);
                    }
                }
            }

            pictureBox3.Image = resultImage;
        }

        private void SubtractWebCam(ref Bitmap a, ref Bitmap b)
        {
            Color mygreen = Color.FromArgb(0, 0, 255);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 50;


            Bitmap camresultImage = new Bitmap(a.Width, a.Height);


            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    Color pixel = a.GetPixel(x, y);
                    Color backpixel = b.GetPixel(x, y);

                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractvalue = Math.Abs(grey - greygreen);

                    if (subtractvalue > threshold)
                    {
                        camresultImage.SetPixel(x, y, backpixel);
                    }
                    else
                    {
                        camresultImage.SetPixel(x, y, pixel);
                    }
                }
            }

            pictureBox3.Image = camresultImage;
        }

    }
    
}
