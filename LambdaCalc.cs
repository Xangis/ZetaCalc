using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LambdaCalc
{
    public partial class LambdaCalc : Form
    {
        List<Decimal> history = new List<Decimal>();
        Decimal value;
        Decimal previousValue;
        Boolean decimalActive;
        Int32 decimalPlaces;
        Mode mode = Mode.None;

        enum Mode
        {
            None,
            Multiply,
            Divide,
            Add,
            Subtract,
            SquareRoot,
            Power,
            Invert
        }

        public LambdaCalc()
        {
            InitializeComponent();
            this.KeyPress += new KeyPressEventHandler(LambdaCalc_KeyPress);
            this.KeyPreview = true;
            this.AcceptButton = this.btnEquals;
        }

        void NumberEntered(int number)
        {
            if (value == 0)
            {
                if (!decimalActive)
                {
                    value = number;
                }
                else
                {
                    value = (Decimal)number / (Decimal)(Math.Pow(10.0, decimalPlaces));
                    decimalPlaces++;
                }
            }
            else
            {
                if (!decimalActive)
                {
                    value = value * 10 + number;
                }
                else
                {
                    value = (Decimal)((Double)value + ((Double)number / (Math.Pow(10.0, (Double)decimalPlaces))));
                    decimalPlaces++;
                }
            }
            UpdateText();
        }

        private void UpdateText()
        {
            if (value == 0)
            {
                if (decimalActive)
                {
                    string tmpText = "0.";
                    for (int i = 1; i < decimalPlaces; i++)
                    {
                        tmpText += "0";
                    }
                    txtValue.Text = tmpText;
                }
                else
                {
                    txtValue.Text = value.ToString();
                }
            }
            else
            {
                txtValue.Text = value.ToString();
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            NumberEntered(1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            NumberEntered(2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            NumberEntered(3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            NumberEntered(4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            NumberEntered(5);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            NumberEntered(6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            NumberEntered(7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            NumberEntered(8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            NumberEntered(9);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            NumberEntered(0);
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            if (!decimalActive)
            {
                decimalActive = true;
                decimalPlaces = 1;
                txtValue.Text += ".";
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            PerformOperation();
        }

        private void PerformOperation()
        {
            Decimal tmp;
            switch (mode)
            {
                case Mode.None:
                    break;
                case Mode.Add:
                    tmp = value;
                    value = previousValue + value;
                    previousValue = tmp;
                    break;
                case Mode.Divide:
                    tmp = value;
                    value = previousValue / value;
                    previousValue = tmp;
                    break;
                case Mode.Multiply:
                    tmp = value;
                    value = previousValue * value;
                    previousValue = tmp;
                    break;
                case Mode.Subtract:
                    tmp = value;
                    value = previousValue - value;
                    previousValue = tmp;
                    break;
                case Mode.Invert:
                    tmp = value;
                    if (value != 0)
                    {
                        value = 1 / value;
                        previousValue = tmp;
                    }
                    break;
                case Mode.Power:
                    tmp = value;
                    value = (Decimal)Math.Pow((Double)previousValue, (Double)value);
                    previousValue = tmp;
                    break;
                case Mode.SquareRoot:
                    previousValue = value;
                    value = (Decimal)Math.Sqrt((Double)value);
                    break;
            }
            mode = Mode.None;
            decimalPlaces = 0;
            decimalActive = false;
            UpdateText();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (mode != Mode.None)
            {
                PerformOperation();
            }
            previousValue = value;
            Reset();
            mode = Mode.Add;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (mode != Mode.None)
            {
                PerformOperation();
            }
            previousValue = value;
            Reset();
            mode = Mode.Subtract;
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            if (mode != Mode.None)
            {
                PerformOperation();
            }
            previousValue = value;
            Reset();
            mode = Mode.Divide;
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            if (mode != Mode.None)
            {
                PerformOperation();
            }
            previousValue = value;
            Reset();
            mode = Mode.Multiply;
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            if (mode != Mode.None)
            {
                PerformOperation();
            }
            previousValue = value;
            Reset();
            mode = Mode.Power;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Reset();
            mode = Mode.None;
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            mode = Mode.SquareRoot;
            if (mode != Mode.None)
            {
                PerformOperation();
            }
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            mode = Mode.Invert; 
            if (mode != Mode.None)
            {
                PerformOperation();
            }
        }

        private void Reset()
        {
            UpdateHistory(value);
            value = 0;
            decimalActive = false;
            decimalPlaces = 0;
            UpdateText();
        }

        void UpdateHistory(Decimal updateVal)
        {
            history.Add(value);
            if (history.Count > 8)
            {
                history.RemoveRange(0, (history.Count - 8));
            }
            if (history.Count > 0)
            {
                history1ToolStripMenuItem.Text = history[0].ToString();
                history1ToolStripMenuItem.Visible = true;
            }
            else
            {
                history1ToolStripMenuItem.Visible = false;
            }
            if (history.Count > 1)
            {
                history2ToolStripMenuItem.Text = history[1].ToString();
                history2ToolStripMenuItem.Visible = true;
            }
            else
            {
                history2ToolStripMenuItem.Visible = false;
            }
            if (history.Count > 2)
            {
                history3ToolStripMenuItem.Text = history[2].ToString();
                history3ToolStripMenuItem.Visible = true;
            }
            else
            {
                history3ToolStripMenuItem.Visible = false;
            }
            if (history.Count > 3)
            {
                history4ToolStripMenuItem.Text = history[3].ToString();
                history4ToolStripMenuItem.Visible = true;
            }
            else
            {
                history4ToolStripMenuItem.Visible = false;
            }
            if (history.Count > 4)
            {
                history5ToolStripMenuItem.Text = history[4].ToString();
                history5ToolStripMenuItem.Visible = true;
            }
            else
            {
                history5ToolStripMenuItem.Visible = false;
            }
            if (history.Count > 5)
            {
                history6ToolStripMenuItem.Text = history[5].ToString();
                history6ToolStripMenuItem.Visible = true;
            }
            else
            {
                history6ToolStripMenuItem.Visible = false;
            }
            if (history.Count > 6)
            {
                history7ToolStripMenuItem.Text = history[6].ToString();
                history7ToolStripMenuItem.Visible = true;
            }
            else
            {
                history7ToolStripMenuItem.Visible = false;
            }
            if (history.Count > 7)
            {
                history8ToolStripMenuItem.Text = history[7].ToString();
                history8ToolStripMenuItem.Visible = true;
            }
            else
            {
                history8ToolStripMenuItem.Visible = false;
            }
            Refresh();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lambda Calc is Copyright 2013-2020 Lambda Centauri\nLambda Calc is free software.\n\nWritten by Jason Champion\n\nhttps://lambdacentauri.com", "About Lambda Calc 1.02");
        }

        private void history1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value = history[0];
            UpdateText();
            decimalActive = false;
            decimalPlaces = 0;
        }

        private void history2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value = history[1];
            UpdateText();
            decimalActive = false;
            decimalPlaces = 0;
        }

        private void history3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value = history[2];
            UpdateText();
            decimalActive = false;
            decimalPlaces = 0;
        }

        private void history4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value = history[3];
            UpdateText();
            decimalActive = false;
            decimalPlaces = 0;
        }

        private void history5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value = history[4];
            UpdateText();
            decimalActive = false;
            decimalPlaces = 0;
        }

        private void history6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value = history[5];
            UpdateText();
            decimalActive = false;
            decimalPlaces = 0;
        }

        private void history7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value = history[6];
            UpdateText();
            decimalActive = false;
            decimalPlaces = 0;
        }

        private void history8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value = history[7];
            UpdateText();
            decimalActive = false;
            decimalPlaces = 0;
        }

        void LambdaCalc_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                    btn0_Click(sender, e);
                    e.Handled = true;
                    break;
                case '1':
                    btn1_Click(sender, e);
                    e.Handled = true;
                    break;
                case '2':
                    btn2_Click(sender, e);
                    e.Handled = true;
                    break;
                case '3':
                    btn3_Click(sender, e);
                    e.Handled = true;
                    break;
                case '4':
                    btn4_Click(sender, e);
                    e.Handled = true;
                    break;
                case '5':
                    btn5_Click(sender, e);
                    e.Handled = true;
                    break;
                case '6':
                    btn6_Click(sender, e);
                    e.Handled = true;
                    break;
                case '7':
                    btn7_Click(sender, e);
                    e.Handled = true;
                    break;
                case '8':
                    btn8_Click(sender, e);
                    e.Handled = true;
                    break;
                case '9':
                    btn9_Click(sender, e);
                    e.Handled = true;
                    break;
                case '.':
                    btnPeriod_Click(sender, e);
                    e.Handled = true;
                    break;
                case '+':
                    btnPlus_Click(sender, e);
                    e.Handled = true;
                    break;
                case '=':
                    btnEquals_Click(sender, e);
                    e.Handled = true;
                    break;
                case '-':
                    btnMinus_Click(sender, e);
                    e.Handled = true;
                    break;
                case '*':
                    btnMultiply_Click(sender, e);
                    e.Handled = true;
                    break;
                case '/':
                    btnDivide_Click(sender, e);
                    e.Handled = true;
                    break;
            }
        }
    }
}
