MIT License https://opensource.org/license/MIT

Copyright 2024 Alyx Dallagiacomo

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
(the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

---

#Dolby Digital Interleaver
##Based on digitalaudionerd's python script 
##https://github.com/digitalaudionerd/Interleave-AC3-TrueHD-Streams 

Adds the DolbyDigital Core to a Dolby TrueHD stream, most commonly 
used in BlueRay 

All the logic is in the InterleaveLib written in .Net Standard 2.0
so you can easily pull it into your own project if you want. 

Usage of the InterleaveLib

`var interleaver = InterleaveLib.Factory.GetInterleaver();
var interleavedBytes = interleaver.InterleaveBitStreams("C:\\temp\\file.ac3", "D:\\temp\\file.mlp");
File.WriteAllBytes("D:\\temp\\file.thd+ac3");`


The GUI Portion is a simple Winforms in .Net 8

