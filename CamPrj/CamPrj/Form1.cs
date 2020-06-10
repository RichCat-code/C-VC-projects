using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CamPrj
{
    public partial class Form1 : Form
    {
        /*Setting up an stopwatch class, as to not overload the Arduino Uno with information sent to it.*/
        /* In a nutshell, make the arduino and UWP program have better responsetime*/
        public Stopwatch stp_wch { get; set; }
        public Form1()
        {
            /*This works for initialising the form. To get the screen up.*/
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*We give a variable("stp_wch") a value of the stopwatch command which let's us start the stop watch
             as soon as the port is opened.*/
            stp_wch = Stopwatch.StartNew();
            Port.Open();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            /*This is going to make us able to interpret coordinates on the form app and make sure that the data is sent
             correctly.*/
            portwrite(new Point(e.X, e.Y));
        }

        public void portwrite(Point coordinates)
        {
            /*This function makes sure that data is checked every 15 milliseconds and sends the coordinates of our mouse to
             The Arduino*/
            if (stp_wch.ElapsedMilliseconds > 15)
            {
                stp_wch = Stopwatch.StartNew();

                /*This will calculate how the servos will interpret the data we send over and act accordingly.*/
                /*It takes the coordinates of our mouse and divides that with the sum of the width and height of the form
                 with 180 (degrees of how much an Servo can rotate.*/
                Port.Write(String.Format("X{0}Y{1}",
                (coordinates.X / (Size.Width / 180)),
                (coordinates.Y / (Size.Height / 180))));
            }
        }
    }
}
