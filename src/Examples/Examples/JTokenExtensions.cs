using System;
using Newtonsoft.Json.Linq;

namespace Examples
{
	public static class JTokenExtensions
	{
		public static void ReplaceWithValue(this JToken toReplace, Func<JToken, JToken> newToken)
		{
			var n = newToken(toReplace);
			toReplace.Replace(n);
		}
	}
}
