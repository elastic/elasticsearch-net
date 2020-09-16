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

using System.Linq;
using Elasticsearch.Net.Utf8Json.Formatters;

namespace Elasticsearch.Net.Utf8Json.Resolvers
{
	internal static class StandardResolver
	{
		/// <summary>AllowPrivate:False, ExcludeNull:False, NameMutate:Original</summary>
		public static readonly IJsonFormatterResolver Default = DefaultStandardResolver.Instance;
	}

	internal static class StandardResolverHelper
	{
		internal static readonly IJsonFormatterResolver[] CompositeResolverBase =
		{
			BuiltinResolver.Instance, // Builtin
			EnumResolver.Default,     // Enum(default => string)
			DynamicGenericResolver.Instance, // T[], List<T>, etc...
			AttributeFormatterResolver.Instance // [JsonFormatter]
		};
	}

	internal sealed class DefaultStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new DefaultStandardResolver();

		private static readonly IJsonFormatter<object> FallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		private DefaultStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
					formatter = (IJsonFormatter<T>)FallbackFormatter;
				else
					formatter = InnerResolver.Instance.GetFormatter<T>();
			}
		}

		private sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			private static readonly IJsonFormatterResolver[] Resolvers =
				StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.Default }).ToArray();

			private InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

			private static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in Resolvers)
					{
						var f = item.GetFormatter<T>();
						if (f != null)
						{
							formatter = f;
							return;
						}
					}
				}
			}
		}
	}
}
