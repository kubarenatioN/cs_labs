using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            foreach (var btn in tableLayoutPanel1.Controls)
            {
                if (btn.GetType() == typeof(Button))
                {
                    Button b = (Button)btn;
                    b.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
                    b.Dock = System.Windows.Forms.DockStyle.Fill;
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderColor = Color.White;
                    b.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
                    b.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    b.ForeColor = System.Drawing.Color.White;
                    b.Margin = new System.Windows.Forms.Padding(8);
                    b.Size = new System.Drawing.Size(96, 70);
                    //b.TabStop = false;
                    b.UseVisualStyleBackColor = false;
                }
            }
        }
        private double memoryVar = 0;
        private double leftOperand = 0;
        private double rightOperand = 0;
        private double currentResult = 0;
        private bool isEnterPressed = false;
        private void Add()
        {
            if (label1.Text[0] == '-')
            {
                leftOperand = Double.Parse(label1.Text.Split('-', '+')[1]);
                rightOperand = Double.Parse(label1.Text.Split('-', '+')[2]);
                currentResult = -leftOperand + rightOperand;
            }
            else
            {
                leftOperand = double.Parse(label1.Text.Split('+')[0]);
                rightOperand = double.Parse(label1.Text.Split('+')[1]);
                currentResult = leftOperand + rightOperand;
            }
        }
        private void Subtract()
        {
            if(label1.Text[0] == '-')
            {
                leftOperand = Convert.ToDouble(label1.Text.Split('-')[1]);
                rightOperand = Convert.ToDouble(label1.Text.Split('-')[2]);
                currentResult = -leftOperand - rightOperand;
            }
            else
            {
                leftOperand = Convert.ToDouble(label1.Text.Split('-')[0]);
                rightOperand = Convert.ToDouble(label1.Text.Split('-')[1]);
                currentResult = leftOperand - rightOperand;
            }
        }
        private void Multiply()
        {
            if (label1.Text[0] == '-')
            {
                leftOperand = Convert.ToDouble(label1.Text.Split('-', '*')[1]);
                rightOperand = Convert.ToDouble(label1.Text.Split('-', '*')[2]);
                currentResult = -leftOperand * rightOperand;
            }
            else
            {
                leftOperand = Convert.ToDouble(label1.Text.Split('*')[0]);
                rightOperand = Convert.ToDouble(label1.Text.Split('*')[1]);
                currentResult = leftOperand * rightOperand;
            }
        }
        private void Divide()
        {
            if (label1.Text[0] == '-')
            {
                leftOperand = Convert.ToDouble(label1.Text.Split('-', '/')[1]);
                rightOperand = Convert.ToDouble(label1.Text.Split('-', '/')[2]);
                currentResult = -leftOperand / rightOperand;
            }
            else
            {
                leftOperand = Convert.ToDouble(label1.Text.Split('/')[0]);
                rightOperand = Convert.ToDouble(label1.Text.Split('/')[1]);
                currentResult = leftOperand / rightOperand;
            }
        }
        private void Mod()
        {
            if (label1.Text[0] == '-')
            {
                leftOperand = Convert.ToDouble(label1.Text.Split('-', '%')[1]);
                rightOperand = Convert.ToDouble(label1.Text.Split('-', '%')[2]);
                currentResult = -leftOperand % rightOperand;
            }
            else
            {
                leftOperand = Convert.ToDouble(label1.Text.Split('%')[0]);
                rightOperand = Convert.ToDouble(label1.Text.Split('%')[1]);
                currentResult = leftOperand % rightOperand;
            }
        }

        private void digit_Click(object sender, EventArgs e)
        {
            label1.Text += ((Button)sender).Text;
        }

        private void clearButton_Clicked(object sender, EventArgs e)
        {
            label1.Text = "";
            currentResult = 0;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && !label1.Text.EndsWith(",") && label1.Text != "")
            {
                if (isEnterPressed)
                {
                    label1.Text = "";
                    isEnterPressed = false;
                }
                label1.Text += ',';
            }
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                if (isEnterPressed)
                {
                    label1.Text = "";
                    isEnterPressed = false;
                }
                label1.Text += e.KeyChar;
            }
            if (e.KeyChar == 'c')
            {
                label1.Text = "";
                currentResult = 0;
            }
            if (e.KeyChar == '+')
            {
                isEnterPressed = false;
                if (label1.Text != "" && label1.Text != ",")
                {
                    //Correct one
                    {//if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/"))
                     //{
                     //    label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "+";//Change ending operator (5+ -> 5-)
                     //}
                     //else if (label1.Text.StartsWith("-"))
                     //{

                        //}
                        //else
                        //{
                        //    label1.Text += '+';//Add after numbers
                        //}
                    }
                    if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/"))
                    {
                        label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "+";//Change ending operator (5+ -> 5-)
                    }
                    else
                    {
                        string s = label1.Text;
                        if (label1.Text.StartsWith("-"))
                        {
                            s = label1.Text.Remove(0, 1);
                        }
                        if (s.Contains('+') || s.Contains('-') || s.Contains('*') || s.Contains('/') || s.Contains('%'))
                        {
                            this.equalsButton.PerformClick();
                            isEnterPressed = false;
                        }
                        label1.Text += '+';
                    }
                }
            }
            if (e.KeyChar == '-')
            {
                isEnterPressed = false;
                if (label1.Text.Length == 0)
                {
                    label1.Text = "-";
                }
                else if (label1.Text != ",")
                {
                    if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/"))
                    {
                        label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "-";
                    }
                    else
                    {
                        string s = label1.Text;
                        if (label1.Text.StartsWith("-"))
                        {
                            s = label1.Text.Remove(0, 1);
                        }
                        if (s.Contains('+') || s.Contains('-') || s.Contains('*') || s.Contains('/') || s.Contains('%'))
                        {
                            this.equalsButton.PerformClick();
                            isEnterPressed = false;
                        }
                        label1.Text += '-';//Add after numbers
                    }
                }
            }
            if (e.KeyChar == '*')
            {
                isEnterPressed = false;
                if (label1.Text != "" && label1.Text != ",")
                {
                    if(label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/"))
                    {
                        label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "*";
                    }
                    else
                    {
                        string s = label1.Text;
                        if (label1.Text.StartsWith("-"))
                        {
                            s = label1.Text.Remove(0, 1);
                        }
                        if (s.Contains('+') || s.Contains('-') || s.Contains('*') || s.Contains('/') || s.Contains('%'))
                        {
                            this.equalsButton.PerformClick();
                            isEnterPressed = false;
                        }
                        label1.Text += '*';
                    }
                }
            }
            if (e.KeyChar == '/')
            {
                isEnterPressed = false;
                if (label1.Text != "" && label1.Text != ",")
                {
                    if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/") || label1.Text.EndsWith("%"))
                    {
                        label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "/";
                    }
                    else
                    {
                        string s = label1.Text;
                        if (label1.Text.StartsWith("-"))
                        {
                            s = label1.Text.Remove(0, 1);
                        }
                        if (s.Contains('+') || s.Contains('-') || s.Contains('*') || s.Contains('/') || s.Contains('%'))
                        {
                            this.equalsButton.PerformClick();
                            isEnterPressed = false;
                        }
                        label1.Text += '/';
                    }
                }
            }
            if (e.KeyChar == '\\')
            {
                isEnterPressed = false;
                if (label1.Text != "" && label1.Text != ",")
                {
                    if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/") || label1.Text.EndsWith("%"))
                    {
                        label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "%";
                    }
                    else
                    {
                        string s = label1.Text;
                        if (label1.Text.StartsWith("-"))
                        {
                            s = label1.Text.Remove(0, 1);
                        }
                        if (s.Contains('+') || s.Contains('-') || s.Contains('*') || s.Contains('/') || s.Contains('%'))
                        {
                            this.equalsButton.PerformClick();
                            isEnterPressed = false;
                        }
                        label1.Text += '%';
                    }
                }
            }
            if (e.KeyChar == 'm')
            {
                if (double.TryParse(label1.Text, out double result))
                {
                    memoryVar = result;
                }
                else if(memoryVar != 0 && (label1.Text.EndsWith("+") || 
                    label1.Text.EndsWith("-") || 
                    label1.Text.EndsWith("*") || 
                    label1.Text.EndsWith("/") || 
                    label1.Text.EndsWith("%")))
                {
                    if(memoryVar < 0)
                    {
                        label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1);
                    }
                    label1.Text += memoryVar;
                }
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                string s = label1.Text;//by default s contains equasion
                if (label1.Text.StartsWith("-"))
                {
                    s = label1.Text.Remove(0, 1);//if equasion starts with minus -> delete it, and check for operation without it 
                }
                if (!(s.EndsWith("+") || s.EndsWith("-") || s.EndsWith("*") || s.EndsWith("/") || s.EndsWith("%")))
                {
                    if (s.Contains('+'))
                    {
                        Add();
                    }
                    if (s.Contains('-'))
                    {
                        Subtract();
                    }
                    if (s.Contains('*'))
                    {
                        Multiply();
                    }
                    if (s.Contains('/'))
                    {
                        Divide();
                    }
                    if (s.Contains('%'))
                    {
                        Mod();
                    }
                    label1.Text = currentResult.ToString();
                    isEnterPressed = true;
                }
                return true;
            }
            else if(keyData == Keys.Back)
            {
                if (label1.Text.Length == 2 && label1.Text.StartsWith("-"))
                {
                    label1.Text = "";
                }
                if(label1.Text != "")
                {
                    label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void equals_Click(object sender, EventArgs e)
        {
            string s = label1.Text;//by default s contains equasion
            if (label1.Text.StartsWith("-"))
            {
                s = label1.Text.Remove(0, 1);//if equasion starts with minus -> delete it, and check for operation without it 
            }
            if (!(s.EndsWith("+") || s.EndsWith("-") || s.EndsWith("*") || s.EndsWith("/") || s.EndsWith("%")))
            {
                if (s.Contains('+'))
                {
                    Add();
                }
                if (s.Contains('-'))
                {
                    Subtract();
                }
                if (s.Contains('*'))
                {
                    Multiply();
                }
                if (s.Contains('/'))
                {
                    Divide();
                }
                if (s.Contains('%'))
                {
                    Mod();
                }
                label1.Text = currentResult.ToString();
                isEnterPressed = true;
            }
        }

        private void memoryButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(label1.Text, out double result))
            {
                memoryVar = result;
            }
            else if (memoryVar != 0 && (label1.Text.EndsWith("+") ||
                label1.Text.EndsWith("-") ||
                label1.Text.EndsWith("*") ||
                label1.Text.EndsWith("/") ||
                label1.Text.EndsWith("%")))
            {
                if (memoryVar < 0)
                {
                    label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1);
                }
                label1.Text += memoryVar;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length == 2 && label1.Text.StartsWith("-"))
            {
                label1.Text = "";
            }
            if (label1.Text != "")
            {
                label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1);
            }
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            isEnterPressed = false;
            if (label1.Text != "" && label1.Text != ",")
            {
                if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/"))
                {
                    label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "+";//Change ending operator (5+ -> 5-)
                }
                else
                {
                    string s = label1.Text;
                    if (label1.Text.StartsWith("-"))
                    {
                        s = label1.Text.Remove(0, 1);
                    }
                    if (s.Contains('+') || s.Contains('-') || s.Contains('*') || s.Contains('/') || s.Contains('%'))
                    {
                        this.equalsButton.PerformClick();
                        isEnterPressed = false;
                    }
                    label1.Text += '+';
                }
            }
        }

        private void minusButton_Click(object sender, EventArgs e)
        {
            isEnterPressed = false;
            if (label1.Text.Length == 0)
            {
                label1.Text = "-";
            }
            else if (label1.Text != ",")
            {
                if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/"))
                {
                    label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "-";
                }
                else
                {
                    string s = label1.Text;
                    if (label1.Text.StartsWith("-"))
                    {
                        s = label1.Text.Remove(0, 1);
                    }
                    if (s.Contains('+') || s.Contains('-') || s.Contains('*') || s.Contains('/') || s.Contains('%'))
                    {
                        this.equalsButton.PerformClick();
                        isEnterPressed = false;
                    }
                    label1.Text += '-';//Add after numbers
                }
            }
        }

        private void multButton_Click(object sender, EventArgs e)
        {
            isEnterPressed = false;
            if (label1.Text != "" && label1.Text != ",")
            {
                if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/"))
                {
                    label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "*";
                }
                else
                {
                    string s = label1.Text;
                    if (label1.Text.StartsWith("-"))
                    {
                        s = label1.Text.Remove(0, 1);
                    }
                    if (s.Contains('+') || s.Contains('-') || s.Contains('*') || s.Contains('/') || s.Contains('%'))
                    {
                        this.equalsButton.PerformClick();
                        isEnterPressed = false;
                    }
                    label1.Text += '*';
                }
            }
        }

        private void divideButton_Click(object sender, EventArgs e)
        {
            isEnterPressed = false;
            if (label1.Text != "" && label1.Text != ",")
            {
                if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/") || label1.Text.EndsWith("%"))
                {
                    label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "/";
                }
                else
                {
                    string s = label1.Text;
                    if (label1.Text.StartsWith("-"))
                    {
                        s = label1.Text.Remove(0, 1);
                    }
                    if (s.Contains('+') || s.Contains('-') || s.Contains('*') || s.Contains('/') || s.Contains('%'))
                    {
                        this.equalsButton.PerformClick();
                        isEnterPressed = false;
                    }
                    label1.Text += '/';
                }
            }
        }

        private void modButton_Click(object sender, EventArgs e)
        {
            isEnterPressed = false;
            if (label1.Text != "" && label1.Text != ",")
            {
                if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("*") || label1.Text.EndsWith("/") || label1.Text.EndsWith("%"))
                {
                    label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1) + "%";
                }
                else
                {
                    string s = label1.Text;
                    if (label1.Text.StartsWith("-"))
                    {
                        s = label1.Text.Remove(0, 1);
                    }
                    if (s.Contains('+') || s.Contains('-') || s.Contains('*') || s.Contains('/') || s.Contains('%'))
                    {
                        this.equalsButton.PerformClick();
                        isEnterPressed = false;
                    }
                    label1.Text += '%';
                }
            }
        }

        private void dotButton_Click(object sender, EventArgs e)
        {
            if (!label1.Text.EndsWith(",") && label1.Text != "")
            {
                if (isEnterPressed)
                {
                    label1.Text = "";
                    isEnterPressed = false;
                }
                label1.Text += ',';
            }
        }
    }
}
