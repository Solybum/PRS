using System;

namespace PSO.PRS
{
    /// <summary>
    /// PRS main functions
    /// </summary>
    public class PRS
    {
        /// <summary>
        /// Compresses data
        /// </summary>
        /// <param name="data">Uncompressed data</param>
        /// <returns>Returns the compressed data</returns>
        public static byte[] Compress(byte[] data)
        {
            Context ctx = new Context(data);

            Compression.Compress(ctx);
            Array.Resize(ref ctx.dst, ctx.dst_pos);

            return ctx.dst;
        }

        /// <summary>
        /// Decompresses data
        /// </summary>
        /// <param name="data">Compressed data</param>
        /// <returns>Returns the decompressed data</returns>
        public static byte[] Decompress(byte[] data)
        {
            Context ctx = new Context(data);

            int decompressed_size = Decompression.Decompress(ctx, true);
            Array.Resize(ref ctx.dst, decompressed_size);
            ctx.Reset();
            Decompression.Decompress(ctx, false);

            return ctx.dst;
        }
    }
}
