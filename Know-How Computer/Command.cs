using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Know_How_Computer
{
    public enum CType
    {
        Jump,
        Inc,
        Dec,
        IfZero,
        Stop
    }

    

    public class Command
    {
        public static int ids=0;
        public int id;
        public int position;
        public int data;
        public CType command;
        public bool disabled = false;

        public Command(CType command, int data)
        {
            this.data = data;
            this.command = command;
        }

        public Command(CType command, int data,int pos)
        {
            this.data = data;
            this.command = command;
            this.position = pos;

        }

        public char Run()
        {
            switch (command)
            {
                case CType.Jump:
                    Form1.pc = data;
                    break;
                case CType.Inc:
                    Form1.regs[data-1].inc();
                    Form1.pc++;
                    break;
                case CType.Dec:
                    if (Form1.regs[data-1].value == 0)
                        return '-';
                    Form1.regs[data-1].dec();
                    Form1.pc++;
                    break;
                case CType.IfZero:
                    if (Form1.regs[data-1].value == 0)
                        Form1.pc += 2;
                    else
                        Form1.pc++;
                    break;
                case CType.Stop:
                    return 'S';
            }
            return 'W';
        }

    }

    public class Register
    {
        public static byte ids = 0;
        public byte ID;
        public int value;
      
        public Point koords = new Point();
        private Size size;
        private Stack<Streichholz> hoelzer = new Stack<Streichholz>();

        public void inc()
        {
            if (value >= 0)
                add();
            value++;
            draw();
                            
        }
        public void dec()
        {
            if (value >= 0)
                hoelzer.Pop();
            value--;
            draw();
            
        }
        
        public Register()
        {
            this.ID = ids++;
            value = 0;
            
        }

        public void Resize(Size size)
        {
            this.size.Width = 102 * size.Width / 686;
            this.size.Height = 102 * size.Height / 665;
            this.koords.X = 374 * size.Width / 686;
            this.koords.Y = 59 * this.ID + 168 * size.Height / 665;
            draw();
        }

        private void add()
        {
            hoelzer.Push(new Streichholz(value + 1));
        }
        public void draw()
        {
            foreach (Streichholz sh in hoelzer)
            {
                sh.Paint(koords,size);
            }
        }



    }
    class Streichholz
    {
        const double ratio = 0.05656324582338902147971360381862;
        public int ID;
        private Image Striker = Properties.Resources.streichholz;
        PictureBox Box = new PictureBox();
        
        public Streichholz(int id)
        {
            //Striker.MakeTransparent(ColorTranslator.FromHtml("#000000"));
            this.ID = id;
            Box.Image = Striker;
            Box.Name = ID.ToString();
            Box.SizeMode = PictureBoxSizeMode.StretchImage;
            
        }
        public void Paint(Point pos,Size size)
        {
            double Height = size.Height;
            Box.Height = size.Height;
            Box.Width = (int)(Height * ratio);
            Box.Top = pos.Y;
            Box.Left = pos.X + Box.Width * ID;
        }
    }

   
}
