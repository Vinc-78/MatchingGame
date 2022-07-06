using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Media;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        private SoundPlayer SoundPlay;

        Random random = new Random();

        List<string> icone = new List<string>()
        { "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z" };

        
        //riferimento alle etichette per il confronto( gestito con time1 )
        Label firstClicked = null;
        Label secondClicked = null;


        public void AssegnaIconaAllaCella() 
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {

                Label iconLabel = control as Label;  //converte la variabile control in un'etichetta denominata iconLabel

                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icone.Count); //uso icon.Count per gestire l'eliminazione dell'elemento scelto
                                                                 // e la riduzione della lista evitando di prendere sempre gli stessi
                    iconLabel.Text = icone[randomNumber];

                    iconLabel.ForeColor = iconLabel.BackColor; // qui per nascondere gli do lo stesso colore dello sfondo
                    icone.RemoveAt(randomNumber);
                }


            }
        
        }
        public Form1()
        {
            InitializeComponent();
            SoundPlay = new SoundPlayer("Ring08.wav");
            AssegnaIconaAllaCella();
            SoundPlay.Play();  // all'avvio fa partire la musica
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Al clik su una label verifica se colorata di nero non fa nulla altrimenti la colora
        private void Label_1(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;  //definisco gli ascolti sulle label

            if (clickedLabel != null)
            {
               
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                VerificaSeHaiVinto(); // verifica se tutte le celle sono attive => hai finito

                //!!!!!!!!!!!!!!!!
                //se i due valori sono uguali non esegui il tick di timer, ma return
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;  //annullando i due clickedLabel
                    secondClicked = null;
                    return;
                }

                timer1.Start();  //Avvio il timer di 750 mms che cancella le celle
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           //ferma il timer
            timer1.Stop();

            // toglie il colore alle icone
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

           //annulla le due variabili di appoggio
            firstClicked = null;
            secondClicked = null;

        }

        public void VerificaSeHaiVinto()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            { 
               Label label = control as Label;

                if (label != null)
                {
                    if (label.ForeColor == label.BackColor)
                    { return; }
                }
            }

            MessageBox.Show("!!!  GRANDE HAI VINTO !!! ");
            Close();  // per ora chiude l'app, ma da valutare un reset e un restart del gioco
         
        
        }
    }
}
