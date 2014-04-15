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
	public interface IPrefixQuery : ITermQuery
	{
		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }
	}

	[JsonConverter(typeof(CustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class PrefixQueryDescriptor<T> : TermQueryDescriptorBase<PrefixQueryDescriptor<T>, T>, ICustomJson, IPrefixQuery where T : class
	{
		RewriteMultiTerm? IPrefixQuery.Rewrite { get; set; }

		public PrefixQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IPrefixQuery)this).Rewrite = rewrite;
			return this;
		}

		object ICustomJson.GetCustomJson()
		{
			var pq = ((IPrefixQuery)this);
			return new Dictionary<object, object>
			{
				{
					pq.Field, new Dictionary<string, object>
					{
						{ "value", pq.Value },
						{ "boost", pq.Boost },
						{ "rewrite", pq.Rewrite },
					}
				}
			};
		}
	}
}
