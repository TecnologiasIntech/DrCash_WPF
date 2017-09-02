﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoctorCashWpf.Views
{
    /// <summary>
    /// Interaction logic for ClosedStatements.xaml
    /// </summary>
    public partial class ClosedStatements : UserControl
    {
        public ClosedStatements()
        {
            InitializeComponent();
        }

        reportService getreport = new reportService();

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var dato = dataGridViewClosedStatement.SelectedItem;
            DataRowView item = dataGridViewClosedStatement.SelectedItem as DataRowView;
            int dato = Convert.ToInt32(item.Row.ItemArray[0]);

            var list = getreport.getCloseTransactions(txtbox_ID.Text, fromdate.Text, todate.Text).list;

            DataTable dt = new DataTable();
            dt.Columns.Add("Transaction ID");
            dt.Columns.Add("Changed");
            dt.Columns.Add("Cash");
            dt.Columns.Add("Credit");
            dt.Columns.Add("Check");
            dt.Columns.Add("Change");

            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].trn_id == dato)
                {
                    dt.Rows.Add(list[i].trn_id, list[i].change, list[i].cash, list[i].credit, list[i].check, list[i].change);
                }
            }
            dataGridViewClosedStatement.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox_ID.Text == "" && fromdate.Text == "" && todate.Text == "")
            {

            }
            else
            {
                var list = getreport.getCloseTransactions(txtbox_ID.Text, fromdate.Text, todate.Text).list;
                dataGridViewClosedStatement.ItemsSource = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("Closed Statement ID");
                dt.Columns.Add("Register");
                dt.Columns.Add("Proceced By");
                dt.Columns.Add("Date");

                for (int i = 0; i < list.Count(); i++)
                {
                    dt.Rows.Add(list[i].closed, list[i].registerId, list[i].modifiedById, list[i].dateRegistered);
                }
                dataGridViewClosedStatement.ItemsSource = dt.DefaultView;
            }            
        }
    }
}