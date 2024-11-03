using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InterleaveLib.BitStream
{
    internal class GetBitStreams
    {
        public byte[] Ac3Data { get; private set; }
        public byte[] MlpData { get; private set; }
        
        private readonly FileInfo _ac3File;
        private readonly FileInfo _mlpFile;

        private static readonly byte[] syncWordAc3 = { 0x0b, 0x77 };
        private static readonly byte[] formatSyncMlp = { 0xf8, 0x72, 0x6f, 0xba };

        public GetBitStreams(string ac3File, string mlpFile)
        {
            if (!File.Exists(ac3File))
                throw new FileNotFoundException(ac3File);

            if (!File.Exists(mlpFile))
                throw new FileNotFoundException(mlpFile);

            _ac3File = new FileInfo(ac3File);
            _mlpFile = new FileInfo(mlpFile);           

            var result = ReadAc3File();
            if (result.IsFailure)
                throw result.Error;

            Ac3Data = result.Value;

            result = ReadMlpFile();
            if (result.IsFailure)
                throw result.Error;

            MlpData = result.Value;
        }

        private Result<byte[], Exception> ReadAc3File()
        {
            byte[] fieData;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (var fileStream = _ac3File.OpenRead())
                        fileStream.CopyTo(ms);

                    fieData = ms.ToArray();
                }
            }
            catch (IOException e)
            {
                return Result.Failure<byte[], Exception>(e);
            }

            if (!fieData.Take(2).SequenceEqual(syncWordAc3))
                return Result.Failure<byte[], Exception>(new Exception("Error: The .ac3 file doesn't start with the AC3 sync word."));

            return Result.Success<byte[], Exception>(fieData);
        }

        private Result<byte[], Exception> ReadMlpFile()
        {
            byte[] fieData;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (var fileStream = _mlpFile.OpenRead())
                        fileStream.CopyTo(ms);

                    fieData = ms.ToArray();
                }
            }
            catch (IOException e)
            {
                return Result.Failure<byte[], Exception>(e);
            }

            if (!fieData.Skip(4).Take(4).SequenceEqual(formatSyncMlp))
                return Result.Failure<byte[], Exception>(new Exception("Error: The .thd file doesn't have the major format sync."));

            return Result.Success<byte[], Exception>(fieData);
        }
    }
}
