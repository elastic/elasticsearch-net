using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IWildcardQuery : ITermQuery
	{
		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }
	}

	[JsonConverter(typeof(CustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class WildcardQueryDescriptor<T> : TermQueryDescriptorBase<WildcardQueryDescriptor<T>, T>, ICustomJson, IWildcardQuery 
		where T : class
	{
		RewriteMultiTerm? IWildcardQuery.Rewrite { get; set; }

		public WildcardQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IWildcardQuery)this).Rewrite = rewrite;
			return this;
		}

		object ICustomJson.GetCustomJson()
		{
			var wq = ((IWildcardQuery)this);
			return new Dictionary<object, object>
			{
				{
					wq.Field, new Dictionary<string, object>
					{
						{ "value", wq.Value },
						{ "boost", wq.Boost },
						{ "rewrite", wq.Rewrite },
					}
				}
			};
		}
	}
}
