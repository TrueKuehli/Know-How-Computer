using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Know_How_Computer
{
    public partial class PickRegister : Form
    {
        public string Type { get; set; }
        public int returnInt;

        public PickRegister()
        {
            InitializeComponent();
        }

        private void PickRegister_Load(object sender, EventArgs e)
        {
            if (this.Type == "O")
            {
                label1.Text = "Bitte geben Sie das Register ein, auf das der Befehl\nausgeführt werden soll:";
                numericUpDown1.Maximum = 8;
            }
            else if (Type == "R")
                label1.Text = "Bitte geben Sie einen Wert für das Programmregister ein:";
            else
            {
                label1.Text = "Bitte geben Sie das Programmregister ein, zu welchem\ngesprungen werden soll:";
                numericUpDown1.Maximum = 21;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.returnInt = (int)numericUpDown1.Value;
            this.Close();
        }

        public int data
        {
            get
            {
                return (int)numericUpDown1.Value;
            }
        }
    }
}
