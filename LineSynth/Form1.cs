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
        PointF[] obs_data = new PointF[2000] ;
        int obs_data_max = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void ReadData()
        {
            // ファイルからテキストを読み出し。
            using (StreamReader r = new StreamReader(@"20141122_0459_3_53pw.txt"))
            {
                int i=0;
                string line;
                while ((line = r.ReadLine()) != null) // 1行ずつ読み出し。
                {
                    // カンマ区切りで分割して配列に格納する
                    string[] stArrayData = line.Split(',');
                    obs_data[i].X = float.Parse(stArrayData[0]);
                    obs_data[i].Y = float.Parse(stArrayData[1]);
                    i++;

                    richTextBox1.AppendText(stArrayData[0]+stArrayData[1]);
                }
                obs_data_max = i;
            }
             
            // ファイルからテキストを読み出し。
            using (StreamReader r = new StreamReader(@"OI.txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null) // 1行ずつ読み出し。
                {
                    richTextBox1.AppendText( line ) ;
                }
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

            for (int i = 0; i < obs_data_max; i++)
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

        private void button1_Click(object sender, EventArgs e)
        {
            ReadData();

            PlotSinCos();
        }
    }
}
