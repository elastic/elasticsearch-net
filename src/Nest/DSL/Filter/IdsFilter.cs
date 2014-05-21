using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<IdsFilter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IIdsFilter : IFilter
	{
		[JsonProperty(PropertyName = "type")]
		IEnumerable<string> Type { get; set; }

		[JsonProperty(PropertyName = "values")]
		IEnumerable<string> Values { get; set; }
	}

	public class IdsFilter : FilterBase, IIdsFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return !((IIdsFilter)this).Values.HasAny() || ((IIdsFilter)this).Values.All(v=>v.IsNullOrEmpty());
			}

		}

		IEnumerable<string> IIdsFilter.Type { get; set; }

		IEnumerable<string> IIdsFilter.Values { get; set; }
	}
}
