using ScottPlot;
using System.Diagnostics;
using System.Globalization;
using ScottPlot.Plottables;

namespace Plot_that_line_P_FUN
{
    /// <summary>
    /// s'occupe de tout ce qui est création du graph
    /// fait les calcule, écris les graph, lit le csv
    /// </summary>
    public partial class Calculate
    {
        string pathdeb = "../../../../cryptoCSV/";

        private ToolTip tooltip = new ToolTip();
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
        /// <param name="openDate">date de début</param>
        /// <param name="endDate">date de fin</param>
        public void PlotSignalDataDate(string label, List<CryptoData> data, DateTime openDate, DateTime endDate)
        {
            var filteredData = data.Where(point => point.Date >= openDate && point.Date <= endDate).ToList();

            double[] yValues = filteredData.Select(point => point.Close).ToArray();
            double[] xValues = filteredData.Select(point => point.Date.ToOADate()).ToArray();

            DateTime start = DateTime.FromOADate(xValues[4]);

            var signalPlot = Form1.FormsPlot1.Plot.Add.Signal(yValues);
            signalPlot.Data.XOffset = start.ToOADate();
            signalPlot.Data.Period = 1.0;

            signalPlot.LegendText = label;
            Form1.FormsPlot1.Plot.ShowLegend(Alignment.UpperCenter, ScottPlot.Orientation.Horizontal);
            static string CustomFormatter(double yValues)
            {
                return $"${yValues}";
            }
            ScottPlot.TickGenerators.NumericAutomatic myTickGenerator = new()
            {
                LabelFormatter = CustomFormatter
            };
            Form1.FormsPlot1.Plot.Axes.Left.TickGenerator = myTickGenerator;

            ScottPlot.Plottables.Crosshair crosshair = Form1.FormsPlot1.Plot.Add.Crosshair(0, 0);
            crosshair.IsVisible = false;
            crosshair.LineWidth = 1;


            Form1.FormsPlot1.MouseMove += (sender, e) =>
            {
                if (xValues == null || yValues == null || xValues.Length == 0)
                    return;

                // Convertir la position de la souris en coordonnées de données
                var mousePixel = new Pixel(e.Location.X, e.Location.Y);
                var mouseCoords = Form1.FormsPlot1.Plot.GetCoordinates(mousePixel);

                // Rechercher le point le plus proche sur la courbe
                var nearestIndex = FindClosestIndex(xValues, mouseCoords.X);
                if (nearestIndex >= 0 && nearestIndex < yValues.Length)
                {
                    double price = yValues[nearestIndex];
                    DateTime date = DateTime.FromOADate(xValues[nearestIndex]);

                    crosshair.IsVisible = true;
                    crosshair.Position = new Coordinates(mouseCoords.X, yValues[nearestIndex]);

                    // Mettre à jour le ToolTip avec les informations correspondantes
                    string info = $"{label}: Date: {date.ToShortDateString()}, Prix: {price}";
                    tooltip.Show(info, Form1.FormsPlot1, e.Location.X + 15, e.Location.Y + 15, 1000);
                }
                else
                {
                    crosshair.IsVisible = false;
                }

                // Rafraîchir le graphique
                Form1.FormsPlot1.Refresh();
            };
        }

        int FindClosestIndex(double[] array, double value)
        {
            int index = Array.BinarySearch(array, value);
            if (index >= 0)
                return index;
            index = ~index;
            if (index == 0)
                return 0;
            if (index >= array.Length)
                return array.Length - 1;
            double prev = array[index - 1];
            double next = array[index];
            return (Math.Abs(value - prev) < Math.Abs(value - next)) ? index - 1 : index;
        }

        /// <summary>
        /// fait les recherche pour les différente crypto (et apelle PlotSignalDataCal pour le calcule)
        /// </summary>
        /// <param name="debut">date de début</param>
        /// <param name="fin">date de fin</param>
        public void Search(DateTime debut, DateTime fin, List<string> changed)
        {
            Form1.FormsPlot1.Plot.Clear();
            foreach (var i in changed)
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