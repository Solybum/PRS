using System;
using System.Collections.Generic;
using System.Text;

namespace PSO.PRS
{
    internal class Context
    {
        internal int bitPosition;
        internal byte controlByte;
        internal int controlByteIndex;

        internal byte[] src;
        internal int src_pos;
        internal byte[] dst;
        internal int dst_pos;

        internal Context(byte[] src)
        {
            this.src_pos = 0;
            this.dst_pos = 0;
            this.controlByte = 0;
            this.bitPosition = 0;
            this.src = src;
            this.dst = new byte[src.Length * 4];
        }
        internal void Reset()
        {
            this.src_pos = 0;
            this.dst_pos = 0;
            this.controlByte = 0;
            this.bitPosition = 0;
        }
    }
}
