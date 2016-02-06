namespace LineSynth
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.numericUpDownTemp = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.numericUpDownNumber = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDown_Ni = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Co = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Fe = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Mn = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Cr = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Ti = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Ca = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_K = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Si = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Al = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Mg = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Na = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_O = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_N = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_C = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_xaxis_max = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_xaxis_min = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_xaxis_interval = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_x2ymax = new System.Windows.Forms.Button();
            this.button_x05ymax = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Ni)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Co)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Fe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Mn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Cr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Ti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Ca)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_K)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Si)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Al)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Mg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Na)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_O)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_N)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_xaxis_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_xaxis_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_xaxis_interval)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(2, 2);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1234, 701);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1242, 680);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "data load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(2, 709);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1234, 65);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // numericUpDownTemp
            // 
            this.numericUpDownTemp.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDownTemp.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownTemp.Location = new System.Drawing.Point(1242, 623);
            this.numericUpDownTemp.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDownTemp.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTemp.Name = "numericUpDownTemp";
            this.numericUpDownTemp.Size = new System.Drawing.Size(75, 23);
            this.numericUpDownTemp.TabIndex = 3;
            this.numericUpDownTemp.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1323, 629);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "K";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1358, 623);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "C",
            "N",
            "O",
            "Na",
            "Mg",
            "Al",
            "Si",
            "K",
            "Ca",
            "Ti",
            "Cr",
            "Mn",
            "Fe",
            "Co",
            "Ni"});
            this.checkedListBox1.Location = new System.Drawing.Point(3, 3);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(75, 340);
            this.checkedListBox1.TabIndex = 6;
            // 
            // numericUpDownNumber
            // 
            this.numericUpDownNumber.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.numericUpDownNumber.CausesValidation = false;
            this.numericUpDownNumber.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDownNumber.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownNumber.Location = new System.Drawing.Point(1242, 652);
            this.numericUpDownNumber.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownNumber.Name = "numericUpDownNumber";
            this.numericUpDownNumber.Size = new System.Drawing.Size(93, 23);
            this.numericUpDownNumber.TabIndex = 7;
            this.numericUpDownNumber.ThousandsSeparator = true;
            this.numericUpDownNumber.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1341, 658);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "量";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numericUpDown_Ni);
            this.panel1.Controls.Add(this.numericUpDown_Co);
            this.panel1.Controls.Add(this.numericUpDown_Fe);
            this.panel1.Controls.Add(this.numericUpDown_Mn);
            this.panel1.Controls.Add(this.numericUpDown_Cr);
            this.panel1.Controls.Add(this.numericUpDown_Ti);
            this.panel1.Controls.Add(this.numericUpDown_Ca);
            this.panel1.Controls.Add(this.numericUpDown_K);
            this.panel1.Controls.Add(this.numericUpDown_Si);
            this.panel1.Controls.Add(this.numericUpDown_Al);
            this.panel1.Controls.Add(this.numericUpDown_Mg);
            this.panel1.Controls.Add(this.numericUpDown_Na);
            this.panel1.Controls.Add(this.numericUpDown_O);
            this.panel1.Controls.Add(this.numericUpDown_N);
            this.panel1.Controls.Add(this.numericUpDown_C);
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Location = new System.Drawing.Point(1242, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 407);
            this.panel1.TabIndex = 9;
            // 
            // numericUpDown_Ni
            // 
            this.numericUpDown_Ni.Location = new System.Drawing.Point(83, 301);
            this.numericUpDown_Ni.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Ni.Name = "numericUpDown_Ni";
            this.numericUpDown_Ni.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Ni.TabIndex = 21;
            this.numericUpDown_Ni.ThousandsSeparator = true;
            // 
            // numericUpDown_Co
            // 
            this.numericUpDown_Co.Location = new System.Drawing.Point(83, 280);
            this.numericUpDown_Co.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Co.Name = "numericUpDown_Co";
            this.numericUpDown_Co.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Co.TabIndex = 20;
            this.numericUpDown_Co.ThousandsSeparator = true;
            // 
            // numericUpDown_Fe
            // 
            this.numericUpDown_Fe.Location = new System.Drawing.Point(83, 259);
            this.numericUpDown_Fe.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Fe.Name = "numericUpDown_Fe";
            this.numericUpDown_Fe.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Fe.TabIndex = 19;
            this.numericUpDown_Fe.ThousandsSeparator = true;
            // 
            // numericUpDown_Mn
            // 
            this.numericUpDown_Mn.Location = new System.Drawing.Point(83, 238);
            this.numericUpDown_Mn.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Mn.Name = "numericUpDown_Mn";
            this.numericUpDown_Mn.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Mn.TabIndex = 18;
            this.numericUpDown_Mn.ThousandsSeparator = true;
            // 
            // numericUpDown_Cr
            // 
            this.numericUpDown_Cr.Location = new System.Drawing.Point(83, 217);
            this.numericUpDown_Cr.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Cr.Name = "numericUpDown_Cr";
            this.numericUpDown_Cr.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Cr.TabIndex = 17;
            this.numericUpDown_Cr.ThousandsSeparator = true;
            // 
            // numericUpDown_Ti
            // 
            this.numericUpDown_Ti.Location = new System.Drawing.Point(83, 196);
            this.numericUpDown_Ti.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Ti.Name = "numericUpDown_Ti";
            this.numericUpDown_Ti.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Ti.TabIndex = 16;
            this.numericUpDown_Ti.ThousandsSeparator = true;
            // 
            // numericUpDown_Ca
            // 
            this.numericUpDown_Ca.Location = new System.Drawing.Point(83, 175);
            this.numericUpDown_Ca.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Ca.Name = "numericUpDown_Ca";
            this.numericUpDown_Ca.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Ca.TabIndex = 15;
            this.numericUpDown_Ca.ThousandsSeparator = true;
            // 
            // numericUpDown_K
            // 
            this.numericUpDown_K.Location = new System.Drawing.Point(83, 154);
            this.numericUpDown_K.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_K.Name = "numericUpDown_K";
            this.numericUpDown_K.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_K.TabIndex = 14;
            this.numericUpDown_K.ThousandsSeparator = true;
            // 
            // numericUpDown_Si
            // 
            this.numericUpDown_Si.Location = new System.Drawing.Point(83, 133);
            this.numericUpDown_Si.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Si.Name = "numericUpDown_Si";
            this.numericUpDown_Si.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Si.TabIndex = 13;
            this.numericUpDown_Si.ThousandsSeparator = true;
            // 
            // numericUpDown_Al
            // 
            this.numericUpDown_Al.Location = new System.Drawing.Point(83, 112);
            this.numericUpDown_Al.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Al.Name = "numericUpDown_Al";
            this.numericUpDown_Al.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Al.TabIndex = 12;
            this.numericUpDown_Al.ThousandsSeparator = true;
            // 
            // numericUpDown_Mg
            // 
            this.numericUpDown_Mg.Location = new System.Drawing.Point(83, 91);
            this.numericUpDown_Mg.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Mg.Name = "numericUpDown_Mg";
            this.numericUpDown_Mg.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Mg.TabIndex = 11;
            this.numericUpDown_Mg.ThousandsSeparator = true;
            // 
            // numericUpDown_Na
            // 
            this.numericUpDown_Na.Location = new System.Drawing.Point(83, 70);
            this.numericUpDown_Na.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_Na.Name = "numericUpDown_Na";
            this.numericUpDown_Na.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_Na.TabIndex = 10;
            this.numericUpDown_Na.ThousandsSeparator = true;
            // 
            // numericUpDown_O
            // 
            this.numericUpDown_O.Location = new System.Drawing.Point(83, 49);
            this.numericUpDown_O.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_O.Name = "numericUpDown_O";
            this.numericUpDown_O.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_O.TabIndex = 9;
            this.numericUpDown_O.ThousandsSeparator = true;
            // 
            // numericUpDown_N
            // 
            this.numericUpDown_N.Location = new System.Drawing.Point(83, 28);
            this.numericUpDown_N.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_N.Name = "numericUpDown_N";
            this.numericUpDown_N.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_N.TabIndex = 8;
            this.numericUpDown_N.ThousandsSeparator = true;
            // 
            // numericUpDown_C
            // 
            this.numericUpDown_C.Location = new System.Drawing.Point(83, 7);
            this.numericUpDown_C.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_C.Name = "numericUpDown_C";
            this.numericUpDown_C.Size = new System.Drawing.Size(88, 19);
            this.numericUpDown_C.TabIndex = 7;
            this.numericUpDown_C.ThousandsSeparator = true;
            // 
            // numericUpDown_xaxis_max
            // 
            this.numericUpDown_xaxis_max.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDown_xaxis_max.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_xaxis_max.Location = new System.Drawing.Point(70, 3);
            this.numericUpDown_xaxis_max.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numericUpDown_xaxis_max.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown_xaxis_max.Name = "numericUpDown_xaxis_max";
            this.numericUpDown_xaxis_max.Size = new System.Drawing.Size(52, 23);
            this.numericUpDown_xaxis_max.TabIndex = 10;
            this.numericUpDown_xaxis_max.Value = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            // 
            // numericUpDown_xaxis_min
            // 
            this.numericUpDown_xaxis_min.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDown_xaxis_min.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_xaxis_min.Location = new System.Drawing.Point(14, 3);
            this.numericUpDown_xaxis_min.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numericUpDown_xaxis_min.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown_xaxis_min.Name = "numericUpDown_xaxis_min";
            this.numericUpDown_xaxis_min.Size = new System.Drawing.Size(52, 23);
            this.numericUpDown_xaxis_min.TabIndex = 11;
            this.numericUpDown_xaxis_min.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown_xaxis_min.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "X";
            // 
            // numericUpDown_xaxis_interval
            // 
            this.numericUpDown_xaxis_interval.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDown_xaxis_interval.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_xaxis_interval.Location = new System.Drawing.Point(126, 3);
            this.numericUpDown_xaxis_interval.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_xaxis_interval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_xaxis_interval.Name = "numericUpDown_xaxis_interval";
            this.numericUpDown_xaxis_interval.Size = new System.Drawing.Size(45, 23);
            this.numericUpDown_xaxis_interval.TabIndex = 13;
            this.numericUpDown_xaxis_interval.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_x2ymax);
            this.panel2.Controls.Add(this.button_x05ymax);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.numericUpDown_xaxis_min);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.numericUpDown_xaxis_interval);
            this.panel2.Controls.Add(this.numericUpDown_xaxis_max);
            this.panel2.Location = new System.Drawing.Point(1242, 462);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 100);
            this.panel2.TabIndex = 14;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // button_x2ymax
            // 
            this.button_x2ymax.Location = new System.Drawing.Point(101, 40);
            this.button_x2ymax.Name = "button_x2ymax";
            this.button_x2ymax.Size = new System.Drawing.Size(70, 23);
            this.button_x2ymax.TabIndex = 17;
            this.button_x2ymax.Text = "2 Ymax";
            this.button_x2ymax.UseVisualStyleBackColor = true;
            this.button_x2ymax.Click += new System.EventHandler(this.button_x2ymax_Click);
            // 
            // button_x05ymax
            // 
            this.button_x05ymax.Location = new System.Drawing.Point(14, 40);
            this.button_x05ymax.Name = "button_x05ymax";
            this.button_x05ymax.Size = new System.Drawing.Size(64, 23);
            this.button_x05ymax.TabIndex = 15;
            this.button_x05ymax.Text = "1/2Ymax";
            this.button_x05ymax.UseVisualStyleBackColor = true;
            this.button_x05ymax.Click += new System.EventHandler(this.button_x05ymax_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "y";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 772);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownNumber);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownTemp);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "LineSynthsis";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Ni)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Co)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Fe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Mn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Cr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Ti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Ca)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_K)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Si)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Al)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Mg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Na)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_O)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_N)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_xaxis_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_xaxis_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_xaxis_interval)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownTemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numericUpDown_Ni;
        private System.Windows.Forms.NumericUpDown numericUpDown_Co;
        private System.Windows.Forms.NumericUpDown numericUpDown_Fe;
        private System.Windows.Forms.NumericUpDown numericUpDown_Mn;
        private System.Windows.Forms.NumericUpDown numericUpDown_Cr;
        private System.Windows.Forms.NumericUpDown numericUpDown_Ti;
        private System.Windows.Forms.NumericUpDown numericUpDown_Ca;
        private System.Windows.Forms.NumericUpDown numericUpDown_K;
        private System.Windows.Forms.NumericUpDown numericUpDown_Si;
        private System.Windows.Forms.NumericUpDown numericUpDown_Al;
        private System.Windows.Forms.NumericUpDown numericUpDown_Mg;
        private System.Windows.Forms.NumericUpDown numericUpDown_Na;
        private System.Windows.Forms.NumericUpDown numericUpDown_O;
        private System.Windows.Forms.NumericUpDown numericUpDown_N;
        private System.Windows.Forms.NumericUpDown numericUpDown_C;
        private System.Windows.Forms.NumericUpDown numericUpDown_xaxis_max;
        private System.Windows.Forms.NumericUpDown numericUpDown_xaxis_min;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_xaxis_interval;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_x2ymax;
        private System.Windows.Forms.Button button_x05ymax;
    }
}

