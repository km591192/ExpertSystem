using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ExpertSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> facts = new List<string>();
        Dictionary<String, String> rule = new Dictionary<String, String>();
        Data_File file = new Data_File();
        Goal goal = new Goal();
        string[] data;

        //add fact
        private void button1_Click(object sender, EventArgs e)
        {
            facts.Add(textBox1.Text.Trim().ToString());
            textBox1.Text = "";
        }

        //add rule
        private void button2_Click(object sender, EventArgs e)
        {
            int flag = 0;
            if (!rule.Keys.Contains(textBox2.Text.Trim().ToString() + "," + textBox3.Text.Trim().ToString()))
            {
                foreach (string el in rule.Keys)
                {
                    string[] arr = el.Split(',');
                    if (arr[0].Trim() == textBox2.Text.Trim().ToString())
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0 && textBox4.TextLength > 0)
                    rule.Add(textBox2.Text.Trim().ToString() + "," + textBox3.Text.Trim().ToString(), textBox4.Text.Trim().ToString());
                else
                    MessageBox.Show("ERROR!!!! Check your rule");
            }

            else
                MessageBox.Show("ERROR! Rule with this number contains in data");

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        //save
        private void button3_Click(object sender, EventArgs e)
        {
            File.WriteAllText("data.txt", String.Empty);
            foreach (string el in facts)
            {
                file.add_To_File("fact(" + el + ")");
                file.add_To_File(Environment.NewLine);
            } 
            foreach(var el in rule)
            {
                file.add_To_File("rule(" + el.Key + "," + el.Value + ")");
                file.add_To_File(Environment.NewLine);
            }
        }

        //load
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            data = file.read_File();
            foreach(string str in data)
            {
                listBox1.Items.Add(str);
            }
        }

        //check
        private void button5_Click(object sender, EventArgs e)
        {
            goal.check_goal(data, textBox5.Text.Trim().ToString(), textBox6.Text.Trim().ToString());
        }
    }
}
