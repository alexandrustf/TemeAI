using System;
using System.Collections.Generic;
using System.Text;

namespace IaTema1
{
    public class State
    {
        public State(int b, int m1, int c1, int pb, int m2, int c2) // m >=c
        {
            this.b = b;
            this.m1 = m1;
            this.m2 = m2;
            this.c1 = c1;
            this.c2 = c2;
            this.pb = pb;
        }
        public int b { get; } //capacitatea barcii
        public int m1 { get; }
        public int m2 { get;  }
        public int c1 { get; }
        public int c2 { get; }
        public int pb { get; } // partea pe care este barca

        public override string ToString()
        {
            return base.ToString() + ": b" + b + " m1: " + m1 + " c1: " + c1 + " pb: " + pb + " m2: " + m2 + " c2: " + c2;
        }
    }
}
