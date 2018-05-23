using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace DescifrarCifrar
{
    public partial class Form1 : Form
    {
        FileStream fs;
        DESCryptoServiceProvider cryptic;
        CryptoStream crStream;
        byte[] bytes;
        public Form1()
        {
            InitializeComponent();
        }

        
        

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if(openFileDialog.ShowDialog() == DialogResult.OK )
            {
                fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.ReadWrite);
                bytes = new byte[fs.Length];           
                

            }
            MessageBox.Show("Archivo abierto");


        }

        private void cifrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cryptic = new DESCryptoServiceProvider();
                cryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                cryptic.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");

                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    int n = fs.Read(bytes, numBytesRead, numBytesToRead);

                    if (n == 0)
                        break;
                    numBytesRead += n;
                    numBytesToRead -= n;

                }

                crStream = new CryptoStream(fs, cryptic.CreateEncryptor(), CryptoStreamMode.Write);
                fs.Seek(0, SeekOrigin.Begin);
                crStream.Write(bytes,0, (int)fs.Length);


            }
            catch (IOException error)
            {
                MessageBox.Show("Error " + error.Message);
            }
            finally
            {
                crStream.Close();
                fs.Close();
            }

            MessageBox.Show("Proceso Terminado");

        }

        private void descifrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cryptic = new DESCryptoServiceProvider();
                cryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                cryptic.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");

                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    int n = fs.Read(bytes, numBytesRead, numBytesToRead);

                    if (n == 0)
                        break;
                    numBytesRead += n;
                    numBytesToRead -= n;

                }

                crStream = new CryptoStream(fs, cryptic.CreateDecryptor(), CryptoStreamMode.Write);
                fs.Seek(0, SeekOrigin.Begin);
                crStream.Write(bytes, 0, (int)fs.Length);


            }
            catch (IOException error)
            {
                MessageBox.Show("Error " + error.Message);
            }
            finally
            {
                crStream.Close();
                fs.Close();
            }

            MessageBox.Show("Se ha descifrado el archivo");

        }
    }
}
 