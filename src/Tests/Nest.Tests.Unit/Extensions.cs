using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.Unit
{
	public static class JsonExtensions
	{
		internal static bool JsonEquals(this string json, string otherjson)
		{
			var nJson = JObject.Parse(json).ToString();
			var nOtherJson = JObject.Parse(otherjson).ToString();
			//Assert.AreEqual(nOtherJson, nJson);
			return nJson == nOtherJson;
		}
	}
}
