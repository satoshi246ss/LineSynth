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
    class Level_Data 
    {
        public double Wavelength; // Observed Wavelength  Air (nm)
        public double Aki;        // Aki s^-1
        public double Ei;         // (eV) Lower level
        public double Ek;         // (eV) Upper level
        public double gi;         // Lower level g
        public double gk;         // Upper level g

        public String Print()
        {
            return "wl:" + Wavelength.ToString() + " Aki:" + Aki.ToString() + " Ei:" + Ei.ToString() + " Ek:" + Ek.ToString() + " gi:" + gi.ToString() + " gk:" + gk.ToString();
        }
    }
    class Line_Data
    {
        public double Wavelength;    // Observed Wavelength  Air (nm)
        public double photon_number; // 光子数

        public String Print()
        {
            return "wl:" + Wavelength.ToString() + " photon:" + photon_number.ToString() ;
        }
    }

    class Atom_Data
    {
        public bool Enabled { get; set; }    // true:有効
        public double number { get; set; }   // 原子数
        public double temperature { get; set; }   // 温度[K]
        public double e0 { get; set; }       // E0
        public int g0 { get; set; }          // g0
        public int atomic_number { get; set; }// 原子番号
        public int state { get; set; }       // 0:中性 1:１価　2:２価 ・・・
        public string name { get; set; }     // 名称 "NaI"等
        public List<Level_Data> leveldata = new List<Level_Data>();
        public List<Line_Data> linedata = new List<Line_Data>();

        public Atom_Data()
        {
            e0 = 0.0;
            g0 = 1;
        }

        public void add(Level_Data d)
        {
            leveldata.Add(d);
        }
        public void add(Line_Data d)
        {
            linedata.Add(d);
        }

        // Line数 
        public int Count()
        {
            return leveldata.Count();
        }

        public void cal_photon()
        {
            if (Enabled)
            {
                double k  = 1.38064852e-23; //m2 kg s-2 K-1  // J/K
                double ev = 1.60218e-19; // 1eV = 1.6e-19 J
                for( int i=0 ; i< leveldata.Count(); i++)
                {
                    Level_Data ld = leveldata[i];
                    double e1 = -(ld.Ek - e0)*ev / (k * temperature);
                    double nn = number * (ld.gk / g0) * Math.Exp(e1);
                    linedata[i].photon_number = nn * ld.Aki;
                }
            }
        }
    }

    public partial class Form1 : Form
    {
        Atom_Data[] atomdata = new Atom_Data[100];
        Level_Data levdata1 = new Level_Data();
        List<PointF> obs_data = new List<PointF>();
        List<PointF> sim_data = new List<PointF>();
        double[] sim_base_data = new double[12000];

        PointF pf;
        double temperature ;
        double electron_density ;
        double k = 1.38064852e-23; //m2 kg s-2 K-1

        private void ReadData()
        {
            // ファイルからテキストを読み出し。
            using (StreamReader r = new StreamReader(@"20141122_0459_3_53pw.txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null) // 1行ずつ読み出し。
                {
                    // カンマ区切りで分割して配列に格納する
                    string[] stArrayData = line.Split(',');
                    pf.X = float.Parse(stArrayData[0]);
                    pf.Y = float.Parse(stArrayData[1]);
                    obs_data.Add(pf);
 
                    richTextBox1.AppendText(stArrayData[0] + stArrayData[1] +"\n");
                }
            }
        }
        private void Cal_Line(int atom_num, int num, double temp)
        {
            atomdata[atom_num].temperature = temp;
            atomdata[atom_num].number = num ;
            atomdata[atom_num].cal_photon();

            for (int i = 0; i < atomdata[atom_num].linedata.Count(); i++)
            {
                int wli = (int)(10 * atomdata[atom_num].linedata[i].Wavelength);
                sim_base_data[wli] = atomdata[atom_num].linedata[i].photon_number;
            }
        }

        private void Cal_All_Line(double temp)
        {
            //atomdata[atom_num].temperature = temp;
            //atomdata[atom_num].cal_photone();
        }

        // ファイルからテキストを読み出し。
        //          1         2         3         4         5         6         7         8         9         0         1         1         2 
        //01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890
        //-------------------------------------------------------------------------------------------------------------------------------
        //          Observed  |  Rel. |    Aki    | Acc. |         Ei           Ek         | Lower level | Upper level |  gi   gk  |Type|
        //         Wavelength |  Int. |    s^-1   |      |        (eV)         (eV)        |-------------|-------------|           |    |
        //          Air (nm)  |  (?)  |           |      |                                 | J           | J           |           |    |
        //-------------------------------------------------------------------------------------------------------------------------------
        //                    |       |           |      |                                 |             |             |           |    |
        //          477.175   |   200 | 7.97e+05  | C    |    7.487795    -    10.085369   | 2           | 2           |   5 - 5   |    |


        private void ReadAtomData(string fn)
        {

            using (StreamReader r = new StreamReader(fn))   //@"OI.txt"
            using (StreamWriter w = new StreamWriter(fn+".txt"))   //@"OI.txt"
            {
                double d;
                string line="", s, s1;

                // 先頭1行基本データ取得
                // E0[eV] g0 　原子番号　　中性・イオン　呼び名
                line = r.ReadLine();
                string[] stArrayData = line.Split(' ');

                int atomic_num = int.Parse(stArrayData[2]);
                int state = int.Parse(stArrayData[3]);

                atomdata[atomic_num].atomic_number = atomic_num;
                atomdata[atomic_num].state = state;
                atomdata[atomic_num].e0 = double.Parse(stArrayData[0]);
                atomdata[atomic_num].g0 = int.Parse(stArrayData[1]);
                atomdata[atomic_num].name = stArrayData[4];

                // 先頭6行スキップ
                for (int i = 0; i < 6; i++)
                {
                    line = r.ReadLine();
                    //richTextBox1.AppendText(line);
                }
                while ((line = r.ReadLine()) != null) // 1行ずつ読み出し。
                {
                    Level_Data levdata = new Level_Data();
                    Line_Data lid = new Line_Data();
                    string[] linesp = line.Split('|');
                    // wavelenth
                    if (double.TryParse(linesp[0], out d))
                    {
                        levdata.Wavelength = d;
                        lid.Wavelength = d;
                        lid.photon_number = 0;
                    }
                    else
                    {
                        continue;
                    }
                    // Aki
                    if (double.TryParse(linesp[2], out d))
                    {
                        levdata.Aki = d;
                    }
                    // Ei
                    string[] linesp2 = linesp[4].Split('-');
                    if (double.TryParse(linesp2[0], out d))
                    {
                        levdata.Ei = d;
                    }
                    // Ek
                    s = linesp2[1];
                    s1 = s.Replace("[", " ").Replace("]", " ");
                    if (double.TryParse(s1, out d))
                    {
                        levdata.Ek = d;
                    }
                    // gi
                    linesp2 = linesp[7].Split('-');
                    if (double.TryParse(linesp2[0], out d))
                    {
                        levdata.gi = d;
                    }
                    // gk
                    if (double.TryParse(linesp2[1], out d))
                    {
                        levdata.gk = d;
                    }
                    atomdata[atomic_num].add(levdata);
                    atomdata[atomic_num].add(lid);
                    w.WriteLine( atomdata[atomic_num].leveldata.Last().Print() ) ;
                }
                foreach (Level_Data ld in atomdata[atomic_num].leveldata)
                {
                    //w.WriteLine(ld.Print());   //richTextBox1.AppendText(line);
                }
                richTextBox1.AppendText(atomic_num.ToString() + ": " + atomdata[atomic_num].leveldata[0].Print() + "\n");
            }
        }

        private void ReadAtomDataAll()
        {
            ReadAtomData("atom\\C_I.txt"); //6
            ReadAtomData("atom\\N_I.txt"); //7
            ReadAtomData("atom\\O_I.txt"); //8
            ReadAtomData("atom\\Na_I.txt");//11
            ReadAtomData("atom\\Mg_I.txt");//12
            ReadAtomData("atom\\Al_I.txt");//13
            ReadAtomData("atom\\Si_I.txt");//14

            ReadAtomData("atom\\K_I.txt ");//19
            ReadAtomData("atom\\Ca_I.txt");//20

            ReadAtomData("atom\\Ti_I.txt");//22
            ReadAtomData("atom\\Cr_I.txt");//24
            ReadAtomData("atom\\Mn_I.txt");//25
            ReadAtomData("atom\\Fe_I.txt");//26
            ReadAtomData("atom\\Co_I.txt");//27
            ReadAtomData("atom\\Ni_I.txt");//28

            //太陽系存在比
            numericUpDown_C.Value = (decimal)1.21e7;
            numericUpDown_N.Value = (decimal)2.48e6;
            numericUpDown_O.Value = (decimal)2.01e7;
            numericUpDown_Na.Value = (decimal)5.70e4;
            numericUpDown_Mg.Value = (decimal)1.075e6;
            numericUpDown_Al.Value = (decimal)8.49e4;
            numericUpDown_Si.Value = (decimal)1.00e6;
            numericUpDown_K.Value = (decimal)3770;
            numericUpDown_Ca.Value = (decimal)6.11e4;
            numericUpDown_Ti.Value = (decimal)2400;
            numericUpDown_Cr.Value = (decimal)1.34e4;
            numericUpDown_Mn.Value = (decimal)9510;
            numericUpDown_Fe.Value = (decimal)9.00e5;
            numericUpDown_Co.Value = (decimal)2250;
            numericUpDown_Ni.Value = (decimal)4.93e4;
        }
        private void checkedList()
        {
            int index ;
            index = checkedListBox1.FindString("C");
            atomdata[ 6].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("N");
            atomdata[ 7].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("O");
            atomdata[ 8].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Na");
            atomdata[11].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Mg");
            atomdata[12].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Al");
            atomdata[13].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Si");
            atomdata[14].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("K");
            atomdata[19].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Ca");
            atomdata[20].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Ti");
            atomdata[23].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Cr");
            atomdata[24].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Mn");
            atomdata[25].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Fe");
            atomdata[26].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Co");
            atomdata[27].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Ni");
            atomdata[28].Enabled = checkedListBox1.GetItemChecked(index);
        }

        private void PlotSinCos()
        {
            // 1.Seriesの追加
            chart1.Series.Clear();
            chart1.Series.Add("sin");
            chart1.Series.Add("sim");

            // 2.グラフのタイプの設定
            chart1.Series["sin"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["sim"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            // 3.座標の入力
            for (double theta = 0.0; theta <= 2 * Math.PI; theta += Math.PI / 360)
            {
                //chart1.Series["sin"].Points.AddXY(theta, Math.Sin(theta));
                chart1.Series["sim"].Points.AddXY(theta, Math.Cos(theta));
            }

            for (int i = 0; i < obs_data.Count; i++)
            {
                chart1.Series["sin"].Points.AddXY(obs_data[i].X, obs_data[i].Y);
            }

            for (int i = 3000; i < 12000 ; i++)
            {
                chart1.Series["sim"].Points.AddXY(i/10.0, sim_base_data[i]);
            }

            //軸ラベルの設定
            string s = chart1.Series["sin"].ChartArea;//
            chart1.ChartAreas[s].AxisX.Title = "WaveLenght(nm)";
            chart1.ChartAreas[s].AxisY.Title = "Relative Value";
            //※Axis.TitleFontでフォントも指定できるがこれはデザイナで変更したほうが楽

            //X軸最小値、最大値、目盛間隔の設定
            chart1.ChartAreas[s].AxisX.Minimum = 300;
            chart1.ChartAreas[s].AxisX.Maximum = 1200;
            chart1.ChartAreas[s].AxisX.Interval = 50;

            //Y軸最小値、最大値、目盛間隔の設定
            //chart1.ChartAreas["area1"].AxisY.Minimum = -1;
            //chart1.ChartAreas["area1"].AxisY.Maximum = 1;
            //chart1.ChartAreas["area1"].AxisY.Interval = 0.2;

        }


    }
}
