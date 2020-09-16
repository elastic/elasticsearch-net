#region Utf8Json License https://github.com/neuecc/Utf8Json/blob/master/LICENSE
// MIT License
//
// Copyright (c) 2017 Yoshifumi Kawai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

namespace Elasticsearch.Net.Utf8Json
{
    // 0 = None, 1 ~ 4 is block token, 5 ~ 9 = value token, 10 ~ 11 = delimiter token
    // you can use range-check if optimization needed.

    internal enum JsonToken : byte
    {
        None = 0,
        /// <summary>{</summary>
        BeginObject = 1,
        /// <summary>}</summary>
        EndObject = 2,
        /// <summary>[</summary>
        BeginArray = 3,
        /// <summary>]</summary>
        EndArray = 4,
        /// <summary>0~9, -</summary>
        Number = 5,
        /// <summary>"</summary>
        String = 6,
        /// <summary>t</summary>
        True = 7,
        /// <summary>f</summary>
        False = 8,
        /// <summary>n</summary>
        Null = 9,
        /// <summary>,</summary>
        ValueSeparator = 10,
        /// <summary>:</summary>
        NameSeparator = 11
    }
}
