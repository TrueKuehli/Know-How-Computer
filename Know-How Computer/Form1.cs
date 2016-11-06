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
        
        public int chosenReg { get; set; }
        public static int[] Register = new int[8];
        public List<Command> Commands = new List<Command>();
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
                    value = c.id; 
            }
            return value;
        }
        
        public void addCommand(CType c,int d,int pos)
        {
            Commands.Add(new Command(c,d,pos));
            Commands[Commands.Count() - 1].id = Commands.IndexOf(Commands[Commands.Count() - 1]);
        }

        /*
        Not needed!
         
        public void initialzeRegister()
        {
            Random zufall = new Random();
            for (int i = 0; i <= Register.Length; i++)
                Register[i] = zufall.Next(0,32);
        }*/

        public void removeCommand(int pos)
        {
            try { Commands.Remove(Commands[posID(pos)]); }
            catch { };
        }

        public  void readfile() //Todo: remove, just a debugging feature anyway
        {
            string[] lines = System.IO.File.ReadAllLines("../../../programm.txt");
            string[] tokens = new string[2];
            int i = 0;
            foreach (string line in lines)
            {
                tokens = line.Split(' ');
                i++;               
                addCommand(stringToType(tokens[0]), Int32.Parse(tokens[1]), i);

                
            }

        }
        public CType stringToType(string s)
        {
            switch (s)
            {
                case "S":
                    return CType.Jump;
                case "+":
                    return CType.Inc;
                case "-":
                    return CType.Dec;
                case "0":
                    return CType.IfZero;
                case "Stop":
                    return CType.Stop;
            }
            return CType.Stop;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Pen.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void MouseDrag(object sender, EventArgs e)
        {
            (sender as Button).DoDragDrop((sender as Button).Text, 
                DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void DropPointDel(object sender, EventArgs e)
        {
            int sendernum;
            if (Int32.TryParse((sender as PictureBox).Name, out sendernum))
            {
                removeCommand(sendernum);
                (sender as PictureBox).Refresh();
            }
        }

        private void DropPointCreate(object sender, DragEventArgs e)
        {
            string data = e.Data.GetData(DataFormats.Text).ToString();

            int dialogResult;

            if ((stringToType(data) != CType.Stop))
            {
                PickRegister PickR = new PickRegister();
                Form1.ActiveForm.Enabled = false;
                if (data == "S")
                {
                    PickR.Type = "S";
                } else
                {
                    PickR.Type = "O";
                }
                PickR.ShowDialog();
                dialogResult = (int)PickR.returnInt;
                Form1.ActiveForm.Enabled = true;
            }
            else
                dialogResult = 0;

            int sendernum;

            if (Int32.TryParse((sender as PictureBox).Name, out sendernum))
            {
                removeCommand(sendernum);
                addCommand(stringToType(data), dialogResult, sendernum);
            }
            (sender as PictureBox).Refresh();

        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            int fSize;
            if (pictureBox1.Width > pictureBox1.Height)
            {
                fSize = 16 * pictureBox1.Height / 665;
            }
            else
            {
                fSize = 16 * pictureBox1.Width / 686;
            }

            Font f = new Font("Arial", fSize);

            for (int i = 0; i < 5; i++)
            {
                CommandPresets[i].Width = pictureBox1.Width * 100 / 686;
                CommandPresets[i].Height = pictureBox1.Height * 50 / 665;
                CommandPresets[i].Left = pictureBox1.Width * 575 / 686;
                CommandPresets[i].Top = pictureBox1.Height * 54 * i / 665 + (10 * Form1.ActiveForm.Height / 665);
                CommandPresets[i].Font = f;
            }

            for (int i = 0; i < 21; i++)
            {
                DropPoints[i].Width = pictureBox1.Width * 102 / 686;
                DropPoints[i].Height = pictureBox1.Height * 220 / 6423;
                DropPoints[i].Left = pictureBox1.Width * 24 / 686;
                DropPoints[i].Top = pictureBox1.Height * 220 * i / 6423 + (163 * pictureBox1.Height / 665);
            }

            Pen.Width = pictureBox1.Width * 175 / 686;
            Pen.Height = pictureBox1.Height * 220 / 6423;
            Pen.Left = pictureBox1.Width * 165 / 686;
            Pen.Top = pictureBox1.Height * 220 * (pc-1) / 6423 + (163 * pictureBox1.Height / 665);

            trackBar1.Width = pictureBox1.Width * 148 / 686;
            trackBar1.Height = pictureBox1.Height * 48 / 665;
            trackBar1.Left = pictureBox1.Width * 526 / 686;
            trackBar1.Top = pictureBox1.Height * 462 / 665;

            label1.Left = pictureBox1.Width * 523 / 686;
            label1.Top = pictureBox1.Height * 434 / 665;
            label1.Font = f;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            trackBar1.Value = 1;
            ((Control)pictureBox1).AllowDrop = true;
            
            for (int i = 0; i < 5; i++)
            {
                CommandPresets[i] = new Button();

                CommandPresets[i].Width = 100;
                CommandPresets[i].Height = 50;
                CommandPresets[i].Left = 575;
                CommandPresets[i].Top = 55 * i + 10;

                CommandPresets[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#f2cf8b");
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

            for (int i = 0; i < 21; i++) {
                DropPoints[i] = new PictureBox();

                DropPoints[i].Width = 102;
                DropPoints[i].Height = pictureBox1.Height * 220 / 6423;
                DropPoints[i].Top = pictureBox1.Height * 220 * i / 6423 + (163 * pictureBox1.Height / 665);
                DropPoints[i].Left = 24;

                DropPoints[i].Paint += new PaintEventHandler(DropPointsDraw);

                DropPoints[i].Name = (i+1).ToString();
                DropPoints[i].Image = Panel;
                DropPoints[i].SizeMode = PictureBoxSizeMode.StretchImage;
                DropPoints[i].AllowDrop = true;

                DropPoints[i].DragEnter += new DragEventHandler(DropPointsEnter);
                DropPoints[i].DragDrop += new DragEventHandler(DropPointCreate);
                DropPoints[i].Click += new EventHandler(DropPointDel);

                this.Controls.Add(DropPoints[i]);
            }

            DropPoints[20].Image = Know_How_Computer.Properties.Resources.PanelLast;

            Controls.SetChildIndex(pictureBox1, 128);

            ResizePen();
        }

        private void ResizePen()
        {
            Pen.Width = pictureBox1.Width * 175 / 686;
            Pen.Height = pictureBox1.Height * 220 / 6423;
            Pen.Left = pictureBox1.Width * 165 / 686;
            Pen.Top = pictureBox1.Height * 220 * (pc - 1) / 6423 + (163 * pictureBox1.Height / 665);
        }

        private void DropPointsEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }


        private void DropPointsDraw(object sender, PaintEventArgs e)
        {
            if (Int32.Parse(posID(Int32.Parse((sender as PictureBox).Name)).ToString()) != -1)
            {
                
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                int i = posID(Int32.Parse((sender as PictureBox).Name));
                CType t = Commands[i].command;
                string tstring;
                
                switch (t)
                {
                    case CType.Dec:
                        tstring = "- " + Commands[i].data.ToString();
                        break;
                    case CType.IfZero:
                        tstring = "0 " + Commands[i].data.ToString();
                        break;
                    case CType.Inc:
                        tstring = "+ " + Commands[i].data.ToString();
                        break;
                    case CType.Jump:
                        tstring = "S " + Commands[i].data.ToString();
                        break;
                    case CType.Stop:
                        tstring = "Stop";
                        break;
                    default:
                        tstring = " ";
                        break;
                }

                int fSize;
                if (pictureBox1.Width > pictureBox1.Height)
                {
                    fSize = 11 * pictureBox1.Height / 665;
                } else
                {
                    fSize = 11 * pictureBox1.Width / 686;
                }

                Font f = new Font("Arial", fSize);
                SizeF textSize = e.Graphics.MeasureString(tstring, f);
                 
                PointF locationToDraw = new PointF();
                locationToDraw.X = ((sender as PictureBox).Width / 2) - (textSize.Width / 2);
                locationToDraw.Y = ((sender as PictureBox).Height / 2) - (textSize.Height / 2);
                e.Graphics.DrawString(tstring, f, Brushes.Black, locationToDraw);
            }
                
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value == 101)
            {
                timer1.Interval = 1;
            }
            else
            {
                timer1.Interval = (101 - trackBar1.Value) * 5000 / 100;
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (posID(pc) == -1)
            {
                timer1.Enabled = false;
                button1.Text = "Start";
                MessageBox.Show("Der PZ steht auf einem leeren Feld!");
                pc = 1;
                ResizePen();
            }
            else
            {
                char result = Commands[posID(pc)].Run();
                if (result == 'S')
                {
                    ResizePen();
                    timer1.Enabled = false;
                    button1.Text = "Start";
                    MessageBox.Show("Ende des Programms erreicht.");
                    pc = 1;
                } else if (result == '-')
                {
                    ResizePen();
                    timer1.Enabled = false;
                    button1.Text = "Start";
                    MessageBox.Show("Datenregister " + Commands[posID(pc)].data + " ist bereits leer!");
                }
                ResizePen();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                timer1.Enabled = true;
                button1.Text = "Stop";
            } else
            {
                timer1.Enabled = false;
                button1.Text = "Start";
            }
        }
    }
}
