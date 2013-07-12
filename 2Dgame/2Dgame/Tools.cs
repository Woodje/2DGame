using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace _2Dgame
{
    class Tools
    {
        public static string[] Readtextfiletostring(string filename)
        {
            int i = 0;
            List<string> filecontent = new List<string>();
            using (StreamReader file = File.OpenText(filename))
            {
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    filecontent.Add(line);
                    i++;
                }
            }
            return filecontent.ToArray();
        }
    }
}
