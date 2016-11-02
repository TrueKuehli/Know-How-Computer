using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Know_How_Computer
{
    class Command
    {
        public int data;

       


    }

    class Inc : Command
    {
        public Inc(int data)
        {
            this.data = data;
        }
        //public int Execute()
        //{

        //}
    }
    class Dec : Command
    {
        public Dec(int data)
        {
            this.data = data;
        }
    }
    class IfZero : Command
    {
        public IfZero(int data)
        {
            this.data = data;
        }
    }
    class Jump : Command
    {
        public Jump(int data)
        {
            this.data = data;
        }
    }
    class Stop : Command
    {
        public Stop()
        {  }

    }
}
