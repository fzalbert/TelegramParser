using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic.Parsing
{
    public class ParseService : IParseService
    {
        public List<string> GetAllKeys()
        {
            List<string> keys;

            using (StreamReader sr = new StreamReader(File.Open("Keys.txt", FileMode.Open)))
            {
                string file = sr.ReadToEnd();

                keys = new List<string>(file.Split());
            }

            throw new NotImplementedException();
        }
    }
}
