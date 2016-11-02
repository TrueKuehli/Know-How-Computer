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
        public int position;
        public int data;
        public CType command;

        public Command(CType command, int data)
        {
            this.data = data;
            this.command = command;
        }

    }

   
}
