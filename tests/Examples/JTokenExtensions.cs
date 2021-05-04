// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Newtonsoft.Json.Linq;

namespace Examples
{
	public static class JTokenExtensions
	{
		public static JToken ReplaceWithValue(this JToken toReplace, Func<JToken, JToken> newToken)
		{
			var n = newToken(toReplace);
			toReplace.Replace(n);
			return n;
		}
		/// <summary>
		/// The docs document queries often in short form e.g
		/// <code>
		/// { "field" : "value" }
		/// </code>
		/// Where as NEST always generates the long form query
		/// <code>
		/// { "field" : { "query" : "value" }
		/// </code>
		/// <c>NOTE:</c> Term query uses a slightly different long form so use <see cref="ToLongFormTermQuery"/> instead.
		/// </summary>
		public static JToken ToLongFormQuery(this JToken querySimple) => querySimple.ReplaceWithValue(v => new JObject { { "query", v } });

		/// <summary>
		/// The docs document term queries often in short form e.g
		/// <code>
		/// { "field" : "value" }
		/// </code>
		/// Where as NEST always generates the long form query
		/// <code>
		/// { "field" : { "value" : "value" }
		/// </code>
		/// <c>NOTE:</c> Term query is slightly special, most queries expand to use the `query` key, use <see cref="ToLongFormQuery"/> instead.
		/// </summary>
		public static JToken ToLongFormTermQuery(this JToken termSimple) => termSimple.ReplaceWithValue(v => new JObject { { "value", v } });

		public static JArray ToJArray(this JToken o)
		{
			if (o is JArray arr) return arr;
			var a = new JArray(o);
			if (o == null) return a;
			o.Replace(a);
			return a;
		}

		public static void AdjustIndexSettings(this Example e) =>
			e.ApplyBodyChanges(o =>
			{
				var settings = ((JObject)o["settings"]);
				var existing = settings["number_of_shards"].Value<int>();
				settings.Remove("number_of_shards");
				settings.Add("index.number_of_shards", existing);
			});

		/// <summary>
		/// Flattens an array into a comma-seperated representation of that array.
		/// <code>"index": ["twitter", "blog"]</code>
		/// Will be converted to
		/// <code>"index": "twitter, blog"</code>
		/// </summary>
		public static JToken Flatten<T>(this JToken o)
		{
			var array = (JArray)o;
			var values = array.Values<T>();
			var flat = string.Join(",", values);
			return JToken.FromObject(flat);
		}

		/// <summary>
		/// Docs document bool query without the clauses as arrays, NEST only every sends them as arrays
		/// This fixes the examples
		/// </summary>
		public static void ToLongFormBoolQuery(this JToken o, Action<JObject> mutateBool = null)
		{
			if (!(o is JObject obj)) return;

			_ = obj["must"].ToJArray();
			_ = obj["must_not"].ToJArray();
			_ = obj["filter"].ToJArray();
			_ = obj["should"].ToJArray();
			mutateBool?.Invoke(obj);
		}

	}

}
