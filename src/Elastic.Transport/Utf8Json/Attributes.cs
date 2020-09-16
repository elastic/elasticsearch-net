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

namespace Elasticsearch.Net.Utf8Json
{
<<<<<<< HEAD
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Property)]
	internal class JsonFormatterAttribute : Attribute
=======
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class JsonFormatterAttribute : Attribute
>>>>>>> everything compiles, utf8 is mostly public in anticipation of move to NEST
    {
        public Type FormatterType { get; }
        public object[] Arguments { get; }

        public JsonFormatterAttribute(Type formatterType) => FormatterType = formatterType;

		public JsonFormatterAttribute(Type formatterType, params object[] arguments)
        {
            FormatterType = formatterType;
            Arguments = arguments;
        }
    }

<<<<<<< HEAD
    [AttributeUsage(AttributeTargets.Constructor)]
	internal class SerializationConstructorAttribute : Attribute
=======
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = true)]
	public class SerializationConstructorAttribute : Attribute
>>>>>>> everything compiles, utf8 is mostly public in anticipation of move to NEST
    {
    }

<<<<<<< HEAD
    [AttributeUsage(AttributeTargets.Interface)]
	internal class InterfaceDataContractAttribute : Attribute
=======
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public class InterfaceDataContractAttribute : Attribute
>>>>>>> everything compiles, utf8 is mostly public in anticipation of move to NEST
    {
    }
}
