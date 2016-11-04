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
using System.IO;
using System.Reflection;



namespace Know_How_Computer
{
     

    public partial class Form1 : Form
    {
        public static int[] Register = new int[8];
        List<Command> Commands = new List<Command>();
        public static int pc =  1; 

        public Button[] CommandPresets = new Button[5];
        public PictureBox[] DropPoints = new PictureBox[21];
        
        public Image Panel = Know_How_Computer.Properties.Resources.Panel;

        public Form1()
        {
            InitializeComponent();
        }

        public int posID(int pos)
        {
            int value = -1;
            foreach (Command c in Commands)
            {
                if (c.position == pos)
                    value = c.position; 
            }
            return value;
        }
        
        public void addCommand(CType c,int d,int pos)
        {
            Commands.Add(new Command(c,d,pos));
        }

        public void initialzeRegister()
        {
            Random zufall = new Random();
            for (int i = 0; i <= Register.Length; i++)
                Register[i] = zufall.Next(0,32);
        }

        public void removeCommand(int pos)
        {
            Commands[posID(pos)].disabled = true;
        }

        public  void readfile()
        {
            string[] lines = System.IO.File.ReadAllLines("../../programm.txt");
            string[] tokens = new string[2];
            int i = 0;
            foreach (string line in lines)
            {
                tokens = line.Split(' ');
                i++;
                switch(tokens[0])
                {
                    case "s":
                        addCommand(CType.Jump, Int32.Parse(tokens[1]), i);
                        break;
                    case "+":
                        addCommand(CType.Inc, Int32.Parse(tokens[1]),i);
                        break;
                    case "-":
                        addCommand(CType.Dec, Int32.Parse(tokens[1]), i);
                        break;
                    case "0":
                        addCommand(CType.IfZero, Int32.Parse(tokens[1]), i);
                        break;
                    case "stop":
                        addCommand(CType.Stop, 0, i);
                        break;
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        private void MouseDrag(object sender, EventArgs e)
        {
            (sender as Button).DoDragDrop((sender as Button).Text, 
                DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void DropPointDel(object sender, EventArgs e)
        {

        }

        private void DropPointCreate(object sender, DragEventArgs e)
        {

        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                CommandPresets[i].Width = pictureBox1.Width * 100 / 686;
                CommandPresets[i].Height = pictureBox1.Height * 50 / 665;
                CommandPresets[i].Left = pictureBox1.Width * 575 / 686;
                CommandPresets[i].Top = pictureBox1.Height * 54 * i / 665 + (10 * Form1.ActiveForm.Height / 665);
            }

            for (int i = 0; i < 21; i++)
            {
                DropPoints[i].Width = pictureBox1.Width * 102 / 686;
                DropPoints[i].Height = pictureBox1.Height * 23 / 665;
                DropPoints[i].Left = pictureBox1.Width * 25 / 686;
                DropPoints[i].Top = pictureBox1.Height * 23 * i / 665 + (162 * pictureBox1.Height / 665);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            ((Control)pictureBox1).AllowDrop = true;

            //PanelCommand = 
            
            for (int i = 0; i < 5; i++)
            {
                CommandPresets[i] = new Button();

                CommandPresets[i].Width = 100;
                CommandPresets[i].Height = 50;
                CommandPresets[i].Left = 575;
                CommandPresets[i].Top = 55 * i + 10;

                CommandPresets[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#EEEEEE");
                CommandPresets[i].ForeColor = System.Drawing.ColorTranslator.FromHtml("#1F1F1F");
                CommandPresets[i].Font = new Font("Arial", 16);

                CommandPresets[i].Name = "CommandPreset " + i;
                CommandPresets[i].MouseDown += new MouseEventHandler(MouseDrag);

                this.Controls.Add(CommandPresets[i]);
                CommandPresets[i].DoDragDrop(CommandPresets[i].Text,
                    DragDropEffects.Copy | DragDropEffects.Move);
            }
            CommandPresets[0].Text = "+";
            CommandPresets[1].Text = "-";
            CommandPresets[2].Text = "S";
            CommandPresets[3].Text = "0";
            CommandPresets[4].Text = "Stop";

            TransparencyKey = Color.Transparent;
            for (int i = 0; i < 21; i++) {
                DropPoints[i] = new PictureBox();

                DropPoints[i].Width = 102;
                DropPoints[i].Height = 23;
                DropPoints[i].Top = 23*i + 162;
                DropPoints[i].Left = 25;

                DropPoints[i].Image = Panel;
                DropPoints[i].AllowDrop = true;

                DropPoints[i].DragDrop += new DragEventHandler(DropPointCreate);
                DropPoints[i].Click += new EventHandler(DropPointDel);

                this.Controls.Add(DropPoints[i]);
            }

            Controls.SetChildIndex(pictureBox1, 128);
        }

        private void DropPointsEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

    }
}
