/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Concurrent;

namespace Nest.Utf8Json
{
	internal class ElasticsearchNetFormatterResolver : IJsonFormatterResolver
	{
		private readonly IJsonFormatter<object> _fallbackFormatter;
		private readonly InnerResolver _innerFormatterResolver;

		public ElasticsearchNetFormatterResolver()
		{
			_innerFormatterResolver = new InnerResolver();
			_fallbackFormatter = new DynamicObjectTypeFallbackFormatter(_innerFormatterResolver);
		}

		public static ElasticsearchNetFormatterResolver Instance { get; } = new ElasticsearchNetFormatterResolver();

		public IJsonFormatter<T> GetFormatter<T>() =>
			typeof(T) == typeof(object)
				? (IJsonFormatter<T>)_fallbackFormatter
				: _innerFormatterResolver.GetFormatter<T>();

		internal sealed class InnerResolver : IJsonFormatterResolver
		{
			private static readonly IJsonFormatterResolver[] Resolvers =
			{
				BuiltinResolver.Instance, // Builtin primitives
				ElasticsearchNetEnumResolver.Instance, // Specialized Enum handling
				AttributeFormatterResolver.Instance, // [JsonFormatter]
				DynamicGenericResolver.Instance, // T[], List<T>, etc...
				ExceptionFormatterResolver.Instance
			};

			private readonly IJsonFormatterResolver _finalFormatter;
			private readonly ConcurrentDictionary<Type, object> _formatters = new ConcurrentDictionary<Type, object>();

			internal InnerResolver() =>
				_finalFormatter =
					DynamicObjectResolver.Create(null, new Lazy<Func<string, string>>(() => StringMutator.Original), true);

			public IJsonFormatter<T> GetFormatter<T>() =>
				(IJsonFormatter<T>)_formatters.GetOrAdd(typeof(T), type =>
				{
					foreach (var item in Resolvers)
					{
						var formatter = item.GetFormatter<T>();
						if (formatter != null)
							return formatter;
					}

					return _finalFormatter.GetFormatter<T>();
				});
		}
	}
}
