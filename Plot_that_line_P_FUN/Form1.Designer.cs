namespace Plot_that_line_P_FUN
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.month = new System.Windows.Forms.Button();
            this.month6 = new System.Windows.Forms.Button();
            this.years = new System.Windows.Forms.Button();
            this.max = new System.Windows.Forms.Button();
            this.week = new System.Windows.Forms.Button();
            this.reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Location = new System.Drawing.Point(29, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(673, 364);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // month
            // 
            this.month.Location = new System.Drawing.Point(134, 414);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(75, 23);
            this.month.TabIndex = 1;
            this.month.Text = "1M";
            this.month.UseVisualStyleBackColor = true;
            this.month.Click += new System.EventHandler(this.month_Click);
            // 
            // month6
            // 
            this.month6.Location = new System.Drawing.Point(215, 414);
            this.month6.Name = "month6";
            this.month6.Size = new System.Drawing.Size(75, 23);
            this.month6.TabIndex = 2;
            this.month6.Text = "6M";
            this.month6.UseVisualStyleBackColor = true;
            this.month6.Click += new System.EventHandler(this.month6_Click);
            // 
            // years
            // 
            this.years.Location = new System.Drawing.Point(296, 414);
            this.years.Name = "years";
            this.years.Size = new System.Drawing.Size(75, 23);
            this.years.TabIndex = 3;
            this.years.Text = "1Y";
            this.years.UseVisualStyleBackColor = true;
            this.years.Click += new System.EventHandler(this.years_Click);
            // 
            // max
            // 
            this.max.Location = new System.Drawing.Point(377, 414);
            this.max.Name = "max";
            this.max.Size = new System.Drawing.Size(75, 23);
            this.max.TabIndex = 4;
            this.max.Text = "MAX";
            this.max.UseVisualStyleBackColor = true;
            this.max.Click += new System.EventHandler(this.max_Click);
            // 
            // week
            // 
            this.week.Location = new System.Drawing.Point(53, 414);
            this.week.Name = "week";
            this.week.Size = new System.Drawing.Size(75, 23);
            this.week.TabIndex = 5;
            this.week.Text = "1W";
            this.week.UseVisualStyleBackColor = true;
            this.week.Click += new System.EventHandler(this.week_Click);
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(607, 414);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 23);
            this.reset.TabIndex = 6;
            this.reset.Text = "Reset";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 487);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.week);
            this.Controls.Add(this.max);
            this.Controls.Add(this.years);
            this.Controls.Add(this.month6);
            this.Controls.Add(this.month);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Graph";
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button month;
        private Button month6;
        private Button years;
        private Button max;
        private Button week;
        private Button reset;
    }
}