using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	/// <summary>
	/// Marker class that signals to the CustomJsonConverter to write the string verbatim
	/// </summary>
	internal class RawJson
	{
		public string Data { get; set; }

		public RawJson(string rawJsonData)
		{
			Data = rawJsonData;
		}
	}
	
	[JsonConverter(typeof(CustomJsonConverter))]
	internal class RawJsonWrapper<T> 
	{
		
	}
}
