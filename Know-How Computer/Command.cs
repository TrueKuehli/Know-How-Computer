using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


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
                    Form1.Register[data]++;
                    Form1.pc++;
                    break;
                case CType.Dec:
                    if (Form1.Register[data] == 0)
                        return '-';
                    Form1.Register[data]--;
                    Form1.pc++;
                    break;
                case CType.IfZero:
                    if (Form1.Register[data] == 0)
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

    class Register
    {
        public static byte ids = 1;
        public byte ID;
        public byte value=0;
        public Point koords = new Point();
        private int Width;
        private int Height;
        private Bitmap Striker = Properties.Resources.streichholz;
        
        public Register()
        {

        }

        public void Resize(int Left,int Right)
        {

        }



    }

   
}
