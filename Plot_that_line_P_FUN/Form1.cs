using ScottPlot.WinForms;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Plot_that_line_P_FUN
{
    /// <summary>
    /// class du form
    /// </summary>
    public partial class Form1 : Form
    {
        string pathdeb = "../../../../cryptoCSV/";
        Calculate calcule = new Calculate();

        public static FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };

        /// <summary>
        /// methode principale du form, dans celle ci on vas faire le graphique de base qui s'affiche au lancement de l'app
        /// </summary>
        public Form1()
        {


            InitializeComponent();
            panel1.Controls.Add(FormsPlot1);


            FormsPlot1.Plot.Title("Prix de la crypto");
            FormsPlot1.Plot.XLabel("Date");
            FormsPlot1.Plot.YLabel("Prix de fermeture");
            pathed();
            FormsPlot1.Plot.Axes.DateTimeTicksBottom();
            FormsPlot1.Refresh();



        }
        public List<string> items = new List<string>();
        public void pathed()
        {

            string path = "../../../../cryptoCSV";
            foreach (var file in Directory.GetFiles(path))
            {
                var fileInfo = new FileInfo(file);
                string absolutePath = fileInfo.FullName.Replace(path, "").TrimStart(Path.DirectorySeparatorChar);
                string[] relativePath = absolutePath.Split("\\");
                items.Add(relativePath[relativePath.Length - 1]);
            }
            coinBox.DataSource = items;
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
            calcule.Search(dateTimePicker1.Value, dateTimePicker2.Value, GetCheckedFiles());
        }

        /// <summary>
        /// date picker du début (si on met le 10.02.2012 alors le graphe finira par cette date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            dateTimePicker1.MinDate = DateTime.MinValue;
            calcule.Search(dateTimePicker1.Value, dateTimePicker2.Value, GetCheckedFiles());
        }
        private List<string> GetCheckedFiles()
        {
            List<string> selectedFiles = new List<string>();


            foreach (var item in coinBox.CheckedItems)
            {
                selectedFiles.Add(item.ToString());
                Debug.WriteLine(item.ToString());
            }

            return selectedFiles;
        }
        private void coinBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcule.Search(dateTimePicker1.Value, dateTimePicker2.Value, GetCheckedFiles());
        }


 
    }

}
