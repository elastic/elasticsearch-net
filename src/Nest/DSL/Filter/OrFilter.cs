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
	[JsonConverter(typeof(ReadAsTypeConverter<OrFilter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IOrFilter : IFilterBase
	{
		[JsonProperty("filters",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeConverter<BaseFilterDescriptor>, CustomJsonConverter>))]
		IEnumerable<IFilterDescriptor> Filters { get; set; }
	}

	public class OrFilter : FilterBase, IOrFilter
	{

		IEnumerable<IFilterDescriptor> IOrFilter.Filters { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				var af = ((IOrFilter)this);
				return !af.Filters.HasAny() || af.Filters.All(f=>f.IsConditionless);
			} 
		}
	}
}
