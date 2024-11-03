using InterleaveLib.BitStream;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterleaveLib
{
    public interface IInterleave
    {
        /// <summary>
        /// InterleaveBitStreams
        /// InterleaveBitStreams a DolbyDigital and Dolby TrueHD stream
        /// into a Dolby TrueHD with AC3 Core
        /// </summary>
        /// <param name="ac3FilePath">Full path to DolbyDigital file</param>
        /// <param name="mlpFilePath">Ful path to Dolby TrueHD file</param>
        /// <returns>Byte span of the Interleaved Bit Stream</returns>
        ReadOnlySpan<byte> InterleaveBitStreams(string ac3FilePath, string mlpFilePath);
    }

    internal class Interleave : IInterleave
    {
        /// <inheritdoc/>
        public ReadOnlySpan<byte> InterleaveBitStreams(string ac3FilePath, string mlpFilePath)
        {
            GetBitStreams getStream = new GetBitStreams(ac3FilePath, mlpFilePath);
            SplitDolbyDigitalFrames splitFrames = new SplitDolbyDigitalFrames(getStream);
            SplitAccessHeaders splitMLP = new SplitAccessHeaders(getStream);
            InterleaveBitStreams interleaveStreams = new InterleaveBitStreams(splitFrames, splitMLP);

            return interleaveStreams.InterleavedBitStream.AsSpan();
        }
    }
}
