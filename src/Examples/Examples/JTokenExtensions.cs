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
		public static JToken FixSimpleTerm(this JToken termSimple) => termSimple.ReplaceWithValue(v=>new JObject { { "value", v } });
		public static JToken FixSimpleQuery(this JToken querySimple) => querySimple.ReplaceWithValue(v=>new JObject { { "query", v } });

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
