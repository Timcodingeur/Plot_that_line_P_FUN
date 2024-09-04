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
        readonly FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };

        public Form1()
        {
           
            InitializeComponent();
            panel1.Controls.Add(FormsPlot1);

            List<CryptoData> bitcoinData = ReadCsv("../../../../Top-100-Crypto-Coins/bitcoin.csv");
            List<CryptoData> bitcoinCashData = ReadCsv("../../../../Top-100-Crypto-Coins/bitcoin_cash.csv");
            List<CryptoData> bnbData = ReadCsv("../../../../Top-100-Crypto-Coins/bnb.csv");
     
            if (bitcoinData == null || bitcoinCashData == null || bnbData == null)
            {
                MessageBox.Show("Erreur lors de la lecture des fichiers CSV.");
                return;
            }

          
            PlotSignalData("Bitcoin", bitcoinData);
            PlotSignalData("Bitcoin Cash", bitcoinCashData);
            PlotSignalData("BNB", bnbData);

            
            FormsPlot1.Plot.Title("Prix de la crypto");
            FormsPlot1.Plot.XLabel("Date");
            FormsPlot1.Plot.YLabel("Prix de fermeture");
            FormsPlot1.Plot.Axes.DateTimeTicksBottom();

            FormsPlot1.Refresh();
        }

        
        public class CryptoData
        {
            public DateTime Date { get; set; }
            public double Close { get; set; }
        }

        
        private List<CryptoData> ReadCsv(string filePath)
        {
            List<CryptoData> data = new List<CryptoData>();
            try
            {
                foreach (var line in File.ReadLines(filePath).Skip(1))
                {
                    var values = line.Split(',');

                    if (DateTime.TryParseExact(values[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) &&
                        double.TryParse(values[4], out double close))
                    {
                        data.Add(new CryptoData { Date = date, Close = close });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier {filePath}:\n{e.Message}");
                return null;
            }
            return data;
        }

       
        private void PlotSignalData(string label, List<CryptoData> data)
        {
            if (data == null || !data.Any()) return;

            
            double[] yValues = data.Select(point => point.Close).ToArray();
            double[] xValues = data.Select(point => point.Date.ToOADate()).ToArray();
            DateTime start = DateTime.FromOADate(xValues[1]);
            
            var signalPlot = FormsPlot1.Plot.Add.Signal(yValues);
            signalPlot.Data.XOffset = start.ToOADate();
            signalPlot.Data.Period = 1.0;
            signalPlot.Label = label;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void reset_Click(object sender, EventArgs e)
        {

        }

        private void max_Click(object sender, EventArgs e)
        {

        }

        private void years_Click(object sender, EventArgs e)
        {

        }

        private void month6_Click(object sender, EventArgs e)
        {

        }

        private void month_Click(object sender, EventArgs e)
        {

        }

        private void week_Click(object sender, EventArgs e)
        {

        }
    }
}
