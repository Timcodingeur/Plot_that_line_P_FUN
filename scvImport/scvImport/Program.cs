namespace scvImport
{
    public class money
    {
        public string _date { get; set; }
        public string _open { get; set; }
        public string _high { get; set; }
        public string _low { get; set; }
        public string _close { get; set; }
        public string _volume { get; set; }
        public string _currency { get; set; }

        public money(string date, string open, string high, string low, string close, string volume, string currency){
            this._date = date;
            this._open = open;
            this._high = high;  
            this._low = low;
            this._close = close;
            this._volume = volume;
            this._currency = currency;

            }
    }


        internal class Program
    {

        public static List<money[]> readPath(string path, string money)
        {
            List<money[]> result = new List<money[]>();
            using (StreamReader sr = new StreamReader(bob))
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var val = line.Split(",");
                    result.Add(new money(val[0], val[1], val[2], val[3], val[4], val[5], val[6], money);
                }
            return result;
        }
        static void Main(string[] args)
        {
            List <money> bitcash= readPath("../Top-100-Crypto-Coins/bitcoin-cash.csv");
            List <money> bitcoin= readPath("../Top-100-Crypto-Coins/bitcoin.csv");
            List <money> binanceUSD= readPath("../Top-100-Crypto-Coins/Binance-USD.csv");
        }
    }
}