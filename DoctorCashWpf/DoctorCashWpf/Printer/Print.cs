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
        private class CrearTicket
        {
            //Creamos un objeto de la clase StringBuilder, en este objeto agregaremos las lineas del ticket
            StringBuilder linea = new StringBuilder();
            //Creamos una variable para almacenar el numero maximo de caracteres que permitiremos en el ticket.
            int maxCar = 48, cortar;//Total de caracteres del ticket

            //Creamos el primer metodo, este dibujara lineas guion.
            public string lineasGuio()
            {
                string lineasGuion = "";
                for (int i = 0; i < maxCar; i++)
                {
                    lineasGuion += "-";//Agregara un guio hasta llegar la numero maximo de caracteres.
                }
                return linea.AppendLine(lineasGuion).ToString(); //Devolvemos la lineaGuion
            }

            //Metodo para dibujar una linea con asteriscos
            public string lineasAsteriscos()
            {
                string lineasAsterisco = "";
                for (int i = 0; i < maxCar; i++)
                {
                    lineasAsterisco += "*";//Agregara un asterisco hasta llegar la numero maximo de caracteres.
                }
                return linea.AppendLine(lineasAsterisco).ToString(); //Devolvemos la linea con asteriscos
            }

            //Realizamos el mismo procedimiento para dibujar una lineas con el signo igual
            public string lineasIgual()
            {
                string lineasIgual = "";
                for (int i = 0; i < maxCar; i++)
                {
                    lineasIgual += "=";//Agregara un igual hasta llegar la numero maximo de caracteres.
                }
                return linea.AppendLine(lineasIgual).ToString(); //Devolvemos la lienas con iguales
            }

            //Creamos el encabezado para los articulos
            public void EncabezadoVenta()
            {
                //Escribimos los espacios para mostrar el articulo. En total tienen que ser 40 caracteres
                linea.AppendLine("ARTÍCULO            |CANT|PRECIO|IMPORTE");
            }

            //Creamos un metodo para poner el texto a la izquierda
            public void TextoIzquierda(string texto)
            {
                //Si la longitud del texto es mayor al numero maximo de caracteres permitidos, realizar el siguiente procedimiento.
                if (texto.Length > maxCar)
                {
                    int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                    for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                    {
                        //Agregamos los fragmentos que salgan del texto
                        linea.AppendLine(texto.Substring(caracterActual, maxCar));
                        caracterActual += maxCar;
                    }
                    //agregamos el fragmento restante
                    linea.AppendLine(texto.Substring(caracterActual, texto.Length - caracterActual));
                }
                else
                {
                    //Si no es mayor solo agregarlo.
                    linea.AppendLine(texto);
                }
            }

            //Creamos un metodo para poner texto a la derecha.
            public void TextoDerecha(string texto)
            {
                //Si la longitud del texto es mayor al numero maximo de caracteres permitidos, realizar el siguiente procedimiento.
                if (texto.Length > maxCar)
                {
                    int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                    for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                    {
                        //Agregamos los fragmentos que salgan del texto
                        linea.AppendLine(texto.Substring(caracterActual, maxCar));
                        caracterActual += maxCar;
                    }
                    //Variable para poner espacios restntes
                    string espacios = "";
                    //Obtenemos la longitud del texto restante.
                    for (int i = 0; i < (maxCar - texto.Substring(caracterActual, texto.Length - caracterActual).Length); i++)
                    {
                        espacios += " ";//Agrega espacios para alinear a la derecha
                    }

                    //agregamos el fragmento restante, agregamos antes del texto los espacios
                    linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
                }
                else
                {
                    string espacios = "";
                    //Obtenemos la longitud del texto restante.
                    for (int i = 0; i < (maxCar - texto.Length); i++)
                    {
                        espacios += " ";//Agrega espacios para alinear a la derecha
                    }
                    //Si no es mayor solo agregarlo.
                    linea.AppendLine(espacios + texto);
                }
            }

            //Metodo para centrar el texto
            public void TextoCentro(string texto)
            {
                if (texto.Length > maxCar)
                {
                    int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                    for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                    {
                        //Agregamos los fragmentos que salgan del texto
                        linea.AppendLine(texto.Substring(caracterActual, maxCar));
                        caracterActual += maxCar;
                    }
                    //Variable para poner espacios restntes
                    string espacios = "";
                    //sacamos la cantidad de espacios libres y el resultado lo dividimos entre dos
                    int centrar = (maxCar - texto.Substring(caracterActual, texto.Length - caracterActual).Length) / 2;
                    //Obtenemos la longitud del texto restante.
                    for (int i = 0; i < centrar; i++)
                    {
                        espacios += " ";//Agrega espacios para centrar
                    }

                    //agregamos el fragmento restante, agregamos antes del texto los espacios
                    linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
                }
                else
                {
                    string espacios = "";
                    //sacamos la cantidad de espacios libres y el resultado lo dividimos entre dos
                    int centrar = (maxCar - texto.Length) / 2;
                    //Obtenemos la longitud del texto restante.
                    for (int i = 0; i < centrar; i++)
                    {
                        espacios += " ";//Agrega espacios para centrar
                    }

                    //agregamos el fragmento restante, agregamos antes del texto los espacios
                    linea.AppendLine(espacios + texto);

                }
            }

            //Metodo para poner texto a los extremos
            public string TextoExtremos(string textoIzquierdo, string textoDerecho)
            {                
                //variables que utilizaremos
                string textoIzq, textoDer, textoCompleto = "", espacios = "";

                //Si el texto que va a la izquierda es mayor a 18, cortamos el texto.
                if (textoIzquierdo.Length > 22)
                {
                    cortar = textoIzquierdo.Length - 22;
                    textoIzq = textoIzquierdo.Remove(22, cortar);
                }
                else
                { textoIzq = textoIzquierdo; }

                textoCompleto = textoIzq;//Agregamos el primer texto.

                if (textoDerecho.Length > 24)//Si es mayor a 20 lo cortamos
                {
                    cortar = textoDerecho.Length - 24;
                    textoDer = textoDerecho.Remove(24, cortar);
                }
                else
                { textoDer = textoDerecho; }

                //Obtenemos el numero de espacios restantes para poner textoDerecho al final
                int nroEspacios = maxCar - (textoIzq.Length + textoDer.Length);
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";//agrega los espacios para poner textoDerecho al final
                }
                textoCompleto += espacios + textoDerecho;//Agregamos el segundo texto con los espacios para alinearlo a la derecha.
                linea.AppendLine(textoCompleto);//agregamos la linea al ticket, al objeto en si.

                return textoCompleto;
            }

            //Metodo para agregar los totales d ela venta
            public void AgregarTotales(string texto, decimal total)
            {
                //Variables que usaremos
                string resumen, valor, textoCompleto, espacios = "";

                if (texto.Length > 25)//Si es mayor a 25 lo cortamos
                {
                    cortar = texto.Length - 25;
                    resumen = texto.Remove(25, cortar);
                }
                else
                { resumen = texto; }

                textoCompleto = resumen;
                valor = total.ToString("#,#.00");//Agregamos el total previo formateo.

                //Obtenemos el numero de espacios restantes para alinearlos a la derecha
                int nroEspacios = maxCar - (resumen.Length + valor.Length);
                //agregamos los espacios
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";
                }
                textoCompleto += espacios + valor;
                linea.AppendLine(textoCompleto);
            }

            //Metodo para agreagar articulos al ticket de venta
            public void AgregaArticulo(string articulo, int cant, decimal precio, decimal importe)
            {
                //Valida que cant precio e importe esten dentro del rango.
                if (cant.ToString().Length <= 5 && precio.ToString().Length <= 7 && importe.ToString().Length <= 8)
                {
                    string elemento = "", espacios = "";
                    bool bandera = false;//Indicara si es la primera linea que se escribe cuando bajemos a la segunda si el nombre del articulo no entra en la primera linea
                    int nroEspacios = 0;

                    //Si el nombre o descripcion del articulo es mayor a 20, bajar a la siguiente linea
                    if (articulo.Length > 20)
                    {
                        //Colocar la cantidad a la derecha.
                        nroEspacios = (5 - cant.ToString().Length);
                        espacios = "";
                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";//Generamos los espacios necesarios para alinear a la derecha
                        }
                        elemento += espacios + cant.ToString();//agregamos la cantidad con los espacios

                        //Colocar el precio a la derecha.
                        nroEspacios = (7 - precio.ToString().Length);
                        espacios = "";
                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";//Genera los espacios
                        }
                        //el operador += indica que agregar mas cadenas a lo que ya existe.
                        elemento += espacios + precio.ToString();//Agregamos el precio a la variable elemento

                        //Colocar el importe a la derecha.
                        nroEspacios = (8 - importe.ToString().Length);
                        espacios = "";
                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";
                        }
                        elemento += espacios + importe.ToString();//Agregamos el importe alineado a la derecha

                        int caracterActual = 0;//Indicara en que caracter se quedo al bajae a la siguiente linea

                        //Por cada 20 caracteres se agregara una linea siguiente
                        for (int longitudTexto = articulo.Length; longitudTexto > 20; longitudTexto -= 20)
                        {
                            if (bandera == false)//si es false o la primera linea en recorrerer, continuar...
                            {
                                //agregamos los primeros 20 caracteres del nombre del articulos, mas lo que ya tiene la variable elemento
                                linea.AppendLine(articulo.Substring(caracterActual, 20) + elemento);
                                bandera = true;//cambiamos su valor a verdadero
                            }
                            else
                                linea.AppendLine(articulo.Substring(caracterActual, 20));//Solo agrega el nombre del articulo

                            caracterActual += 20;//incrementa en 20 el valor de la variable caracterActual
                        }
                        //Agrega el resto del fragmento del  nombre del articulo
                        linea.AppendLine(articulo.Substring(caracterActual, articulo.Length - caracterActual));

                    }
                    else //Si no es mayor solo agregarlo, sin dar saltos de lineas
                    {
                        for (int i = 0; i < (20 - articulo.Length); i++)
                        {
                            espacios += " "; //Agrega espacios para completar los 20 caracteres
                        }
                        elemento = articulo + espacios;

                        //Colocar la cantidad a la derecha.
                        nroEspacios = (5 - cant.ToString().Length);// +(20 - elemento.Length);
                        espacios = "";
                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";
                        }
                        elemento += espacios + cant.ToString();

                        //Colocar el precio a la derecha.
                        nroEspacios = (7 - precio.ToString().Length);
                        espacios = "";
                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";
                        }
                        elemento += espacios + precio.ToString();

                        //Colocar el importe a la derecha.
                        nroEspacios = (8 - importe.ToString().Length);
                        espacios = "";
                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";
                        }
                        elemento += espacios + importe.ToString();

                        linea.AppendLine(elemento);//Agregamos todo el elemento: nombre del articulo, cant, precio, importe.
                    }
                }
                else
                {
                    linea.AppendLine("Los valores ingresados para esta fila");
                    linea.AppendLine("superan las columnas soportdas por éste.");
                    throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportdas por éste.");
                }
            }

            //Metodos para enviar secuencias de escape a la impresora
            //Para cortar el ticket
            public void CortaTicket()
            {
                linea.AppendLine("\x1B" + "m"); //Caracteres de corte. Estos comando varian segun el tipo de impresora
                linea.AppendLine("\x1B" + "d" + "\x09"); //Avanza 9 renglones, Tambien varian
            }
            //Para abrir el cajon
            public void AbreCajon()
            {
                //Estos tambien varian, tienen que ever el manual de la impresora para poner los correctos.
                linea.AppendLine("\x1B" + "p" + "\x00" + "\x0F" + "\x96"); //Caracteres de apertura cajon 0
                                                                           //linea.AppendLine("\x1B" + "p" + "\x01" + "\x0F" + "\x96"); //Caracteres de apertura cajon 1
            }
            //Para mandara a imprimir el texto a la impresora que le indiquemos.
            public void ImprimirTicket(string impresora)
            {
                //Este metodo recibe el nombre de la impresora a la cual se mandara a imprimir y el texto que se imprimira.
                //Usaremos un código que nos proporciona Microsoft. https://support.microsoft.com/es-es/kb/322091

                RawPrinterHelper.SendStringToPrinter(impresora, linea.ToString()); //Imprime texto.
                linea.Clear();//Al cabar de imprimir limpia la linea de todo el texto agregado.
            }
        }
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
            //para usar las otras funciones del metodo ticket
            CrearTicket ticket = new CrearTicket();

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
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Patient Name", transaction.patientFirstName)));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Amount Chanrge", transaction.amountCharged.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Cash", transaction.cash.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Credit", transaction.credit.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Check", transaction.check.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Check #", transaction.checkNumber.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Copayment", transaction.copayment.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Selfpay", transaction.selfPay.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Deductible", transaction.deductible.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Labs", transaction.labs.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Other", transaction.other.ToString())));            
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Right());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Total", transaction.total_cash.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Amount", transaction.total_amount.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Change", transaction.change.ToString())));                    


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

            //para usar las otras funciones del metodo ticket
            CrearTicket ticket = new CrearTicket();
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
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Cash", transaction.cash.ToString())));                     


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
            //para usar las otras funciones del metodo ticket
            CrearTicket ticket = new CrearTicket();
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
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Information\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("100's", transaction.clt_100_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("50's", transaction.clt_50_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("20's", transaction.clt_20_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("10's", transaction.clt_10_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("5's", transaction.clt_5_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("1's", transaction.clt_1_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Total Cash", transaction.clt_total_cash.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Total Credit", transaction.clt_total_credit.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("total Check", transaction.clt_total_check.ToString())));                               


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

            //para usar las otras funciones del metodo ticket
            CrearTicket ticket = new CrearTicket();

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
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Totals\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Initial Cash", transaction.clt_initial_cash.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Amount Charged", transaction.clt_checks_amount.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Cash", transaction.clt_total_cash.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Credit Card", transaction.clt_total_credit.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Check", transaction.clt_total_check.ToString())));            
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Report\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("100's", transaction.clt_100_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("50's", transaction.clt_50_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("20's", transaction.clt_20_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("10's", transaction.clt_10_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("5's", transaction.clt_5_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("1's", transaction.clt_1_bills.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Total Cash", transaction.clt_total_cash.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Total Credit", transaction.clt_total_credit.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("total Check", transaction.clt_total_check.ToString())));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.TextoExtremos("Balance", transaction.clt_balance.ToString())));


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



