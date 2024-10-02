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
            panel1 = new Panel();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            coinBox = new CheckedListBox();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonShadow;
            panel1.Location = new Point(29, 32);
            panel1.Name = "panel1";
            panel1.Size = new Size(673, 364);
            panel1.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(45, 423);
            dateTimePicker1.MaxDate = new DateTime(2015, 8, 23, 0, 0, 0, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 1;
            dateTimePicker1.Value = new DateTime(2015, 8, 23, 0, 0, 0, 0);
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(277, 423);
            dateTimePicker2.MaxDate = new DateTime(2022, 8, 23, 0, 0, 0, 0);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(200, 23);
            dateTimePicker2.TabIndex = 2;
            dateTimePicker2.Value = new DateTime(2022, 8, 23, 0, 0, 0, 0);
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            // 
            // coinBox
            // 
            coinBox.FormattingEnabled = true;
            coinBox.Location = new Point(545, 417);
            coinBox.Name = "coinBox";
            coinBox.Size = new Size(120, 58);
            coinBox.TabIndex = 4;
            coinBox.SelectedIndexChanged += coinBox_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 487);
            Controls.Add(coinBox);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Graph";
            ResumeLayout(false);
        }

        #endregion
        public DateTimePicker dateTimePicker2;
        public Panel panel1;
        public DateTimePicker dateTimePicker1;
        public CheckedListBox coinBox;
    }
}