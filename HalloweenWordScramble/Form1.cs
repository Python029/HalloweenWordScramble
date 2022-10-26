using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace HalloweenScramble
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string word = "";
        List<string> wordslist = new List<string>();
        string addword = "";
        Random rnd = new Random();
        List<char> w = new List<char>();
        int thing = 0;
        string scramble;
        private void CreatePDF()
        {
            #region Text
            //Create PDF Document
            PdfDocument document = new PdfDocument();
            //You will have to add Page in PDF Document
            PdfPage page = document.AddPage();
            //For drawing in PDF Page you will nedd XGraphics Object
            XGraphics gfx = XGraphics.FromPdfPage(page);
            //For Test you will have to define font to be used
            XFont font = new XFont("Viner Hand ITC", 37, XFontStyle.Bold);
            //Finally use XGraphics & font object to draw text in PDF Page
            gfx.DrawString("Halloween Word Scramble", font, XBrushes.Black,
            new XRect(0, -280, page.Width, page.Height), XStringFormats.Center);
            //Add Words
            XFont wordfont = new XFont("Viner Hand ITC", 20, XFontStyle.Bold);
            for (int i=0;i<wordslist.Count;i++)
            {
                addword = $"{i+1}. {wordslist[i]}";
                
                gfx.DrawString(addword, wordfont, XBrushes.Black,
                new XRect(90, -200+(45*i), page.Width, page.Height), XStringFormats.CenterLeft);
                gfx.DrawString("_____________________", wordfont, XBrushes.Black,
                new XRect(-85, -200 + (45 * i), page.Width, page.Height), XStringFormats.CenterRight);

            }
            #endregion
            #region Image
            
            #endregion

            //Specify file name of the PDF file
            string filename = "FirstPDFDocument.pdf";
            //Save PDF File
            document.Save(filename);
            //Load PDF File for viewing
            Process.Start(filename);
        }
        #region Button
        private void Button()
        {
            scramble = "";
            //Grab Word from Text Box
            word = txtWords.Text;
            word.ToCharArray();
            for (int i = 0; i < word.Length; i++)
            {
                w.Add(word[i]);
            }
            //MessageBox.Show(w[3].ToString());
            //Scramble Word
            for (int i = 0; i < word.Length; i++)
            {
                thing = rnd.Next(0, w.Count);
                scramble += w[thing];
                w.RemoveAt(thing);
            }
            if (scramble.Length == word.Length)
            {
                wordslist.Add(scramble);
            }
            txtWords.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Button();
            txtWords.Focus();
            txtWords.Clear();
        }
        #endregion

        #region Dropdown
        private void exportPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatePDF();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Do you want to save your word list to a PDF?", "Save Words?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                CreatePDF();
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                Application.Exit();
            }   
        }
        #endregion
    }
}