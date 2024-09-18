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

    public partial class Calcule 
    {


        public List<CryptoData> ReadCsv(string filePath)
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


        public void PlotSignalData(string label, List<CryptoData> data)
        {

            if (data == null || !data.Any()) return;


            double[] yValues = data.Select(point => point.Close).ToArray();
            double[] xValues = data.Select(point => point.Date.ToOADate()).ToArray();
            DateTime start = DateTime.FromOADate(xValues[1]);

            var signalPlot = Form1.FormsPlot1.Plot.Add.Signal(yValues);
            signalPlot.Data.XOffset = start.ToOADate();
            signalPlot.Data.Period = 1.0;
            signalPlot.LegendText = label;
        }

        public void PlotSignalDataCal(string label, List<CryptoData> data, DateTime date1, DateTime date2)
        {

            var filteredData = data.Where(point => point.Date >= date1 && point.Date <= date2).ToList();

            double[] yValues = filteredData.Select(point => point.Close).ToArray();
            double[] xValues = filteredData.Select(point => point.Date.ToOADate()).ToArray();

            DateTime start = DateTime.FromOADate(xValues[1]);

            var signalPlot = Form1.FormsPlot1.Plot.Add.Signal(yValues);
            signalPlot.Data.XOffset = start.ToOADate();
            signalPlot.Data.Period = 1.0;
            signalPlot.LegendText = label;

        }

        public void search(DateTime debut, DateTime fin)
        {
            try
            {
                Form1.FormsPlot1.Plot.Clear();
                List<CryptoData> bitcoinData = ReadCsv("../../../../Top-100-Crypto-Coins/bitcoin.csv");
                List<CryptoData> bitcoinCashData = ReadCsv("../../../../Top-100-Crypto-Coins/bitcoin_cash.csv");
                List<CryptoData> bnbData = ReadCsv("../../../../Top-100-Crypto-Coins/bnb.csv");


                PlotSignalDataCal("Bitcoin", bitcoinData, debut, fin);
                PlotSignalDataCal("Bitcoin Cash", bitcoinCashData, debut, fin);
                PlotSignalDataCal("BNB", bnbData, debut, fin);
                Form1.FormsPlot1.Plot.Axes.DateTimeTicksBottom();
                Form1.FormsPlot1.Refresh();
            }
            catch
            {

            }
        }
    }
}
