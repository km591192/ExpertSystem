using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExpertSystem
{
    class Data_File
    {
        public void add_To_File(string s)
        {
            File.AppendAllText("data.txt", s);
        }
        public string[] read_File()
        {
            return File.ReadAllLines("data.txt"); ;
        }
        /*
        public string[] read_File()
        {
            return File.ReadAllLines("data1.txt"); 
        }
          */
    }
}
