using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public class Telnokok
        {
            public string nev;
            public string kedet;
            public string vege;
            public string part;
            public string szuletett;
            public string meghalt;
            

            public Telnokok(string row)
            {
                string[] seged = row.Split('|');
                this.nev = seged[0];
                this.kedet = seged[1];
                this.vege = seged[2];
                this.part = seged[3];
                this.szuletett = seged[4];
                this.meghalt = seged[5];
               
            }
        }
        static List<Telnokok> elnokok = new List<Telnokok>();

        public Form1()
        {


            InitializeComponent();
            Updategrid();
            Beolvas();
            Iras();
            Adattorles();
        }

        private void Beolvas()
        {
            dataGrid.Rows.Clear();
            elnokok.Clear();
            try
            {
                StreamReader file = new StreamReader("elnokok.txt");



                //file.ReadLine();
                while (!file.EndOfStream)
                {
                    elnokok.Add(new Telnokok(file.ReadLine()));
                }
                file.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Hiba a készlet beolvasása közben", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            Updategrid();

        }

        private void Iras()
        {
            try
            {

                StreamWriter file = new StreamWriter("elnokok.txt", true);

                
                file.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}",
                    textBox1.Text,
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text,
                    textBox6.Text
                    );

                file.Close();
                MessageBox.Show("Autosave");
            }
            catch (IOException ex)
            {
                MessageBox.Show("HIBA: " + ex.Message);
            }
        }



        private void Adattorles()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void Updategrid()
        {
            dataGrid.Rows.Clear();

            // Sorok hozzáadása a DataGridView-hez
            elnokok.ForEach(item =>
            {
                dataGrid.Rows.Add();
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[0].Value = item.nev;
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[1].Value = item.kedet;
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[2].Value = item.vege;
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Value = item.part;
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[4].Value = item.szuletett;
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[5].Value = item.meghalt;
            });

            // Ne legyen semmi kijelölve alapértelmezetten
            dataGrid.ClearSelection();
        }

        private void dataGrid_SelectionChanged(object sender, EventArgs e)
        {
            // Ha nincs sor kiválasztva, ne csináljon semmit
            if (dataGrid.CurrentRow != null)
            {
                int index = dataGrid.CurrentRow.Index;

                // Ha van kiválasztott sor, másold át az adatokat a textBoxokba
                if (index > -1)
                {
                    textBox1.Text = dataGrid.Rows[index].Cells[0].Value.ToString();
                    textBox2.Text = dataGrid.Rows[index].Cells[1].Value.ToString();
                    textBox3.Text = dataGrid.Rows[index].Cells[2].Value.ToString();
                    textBox4.Text = dataGrid.Rows[index].Cells[3].Value.ToString();
                    textBox5.Text = dataGrid.Rows[index].Cells[4].Value.ToString();
                    textBox6.Text = dataGrid.Rows[index].Cells[5].Value.ToString();
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Iras();
            Beolvas();
            Adattorles();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // módositás 


            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Nem adtál meg minden adatot");
            }
            else
            {
                

                elnokok[dataGrid.CurrentRow.Index].nev = textBox1.Text;
                elnokok[dataGrid.CurrentRow.Index].kedet = textBox2.Text;
                elnokok[dataGrid.CurrentRow.Index].vege = textBox3.Text;
                elnokok[dataGrid.CurrentRow.Index].part = textBox4.Text;
                elnokok[dataGrid.CurrentRow.Index].szuletett = textBox5.Text;
                elnokok[dataGrid.CurrentRow.Index].meghalt = textBox5.Text;

                Updategrid();
                MessageBox.Show("Adat módosítva!");
                
            }
            

        }
 
        

        private void button2_Click(object sender, EventArgs e)
        {
            //törlés

            if (MessageBox.Show("Biztosan törlöd az adatot?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                

                //isChanged = true;
                int index = dataGrid.CurrentRow.Index;
                elnokok.RemoveAt(index);
                Updategrid();
                MessageBox.Show("Adat törölve!");
                Adattorles();


            }
            
        }
    }
}
