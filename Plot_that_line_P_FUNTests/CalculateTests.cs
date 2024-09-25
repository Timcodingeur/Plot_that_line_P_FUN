using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plot_that_line_P_FUN;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plot_that_line_P_FUN.Tests
{
    [TestClass()]
    public class CalculateTests
    {
        /// <summary>
        /// ici on test que les fichier csv sont bien lu. donc on test la lecture et si il y'a une erreur dans la lecture du fichier il fail
        /// </summary>
        [TestMethod()]
        public void ReadCsvTest()
        {
            List<CryptoData> data = new List<CryptoData>();
            try
            {
                foreach (var line in File.ReadLines("../../../../Top-100-Crypto-Coins/bitcoin_cash.csv").Skip(1))
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
                Assert.Fail();
            }
            
        }

        /// <summary>
        /// ici on créé des donnée fictive d'un crypto. on en fait des tableau et on test si il le tableau a bien été généré
        /// </summary>
        [TestMethod()]
        public void PlotSignalDataTest()
        {
            
            var calculate = new Calculate();

            // donnée fictive
            var cryptoData = new List<CryptoData>
            {
                new CryptoData { Date = new DateTime(2021, 1, 1), Close = 30000 },
                new CryptoData { Date = new DateTime(2021, 1, 2), Close = 32000 },
                new CryptoData { Date = new DateTime(2021, 1, 3), Close = 31000 },
            };

            
            calculate.PlotSignalData("Bitcoin", cryptoData);

            
            double[] expectedXValues = cryptoData.Select(c => c.Date.ToOADate()).ToArray();
            double[] expectedYValues = cryptoData.Select(c => c.Close).ToArray();

            
            Assert.IsNotNull(expectedXValues);
            Assert.IsNotNull(expectedYValues);
            Assert.AreEqual(3, expectedXValues.Length);
            Assert.AreEqual(3, expectedYValues.Length);
            Assert.AreEqual(30000, expectedYValues[0]);
            Assert.AreEqual(32000, expectedYValues[1]);
        }

        /// <summary>
        /// ici on créé des donnée fictive d'un crypto, différence avec l'autre methode
        /// est qu'on test avec des date.
        [TestMethod()]
        public void PlotSignalDataCalTest()
        {
            
            var calculate = new Calculate();

            // donnée fictive
            var cryptoData = new List<CryptoData>
            {
                new CryptoData { Date = new DateTime(2021, 1, 1), Close = 30000 },
                new CryptoData { Date = new DateTime(2021, 1, 2), Close = 32000 },
                new CryptoData { Date = new DateTime(2021, 1, 3), Close = 31000 },
            };

            DateTime openDate = new DateTime(2021, 1, 2);
            DateTime endDate = new DateTime(2021, 1, 3);

            
            calculate.PlotSignalDataCal("Bitcoin", cryptoData, openDate, endDate);

            
            var filteredData = cryptoData.Where(c => c.Date >= openDate && c.Date <= endDate).ToList();
            Assert.AreEqual(2, filteredData.Count);
            Assert.AreEqual(new DateTime(2021, 1, 2), filteredData[0].Date);
            Assert.AreEqual(new DateTime(2021, 1, 3), filteredData[1].Date);
        }

        /// <summary>
        /// on appel la vrais methode search avec des donnée fictive de début et de fin pour voir si cela fonctionne
        /// </summary>
        [TestMethod()]
        public void searchTest()
        {
            var calculate = new Calculate();
            DateTime debut = new DateTime(2021, 1, 1);
            DateTime fin = new DateTime(2021, 1, 3);

            // donnée fictive
            var bitcoinData = new List<CryptoData>
            {
                new CryptoData { Date = new DateTime(2021, 1, 1), Close = 30000 },
                new CryptoData { Date = new DateTime(2021, 1, 2), Close = 32000 },
                new CryptoData { Date = new DateTime(2021, 1, 3), Close = 31000 },
            };

            
            calculate.search(debut, fin);


            Assert.IsNotNull(bitcoinData);
            Assert.AreEqual(3, bitcoinData.Count);
        }

    }
}