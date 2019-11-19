using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //Eshte butoni me ane te cilit ne mund te marrim foton ne te cilen duam te fshehim tekstin
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
           
            
                //Hapim dritaren ku do ta marrim File-in
                OpenFileDialog hapjaEDialogut = new OpenFileDialog();

                //Me kete kemi specifkuar tipin e file-it
                hapjaEDialogut.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                //Specifikimi i path-it ku ndodhet fajlli me te cilin duam te manipulojme
                hapjaEDialogut.InitialDirectory = @"C:\Users\Twin\Desktop";

                //Vendosja e path-it te fajllit
                if (hapjaEDialogut.ShowDialog() == DialogResult.OK)
                {
                    textBoxFilePath.Text = hapjaEDialogut.FileName.ToString();
                    pictureBox1.ImageLocation = textBoxFilePath.Text;
                }
            }

         //Krijimi i butonit per Enkodim
        private void btnEncode_Click(object sender, EventArgs e)
        {
            //Krijimi i nje insance te klases Bitmap per te manipuluar me fotot qe jane te definuara ne pixels
            Bitmap imazhi = new Bitmap(textBoxFilePath.Text);

            //Me ane te unazave po pershkojme Width&Height te file-it
            for (int i = 0; i < imazhi.Width; i++)
            {
                for (int j = 0; j < imazhi.Height; j++)
                {
                    //Imazhit te mapuar po i marrim secilin pixel
                    Color pixel = imazhi.GetPixel(i, j);

                    //Shfaqja e ngjyrave RGB per secilin pixel
                    if (i < 1 && j < textBoxMessage.TextLength)
                    {
                        Console.WriteLine(" R= [" + i + "][" + j + "] = " + pixel.R);
                        Console.WriteLine(" G= [" + i + "][" + j + "] = " + pixel.G);
                        Console.WriteLine(" B= [" + i + "][" + j + "] = " + pixel.B);

                        //Konvertimi me qellim te shfaqjes ne console te shkronjes perkatese
                        char shkronja = Convert.ToChar(textBoxMessage.Text.Substring(j, 1));
                        int vleraEKonvertuar = Convert.ToInt32(shkronja);
                        Console.WriteLine("shkronja : " + shkronja + " vleraEKonvertuar : " + vleraEKonvertuar);

                        imazhi.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, vleraEKonvertuar));

                    }

                    if (i == imazhi.Width - 1 && j == imazhi.Height - 1)
                    {
                        imazhi.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, textBoxMessage.TextLength));
                    }
                }
            }

            //Krijohet mundesia per ruajtjen e File-it
            SaveFileDialog ruajtjaEFajllit = new SaveFileDialog();

            ruajtjaEFajllit.Filter = "Image Files (*.png, *.jpg) | *.png; *.jpg";
            ruajtjaEFajllit.InitialDirectory = @"C:\Users\Twin\Desktop";

            if (ruajtjaEFajllit.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath.Text = ruajtjaEFajllit.FileName.ToString();
                pictureBox1.ImageLocation = textBoxFilePath.Text;
                imazhi.Save(textBoxFilePath.Text);
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            Bitmap imazhi = new Bitmap(textBoxFilePath.Text);
            string tekstiFshehur = "";

            //Ngjashem kalojme neper secilin pixel te imazhit
            Color lastpixel = imazhi.GetPixel(imazhi.Width - 1, imazhi.Height - 1);
            int msgLength = lastpixel.B;

            for (int i = 0; i < imazhi.Width; i++)
            {
                for (int j = 0; j < imazhi.Height; j++)
                {
                    Color pixel = imazhi.GetPixel(i, j);

                    if (i < 1 && j < msgLength)
                    {
                        int value = pixel.B;
                        char c = Convert.ToChar(value);
                        string shkronja = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });
                        tekstiFshehur = tekstiFshehur + shkronja;
                    }
                }
            }
            textBoxMessage.Text = tekstiFshehur;
        }
    
}
    }
    

