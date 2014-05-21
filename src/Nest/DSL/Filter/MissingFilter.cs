using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<MissingFilter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMissingFilter : IFilter
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }
	}

	public class MissingFilter : FilterBase, IMissingFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return ((IMissingFilter)this).Field.IsConditionless();
			}

		}

		PropertyPathMarker IMissingFilter.Field { get; set;}
	}
}
