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

namespace Elasticsearch.Net.Utf8Json.Formatters
{
	internal sealed class NullableFormatter<T> : IJsonFormatter<T?>
        where T : struct
    {
        public void Serialize(ref JsonWriter writer, T? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
				writer.WriteNull();
			else
				formatterResolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Value, formatterResolver);
		}

        public T? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			return formatterResolver.GetFormatterWithVerify<T>().Deserialize(ref reader, formatterResolver);
		}
    }

	internal sealed class StaticNullableFormatter<T> : IJsonFormatter<T?>
        where T : struct
    {
		private readonly IJsonFormatter<T> _underlyingFormatter;

        public StaticNullableFormatter(IJsonFormatter<T> underlyingFormatter) => _underlyingFormatter = underlyingFormatter;

		public StaticNullableFormatter(Type formatterType, object[] formatterArguments)
        {
            try
            {
                _underlyingFormatter = (IJsonFormatter<T>)Activator.CreateInstance(formatterType, formatterArguments);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Can not create formatter from JsonFormatterAttribute, check the target formatter is public and has constructor with right argument. FormatterType:" + formatterType.Name, ex);
            }
        }

        public void Serialize(ref JsonWriter writer, T? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
				writer.WriteNull();
			else
				_underlyingFormatter.Serialize(ref writer, value.Value, formatterResolver);
		}

        public T? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
				return null;
			else
				return _underlyingFormatter.Deserialize(ref reader, formatterResolver);
		}
    }
}
