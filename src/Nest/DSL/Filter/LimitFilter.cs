using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<LimitFilter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ILimitFilter : IFilter
	{
		[JsonProperty(PropertyName = "value")]
		int? Value { get; set; }
	}

	public class LimitFilter : FilterBase, ILimitFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return !((ILimitFilter)this).Value.HasValue;
			}

		}

		int? ILimitFilter.Value { get; set;}
	}
}
