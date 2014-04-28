using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using Elasticsearch.Net;

namespace Nest
{
	public interface IAndFilter : IFilterBase
	{
		[JsonProperty("filters")]
		IEnumerable<IFilterDescriptor> Filters { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class AndFilter : FilterBase, IAndFilter
	{

		IEnumerable<IFilterDescriptor> IAndFilter.Filters { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				var af = ((IAndFilter)this);
				return !af.Filters.HasAny() || af.Filters.All(f=>f.IsConditionless);
			} 
		}
	}
}
