using System.Diagnostics;
using System.Globalization;

namespace Plot_that_line_P_FUN
{
    /// <summary>
    /// s'occupe de tout ce qui est création du graph
    /// fait les calcule, écris les graph, lit le csv
    /// </summary>
    public partial class Calculate
    {

        string pathdeb = "../../../../cryptoCSV/";

        /// <summary>
        /// lit les fichier csv et prend les date et les close
        /// </summary>
        /// <param name="filePath">chemin du fichier csv a lire</param>
        /// <returns></returns>
        public List<CryptoData> ReadCsv(string filePath)
        {
            List<CryptoData> data = new List<CryptoData>();
            try
            {
                foreach (var line in File.ReadLines(filePath).Skip(1))
                {
                    var values = line.Split(',');
                    //on vérifie les type de date, si par exemple a la place d'une date on a écris paprika, bah cela bah pas garder cela
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
      

        /// <summary>
        /// vas poser les point pour l'affichage via date
        /// </summary>
        /// <param name="label">nom de la crypto</param>
        /// <param name="data">donné de la crypto (close et date)</param>
        /// <param name="openDate">date de début</param>x
        /// <param name="endDate">date de fin</param>

        public void PlotSignalDataDate(string label, List<CryptoData> data, DateTime openDate, DateTime endDate)
        {

            var filteredData = data.Where(point => point.Date >= openDate && point.Date <= endDate).ToList();

            double[] yValues = filteredData.Select(point => point.Close).ToArray();
            double[] xValues = filteredData.Select(point => point.Date.ToOADate()).ToArray();

            DateTime start = DateTime.FromOADate(xValues[0]);

            var signalPlot = Form1.FormsPlot1.Plot.Add.Signal(yValues);
            signalPlot.Data.XOffset = start.ToOADate();
            signalPlot.Data.Period = 1.0;
            signalPlot.LegendText = label;
        }

        /// <summary>
        /// fait les recherche pour les différente crypto (et apelle PlotSignalDataCal pour le calcule)
        /// </summary>
        /// <param name="debut">date de début</param>
        /// <param name="fin">date de fin</param>
        public void Search(DateTime debut, DateTime fin, List <string> changed)
        {
          
                Form1.FormsPlot1.Plot.Clear();
                foreach(var i in changed)
                {
                    List<CryptoData> Data = ReadCsv(pathdeb + i);
                    Debug.WriteLine(i, Data, debut, fin);
                    PlotSignalDataDate(i, Data, debut, fin);
                }               
                Form1.FormsPlot1.Plot.Axes.DateTimeTicksBottom();
                Form1.FormsPlot1.Refresh();
        }
    }
}
