using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Globalization;
using Nest.Resolvers;

namespace Nest
{
	internal static class Extensions
	{

		internal static bool JsonEquals(this string json, string otherjson)
		{
			var nJson = JObject.Parse(json).ToString();
			var nOtherJson = JObject.Parse(otherjson).ToString();
			return nJson == nOtherJson;
		}

		internal static bool IsNullOrEmpty(this TypeNameMarker value)
		{
			return value == null || value.GetHashCode() == 0;
		}


	}



}
