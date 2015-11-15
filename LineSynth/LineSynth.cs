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

    class Atom_Data
    {
        public bool Enabled { get; set; }    // true:有効
        public double number { get; set; }   // 相対原子数
        public double e0 { get; set; }       // E0
        public int g0 { get; set; }          // g0
        public int atomic_number { get; set; }// 原子番号
        public int state { get; set; }       // 0:中性 1:１価　2:２価 ・・・
        public string name { get; set; }     // 名称 "NaI"等
        public List<Level_Data> leveldata = new List<Level_Data>();

        public Atom_Data()
        {
            e0 = 0.0;
            g0 = 1;
        }

        public void add(Level_Data d)
        {
            leveldata.Add(d);
        }
    }

    public partial class Form1 : Form
    {
        Atom_Data[] atomdata = new Atom_Data[100];
        Level_Data levdata = new Level_Data();
        List<PointF> obs_data = new List<PointF>();
        PointF pf;

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
                string line="", s;

                // 先頭1行基本データ取得
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
                int base_pos = line.IndexOf("|");
                while ((line = r.ReadLine()) != null) // 1行ずつ読み出し。
                {
                    // wavelenth
                    s = line.Substring(base_pos-11, 9);
                    if (double.TryParse(s, out d))
                    {
                        levdata.Wavelength = d;
                    }
                    // Aki
                    s = line.Substring(base_pos+10, 10);
                    if (double.TryParse(s, out d))
                    {
                        levdata.Aki = d;
                    }
                    // Ei
                    s = line.Substring(base_pos+31, 10);
                    if (double.TryParse(s, out d))
                    {
                        levdata.Ei = d;
                    }
                    // Ek
                    s = line.Substring(base_pos+49, 10);
                    if (double.TryParse(s, out d))
                    {
                        levdata.Ek = d;
                    }
                    // gi
                    s = line.Substring(base_pos+91, 2); //111
                    if (double.TryParse(s, out d))
                    {
                        levdata.gi = d;
                    }
                    // gk
                    s = line.Substring(base_pos+ 97, 2);//117
                    if (double.TryParse(s, out d))
                    {
                        levdata.gk = d;
                    }
                    atomdata[atomic_num].add(levdata);
                    w.WriteLine(atomdata[atomic_num].leveldata.Last.Print() );
                }
                foreach (Level_Data ld in atomdata[atomic_num].leveldata)
                {
                    w.WriteLine(ld.Print());   //richTextBox1.AppendText(line);
                }
                richTextBox1.AppendText(atomic_num.ToString() + ": " + atomdata[atomic_num].leveldata[0].Print() + "\n");
            }
        }

        private void PlotSinCos()
        {
            // 1.Seriesの追加
            chart1.Series.Clear();
            chart1.Series.Add("sin");
            chart1.Series.Add("cos");

            // 2.グラフのタイプの設定
            chart1.Series["sin"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["cos"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            // 3.座標の入力
            for (double theta = 0.0; theta <= 2 * Math.PI; theta += Math.PI / 360)
            {
                //chart1.Series["sin"].Points.AddXY(theta, Math.Sin(theta));
                chart1.Series["cos"].Points.AddXY(theta, Math.Cos(theta));
            }

            for (int i = 0; i < obs_data.Count; i++)
            {
                chart1.Series["sin"].Points.AddXY(obs_data[i].X, obs_data[i].Y);
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
