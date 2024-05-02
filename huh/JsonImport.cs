using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace huh
{
    public class JsonImport
    {
        public string path;

        public void JI(out ViewForJson graphj)
        {
            string jsonFile = File.ReadAllText(path);


            graphj = JsonConvert.DeserializeObject<ViewForJson>(jsonFile)!;

        }
    }
}
