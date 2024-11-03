using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterleaveLib.BitStream
{
    internal class SplitAccessHeaders
    {
        public List<byte[]> FormattedAccessHeaders { get; private set; }

        private readonly GetBitStreams _getBitStreams;
        private List<byte[]> _accessHeaderList = new List<byte[]>();
        private int _startByte = 0;
        private byte _fourLSBOperand = 0x0f;

        public SplitAccessHeaders(GetBitStreams getBitStreams)
        {
            _getBitStreams = getBitStreams;
            FormattedAccessHeaders = new List<byte[]>();

            SplitAccessHeaderLoop();
            FormatAccessHeaders();
            if (_accessHeaderList.Count % 192 != 0)
            {
                FormatLeftOverAccessHeaders();
            }
        }

        private void GetAccessUnitLength()
        {
            byte[] accessUnitWordLength = _getBitStreams.MlpData.Skip(_startByte).Take(2).ToArray();
            accessUnitWordLength[0] = (byte)(_getBitStreams.MlpData[_startByte] & _fourLSBOperand);
            int accessUnitLength = BitConverter.ToUInt16(accessUnitWordLength.Reverse().ToArray(), 0) * 2;
            _accessHeaderList.Add(_getBitStreams.MlpData.Skip(_startByte).Take(accessUnitLength).ToArray());
            _startByte += accessUnitLength;
        }

        private void SplitAccessHeaderLoop()
        {
            while (_startByte < _getBitStreams.MlpData.Length)
            {
                GetAccessUnitLength();
            }
        }

        private void FormatAccessHeaders()
        {
            int numberOfSegments = _accessHeaderList.Count / 192;
            for (int i = 0; i < numberOfSegments * 192; i += 192)
            {
                FormattedAccessHeaders.Add(_accessHeaderList.Skip(i).Take(39).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(_accessHeaderList.Skip(i + 39).Take(38).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(_accessHeaderList.Skip(i + 77).Take(39).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(_accessHeaderList.Skip(i + 116).Take(38).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(_accessHeaderList.Skip(i + 154).Take(38).SelectMany(x => x).ToArray());
            }
        }

        private void FormatLeftOverAccessHeaders()
        {
            int fullLength = _accessHeaderList.Count;
            int startIndex = (fullLength / 192) * 192;
            var temp = _accessHeaderList.Skip(startIndex).ToList();
            int tempLength = temp.Count;
            if (tempLength <= 39)
            {
                FormattedAccessHeaders.Add(temp.SelectMany(x => x).ToArray());
            }
            else if (tempLength <= 77)
            {
                FormattedAccessHeaders.Add(temp.Take(39).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(temp.Skip(39).SelectMany(x => x).ToArray());
            }
            else if (tempLength <= 116)
            {
                FormattedAccessHeaders.Add(temp.Take(39).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(temp.Skip(39).Take(38).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(temp.Skip(77).SelectMany(x => x).ToArray());
            }
            else if (tempLength <= 154)
            {
                FormattedAccessHeaders.Add(temp.Take(39).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(temp.Skip(39).Take(38).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(temp.Skip(77).Take(39).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(temp.Skip(116).SelectMany(x => x).ToArray());
            }
            else
            {
                FormattedAccessHeaders.Add(temp.Take(39).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(temp.Skip(39).Take(38).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(temp.Skip(77).Take(39).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(temp.Skip(116).Take(38).SelectMany(x => x).ToArray());
                FormattedAccessHeaders.Add(temp.Skip(154).SelectMany(x => x).ToArray());
            }
        }
    }
}
