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
        public List<Register> regs = new List<Register>();
        public static int pc =  1; 

        public Button[] CommandPresets = new Button[5];
        public PictureBox[] DropPoints = new PictureBox[21];
        public Label[] matches = new Label[8];
        public Button saveButton = new Button();
        public Button loadButton = new Button();


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
            removeCommand(pos);
            Commands.Add(new Command(c,d,pos));
            //Commands[Commands.Count() - 1].id = Commands.IndexOf(Commands[Commands.Count() - 1]);
        }

        public void removeCommand(int pos)
        {
            try
            { //Commands.Remove(Commands[posID(pos)]); }

                Commands[posID(pos)].disabled = true;
                Commands[posID(pos)].position = -1;
            }
            catch { };
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
                default:
                    return CType.Error;
            }
        }
        public string TypeToString(CType s)
        {
            switch (s)
            {
                case CType.Jump:
                    return "S";
                case CType.Inc:
                    return "+";
                case CType.Dec:
                    return "-";
                case CType.IfZero:
                    return "0";
                case CType.Stop:
                    return "Stop";
                default:
                    return "";
            }          
        }

        public void readfile(object sender, MouseEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "KnowHowComputer Datei|*.khc";
            open.Title = "KnowHowComtuter Datei laden:";
            open.ShowDialog();
            if (DialogResult==DialogResult.OK)
            if (open.FileName != "")
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(open.FileName);
                    string[] tokens = new string[2];
                    int i = 0;
                    foreach (string line in lines)
                    {
                        tokens = line.Split(' ');
                        i++;
                        addCommand(stringToType(tokens[0]), Int32.Parse(tokens[1]), i);

                    }
                }
                catch (Exception h)
                {
                    MessageBox.Show("Ausnahme trat auf: \n" + e, "Fehler", MessageBoxButtons.OK); //Fehlermeldung für unbehandelten Fehler
                }
            }
            else //Fehlermeldung bei leerem Dateinamen
            {
                MessageBox.Show("Es wurde kein Dateiname eingegeben.", "Fehlender Dateiname", MessageBoxButtons.OK);
            };

        }
        
             
           

        

        public void writefile(object sender, MouseEventArgs h)
        {
            SaveFileDialog sFD = new SaveFileDialog();
            sFD.Filter = "KnowHowComputer Datei|*.khc";
            sFD.Title = "Ergebnisse speichern unter:";
            sFD.ShowDialog(); //Zeigt Dialog zum Abspeichern an
            if (sFD.FileName != "")
            {
                try
                {
                    List<string> lines = new List<string>();
                    int i = 0;
                    while (posID(++i)!=-1)
                    {
                        lines.Add(TypeToString(Commands[posID(i)].command)+" "+ Commands[posID(i)].data.ToString());
                    }
                    
                    System.IO.File.WriteAllLines(sFD.FileName, lines); 
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("Ausnahme trat auf: \n" + e, "Fehler", MessageBoxButtons.OK); //Fehlermeldung für unbehandelten Fehler
                }
            }
            else //Fehlermeldung bei leerem Dateinamen
            {
                MessageBox.Show("Es wurde kein Dateiname eingegeben.", "Fehlender Dateiname", MessageBoxButtons.OK);
            };

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
                addCommand(stringToType(data), dialogResult, sendernum);
            }
            (sender as PictureBox).Refresh();

        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            int fSize;
            int gSize;
            if (pictureBox1.Width > pictureBox1.Height)
            {
                fSize = 16 * pictureBox1.Height / 665;
                gSize = 15 * pictureBox1.Height / 665;
            }
            else
            {
                fSize = 16 * pictureBox1.Width / 686;
                gSize = 15 * pictureBox1.Width / 686;
            }

            Font f = new Font("Arial", fSize);
            Font g = new Font("Arial", gSize);

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

            for (int i=0; i<8; i++)
            {
                matches[i].Left = pictureBox1.Width * 380 / 686;
                matches[i].Top = pictureBox1.Height * 60 / 665 * i + 180 * pictureBox1.Height / 665;
                matches[i].Font = g;
            }

            button1.Width = pictureBox1.Width * 147 / 686;
            button1.Height = pictureBox1.Height * 69 / 665;
            button1.Left = pictureBox1.Width * 527 / 686;
            button1.Top = pictureBox1.Height * 362 / 665;

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

                CommandPresets[i].BackColor = ColorTranslator.FromHtml("#f2cf8b");
                CommandPresets[i].ForeColor = ColorTranslator.FromHtml("#1F1F1F");
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

                DropPoints[i].Name = (i + 1).ToString();
                DropPoints[i].Image = Panel;
                DropPoints[i].SizeMode = PictureBoxSizeMode.StretchImage;
                DropPoints[i].AllowDrop = true;

                DropPoints[i].DragEnter += new DragEventHandler(DropPointsEnter);
                DropPoints[i].DragDrop += new DragEventHandler(DropPointCreate);
                DropPoints[i].Click += new EventHandler(DropPointDel);

                this.Controls.Add(DropPoints[i]);
            } 

            DropPoints[20].Image = Know_How_Computer.Properties.Resources.PanelLast;

            

            ResizePen();

            for (int i = 0; i<8; i++)
            {
                matches[i] = new Label();
                matches[i].AutoSize = true;
                matches[i].Text = "0";
                matches[i].Left = 380;
                matches[i].Top = 60*i+180;
                matches[i].Font = new Font("Arial", 15);
                matches[i].Name = i.ToString();
                matches[i].MouseClick += new MouseEventHandler(buttonClick);
                matches[i].MouseDoubleClick += new MouseEventHandler(buttonRClick);
                matches[i].BackColor = Color.FromArgb(255, 250, 243, 225);

                this.Controls.Add(matches[i]);
                
            }



        


            Controls.SetChildIndex(pictureBox1, 128);
        }

        private void ResizePen()
        {
            Pen.Width = pictureBox1.Width * 175 / 686;
            Pen.Height = pictureBox1.Height * 220 / 6423;
            Pen.Left = pictureBox1.Width * 165 / 686;
            Pen.Top = pictureBox1.Height * 220 * (pc - 1) / 6423 + (163 * pictureBox1.Height / 665);
        }
        private void DrawRegs()
        {

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
                DrawRegs();
            }
            for(int i=0; i<8; i++)
            {
                try { matches[i].Text = Register[i+1].ToString(); }
                catch { };
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

        private void buttonClick(object sender, EventArgs e)
        {
            int dialogResult;

            
                PickRegister PickR = new PickRegister();
                Form1.ActiveForm.Enabled = false;
                
                PickR.Type = "R";
                
                PickR.ShowDialog();
                dialogResult = (int)PickR.returnInt;
                Form1.ActiveForm.Enabled = true;
            

            
            Register[Int32.Parse((sender as Label).Name) + 1] = dialogResult;
            (sender as Label).Text = Register[Int32.Parse((sender as Label).Name) + 1].ToString();
        }

        private void buttonRClick(object sender, EventArgs e)
        {
            Register[Int32.Parse((sender as Label).Name) + 1]-=2;
            if (Register[Int32.Parse((sender as Label).Name) + 1] < 0)
            {
                Register[Int32.Parse((sender as Label).Name) + 1] = 0;
            }
            (sender as Label).Text = Register[Int32.Parse((sender as Label).Name) + 1].ToString();
        }


        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            readfile(null, null);
        }

        private void speichernUnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            writefile(null, null);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            menuStrip1.Visible = (e.Location.Y < 20) ? true : false;
        }
    }
}
