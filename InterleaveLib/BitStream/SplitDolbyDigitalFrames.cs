using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterleaveLib.BitStream
{
    internal class SplitDolbyDigitalFrames
    {
        public List<byte[]> FrameList { get; private set; }

        private readonly GetBitStreams _getBitStreams;
        private static readonly int[] _bitRates = { 64, 80, 96, 112, 128, 160, 192, 224, 256, 320, 384, 448, 512, 640, 768, 896, 1024, 1152, 1280 };               

        public SplitDolbyDigitalFrames(GetBitStreams getBitStreams)
        {
            FrameList = new List<byte[]>();
            _getBitStreams = getBitStreams;

            var samplingResult = CheckSamplingFrequency();
            if (samplingResult.IsFailure)
                throw samplingResult.Error;            

            var frameSizeResult = GetFrameSize(samplingResult.Value);
            if (frameSizeResult.IsFailure)
                throw frameSizeResult.Error;
            
            var splitFramesResult = SplitFrames(frameSizeResult.Value);
            if (!splitFramesResult.IsFailure)
                throw splitFramesResult.Error;

            FrameList = splitFramesResult.Value;
        }

        private Result<byte, Exception> CheckSamplingFrequency()
        {
            byte twoMSBOperand = 0xc0;
            var codeByte = _getBitStreams.Ac3Data[4];
            
            if ((codeByte & twoMSBOperand) != 0)            
               return Result.Failure<byte, Exception>(new Exception("The AC3 bit stream has an unsupported sampling frequency."));
                
            return Result.Success<byte, Exception>(codeByte);
        }

        private Result<int, Exception> GetFrameSize(byte codeByte)
        {
            try
            {
                byte sixLSBOperand = 0x3f;
                int frameSizeCode = (codeByte & sixLSBOperand) >> 1;
                var frameSize = _bitRates[frameSizeCode] * 2;

                return Result.Success<int, Exception>(frameSize);
            }
            catch (Exception e)
            {
                return Result.Failure<int, Exception>(e);
            }
        }

        private Result<List<byte[]>, Exception> SplitFrames(int frameSize)
        {
            if (_getBitStreams.Ac3Data.Length % frameSize != 0)
                return Result.Failure<List<byte[]>, Exception>(new Exception("There's a problem with the AC3 frames."));               
            
            
            int numberOfFrames = _getBitStreams.Ac3Data.Length / frameSize;
            var frameList = new List<byte[]>();

            try
            {
                for (int i = 0; i < numberOfFrames; i++)
                    frameList.Add(_getBitStreams.Ac3Data.Skip(i * frameSize).Take(frameSize).ToArray());
            }
            catch (Exception e)
            {
                return Result.Failure<List<byte[]>, Exception>(e);
            }

            return Result.Success<List<byte[]>, Exception>(frameList);
        }
    }
}
