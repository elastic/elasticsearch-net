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
	public interface IOrFilter : IFilterBase
	{
		[JsonProperty("filters")]
		IEnumerable<IFilterDescriptor> Filters { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
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
