using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.WinForms;

namespace Plot_that_line_P_FUN
{

    public partial class Form1 : Form 
    {
        Calcule calcule = new Calcule();

        public static FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };
     
        public Form1()
        {
            
            List<CryptoData> bitcoinData = calcule.ReadCsv("../../../../Top-100-Crypto-Coins/bitcoin.csv");
            List<CryptoData> bitcoinCashData = calcule.ReadCsv("../../../../Top-100-Crypto-Coins/bitcoin_cash.csv");
            List<CryptoData> bnbData = calcule.ReadCsv("../../../../Top-100-Crypto-Coins/bnb.csv");
            InitializeComponent();
            panel1.Controls.Add(FormsPlot1);
            if (bitcoinData == null || bitcoinCashData == null || bnbData == null)
            {
                MessageBox.Show("Erreur lors de la lecture des fichiers CSV.");
                return;
            }


            calcule.PlotSignalData("Bitcoin", bitcoinData);
            calcule.PlotSignalData("Bitcoin Cash", bitcoinCashData);
            calcule.PlotSignalData("BNB", bnbData);
            FormsPlot1.Plot.Title("Prix de la crypto");
            FormsPlot1.Plot.XLabel("Date");
            FormsPlot1.Plot.YLabel("Prix de fermeture");
            FormsPlot1.Plot.Axes.DateTimeTicksBottom();
            FormsPlot1.Refresh();  
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

           
            dateTimePicker2.MinDate = dateTimePicker1.Value;

            dateTimePicker2.MaxDate = new DateTime(2022, 08, 23);
            calcule.search(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            dateTimePicker1.MinDate = DateTime.MinValue;
            calcule.search(dateTimePicker1.Value, dateTimePicker2.Value);
        }


    }
   
}
