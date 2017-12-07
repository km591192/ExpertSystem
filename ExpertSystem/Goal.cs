using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpertSystem
{
    class Goal
    {
        List<string> facts = new List<string>();
        Dictionary<String, String> rules = new Dictionary<String, String>();
        string [,] arr_rules ;

        private void data_in_arr(string [] data)
        {
            foreach (string str in data)
            {
                if (str.Contains("fact"))
                {
                    string[] arr1 = str.Split('(', ')');
                    facts.Add(arr1[1]);
                }
            }
            arr_rules = new string[3, data.Length - facts.Count];
            int h = 0;
            foreach (string str in data)
            {
                if (str.Contains("rule"))
                {
                    string[] arr1 = str.Split('(',',',')');
                    string tempstr = "";
                    for (int i = 3; i < arr1.Length - 1; i++)
                        tempstr += arr1[i] + ",";
                    rules.Add(arr1[1] + "," + arr1[2], tempstr);
                    arr_rules[0, h] = arr1[1];
                    arr_rules[1, h] = arr1[2];
                    arr_rules[2, h] = tempstr;
                        h++;
                }
            }
        }

        public void check_goal(string [] data,string goal, string user_facts)
        {
            facts.Clear();
            rules.Clear();
            data_in_arr(data);
                int count = 0, flag = 0;
            string[] arr = user_facts.Split(',');
            for (int j = 0; j < facts.Count; j++ )
                for (int i = 0; i < arr.Length; i++)
                {
                    if (facts[j].Equals(arr[i]))
                        count++;
                }
            if (count == arr.Length)
                flag = 1;
            else { flag = 0; MessageBox.Show("No facts in data."); }

            count = 0;
            int breakflag = 0;
            int lengthdictold = rules.Keys.Count, lengthdictnew = 0;
            int iter = 0;
            while (lengthdictnew < lengthdictold || iter != 1000)
            {
                lengthdictold = rules.Keys.Count;
                for (int i = 0; i < arr_rules.Length/3; i++)
                {
                    breakflag = 0;
                    string[] facts_from_rules = arr_rules[2, i].Split(',');
                    for (int h = 0; h < facts_from_rules.Length; h++)
                    {
                        for (int g = 0; g < arr.Length; g++)
                        {
                            if (facts_from_rules[h].Equals(arr[g]))
                                count++;
                            if ((facts_from_rules[facts_from_rules.Length - 1] == "" && count == facts_from_rules.Length - 1) || count == facts_from_rules.Length)
                            {
                                foreach(string el in rules.Keys)
                                {
                                    if (el.Equals(arr_rules[0, i] + "," + arr_rules[1, i]))
                                    {
                                        Array.Resize(ref arr, arr.Length + 1);
                                        arr[arr.Length - 1] = arr_rules[1, i];
                                        rules.Remove(arr_rules[0, i] + "," + arr_rules[1, i]);
                                        lengthdictnew = rules.Keys.Count;
                                        count = 0;
                                        breakflag = 1;
                                        break;
                                    }
                                }
                            }
                            if (arr[g].Equals(goal))
                            { flag = 2; break; }
                            if (breakflag == 1) break;
                        }
                        if (breakflag == 1) break;
                    }
                    count = 0;
                }
                iter++;
            }

            if (flag == 2)
            { MessageBox.Show("TRUE"); }
            if (flag != 2)
            { MessageBox.Show("False"); }
        }
    }
}
