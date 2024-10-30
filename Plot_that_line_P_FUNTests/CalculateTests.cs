using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plot_that_line_P_FUN;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Plot_that_line_P_FUN.Tests
{
    [TestClass()]
    public class CalculateTests
    {
        private readonly Calculate calculate = new Calculate();

        [TestMethod()]
        public void ReadCsvTest()
        {
            var data = calculate.ReadCsv("../../../../cryptoCSV/bitcoin_cash.csv");
            Assert.IsNotNull(data, "Le fichier CSV n'a pas été lu correctement.");
            Assert.IsTrue(data.Count > 0, "Le fichier CSV ne contient aucune donnée.");
        }

        [TestMethod()]
        public void PlotSignalDataTest()
        {
            var cryptoData = new List<CryptoData>
            {
                new CryptoData { Date = new DateTime(2021, 1, 1), Close = 30000 },
                new CryptoData { Date = new DateTime(2021, 1, 2), Close = 32000 },
                new CryptoData { Date = new DateTime(2021, 1, 3), Close = 31000 },
            };

            DateTime openDate = new DateTime(2021, 1, 1);
            DateTime endDate = new DateTime(2021, 1, 3);

            calculate.PlotSignalDataDate("Bitcoin", cryptoData, openDate, endDate);

            Assert.AreEqual(3, cryptoData.Count);
            Assert.AreEqual(30000, cryptoData[0].Close);
            Assert.AreEqual(32000, cryptoData[1].Close);
        }

        [TestMethod()]
        public void PlotSignalDataCalTest()
        {
            var cryptoData = new List<CryptoData>
            {
                new CryptoData { Date = new DateTime(2021, 1, 1), Close = 30000 },
                new CryptoData { Date = new DateTime(2021, 1, 2), Close = 32000 },
                new CryptoData { Date = new DateTime(2021, 1, 3), Close = 31000 },
            };

            DateTime openDate = new DateTime(2021, 1, 2);
            DateTime endDate = new DateTime(2021, 1, 3);

            calculate.PlotSignalDataDate("Bitcoin", cryptoData, openDate, endDate);

            var filteredData = cryptoData.Where(c => c.Date >= openDate && c.Date <= endDate).ToList();
            Assert.AreEqual(2, filteredData.Count, "Les données filtrées n'ont pas la taille attendue.");
            Assert.AreEqual(new DateTime(2021, 1, 2), filteredData[0].Date);
            Assert.AreEqual(new DateTime(2021, 1, 3), filteredData[1].Date);
        }

        [TestMethod()]
        public void SearchTest()
        {
            DateTime debut = new DateTime(2021, 1, 1);
            DateTime fin = new DateTime(2021, 1, 3);
            var changed = new List<string> { "bitcoin_cash.csv" };

            calculate.Search(debut, fin, changed);

            // Charge les données brutes et les filtre entre les dates spécifiées
            var bitcoinData = calculate.ReadCsv("../../../../cryptoCSV/bitcoin_cash.csv");
            var filteredData = bitcoinData.Where(data => data.Date >= debut && data.Date <= fin).ToList();

            Assert.IsNotNull(filteredData, "Les données filtrées ne doivent pas être nulles.");
            Assert.AreEqual(3, filteredData.Count, "Le nombre de données chargées dans l'intervalle ne correspond pas.");
        }
    }
}
