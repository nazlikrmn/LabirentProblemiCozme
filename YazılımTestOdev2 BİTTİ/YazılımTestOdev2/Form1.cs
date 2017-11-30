using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YazılımTestOdev2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        Button b;
        int sayi;
        public Image image;
        int sayac1 = 0;
        int sayac2 = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        bool[,] dizi;
        Button[,] butonDizisi;
        int sayi1Max, sayi2Max;
        private void btnCiz_Click(object sender, EventArgs e)
        {
            int sayi1=10, sayi2=10;
            dizi = new bool[sayi1, sayi2];
            butonDizisi = new Button[sayi1, sayi2];
            int i,j;
            for(i=0;i<sayi1;i++)
            {
                for(j=0;j<sayi2;j++)
                {
                    dizi[i, j] = true ;
                }
            }
            sayi1Max = sayi1;
            sayi2Max = sayi2;
            Ciz(sayi1, sayi2);
        }
        private void Ciz(int Sayi1, int Sayi2)
        {
            int i, j;
            for (i = 0; i < Sayi1; i++)
            {
                for (j = 0; j < Sayi2; j++)
                {
                    if (i == 0)
                        dizi[i, j] = false;
                    else if (j == 0)
                        dizi[i, j] = false;
                    else if (j == Sayi2 - 1)
                        dizi[i, j] = false;
                    else if (i == Sayi1 - 1)
                        dizi[i, j] = false;
                }
            }
            int Point1 = 0, Point2 = 0;
            for ( i = 0; i < Sayi1; i++)
            {
                for (j = 0; j < Sayi2; j++)
                {
                    b = new Button();
                    
                  
                    if (dizi[i, j] == false)
                    {
                        b.Image = Resource1.duvar;
                    }
                    else
                    {
                        b.Text = "";
                        b.BackColor = Color.Transparent;
                    }

                    b.FlatStyle = FlatStyle.Flat;
                    b.Top = Point2;
                    b.Left = Point1;
                    b.Height = 50;
                    b.Width = 50;
                    b.Tag = i + "," + j;
             
                    // b.Click += btnDuvar_Click;

                    b.Click += new EventHandler(dinamikMetod);
                    /*if (image == Resource1.fare)
                        b.Click += btnFare_Click;
                    else if (image == Resource1.peynir)
                        b.Click += btnPeynir_Click;
                    else*/
                   
                    pnlIcerik.Controls.Add(b);
                    butonDizisi[i, j] = b;
                    if (j == Sayi2 - 1)
                    {
                        Point1 = 0;
                        Point2 += 50;
                    }
                    else
                    {
                        Point1 += 50;
                    }
                }
            }


                /* b.Click += new EventHandler(dinamikMetod);
                   if (image == Resource1.fare)
                       b.Click += btnFare_Click;
                   else if (image == Resource1.peynir)
                       b.Click += btnPeynir_Click;
                   else
                       b.Click += btnDuvar_Click;
                   pnlIcerik.Controls.Add(b);*/
        }
        public Point startLocation;
        public Point endLocation;
        public void dinamikMetod(object sender,EventArgs e)
        {
            Button buton = (sender as Button);

            /*  if (image.Flags == Resource1.fare.Flags)
                 buton.Image=image;
            else if (image.Flags == Resource1.peynir.Flags)
                 buton.Image = image;
              else
                  buton.Image = Resource1.duvar;*/
            Point tıklanan=new Point();
            for (int i = 0; i < sayi1Max; i++)
            {
                for (int j = 0; j < sayi2Max; j++)
                {
                    if (butonDizisi[i, j] == buton)
                    {
                        tıklanan.X = i;
                        tıklanan.Y = j;
                    }
                }
            }
            if (sayi==2)
            {
                buton.Image = image;
                startLocation = tıklanan;
            }
           else if (sayi==1)
            {
                buton.Image = image;
                endLocation = tıklanan;
            }
            else
            {
                buton.Image = Resource1.duvar;
                dizi[tıklanan.X, tıklanan.Y] = false;
            }

            sayi = 0;

        }
       /* private void btnDuvar_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] bol = new string[2];
            bol = b.Tag.ToString().Split(',');
            b.Image = Resource1.duvar;
            b.Text = "";
            dizi[Convert.ToInt32(bol[0]), Convert.ToInt32(bol[1])] = -1;
        }*/

        private void btnFare_Click(object sender, EventArgs e)
        {
            
           
            if (sayac1 == 0)
            {
                //Button b = sender as Button;
                //string[] bol = new string[2];
                image = Resource1.fare;
                sayi = 2;
                //b.Click += new EventHandler(dinamikMetod);
                b.Text = "";
                sayac1++;
            }
            else
                MessageBox.Show("1'den fazla fare yerleştirilemez");
        }

        private void btnPeynir_Click(object sender, EventArgs e)
        {
            
           
            if (sayac2 == 0)
            {
               /* Button b = sender as Button;
                string[] bol = new string[2];*/
                image = Resource1.peynir;
                sayi = 1;
                b.Text = "";
                sayac2++;
            }
            else
                MessageBox.Show("1'den fazla peynir yerleştirilemez");
        }
        private SearchParameters searchParameters;
        private void btnBasla_Click(object sender, EventArgs e)
        {
            this.searchParameters = new SearchParameters(startLocation, endLocation, dizi);
            pathFinder PathFinder = new pathFinder(searchParameters);
            List<Point> path = PathFinder.FindPath();

            btnCiz.Enabled = false;
            btnBasla.Enabled = false;
            btnFare.Enabled = false;
            btnPeynir.Enabled = false;
            Hareket(path);
                       
        }
        private void Hareket(List<Point> yol)
        {
            foreach (Point point in yol)
            {
                butonDizisi[point.X, point.Y].Image = Resource1.images;
                if (point == endLocation)
                    MessageBox.Show("Mission Completed");
            }
        }
        
        
    }
}
