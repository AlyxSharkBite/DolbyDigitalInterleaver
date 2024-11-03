using System;
using System.Collections.Generic;
using System.Text;

namespace InterleaveLib.Factory
{
    public static class InterleaveFactory
    {
        public static IInterleave GetInterleaver() { return new Interleave(); }
    }
}
