using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(CustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class Wildcard : Term, IQuery, IMultiTermQuery, ICustomJson
	{
		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof(StringEnumConverter))]
		public RewriteMultiTerm? Rewrite { get; set; }


		object ICustomJson.GetCustomJson()
		{	
			return new Dictionary<object, object>
			{
				{
					Field, new Dictionary<string, object>
					{
						{ "value", Value },
						{ "boost", Boost },
						{ "rewrite", Rewrite },
					}
				}
			};
		}
	}
}
