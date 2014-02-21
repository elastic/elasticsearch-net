using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Converters;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(CustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class Prefix : Term, IQuery, IMultiTermQuery, ICustomJson
	{
		bool IQuery.IsConditionless { get { return this.Value == null || this.Value.ToString().IsNullOrEmpty() || this.Field.IsConditionless(); } }

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
