using System;
using System.Collections.Generic;
using System.Text;

namespace PSO.PRS
{
    internal class Decompression
    {
        internal static int Decompress(Context ctx, bool size_only)
        {
            bool copy = !size_only;

            ctx.controlByte = ReadByte(ctx);

            while (true)
            {
                if (ReadBit(ctx) == 1)
                {
                    CopyByte(ctx, copy);
                }
                else
                {
                    int length;
                    int offset;

                    if (ReadBit(ctx) == 1)
                    {
                        offset = ReadShort(ctx);
                        if (offset == 0)
                        {
                            break;
                        }

                        length = offset & 0b111;
                        offset >>= 3;
                        offset |= -0x2000;

                        if (length == 0)
                        {
                            length = ReadByte(ctx);
                            length += 1;
                        }
                        else
                        {
                            length += 2;
                        }
                    }
                    else
                    {
                        length = ReadBit(ctx);
                        length <<= 1;
                        length |= ReadBit(ctx);
                        length += 2;

                        offset = ReadByte(ctx);
                        offset |= -0x100;
                    }

                    while (length > 0)
                    {
                        CopyByteAt(ctx, offset, copy);
                        length -= 1;
                    }
                }
            }
            return ctx.dst_pos;
        }

        internal static int ReadBit(Context ctx)
        {
            if (ctx.bitPosition >= 8)
            {
                ctx.controlByte = ReadByte(ctx);
                ctx.bitPosition = 0;
            }

            int flag = ctx.controlByte & 1;

            ctx.controlByte >>= 1;
            ctx.bitPosition++;

            return flag;
        }
        internal static byte ReadByte(Context ctx)
        {
            int result;
            result = ctx.src[ctx.src_pos];
            ctx.src_pos += 1;
            return (byte)result;
        }
        internal static ushort ReadShort(Context ctx)
        {
            int result;
            result = ctx.src[ctx.src_pos] + (ctx.src[ctx.src_pos + 1] << 8);
            ctx.src_pos += 2;
            return (ushort)result;
        }

        internal static void CopyByte(Context ctx, bool copy)
        {
            if (copy)
            {
                ctx.dst[ctx.dst_pos] = ctx.src[ctx.src_pos];
            }
            ctx.src_pos += 1;
            ctx.dst_pos += 1;
        }
        internal static void CopyByteAt(Context ctx, int offset, bool copy)
        {
            if (copy)
            {
                ctx.dst[ctx.dst_pos] = ctx.dst[ctx.dst_pos + offset];
            }
            ctx.dst_pos += 1;
        }
    }
}
