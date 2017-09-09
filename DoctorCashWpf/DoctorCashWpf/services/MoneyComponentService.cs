using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DoctorCashWpf
{
    class MoneyComponentService
    {
        private string separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        
        public void AddFloatToComponent(TextBox txtbox)
        {
            if (!txtbox.Text.Contains(separator))
            {
                txtbox.Text = txtbox.Text + separator + "00";
            }
        }

        public void AddFloatToComponent(TextBlock txtbock)
        {
            if (!txtbock.Text.Contains(separator))
            {
                txtbock.Text = txtbock.Text + separator + "00";
            }
        }

        public void AddFloatToComponent(string txtbock)
        {
            if (!txtbock.Contains(separator))
            {
                txtbock = txtbock + separator + "00";
            }
        }

        public moneyComponent convertComponentToMoneyFormat(TextBox txtBox, Action function)
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
                    }else if(txtBox.Text[0] == '-')
                    {
                        txtBox.Text = "$0" + separator + "00";
                        error = "Negative Values";
                    }
                    else
                    {
                        txtBox.Text = "$" + txtBox.Text;
                    }
                }
                else
                {
                    if (!Char.IsNumber(txtBox.Text.Remove(0, 1)[0]))
                    {
                        txtBox.Text = "$0"+ separator + "00";
                        error = "Only Numbers";
                    }
                }
            }
            else
            {
                txtBox.Text = "$0" + separator + "00";
            }

            function();

            AddFloatToComponent(txtBox);
            item.error = error;
            item.TextboxComponent = txtBox;

            return item;
        }        

        public moneyComponent convertComponentToMoneyFormat(TextBlock txtBox)
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
                        txtBox.Text = "$" + txtBox.Text;
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

            AddFloatToComponent(txtBox);
            item.error = error;
            item.labelComponent = txtBox;

            return item;
        }
        public moneyComponent convertComponentToMoneyFormat(string txt)
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
                        txt = "$" + txt;
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

            AddFloatToComponent(txt);
            item.error = error;
            item.txtComponent = txt;

            return item;
        }

        public TextBlock getMoneyComponentInZero(TextBlock txt)
        {
            txt.Text = "$0" + separator + "00";
            return txt;
        }

        public TextBox getMoneyComponentInZero(TextBox txt)
        {
            txt.Text = "$0" + separator + "00";
            return txt;
        }

        public string getFormatMoneyComponentInZero()
        {
            return "$0" + separator + "00";
        }
    }
}
