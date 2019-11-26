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
		/// <summary>
		/// Docs document bool query without the clauses as arrays, NEST only every sends them as arrays
		/// This fixes the examples
		/// </summary>
		public static (JArray must, JArray mustNot, JArray filter, JArray should) FixBoolQuery(this JToken o)
		{
			var empty = new JArray();
			if (!(o is JObject obj)) return (empty, empty, empty, empty);

			obj = obj.ContainsKey("bool") ? obj["bool"] as JObject : obj;

			if (obj == null) return (empty, empty, empty, empty);

			var m = obj["must"].ToJArray();
			var mn = obj["must_not"].ToJArray();
			var f = obj["filter"].ToJArray();
			var s = obj["should"].ToJArray();
			return (m, mn, f, s);
		}

	}

}
