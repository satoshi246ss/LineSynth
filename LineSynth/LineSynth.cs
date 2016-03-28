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
using System.Windows.Forms.DataVisualization.Charting;

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

    class EnergyLevel
    {
        public int gi { get; set; }
        public double Ei { get; set; }       // (eV) Level
        public String Print()
        {
            return "gi:" + gi.ToString() + " Level:" + Ei.ToString();
        }
    }

    class Atom_Data
    {
        const double k  = 1.38064852e-23; //ボルツマン定数[J/K]
        const double ev = 1.60217733e-19; //電子ボルト[J]    1eV = 1.6e-19 J
        const double h  = 6.62607004e-34; //プランク定数[Js] 4.135667662e-15;//[eVs]
        const double me = 9.10938188e-31; //電子質量[kg]
  
        public bool   Enabled { get; set; }          // true:有効
        public double number { get; set; }           // 原子数
        public double temperature  { get; set; }     // 温度[K]
        public double electron_dens{ get; set; }     // 電子密度[m-3]
        public double ionization_energy { get; set; }// イオン化エネルギー[eV]
        public double ni_na { get; set; }    // Ni/Na イオン、原子の比率
        public double e0 { get; set; }       // E0
        public int g0 { get; set; }          // g0
        public double Zi { get; set; }       // 分配関数 
        public double Zii { get; set; }      // 1価分配関数 
        public int atomic_number { get; set;}// 原子番号
        public int state { get; set; }       // 0:中性 1:１価　2:２価 ・・・
        public string name { get; set; }     // 名称 "NaI"等
        public double Be { get; set; }       // Be:分子回転定数
        public List<Level_Data> leveldata = new List<Level_Data>();
        public List<Line_Data>   linedata       = new List<Line_Data>();
        public List<EnergyLevel> energy_level_1 = new List<EnergyLevel>();
        // イオン用
        public List<Level_Data > leveldata_2 = new List<Level_Data>();
        public List<Line_Data  > linedata_2  = new List<Line_Data>();
        public List<EnergyLevel> energy_level_2 = new List<EnergyLevel>();

        public double ionization_rate()
        {
            return (1.0 / (1 + (1 / ni_na)));
        }
        public double atom_rate()
        {
            return (1.0 - ionization_rate());
        }

        public Atom_Data()
        {
            e0 = 0.0;
            g0 = 1;
        }
        // 中性
        public void add(Level_Data d)
        {
            leveldata.Add(d);
        }
        public void add(Line_Data d)
        {
            linedata.Add(d);
        }
        public void add_1(EnergyLevel d)
        {
            energy_level_1.Add(d);
        }
        // １価イオン
        public void add_2(Level_Data d)
        {
            leveldata_2.Add(d);
        }
        public void add_2(Line_Data d)
        {
            linedata_2.Add(d);
        }
        public void add_2(EnergyLevel d)
        {
            energy_level_2.Add(d);
        }

        // Line数 
        public int Count()
        {
            return leveldata.Count();
        }

        // 分配関数の計算
        // 1[eV] = 1.16045x10^4[K]
        public double cal_PartionFunction()
        {
            Zi = 0;
            foreach (EnergyLevel ld in energy_level_1)
            {
                double e1 = -(ld.Ei * ev) / (k * temperature);
                Zi += ld.gi * Math.Exp(e1);
            }
            Zii = 0;
            foreach (EnergyLevel ld in energy_level_2)
            {
                double e1 = -(ld.Ei * ev) / (k * temperature);
                Zii += ld.gi * Math.Exp(e1);
            }
            return Zi;
        }
 
        // イオン化率の計算
        public double cal_ionizationRate()
        {
            ni_na = 0;
            if (Zii > 0)
            {
                //double saha = 
                double a = Math.Pow(2 * Math.PI * me * k * temperature / (h * h), 1.5);
                double b = Math.Exp(-ionization_energy * ev / (k * temperature));
                ni_na = 2 * Math.Pow(2 * Math.PI * me * k * temperature/(h * h), 1.5)  * (Zii / Zi) * Math.Exp(-ionization_energy * ev / (k * temperature)) / electron_dens;
            }
            return ni_na;
        }

        // ライン毎のフォトン数計算
        public void cal_photon()
        {
            if (Enabled)
            {
                // 中性
                for (int i = 0; i < leveldata.Count(); i++)
                {
                    Level_Data ld = leveldata[i];
                    double e1 = -(ld.Ek - 0.0) * ev / (k * temperature);
                    double nn = number * (ld.gk / Zi) * Math.Exp(e1);
                    if (atomic_number < 200)
                    {
                        // 単原子
                        linedata[i].photon_number = nn * ld.Aki;
                    }
                    else
                    {
                        // 分子
                        double photon_number = nn * ld.Aki;

                        linedata[i].photon_number = nn * ld.Aki;

                    }
                }
                // 1価イオン
                for(int i = 0; i < leveldata_2.Count(); i++)
                {
                    Level_Data ld = leveldata_2[i];
                    double e1 = -(ld.Ek - 0.0) * ev / (k * temperature);
                    double nn = number * (ld.gk / Zii) * Math.Exp(e1);
                    linedata_2[i].photon_number = nn * ld.Aki;
                }
            }
        }
    }
    //
    //
    //
    public partial class Form1 : Form
    {
        const int Max_Atom_Num= 300;
        const int Sim_max_bin = 12000;
        const int Sim_wl_step = 100; //  1 unit = 0.001nm
        Atom_Data[] atomdata = new Atom_Data[Max_Atom_Num];
        Molecular_band_system N2_1st = new Molecular_band_system();
        Level_Data levdata1 = new Level_Data();
        List<PointF> obs_data = new List<PointF>();
        List<PointF> sim_data = new List<PointF>();
        double[] sim_base_data = new double[Sim_max_bin];

        PointF pf;
        double temperature ;
        double electron_density ;
        //double k = 1.38064852e-23; //m2 kg s-2 K-1

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
        private void Cal_Line(int atom_num, double num, double temp, double dens_e=-1.0)
        {
            atomdata[atom_num].temperature = temp;
            atomdata[atom_num].number = num;
            atomdata[atom_num].electron_dens = dens_e;
            atomdata[atom_num].cal_PartionFunction();//cal Z,Zii
            atomdata[atom_num].cal_ionizationRate();
            label_ionizationRate.Text = (atomdata[atom_num].ionization_rate()).ToString();
            atomdata[atom_num].cal_photon();
            // 中性
            for (int i = 0; i < atomdata[atom_num].linedata.Count(); i++)
            {
                int wli = (int)((1000 / Sim_wl_step) * atomdata[atom_num].linedata[i].Wavelength);
                if (wli < Sim_max_bin)
                {
                    sim_base_data[wli] += atomdata[atom_num].linedata[i].photon_number * atomdata[atom_num].atom_rate() ;
                }
            }
            // 1価イオン
            for (int i = 0; i < atomdata[atom_num].linedata_2.Count(); i++)
            {
                int wli = (int)((1000 / Sim_wl_step) * atomdata[atom_num].linedata_2[i].Wavelength);
                if (wli < Sim_max_bin)
                {
                    sim_base_data[wli] += atomdata[atom_num].linedata_2[i].photon_number * atomdata[atom_num].ionization_rate() ;
                }
            }
        }

        // ビンをクリア
        private void Clear_Line()
        {
            for (int i = 0; i < Sim_max_bin; i++)
            {
                sim_base_data[i] = 0 ;
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

                    if (state == 0)
                    {
                        atomdata[atomic_num].add(levdata);
                        atomdata[atomic_num].add(lid);
                        w.WriteLine(atomdata[atomic_num].leveldata.Last().Print());
                    }
                    else if (state == 1)
                    {
                        atomdata[atomic_num].ionization_energy = double.Parse(stArrayData[0]);
                        atomdata[atomic_num].add_2(levdata);
                        atomdata[atomic_num].add_2(lid);
                        w.WriteLine(atomdata[atomic_num].leveldata_2.Last().Print());
                    }
                }
                foreach (Level_Data ld in atomdata[atomic_num].leveldata)
                {
                    //w.WriteLine(ld.Print());   //richTextBox1.AppendText(line);
                }
                richTextBox1.AppendText(atomic_num.ToString() + ": " + atomdata[atomic_num].leveldata[0].Print() + "\n");
            }
        }

        // ファイルからテキストを読み出し。
        //          1         2         3         4         5         6         7         8         9         0         1         1         2 
        //01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890
        //-------------------------------------------------------------------------------
        //Configuration          | Term   |   J |                   Level    | Reference
        //-----------------------|--------|-----|----------------------------|-----------
        //                       |        |     |                            |           
        //2s2.2p4                | 3P     |   2 |              0.000000      |     L7288
        //                       |        |   1 |              0.0196224     |          
        //                       |        |   0 |              0.0281416     |          
        //                       |        |     |                            |           
        //2s2.2p4                | 1D     |   2 |              1.9673641     |          
        //-----------------------|--------|-----|----------------------------|----------- ///END
        //                       |        |     |                            |           
        //2s2.2p4                | 1D     |   2 |              1.9673641     |          
 
        private void ReadEnergyLevel(string fn, int atomic_num, int state)
        {

            using (StreamReader r = new StreamReader(fn))            //@"O_ii_level.txt"
            using (StreamWriter w = new StreamWriter(fn + ".txt"))   //@"O_ii_level.txt.txt"
            {
                double d;
                string line = "", s;

                string[] stArrayData = line.Split(' ');

                // 先頭4行スキップ
                for (int i = 0; i < 4; i++)
                {
                    line = r.ReadLine();
                    //richTextBox1.AppendText(line);
                }
                while ((line = r.ReadLine()) != null && line.IndexOf("---") < 0 ) // 1行ずつ読み出し。
                {
                    EnergyLevel el = new EnergyLevel();
                    string[] linesp = line.Replace("[", " ").Replace("]", " ").Replace("?", " ").Split('|');
                    // energy level
                    s = linesp[3];
                    //s1 = s.Replace("[", " ").Replace("]", " ");
                    if (double.TryParse(s, out d))
                    {
                        el.Ei = d;
                    }
                    else
                    {
                        continue;
                    }
                    // gi
                    if (linesp[2].IndexOf("/") >= 0)
                    {
                        // 分数の場合
                        string[] linesp1 = linesp[2].Split('/');
                        int a1, a2;
                        int.TryParse(linesp1[0], out a1);
                        int.TryParse(linesp1[1], out a2);
                        d = 1 + 2.0 * (double)a1 / (double)a2;
                    }
                    else
                    {
                        if (double.TryParse(linesp[2], out d))
                        {
                            d = (int)(d * 2 + 1);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    el.gi = (int)d;
                    // 
                    if (state == 0)
                    {
                        atomdata[atomic_num].add_1(el);
                    }
                    else if (state == 1)
                    {
                        atomdata[atomic_num].add_2(el);
                    }

                    w.WriteLine(el.Print());   //richTextBox1.AppendText(line);
                }
                foreach ( EnergyLevel el1 in atomdata[atomic_num].energy_level_1 )
                {
                //    w.WriteLine(el1.Print());   //richTextBox1.AppendText(line);
                }
                richTextBox1.AppendText(atomic_num.ToString() + ": " + atomdata[atomic_num].leveldata[0].Print() + "\n");
            }
        }

        private void ReadMolecularDataAll()
        {
            N2_1st.ReadAkiData("atom\\N2_1st_pos_Aki_21_21.txt");
            N2_1st.make_level_data_N2("atom\\Molecular");
            N2_1st.make_Aki_data_N2("atom\\Molecular", temperature );
            ReadAtomData("atom\\Molecular_Aki_N2.txt");  // 200 N2 First Positive
            ReadEnergyLevel("atom\\Molecular_N2.txt", 200, 0);
        }

        private void ReadAtomDataAll()
        {            
            ReadAtomData("atom\\H_I.txt");  // 1
            ReadEnergyLevel("atom\\H_I_Level.txt", 1, 0);
            // 3 Li
            ReadAtomData("atom\\Li_I.txt"); 
            ReadAtomData("atom\\Li_II.txt");
            ReadEnergyLevel("atom\\Li_I_Level.txt",  3, 0);
            ReadEnergyLevel("atom\\Li_II_Level.txt", 3, 1);
            // 6 C
            ReadAtomData("atom\\C_I.txt");
            ReadAtomData("atom\\C_II.txt");
            ReadEnergyLevel("atom\\C_I_Level.txt",  6, 0);
            ReadEnergyLevel("atom\\C_II_Level.txt", 6, 1);

            ReadAtomData("atom\\N_I.txt");  // 7
            ReadAtomData("atom\\N_II.txt"); // 7
            ReadEnergyLevel("atom\\N_I_Level.txt",  7, 0); //7
            ReadEnergyLevel("atom\\N_II_Level.txt", 7, 1); //7

            ReadAtomData("atom\\O_I.txt");  // 8
            ReadAtomData("atom\\O_II.txt"); // 8
            ReadEnergyLevel("atom\\O_I_Level.txt",  8, 0); //8
            ReadEnergyLevel("atom\\O_II_Level.txt", 8, 1); //8

            ReadAtomData("atom\\Na_I.txt"); //11
            ReadAtomData("atom\\Na_II.txt");//11
            ReadEnergyLevel("atom\\Na_I_Level.txt",  11, 0);//11
            ReadEnergyLevel("atom\\Na_II_Level.txt", 11, 1);//11

            ReadAtomData("atom\\Mg_I.txt"); //12
            ReadAtomData("atom\\Mg_II.txt");//12
            ReadEnergyLevel("atom\\Mg_I_Level.txt" , 12, 0);//12
            ReadEnergyLevel("atom\\Mg_II_Level.txt", 12, 1);//12

            ReadAtomData("atom\\Al_I.txt"); //13
            ReadAtomData("atom\\Al_II.txt");//13
            ReadEnergyLevel("atom\\Al_I_Level.txt" , 13, 0);//13
            ReadEnergyLevel("atom\\Al_II_Level.txt", 13, 1);//13

            ReadAtomData("atom\\Si_I.txt"); //14
            ReadAtomData("atom\\Si_II.txt");//14
            ReadEnergyLevel("atom\\Si_I_Level.txt" , 14, 0);//14
            ReadEnergyLevel("atom\\Si_II_Level.txt", 14, 1);//14

            ReadAtomData("atom\\K_I.txt "); //19
            ReadAtomData("atom\\K_II.txt ");//19
            ReadEnergyLevel("atom\\K_I_Level.txt" ,  19, 0);//19
            ReadEnergyLevel("atom\\K_II_Level.txt",  19, 1);//19

            ReadAtomData("atom\\Ca_I.txt"); //20
            ReadAtomData("atom\\Ca_II.txt");//20
            ReadEnergyLevel("atom\\Ca_I_Level.txt" , 20, 0);//20
            ReadEnergyLevel("atom\\Ca_II_Level.txt", 20, 1);//20

            ReadAtomData("atom\\Ti_I.txt"); //22
            ReadAtomData("atom\\Ti_II.txt");//22
            ReadEnergyLevel("atom\\Ti_I_Level.txt",  22, 0);//22
            ReadEnergyLevel("atom\\Ti_II_Level.txt", 22, 1);//22

            ReadAtomData("atom\\Cr_I.txt"); //24
            ReadAtomData("atom\\Cr_II.txt");//24
            ReadEnergyLevel("atom\\Cr_I_Level.txt",  24, 0);//24
            ReadEnergyLevel("atom\\Cr_II_Level.txt", 24, 1);//24

            ReadAtomData("atom\\Mn_I.txt"); //25
            ReadAtomData("atom\\Mn_II.txt");//25
            ReadEnergyLevel("atom\\Mn_I_Level.txt",  25, 0);//25
            ReadEnergyLevel("atom\\Mn_II_Level.txt", 25, 1);//25

            ReadAtomData("atom\\Fe_I.txt"); //26
            ReadAtomData("atom\\Fe_II.txt");//26
            ReadEnergyLevel("atom\\Fe_I_Level.txt",  26, 0);//26
            ReadEnergyLevel("atom\\Fe_II_Level.txt", 26, 1);//26

            ReadAtomData("atom\\Co_I.txt"); //27
            ReadAtomData("atom\\Co_II.txt");//27
            ReadEnergyLevel("atom\\Co_I_Level.txt",  27, 0);//27
            ReadEnergyLevel("atom\\Co_II_Level.txt", 27, 1);//27

            ReadAtomData("atom\\Ni_I.txt");  //28
            ReadAtomData("atom\\Ni_II.txt"); //28
            ReadEnergyLevel("atom\\Ni_I_Level.txt",  28, 0);//28
            ReadEnergyLevel("atom\\Ni_II_Level.txt", 28, 1);//28

            //太陽系存在比 Anders & Ebihara(1982)
            numericUpDown_H.Value  = (decimal)2.72e10;
            numericUpDown_Li.Value = (decimal)59.7;
            numericUpDown_C.Value  = (decimal)1.21e7;
            numericUpDown_N.Value  = (decimal)2.48e6;
            numericUpDown_O.Value  = (decimal)2.01e7;
            numericUpDown_Na.Value = (decimal)5.70e4;
            numericUpDown_Mg.Value = (decimal)1.075e6;
            numericUpDown_Al.Value = (decimal)8.49e4;
            numericUpDown_Si.Value = (decimal)1.00e6;
            numericUpDown_K.Value  = (decimal)3770;
            numericUpDown_Ca.Value = (decimal)6.11e4;
            numericUpDown_Ti.Value = (decimal)2400;
            numericUpDown_Cr.Value = (decimal)1.34e4;
            numericUpDown_Mn.Value = (decimal)9510;
            numericUpDown_Fe.Value = (decimal)9.00e5;
            numericUpDown_Co.Value = (decimal)2250;
            numericUpDown_Ni.Value = (decimal)4.93e4;
            numericUpDown_N2_1P.Value = (decimal)2.5e6;
        }
        private void checkedList()
        {
            int index ;
            index = checkedListBox1.FindString("H");
            atomdata[ 1].Enabled = checkedListBox1.GetItemChecked(index);
            index = checkedListBox1.FindString("Li");
            atomdata[ 3].Enabled = checkedListBox1.GetItemChecked(index);
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
            atomdata[22].Enabled = checkedListBox1.GetItemChecked(index);
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

            index = checkedListBox1.FindString("N2 1P");
            atomdata[200].Enabled = checkedListBox1.GetItemChecked(index);
        }
        private void cal_line_all()
        {
            Clear_Line();
            double n = (double)numericUpDownNumber.Value;
            double temp = (double)numericUpDownTemp.Value;
            double dens_e = (double)numericUpDown_DensE.Value * Math.Pow(10, (double)numericUpDown_DensE_exp.Value);
            int index, elm;
            index = checkedListBox1.FindString("H"); elm = 1;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_H.Value * n, temp, dens_e);
                label_H_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Li"); elm = 3;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Li.Value * n, temp, dens_e);
                label_Li_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("C"); elm = 6;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_C.Value * n, temp, dens_e);
                label_C_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("N"); elm = 7;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_N.Value * n, temp, dens_e);
                label_N_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("O"); elm = 8;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_O.Value * n, temp, dens_e);
                label_O_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Na"); elm = 11;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Na.Value * n, temp, dens_e);
                label_Na_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Mg"); elm = 12;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Mg.Value * n, temp, dens_e);
                label_Mg_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Al"); elm = 13;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Al.Value * n, temp, dens_e);
                label_Al_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Si"); elm = 14;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Si.Value * n, temp, dens_e);
                label_Si_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("K"); elm = 19;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_K.Value * n, temp, dens_e);
                label_K_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Ca"); elm = 20;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Ca.Value * n, temp, dens_e);
                label_Ca_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Ti"); elm = 22;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Ti.Value * n, temp, dens_e);
                label_Ti_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Cr"); elm = 24;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Cr.Value * n, temp, dens_e);
                label_Cr_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Mn"); elm = 25;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Mn.Value * n, temp, dens_e);
                label_Mn_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Fe"); elm = 26;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Fe.Value * n, temp, dens_e);
                label_Fe_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Co"); elm = 27;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Co.Value * n, temp, dens_e);
                label_Co_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("Ni"); elm = 28;
            if (atomdata[elm].Enabled)
            {
                Cal_Line(elm, (double)numericUpDown_Ni.Value * n, temp, dens_e);
                label_Ni_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
            index = checkedListBox1.FindString("N2 1P"); elm = 200;
            if (atomdata[elm].Enabled)
            {
                N2_1st.make_Aki_data_N2("atom\\Molecular", temperature);
                ReadAtomData("atom\\Molecular_Aki_N2.txt");  // 200 N2 First Positive
                ReadEnergyLevel("atom\\Molecular_N2.txt", 200, 0);

                Cal_Line(elm, (double)numericUpDown_N2_1P.Value * n, temp, dens_e);
                label_N2_ion_rate.Text = (atomdata[elm].ionization_rate()).ToString("0.000");
            }
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

            chart1.Series["sim"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            //軸ラベルの設定
            string s = chart1.Series["sin"].ChartArea;//
            chart1.ChartAreas[s].AxisX.Title = "WaveLenght(nm)";
            chart1.ChartAreas[s].AxisY.Title = "Relative Value";
            //※Axis.TitleFontでフォントも指定できるがこれはデザイナで変更したほうが楽

            //X軸最小値、最大値、目盛間隔の設定
            chart1.ChartAreas[s].AxisX.Minimum = (double)numericUpDown_xaxis_min.Value ;
            chart1.ChartAreas[s].AxisX.Maximum = (double)numericUpDown_xaxis_max.Value ;
            chart1.ChartAreas[s].AxisX.Interval = (double)numericUpDown_xaxis_interval.Value;

            //Y軸最小値、最大値、目盛間隔の設定
            //chart1.ChartAreas["area1"].AxisY.Minimum = -1;
            //chart1.ChartAreas["area1"].AxisY.Maximum = 1;
            //chart1.ChartAreas["area1"].AxisY.Interval = 0.2;

            //凡例に表示される文字列を指定
            chart1.Series["sim"].Name = "Simulation";

            //凡例の作成と位置情報の指定
            Legend leg = new Legend();
            leg.DockedToChartArea = "area1";
            leg.Alignment = StringAlignment.Near;
        }

        private void button_x05ymax_Click(object sender, EventArgs e)
        {
            string s = chart1.Series["Simulation"].ChartArea;//
            double ymin = chart1.ChartAreas[s].AxisY2.Minimum;
            double ymax = chart1.ChartAreas[s].AxisY2.Maximum;
            chart1.ChartAreas[s].AxisY2.Minimum = ymin * 0.5;
            chart1.ChartAreas[s].AxisY2.Maximum = ymax * 0.5;
        }
        // 動作不良
        private void button_x2ymax_Click(object sender, EventArgs e)
        {
            string s = chart1.Series["Simulation"].ChartArea;//
            double ymin = chart1.ChartAreas[s].AxisY2.Minimum;
            double ymax = chart1.ChartAreas[s].AxisY2.Maximum;
            chart1.ChartAreas[s].AxisY2.Minimum = ymin * 2;
            chart1.ChartAreas[s].AxisY2.Maximum = ymax * 2;        
        }

    }
}
