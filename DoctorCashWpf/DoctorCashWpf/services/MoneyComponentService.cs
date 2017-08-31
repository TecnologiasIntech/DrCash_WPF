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

        public void AddFloatToComponent(TextBox txtbox)
        {
            if (!txtbox.Text.Contains('.'))
            {
                txtbox.Text = txtbox.Text + ".00";
            }
        }

        public void AddFloatToComponent(TextBlock txtbock)
        {
            if (!txtbock.Text.Contains('.'))
            {
                txtbock.Text = txtbock.Text + ".00";
            }
        }

        public void convertComponentToMoneyFormat(TextBox txtBox, Action function)
        {
            if (txtBox.Text != "")
            {
                if (txtBox.Text[0] != '$')
                {
                    if (Char.IsNumber(txtBox.Text[0]) && txtBox.Text[0] != '-')
                    {
                        txtBox.Text = "$" + txtBox.Text;
                    }
                    else
                    {
                        txtBox.Text = "$0.00";
                    }
                }
                else
                {
                    if (!Char.IsNumber(txtBox.Text.Remove(0, 1)[0]))
                    {
                        txtBox.Text = "$0.00";
                    }
                }
            }
            else
            {
                txtBox.Text = "$0.00";
            }

            function();

            AddFloatToComponent(txtBox);
        }

        public void convertComponentToMoneyFormat(TextBlock txtBox)
        {
            if (txtBox.Text != "")
            {
                if (txtBox.Text[0] != '$')
                {
                    if (Char.IsNumber(txtBox.Text[0]) || txtBox.Text[0] == '-')
                    {
                        txtBox.Text = "$" + txtBox.Text;
                    }
                    else
                    {
                        txtBox.Text = "$0.00";
                    }
                }
                else
                {
                    if (!Char.IsNumber(txtBox.Text.Remove(0, 1)[0]))
                    {
                        txtBox.Text = "$0.00";
                    }
                }
            }
            else
            {
                txtBox.Text = "$0.00";
            }

            AddFloatToComponent(txtBox);
        }

    }
}
