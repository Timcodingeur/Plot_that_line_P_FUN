namespace scvImport
{
    internal class Program
    {

        public static List<string[]> readPath(string bob)
        {
            List<string[]> result = new List<string[]>();
            using (StreamReader sr = new StreamReader(bob))
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var val = line.Split(",");
                    result.Add()
                }
        }
        static void Main(string[] args)
        {
            List <>= readPath("");
        }
    }
}