// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace Elastic.Transport.Extensions
{
	internal static class JsonElementExtensions
	{
		/// <summary>
		/// Fully consumes a json element representing a json object. Meaning it will attempt to unwrap all JsonElement values
		/// recursively to their actual types. This should only be used in the context of <see cref="DynamicDictionary"/> which is
		/// allowed to be slow yet convenient
		/// </summary>
		public static IDictionary<string, object> ToDictionary(this JsonElement e) =>
			e.ValueKind switch
			{
				JsonValueKind.Object => e.EnumerateObject()
					.Aggregate(new Dictionary<string, object>(), (dict, je) =>
					{
						dict.Add(je.Name, DynamicValue.ConsumeJsonElement(typeof(object), je.Value));
						return dict;
					}),
				JsonValueKind.Array => e.EnumerateArray()
					.Select((je, i) => (i, o: DynamicValue.ConsumeJsonElement(typeof(object), je)))
					.Aggregate(new Dictionary<string, object>(), (dict, t) =>
					{
						dict.Add(t.i.ToString(CultureInfo.InvariantCulture), t.o);
						return dict;
					}),
				_ => null
			};
	}
}
