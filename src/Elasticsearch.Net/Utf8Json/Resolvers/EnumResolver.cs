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
using System.Reflection;
using Elasticsearch.Net.Utf8Json.Formatters;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net.Utf8Json.Resolvers
{
	internal static class EnumResolver
	{
		/// <summary>Serialize as Name.</summary>
		public static readonly IJsonFormatterResolver Default = EnumDefaultResolver.Instance;
		/// <summary>Serialize as Value.</summary>
		public static readonly IJsonFormatterResolver UnderlyingValue = EnumUnderlyingValueResolver.Instance;
	}

	internal sealed class EnumDefaultResolver : IJsonFormatterResolver
	{
		public static readonly IJsonFormatterResolver Instance = new EnumDefaultResolver();

		private EnumDefaultResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				var type = typeof(T);

				if (type.IsNullable())
				{
					// build underlying type and use wrapped formatter.
					type = type.GenericTypeArguments[0];
					if (!type.IsEnum)
						return;

					var innerFormatter = Instance.GetFormatterDynamic(type);
					if (innerFormatter == null)
						return;

					formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(type), innerFormatter);
					return;
				}

				if (typeof(T).IsEnum)
					formatter = new EnumFormatter<T>(true);
			}
		}
	}

	internal sealed class EnumUnderlyingValueResolver : IJsonFormatterResolver
	{
		public static readonly IJsonFormatterResolver Instance = new EnumUnderlyingValueResolver();

		private EnumUnderlyingValueResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				var type = typeof(T);

				if (type.IsNullable())
				{
					// build underlying type and use wrapped formatter.
					type = type.GenericTypeArguments[0];
					if (!type.IsEnum) return;

					var innerFormatter = Instance.GetFormatterDynamic(type);
					if (innerFormatter == null)
						return;

					formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(type), innerFormatter);
					return;
				}

				if (typeof(T).IsEnum)
					formatter = new EnumFormatter<T>(false);
			}
		}
	}
}
