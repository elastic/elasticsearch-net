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
		/// <summary>AllowPrivate:False, ExcludeNull:False, NameMutate:CamelCase</summary>
		public static readonly IJsonFormatterResolver CamelCase = CamelCaseStandardResolver.Instance;
		/// <summary>AllowPrivate:False, ExcludeNull:False, NameMutate:SnakeCase</summary>
		public static readonly IJsonFormatterResolver SnakeCase = SnakeCaseStandardResolver.Instance;
		/// <summary>AllowPrivate:False, ExcludeNull:True,  NameMutate:Original</summary>
		public static readonly IJsonFormatterResolver ExcludeNull = ExcludeNullStandardResolver.Instance;
		/// <summary>AllowPrivate:False, ExcludeNull:True,  NameMutate:CamelCase</summary>
		public static readonly IJsonFormatterResolver ExcludeNullCamelCase = ExcludeNullCamelCaseStandardResolver.Instance;
		/// <summary>AllowPrivate:False, ExcludeNull:True,  NameMutate:SnakeCase</summary>
		public static readonly IJsonFormatterResolver ExcludeNullSnakeCase = ExcludeNullSnakeCaseStandardResolver.Instance;

		/// <summary>AllowPrivate:True,  ExcludeNull:False, NameMutate:Original</summary>
		public static readonly IJsonFormatterResolver AllowPrivate = AllowPrivateStandardResolver.Instance;
		/// <summary>AllowPrivate:True,  ExcludeNull:False, NameMutate:CamelCase</summary>
		public static readonly IJsonFormatterResolver AllowPrivateCamelCase = AllowPrivateCamelCaseStandardResolver.Instance;
		/// <summary>AllowPrivate:True,  ExcludeNull:False, NameMutate:SnakeCase</summary>
		public static readonly IJsonFormatterResolver AllowPrivateSnakeCase = AllowPrivateSnakeCaseStandardResolver.Instance;
		/// <summary>AllowPrivate:True,  ExcludeNull:True,  NameMutate:Original</summary>
		public static readonly IJsonFormatterResolver AllowPrivateExcludeNull = AllowPrivateExcludeNullStandardResolver.Instance;
		/// <summary>AllowPrivate:True,  ExcludeNull:True,  NameMutate:CamelCase</summary>
		public static readonly IJsonFormatterResolver AllowPrivateExcludeNullCamelCase = AllowPrivateExcludeNullCamelCaseStandardResolver.Instance;
		/// <summary></summary>AllowPrivate:True,  ExcludeNull:True,  NameMutate:SnakeCase</summary>
		public static readonly IJsonFormatterResolver AllowPrivateExcludeNullSnakeCase = AllowPrivateExcludeNullSnakeCaseStandardResolver.Instance;
	}

	internal static class StandardResolverHelper
	{
		internal static readonly IJsonFormatterResolver[] CompositeResolverBase = new[]
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

		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		DefaultStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.Default }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class CamelCaseStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new CamelCaseStandardResolver();

		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		CamelCaseStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.CamelCase }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class SnakeCaseStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new SnakeCaseStandardResolver();

		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		SnakeCaseStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.SnakeCase }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class ExcludeNullStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new ExcludeNullStandardResolver();

		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		ExcludeNullStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.ExcludeNull }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class ExcludeNullCamelCaseStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new ExcludeNullCamelCaseStandardResolver();

		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		ExcludeNullCamelCaseStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.ExcludeNullCamelCase }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class ExcludeNullSnakeCaseStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new ExcludeNullSnakeCaseStandardResolver();


		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		ExcludeNullSnakeCaseStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.ExcludeNullSnakeCase }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class AllowPrivateStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new AllowPrivateStandardResolver();


		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		AllowPrivateStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.AllowPrivate }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class AllowPrivateCamelCaseStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new AllowPrivateCamelCaseStandardResolver();


		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		AllowPrivateCamelCaseStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.AllowPrivateCamelCase }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class AllowPrivateSnakeCaseStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new AllowPrivateSnakeCaseStandardResolver();


		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		AllowPrivateSnakeCaseStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.AllowPrivateSnakeCase }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class AllowPrivateExcludeNullStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new AllowPrivateExcludeNullStandardResolver();


		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		AllowPrivateExcludeNullStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.AllowPrivateExcludeNull }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class AllowPrivateExcludeNullCamelCaseStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new AllowPrivateExcludeNullCamelCaseStandardResolver();


		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		AllowPrivateExcludeNullCamelCaseStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.AllowPrivateExcludeNullCamelCase }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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

	internal sealed class AllowPrivateExcludeNullSnakeCaseStandardResolver : IJsonFormatterResolver
	{
		// configure
		public static readonly IJsonFormatterResolver Instance = new AllowPrivateExcludeNullSnakeCaseStandardResolver();


		static readonly IJsonFormatter<object> fallbackFormatter = new DynamicObjectTypeFallbackFormatter(InnerResolver.Instance);

		AllowPrivateExcludeNullSnakeCaseStandardResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				if (typeof(T) == typeof(object))
				{
					formatter = (IJsonFormatter<T>)fallbackFormatter;
				}
				else
				{
					formatter = InnerResolver.Instance.GetFormatter<T>();
				}
			}
		}

		sealed class InnerResolver : IJsonFormatterResolver
		{
			public static readonly IJsonFormatterResolver Instance = new InnerResolver();

			static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.Concat(new[] { DynamicObjectResolver.AllowPrivateExcludeNullSnakeCase }).ToArray();

			InnerResolver()
			{
			}

			public IJsonFormatter<T> GetFormatter<T>()
			{
				return FormatterCache<T>.formatter;
			}

			static class FormatterCache<T>
			{
				public static readonly IJsonFormatter<T> formatter;

				static FormatterCache()
				{
					foreach (var item in resolvers)
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
