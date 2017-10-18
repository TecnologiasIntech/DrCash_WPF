using System;
using System.Windows.Controls;

namespace DoctorCashWpf
{
    class MoneyFormatService
    {
        private string separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        
        public void AddFloat(TextBox txtbox)
        {
            if (!txtbox.Text.Contains(separator))
            {
                txtbox.Text = txtbox.Text + separator + "00";
            }
        }

        public void AddFloat(TextBlock txtbock)
        {
            if (!txtbock.Text.Contains(separator))
            {
                txtbock.Text = txtbock.Text + separator + "00";
            }
        }

        public string AddFloat(string txtbock)
        {
            if (!txtbock.Contains(separator))
            {
                txtbock = txtbock + separator + "00";
            }
            return txtbock;
        }

        public moneyComponent convertToMoneyFormat(TextBox txtBox, Action function)
        {
            var error = "";
            var item = new moneyComponent();

            if (txtBox.Text != "")
            {
                if (txtBox.Text[0] == '$')
                {
                    txtBox.Text = txtBox.Text.Remove(0, 1);
                }

                if (containLetter(txtBox.Text))
                {
                    txtBox.Text = "$0" + separator + "00";
                    error = "Only Numbers";
                }
                else if (txtBox.Text[0] == '-')
                {
                    txtBox.Text = "$0" + separator + "00";
                    error = "Negative Values";
                }
                else if (containFloat(txtBox.Text))
                {
                    txtBox.Text = "$" + changeFloat(txtBox.Text);
                }
                else
                {
                    txtBox.Text = "$" + txtBox.Text + separator + "00";
                }
            }
            else
            {
                txtBox.Text = "$0" + separator + "00";
            }

            function();

            AddFloat(txtBox);
            item.error = error;
            item.TextboxComponent = txtBox;

            return item;
        }    
        
        private bool containLetter( string text )
        {
            bool isLetter = false;

            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsLetter(text[i]))
                {
                    isLetter = true;
                }
            }

            return isLetter;
        }

        private bool containFloat(string text)
        {
            bool isFloat = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ',' || text[i] == '.')
                {
                    isFloat = true;
                }
            }

            return isFloat;
        }

        private string changeFloat(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ',' || text[i] == '.')
                {
                    text = text.Replace(text[i], Convert.ToChar(separator));
                }
            }

            return text;
        }

        public moneyComponent convertToMoneyFormat(TextBlock txtBox)
        {
            var error = "";
            var item = new moneyComponent();

            if (txtBox.Text != "")
            {
                if (txtBox.Text[0] != '$')
                {
                    if (!Char.IsNumber(txtBox.Text[0]))
                    {
                        txtBox.Text = "$0" + separator + "00";
                        error = "Only Numbers";
                    }
                    else if (txtBox.Text[0] == '-')
                    {
                        txtBox.Text = "$0"+ separator + "00";
                        error = "Negative Values";
                    }
                    else
                    {
                        txtBox.Text = "$" + txtBox.Text + separator + "00";
                    }
                }
                else
                {
                    if (!Char.IsNumber(txtBox.Text.Remove(0, 1)[0]))
                    {
                        txtBox.Text = "$0" + separator + "00";
                        error = "Only Numbers";
                    }
                }
            }
            else
            {
                txtBox.Text = "$0" + separator + "00";
            }

            AddFloat(txtBox);
            item.error = error;
            item.labelComponent = txtBox;

            return item;
        }
        public moneyComponent convertToMoneyFormat(string txt)
        {
            var error = "";
            var item = new moneyComponent();

            if (txt != "")
            {
                if (txt[0] != '$')
                {
                    if (!Char.IsNumber(txt[0]))
                    {
                        txt= "$0" + separator + "00";
                        error = "Only Numbers";
                    }
                    else if (txt[0] == '-')
                    {
                        txt = "$0" + separator + "00";
                        error = "Negative Values";
                    }
                    else
                    {
                        txt = "$" + txt + separator + "00";
                    }
                }
                else
                {
                    if (!Char.IsNumber(txt.Remove(0, 1)[0]))
                    {
                        txt = "$0" + separator + "00";
                        error = "Only Numbers";
                    }
                }
            }
            else
            {
                txt = "$0" + separator + "00";
            }

            txt=AddFloat(txt);
            item.error = error;
            item.txtComponent = txt;

            return item;
        }

        public TextBlock getMoneyFormatInZero(TextBlock txt)
        {
            txt.Text = "$0" + separator + "00";
            return txt;
        }

        public TextBox getMoneyFormatInZero(TextBox txt)
        {
            txt.Text = "$0" + separator + "00";
            return txt;
        }

        public string getMoneyFormatInZero()
        {
            return "$0" + separator + "00";
        }
    }
}
