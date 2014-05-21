using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<ExistsFilter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IExistsFilter : IFilter
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }
	}

	public class ExistsFilter : FilterBase, IExistsFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return ((IExistsFilter)this).Field.IsConditionless();
			}
		}

		PropertyPathMarker IExistsFilter.Field { get; set;}
		
	}
}
