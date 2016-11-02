using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Know_How_Computer
{
    public enum CType
    {
        Inc,
        Dec,
        IfZero,
        Stop
    }

    

    class Command
    {
        public static int ids=0;
        public int id;
        public int position;
        public int data;
        public CType command;

        public Command(CType command, int data)
        {
            this.data = data;
            this.command = command;
            this.id = ids++;
            
        }

        public int Run()
        {
            switch (command)
            {
                case CType.Inc:
                    Form1.Register[data] += 1;
                    Form1.pc++;
                    break;
                case CType.Dec:
                    Form1.Register[data] -= 1;
                    Form1.pc++;
                    break;
                case CType.IfZero:
                    if (Form1.Register[data] == 0)
                        Form1.pc += 2;
                    else
                        Form1.pc++;
                    break;
                case CType.Stop:
                    Environment.Exit(0);
                    break;

            }
        }

    }

   
}
