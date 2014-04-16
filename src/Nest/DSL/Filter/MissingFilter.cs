using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMissingFilter : IFilterBase
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }
	}

	public class MissingFilter : FilterBase, IMissingFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IMissingFilter)this).Field.IsConditionless();
			}

		}

		PropertyPathMarker IMissingFilter.Field { get; set;}
	}
}
