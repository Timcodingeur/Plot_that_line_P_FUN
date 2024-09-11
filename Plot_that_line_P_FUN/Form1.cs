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
            
            List<CryptoData> bitcoinData = ReadCsv("../../../../Top-100-Crypto-Coins/bitcoin.csv");
            List<CryptoData> bitcoinCashData = ReadCsv("../../../../Top-100-Crypto-Coins/bitcoin_cash.csv");
            List<CryptoData> bnbData = ReadCsv("../../../../Top-100-Crypto-Coins/bnb.csv");
            InitializeComponent();
            panel1.Controls.Add(FormsPlot1);
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
            signalPlot.LegendText = label;
        }

        private void PlotSignalDataCal(string label, List<CryptoData> data, DateTime date1, DateTime date2)
        {

            var filteredData = data.Where(point => point.Date >= date1 && point.Date <= date2).ToList();

            double[] yValues = filteredData.Select(point => point.Close).ToArray();
            double[] xValues = filteredData.Select(point => point.Date.ToOADate()).ToArray();

            DateTime start = DateTime.FromOADate(xValues[1]);

            var signalPlot = FormsPlot1.Plot.Add.Signal(yValues);
            signalPlot.Data.XOffset = start.ToOADate();
            signalPlot.Data.Period = 1.0;
            signalPlot.LegendText = label;

        }
        public void search()
        {
            try
            {
                FormsPlot1.Plot.Clear();
                List<CryptoData> bitcoinData = ReadCsv("../../../../Top-100-Crypto-Coins/bitcoin.csv");
                List<CryptoData> bitcoinCashData = ReadCsv("../../../../Top-100-Crypto-Coins/bitcoin_cash.csv");
                List<CryptoData> bnbData = ReadCsv("../../../../Top-100-Crypto-Coins/bnb.csv");


                PlotSignalDataCal("Bitcoin", bitcoinData, dateTimePicker1.Value, dateTimePicker2.Value);
                PlotSignalDataCal("Bitcoin Cash", bitcoinCashData, dateTimePicker1.Value, dateTimePicker2.Value);
                PlotSignalDataCal("BNB", bnbData, dateTimePicker1.Value, dateTimePicker2.Value);
                FormsPlot1.Plot.Axes.DateTimeTicksBottom();
                FormsPlot1.Refresh();
            }
            catch
            {

            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

           
            dateTimePicker2.MinDate = dateTimePicker1.Value;

            dateTimePicker2.MaxDate = new DateTime(2022, 08, 23);
            search();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            dateTimePicker1.MinDate = DateTime.MinValue;
            search();
        }


    }
    public class CryptoData
    {
        public DateTime Date { get; set; }
        public double Close { get; set; }
    }
}
