// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Elasticsearch.Net.Extensions
{
	internal static class EnumExtensions
	{
		internal static readonly ConcurrentDictionary<Type, Func<Enum, string>> EnumStringResolvers = new ConcurrentDictionary<Type, Func<Enum, string>>();
		public static string GetStringValue(this Enum e)
		{
			var type = e.GetType();
			var resolver = EnumStringResolvers.GetOrAdd(type, GetEnumStringResolver);
			return resolver(e);
		}

		private class EnumDictionary : Dictionary<Enum, string>
		{
			public EnumDictionary(int capacity): base(capacity)
			{
			}

			public Func<Enum, string> Resolver
			{
				get;
				set;
			}
		}


		private static Func<Enum, string> GetEnumStringResolver(Type type)
		{
			var values = Enum.GetValues(type);
			var dictionary = new EnumDictionary(values.Length);
			for (var index = 0; index < values.Length; index++)
			{
				var value = values.GetValue(index);
				var info = type.GetField(value.ToString());
				var da = (EnumMemberAttribute[])info.GetCustomAttributes(typeof(EnumMemberAttribute), false);
				var stringValue = da.Length > 0 ? da[0].Value : Enum.GetName(type, value);
				dictionary.Add((Enum)value, stringValue);
			}

			var isFlag = type.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0;
			return (e) =>
			{
				if (isFlag)
				{
					var list = new List<string>();
					foreach (var kv in dictionary)
					{
						if (e.HasFlag(kv.Key))
							list.Add(kv.Value);
					}

					return string.Join(",", list);
				}
				else
				{
					return dictionary[e];
				}
			}

			;
		}
	}
}
