using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterUtility;
using System.Collections;
using System.Drawing;
using System.IO;

namespace DoctorCashWpf.Printer
{
    class Print
    {

        public void print()
        {
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes(@"D:\logo.bmp");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Invoice\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Invoice No. : 12345\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date        : 12/12/2015\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Itm                      Qty      Net   Total\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "item 1", 12, 11, 144.00));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "item 2", 12, 11, 144.00));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Right());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Total\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("288.00\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128("12345"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print("12345", PrinterUtility.Enums.QrCodeSize.Grande));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, "-------------------Thank you for coming------------------------\n");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());


            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public void printCashIn(transaction transaction)
        {
            //primera parte del ticket
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes(@"D:\logo.bmp");            
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));
            
            //donde ira todo lo que le vamos a mandar a imprimir
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Cash In\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Patient Name", "", "", transaction.patientFirstName));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n","Amount Chanrge" , "", "", transaction.amountCharged));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Cash", "", "", transaction.cash));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Credit", "", "", transaction.credit));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Check", "", "", transaction.check));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Check #", "", "", transaction.checkNumber));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Copayment", "", "", transaction.copayment));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "SelfPay", "", "", transaction.selfPay));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Deductible", "", "", transaction.deductible));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Laps", "", "", transaction.labs));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Other", "", "", transaction.other));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Right());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Total  "+transaction.total_cash+"\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Amount  " + transaction.total_amount + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Change  " + transaction.change + "\n"));            


            //parte final de ticket
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());            
            
            
            //numero aleatorio para el codigo QR y Codigo de barra
            Random rnd = new Random();
            int number = rnd.Next();
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(number+"\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128(number.ToString()));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print(number.ToString(), PrinterUtility.Enums.QrCodeSize.Grande));

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Printed by: " + userInformation.user.usr_Username + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date: " + DateTime.Now.ToLongDateString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public void printCashOut(transaction transaction)
        {
            //primera parte del ticket
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes(@"D:\logo.bmp");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));

            //donde ira todo lo que le vamos a mandar a imprimir
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Cash Out\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());            
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Cash", "", "", transaction.cash));            


            //parte final de ticket
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());


            //numero aleatorio para el codigo QR y Codigo de barra
            Random rnd = new Random();
            int number = rnd.Next();
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(number + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128(number.ToString()));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print(number.ToString(), PrinterUtility.Enums.QrCodeSize.Grande));

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Printed by: " + userInformation.user.usr_Username + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date: " + DateTime.Now.ToLongDateString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public void printCloseDate(closeDate transaction)
        {
            //primera parte del ticket
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes(@"D:\logo.bmp");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));

            //donde ira todo lo que le vamos a mandar a imprimir
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Close Date\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Information\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "100´s", "", "", transaction.clt_100_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "50´s", "", "", transaction.clt_50_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "20´s", "", "", transaction.clt_20_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "10´s", "", "", transaction.clt_10_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "5´s", "", "", transaction.clt_5_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "1´s #", "", "", transaction.clt_1_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Total Cash", "", "", transaction.clt_total_cash));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Total Credit", "", "", transaction.clt_total_credit));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Total Check", "", "", transaction.clt_total_check));                       


            //parte final de ticket
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());


            //numero aleatorio para el codigo QR y Codigo de barra
            Random rnd = new Random();
            int number = rnd.Next();
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(number + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128(number.ToString()));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print(number.ToString(), PrinterUtility.Enums.QrCodeSize.Grande));

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Printed by: " + userInformation.user.usr_Username + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date: " + DateTime.Now.ToLongDateString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public void printClosedStatement(closeDate transaction)
        {
            //primera parte del ticket
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes(@"D:\logo.bmp");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));

            //donde ira todo lo que le vamos a mandar a imprimir
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Closing Statement\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Totals\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Initial Cash", "", "", transaction.clt_initial_cash));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Amount Charged", "", "", transaction.clt_checks_amount));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Cash", "", "", transaction.clt_total_cash));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Credit Card", "", "", transaction.clt_total_credit));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Check", "", "", transaction.clt_total_check));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Report\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "100´s", "", "", transaction.clt_100_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "50´s", "", "", transaction.clt_50_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "20´s", "", "", transaction.clt_20_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "10´s", "", "", transaction.clt_10_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "5´s", "", "", transaction.clt_5_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "1´s #", "", "", transaction.clt_1_bills));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Cash", "", "", transaction.clt_total_cash));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Credit Card", "", "", transaction.clt_total_credit));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "Check", "", "", transaction.clt_total_check));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "BAlance", "", "", transaction.clt_balance));


            //parte final de ticket
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());


            //numero aleatorio para el codigo QR y Codigo de barra
            Random rnd = new Random();
            int number = rnd.Next();
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(number + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128(number.ToString()));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print(number.ToString(), PrinterUtility.Enums.QrCodeSize.Grande));

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Printed by: " + userInformation.user.usr_Username + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date: " + DateTime.Now + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public byte[] CutPage()
        {
            List<byte> oby = new List<byte>();
            oby.Add(Convert.ToByte(Convert.ToChar(0x1D)));
            oby.Add(Convert.ToByte('V'));
            oby.Add((byte)66);
            oby.Add((byte)3);
            return oby.ToArray();
        }
        public byte[] GetLogo(string LogoPath)
        {
            List<byte> byteList = new List<byte>();
            if (!File.Exists(LogoPath))
                return null;
            BitmapData data = GetBitmapData(LogoPath);
            BitArray dots = data.Dots;
            byte[] width = BitConverter.GetBytes(data.Width);

            int offset = 0;
            MemoryStream stream = new MemoryStream();
            // BinaryWriter bw = new BinaryWriter(stream);
            byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
            //bw.Write((char));
            byteList.Add(Convert.ToByte('@'));
            //bw.Write('@');
            byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
            // bw.Write((char)0x1B);
            byteList.Add(Convert.ToByte('3'));
            //bw.Write('3');
            //bw.Write((byte)24);
            byteList.Add((byte)24);
            while (offset < data.Height)
            {
                byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
                byteList.Add(Convert.ToByte('*'));
                //bw.Write((char)0x1B);
                //bw.Write('*');         // bit-image mode
                byteList.Add((byte)33);
                //bw.Write((byte)33);    // 24-dot double-density
                byteList.Add(width[0]);
                byteList.Add(width[1]);
                //bw.Write(width[0]);  // width low byte
                //bw.Write(width[1]);  // width high byte

                for (int x = 0; x < data.Width; ++x)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        byte slice = 0;
                        for (int b = 0; b < 8; ++b)
                        {
                            int y = (((offset / 8) + k) * 8) + b;
                            // Calculate the location of the pixel we want in the bit array.
                            // It'll be at (y * width) + x.
                            int i = (y * data.Width) + x;

                            // If the image is shorter than 24 dots, pad with zero.
                            bool v = false;
                            if (i < dots.Length)
                            {
                                v = dots[i];
                            }
                            slice |= (byte)((v ? 1 : 0) << (7 - b));
                        }
                        byteList.Add(slice);
                        //bw.Write(slice);
                    }
                }
                offset += 24;
                byteList.Add(Convert.ToByte(0x0A));
                //bw.Write((char));
            }
            // Restore the line spacing to the default of 30 dots.
            byteList.Add(Convert.ToByte(0x1B));
            byteList.Add(Convert.ToByte('3'));
            //bw.Write('3');
            byteList.Add((byte)30);
            return byteList.ToArray();
            //bw.Flush();
            //byte[] bytes = stream.ToArray();
            //return logo + Encoding.Default.GetString(bytes);
        }

        public BitmapData GetBitmapData(string bmpFileName)
        {
            using (var bitmap = (Bitmap)Bitmap.FromFile(bmpFileName))
            {
                var threshold = 127;
                var index = 0;
                double multiplier = 570; // this depends on your printer model. for Beiyang you should use 1000
                double scale = (double)(multiplier / (double)bitmap.Width);
                int xheight = (int)(bitmap.Height * scale);
                int xwidth = (int)(bitmap.Width * scale);
                var dimensions = xwidth * xheight;
                var dots = new BitArray(dimensions);

                for (var y = 0; y < xheight; y++)
                {
                    for (var x = 0; x < xwidth; x++)
                    {
                        var _x = (int)(x / scale);
                        var _y = (int)(y / scale);
                        var color = bitmap.GetPixel(_x, _y);
                        var luminance = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                        dots[index] = (luminance < threshold);
                        index++;
                    }
                }

                return new BitmapData()
                {
                    Dots = dots,
                    Height = (int)(bitmap.Height * scale),
                    Width = (int)(bitmap.Width * scale)
                };
            }
        }

        public class BitmapData
        {
            public BitArray Dots
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int Width
            {
                get;
                set;
            }
        }
    }
}



