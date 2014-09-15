using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public static class FuzzinessConverterHelper
	{
		public static IFuzziness ReadJson(IDictionary<string, JToken> jObject)
		{
			JToken jToken;

			if (!jObject.TryGetValue("fuzziness", out jToken)) return null;
			if (jToken.Type == JTokenType.String) return Fuzziness.Auto;
			if (jToken.Type == JTokenType.Float) return Fuzziness.Ratio(jToken.Value<double>());
			if (jToken.Type == JTokenType.Integer) return Fuzziness.EditDistance(jToken.Value<int>());

			return null;
		}
	}
}