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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            Know_How_Computer.Form1.ActiveForm.Width = (1800 / 1745) * Know_How_Computer.Form1.ActiveForm.Height;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button[] CommandPresets = new Button[5];
            for (int i = 0; i < 5; i++)
            {
                CommandPresets[i] = new Button();
                CommandPresets[i].Text = "Hello";
                CommandPresets[i].DoDragDrop(CommandPresets[i].Text,
                    DragDropEffects.Copy | DragDropEffects.Move);

                CommandPresets[i].MouseDown += new MouseEventHandler(MouseDrag);
                CommandPresets[i].Width = 100;
                CommandPresets[i].Height = 100;
                CommandPresets[i].Location = new Point(100*i,20);
                CommandPresets[i].BackColor = Color.Red;
                CommandPresets[i].ForeColor = Color.Blue;
                CommandPresets[i].Font = new Font("Arial", 16);
                CommandPresets[i].Name = "CommandPreset " + i;

                this.Controls.Add(CommandPresets[i]);
            }
            CommandPresets[0].Text = "+";
            CommandPresets[1].Text = "-";
            CommandPresets[2].Text = "S";
            CommandPresets[3].Text = "0";
            CommandPresets[4].Text = "Stop";

            Controls.SetChildIndex(pictureBox1, 128);
        }

        private void MouseDrag(object sender, EventArgs e)
        {
            (sender as Button).DoDragDrop((sender as Button).Text, 
                DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
