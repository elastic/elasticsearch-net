using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(TermConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class Wildcard : Term, IQuery, IMultiTermQuery
	{
		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof(StringEnumConverter))]
		public RewriteMultiTerm? Rewrite { get; set; }

		public Wildcard()
		{

		}
	}
}
