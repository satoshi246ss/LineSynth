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
            temperature = (double)numericUpDownTemp.Value;
            ReadData();

            ReadAtomData("atom\\C_I.txt");
            ReadAtomData("atom\\N_I.txt");
            ReadAtomData("atom\\O_I.txt");
            ReadAtomData("atom\\Na_I.txt");
            ReadAtomData("atom\\Mg_I.txt");
            ReadAtomData("atom\\K_I.txt ");
            ReadAtomData("atom\\Ca_I.txt");
            ReadAtomData("atom\\Fe_I.txt");

            PlotSinCos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = checkedListBox1.FindString("Na");
            atomdata[11].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Mg");
            atomdata[12].Enabled = checkedListBox1.GetItemChecked(index);

            Cal_Line(12, (int)numericUpDownNumber.Value, (double)numericUpDownTemp.Value);
            PlotSinCos();
        }
    }
}
