﻿using System;
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
            for (int i = 0; i < Max_Atom_Num; i++)
            {
                atomdata[i] = new Atom_Data();
            }
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            temperature      = (double)numericUpDownTemp.Value;
            electron_density = (double)numericUpDown_DensE.Value * Math.Pow(10,(double)numericUpDown_DensE_exp.Value);
            ReadData();
            ReadAtomDataAll();
            ReadMolecularDataAll();
    
            PlotSinCos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            temperature = (double)numericUpDownTemp.Value;
            electron_density = (double)numericUpDown_DensE.Value * Math.Pow(10, (double)numericUpDown_DensE_exp.Value);
            checkedList();
            cal_line_all();

            PlotSinCos();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            button2_Click(sender, e);
        }
    }
}
