using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Know_How_Computer;

namespace Know_How_Computer
{
     

    public partial class Form1 : Form
    {
        public static int[] Register = new int[8];
        List<Command> Commands = new List<Command>();
        public static int pc =  1; 

        public Button[] CommandPresets = new Button[5];
        public Form1()
        {
            InitializeComponent();
        }

        
        public void RunStep()
        {
            switch (Commands[pc].command)
            {
                case CType.Inc:
                    Register[Commands[pc].data] += 1;
                    pc++;
                    break;
                case CType.Dec:
                    Register[Commands[pc].data] -= 1;
                    break;
                    pc++;
                case CType.IfZero:
                    if (Register[Commands[pc].data] == 0)
                        pc += 2;
                    else
                        pc++;
                    break;
                case CType.Stop:
                    Environment.Exit(0);
                    break;

            }
            

        }

        public void initialzeRegister()
        {

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

        private void Form1_Load(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 5; i++)
            {
                CommandPresets[i] = new Button();
                CommandPresets[i].Text = "Hello";
                CommandPresets[i].DoDragDrop(CommandPresets[i].Text,
                    DragDropEffects.Copy | DragDropEffects.Move);

                CommandPresets[i].MouseDown += new MouseEventHandler(MouseDrag);

                CommandPresets[i].Width = 100;
                CommandPresets[i].Height = 50;
                CommandPresets[i].Left = 575;
                CommandPresets[i].Top = 55*i+10;

                CommandPresets[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#EEEEEE");
                CommandPresets[i].ForeColor = System.Drawing.ColorTranslator.FromHtml("#1F1F1F");
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


        private void Form1_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                CommandPresets[i].Width = Form1.ActiveForm.Width * 100 / 702;
                CommandPresets[i].Height = Form1.ActiveForm.Height * 50 / 703;
                CommandPresets[i].Top = Form1.ActiveForm.Height * 55 * i / 703 + (10 * Form1.ActiveForm.Height / 703);
                CommandPresets[i].Left = Form1.ActiveForm.Width * 575 / 702;
            }
        }
    }
}
