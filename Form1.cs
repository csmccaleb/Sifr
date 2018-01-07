using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Sifr
{
    public partial class Sifr : MetroFramework.Forms.MetroForm
    {
        public Sifr()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // more information on Rijndael here https://en.wikipedia.org/wiki/Advanced_Encryption_Standard
            sifrObj = Rijndael.Create();
        }

        string sifrData;
        byte[] sifrbytes;
        byte[] plainbytes;
        byte[] plainbytes2;
        byte[] plainKey;

        SymmetricAlgorithm sifrObj;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sifrData = textBox1.Text;
            plainbytes = Encoding.ASCII.GetBytes(sifrData);
            //adjust this key to whatever you want
            plainKey = Encoding.ASCII.GetBytes("0123456789abcdef");
            sifrObj.Key = plainKey;
            sifrObj.Mode = CipherMode.CBC;
            sifrObj.Padding = PaddingMode.ISO10126;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, sifrObj.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(plainbytes, 0, plainbytes.Length);
            cs.Close();
            sifrbytes = ms.ToArray();
            ms.Close();
            textBox2.Text = Encoding.ASCII.GetString(sifrbytes);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.MemoryStream ms1 = new System.IO.MemoryStream(sifrbytes);
            CryptoStream cs1 = new CryptoStream(ms1, sifrObj.CreateDecryptor(), CryptoStreamMode.Read);

            cs1.Read(sifrbytes, 0, sifrbytes.Length);
            plainbytes2 = ms1.ToArray();
            cs1.Close();
            ms1.Close();

            textBox3.Text = Encoding.ASCII.GetString(plainbytes2);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
