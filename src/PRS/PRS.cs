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
        /// <param name="searchBufferSize">The number of bytes to search for matching patterns. Range: from 255 (0xFF, fastest) to 8191 (0x1FFF, maximum compression). Default value is 8176 (0x1FF0).</param>
        /// <returns>Returns the compressed data</returns>
        public static byte[] Compress(byte[] data, int searchBufferSize = 8176)
        {
            Context ctx = new Context(data);

            Compression.Compress(ctx, searchBufferSize);
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
