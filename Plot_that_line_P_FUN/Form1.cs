using ScottPlot.WinForms;

namespace Plot_that_line_P_FUN
{
    /// <summary>
    /// class du form
    /// </summary>
    public partial class Form1 : Form 
    {
        string pathdeb = "./Top-100-Crypto-Coins/";
        Calculate calcule = new Calculate();

        public static FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };
     
        /// <summary>
        /// methode principale du form, dans celle ci on vas faire le graphique de base qui s'affiche au lancement de l'app
        /// </summary>
        public Form1()
        {
            
            List<CryptoData> bitcoinData = calcule.ReadCsv(pathdeb+"bitcoin.csv");
            List<CryptoData> bitcoinCashData = calcule.ReadCsv(pathdeb + "bitcoin_cash.csv");
            List<CryptoData> bnbData = calcule.ReadCsv(pathdeb + "bnb.csv");
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

        /// <summary>
        /// date picker du début (si on met le 10.02.2012 alors le graphe commencera par cette date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {


           
            dateTimePicker2.MinDate = dateTimePicker1.Value;

            dateTimePicker2.MaxDate = new DateTime(2022, 08, 23);
            calcule.Search(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        /// <summary>
        /// date picker du début (si on met le 10.02.2012 alors le graphe finira par cette date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            dateTimePicker1.MinDate = DateTime.MinValue;
            calcule.Search(dateTimePicker1.Value, dateTimePicker2.Value);
        }


    }
   
}
