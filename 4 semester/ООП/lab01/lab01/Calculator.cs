using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab01
{
    public partial class Calculator : Form, ICalculator
    {
        private string res;
        private string[] simbols = { "+", "-", "*", "/", "//", "%"};
        public Calculator()
        {
            InitializeComponent();
        }

        public void Calculate()
        {
            try
            {
                int num1, num2;
                bool isNum1Valid = int.TryParse(firstnumber.Text, out num1);
                bool isNum2Valid = int.TryParse(secondnumber.Text, out num2);

                if (!isNum1Valid)
                {
                    error1.Text = "Неправильный формат";
                }
                else
                {
                    error1.Text = "";
                }

                if (!isNum2Valid)
                {
                    error2.Text = "Неправильный формат";
                }
                else
                {
                    error2.Text = "";
                }

                if (isNum1Valid && isNum2Valid)
                {
                    for (int i = 0; i < simbols.Length; i++)
                    {
                        if (comboBox1.Text == simbols[i])
                        {
                            switch (comboBox1.Text)
                            {
                                case "+":
                                {
                                    result.Text = Convert.ToString(num1 + num2);
                                    break;
                                }
                                case "-":
                                {
                                    result.Text = Convert.ToString(num1 - num2);
                                    break;
                                }
                                case "*":
                                {
                                    result.Text = Convert.ToString(num1 * num2);
                                    break;
                                }
                                case "/":
                                {
                                    double num11 = Convert.ToDouble(num1);
                                    double num22 = Convert.ToDouble(num2);
                                    result.Text = Convert.ToString(num11 / num22);
                                    break;
                                }
                                case "//":
                                {
                                    result.Text = Convert.ToString(num1 / num2);
                                    break;
                                }
                                case "%":
                                {
                                    result.Text = Convert.ToString(num1 % num2);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        public void Clear()
        {
            firstnumber.Text = "";
            secondnumber.Text = "";
            result.Text = "";
        }

        public void SaveResult()
        {
            res = result.Text;
        }

        public void ExtractResult()
        {
            firstnumber.Text = res;
        }

        private void send_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void save_Click(object sender, EventArgs e)
        {
            SaveResult();
        }

        private void extract_Click(object sender, EventArgs e)
        {
            ExtractResult();
        }
    }
}