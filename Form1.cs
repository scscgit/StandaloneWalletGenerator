using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace StandaloneWalletGenerator
{
    public partial class Form1 : Form
    {
        private int _bits;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedBits();
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            ChangedBits();
        }

        private void ChangedBits()
        {
            try
            {
                _bits = int.Parse("" == comboBox1.Text ? "0" : comboBox1.Text);
            }
            catch (Exception)
            {
                // Force validation
                comboBox1.Text = "";
                ChangedBits();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = new string('0', _bits);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = new string('1', _bits);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_bits % 8 != 0)
            {
                textBox1.Text = "Please use bits as a multiple of 8 :)";
                return;
            }

            try
            {
                textBox1.Text =
                    new RandomOrgEntropyService().GetRandomBytes(_bits / 8)
                        .ToList()
                        .Select(b => Convert.ToString(b, 2).PadLeft(8, '0'))
                        .Aggregate((a, b) => a + b);
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = new string('0', _bits);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = new string('1', _bits);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (_bits == 0)
            {
                textBox2.Text = "";
                return;
            }

            if (_bits % 8 != 0)
            {
                textBox2.Text = "Please use bits as a multiple of 8 :)";
                return;
            }

            var bytes = new byte[_bits / 8];
            new Random().NextBytes(bytes);
            textBox2.Text = bytes.ToList()
                .Select(b => Convert.ToString(b, 2).PadLeft(8, '0'))
                .Aggregate((a, b) => a + b);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox3.Text = new string('0', _bits);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox3.Text = new string('1', _bits);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (_bits != textBox1.Text.Length
                || textBox1.Text.Length != textBox2.Text.Length
                || textBox2.Text.Length != textBox3.Text.Length)
            {
                textBox4result.Text =
                    $"Lengths don't equal the chosen bit length {_bits} ({textBox1.Text.Length}, {textBox2.Text.Length}, {textBox3.Text.Length})";
                return;
            }

            try
            {
                textBox4result.Text = "";
                for (var i = 0; i < _bits; i++)
                {
                    var add = int.Parse(textBox1.Text[i].ToString())
                              ^ int.Parse(textBox2.Text[i].ToString())
                              ^ int.Parse(textBox3.Text[i].ToString());
                    textBox4result.Text += add;
                }
            }
            catch (Exception ex)
            {
                textBox4result.Text = ex.Message;
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", e.LinkText);
        }
    }
}
