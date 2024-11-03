using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterleaveLib.BitStream
{
    internal class InterleaveBitStreams 
    {
        private readonly SplitDolbyDigitalFrames _splitDolbyDigitalFrames;
        private readonly SplitAccessHeaders _splitAccessHeaders;

        public byte[] InterleavedBitStream { get; private set; }

        public InterleaveBitStreams(SplitDolbyDigitalFrames splitDolbyDigitalFrames, SplitAccessHeaders splitAccessHeaders)
        {
            _splitDolbyDigitalFrames = splitDolbyDigitalFrames;
            _splitAccessHeaders = splitAccessHeaders;

            var minMaxResult = FindMinMaxLengths();
            if (minMaxResult.IsFailure)
                throw minMaxResult.Error;

            var interleavedResult = CreateInterleavedList(minMaxResult.Value);
            if (interleavedResult.IsFailure)
                throw interleavedResult.Error;

            InterleavedBitStream = interleavedResult.Value
                .SelectMany(x => x)
                .ToArray();
        }

        private Result<MlpDdMinMax, Exception> FindMinMaxLengths()
        {
            MlpDdMinMax mlpDdMinMax = new MlpDdMinMax
            {
                lenDD = _splitDolbyDigitalFrames.FrameList.Count,
                lenMLP = _splitAccessHeaders.FormattedAccessHeaders.Count,
            };

            try
            {
                mlpDdMinMax.minimum = Math.Min(mlpDdMinMax.lenDD, mlpDdMinMax.lenMLP);
                mlpDdMinMax.maximum = Math.Max(mlpDdMinMax.lenDD, mlpDdMinMax.lenMLP);
            }
            catch (Exception ex)
            {
                return Result.Failure<MlpDdMinMax, Exception>(ex);
            }

            return Result.Success<MlpDdMinMax, Exception>(mlpDdMinMax);
        }

        private Result<List<byte[]>, Exception> CreateInterleavedList(MlpDdMinMax mlpDdMinMax)
        {
            var interleavedList = new List<byte[]>();

            try
            {
                for (int i = 0; i < mlpDdMinMax.minimum; i++)
                {
                    interleavedList.Add(_splitDolbyDigitalFrames.FrameList[i]);
                    interleavedList.Add(_splitAccessHeaders.FormattedAccessHeaders[i]);
                }

                if (mlpDdMinMax.lenDD != mlpDdMinMax.lenMLP)
                {
                    for (int i = mlpDdMinMax.minimum; i < mlpDdMinMax.maximum; i++)
                    {
                        if (i < mlpDdMinMax.lenDD)
                        {
                            interleavedList.Add(_splitDolbyDigitalFrames.FrameList[i]);
                        }
                        if (i < mlpDdMinMax.lenMLP)
                        {
                            interleavedList.Add(_splitAccessHeaders.FormattedAccessHeaders[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Result.Failure<List<byte[]>, Exception>(ex);
            }

            return Result.Success<List<byte[]>, Exception>(interleavedList);
        }
    }
}
