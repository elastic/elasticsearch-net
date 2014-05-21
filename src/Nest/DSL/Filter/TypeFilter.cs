using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<TypeFilter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITypeFilter : IFilter
	{
		[JsonProperty(PropertyName = "value")]
		TypeNameMarker Value { get; set; }
	}

	public class TypeFilter : FilterBase, ITypeFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return ((ITypeFilter)this).Value.IsConditionless();
			}

		}

		[JsonProperty(PropertyName = "value")]
		TypeNameMarker ITypeFilter.Value { get; set; }
	}
}
