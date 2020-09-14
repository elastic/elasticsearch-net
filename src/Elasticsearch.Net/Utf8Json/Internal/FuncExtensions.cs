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

using System;
// ReSharper disable All

namespace Elasticsearch.Net.Utf8Json.Internal
{
    // Unity compiler can't understand this.

    internal static class FuncExtensions
    {
        // hack of avoid closure allocation(() => value).
        public static Func<T> AsFunc<T>(this T value) => new Func<T>(value.ReturnBox<T>);

		public static Func<T> AsFuncFast<T>(this T value) where T : class => new Func<T>(value.Return<T>);

		static T Return<T>(this T value) => value;

		static T ReturnBox<T>(this object value) => (T)value;
	}
}

