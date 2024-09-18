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

        [TestMethod()]
        public void PlotSignalDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PlotSignalDataCalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void searchTest()
        {
            Assert.Fail();
        }
    }
}