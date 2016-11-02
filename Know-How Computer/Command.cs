using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Know_How_Computer
{
    enum CType
    {
        Inc,
        Dec,
        IfZero,
        Stop
    }

    

    class Command
    {

        public int data;
        CType commands;

        public Command(CType command, int data)
        {
            this.data = data;
            this.commands = command;
        }

    }

   
}
