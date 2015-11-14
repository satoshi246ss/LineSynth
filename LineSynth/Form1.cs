using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineSynth
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            //init
            for (int i = 0; i < 100; i++)
            {
                atomdata[i] = new Atom_Data();
            }
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadData();

            ReadAtomData("OI.txt", 8, 0);

            PlotSinCos();
        }
    }
}
