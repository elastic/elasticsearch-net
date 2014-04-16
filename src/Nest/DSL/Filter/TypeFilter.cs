using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITypeFilter : IFilterBase
	{
		[JsonProperty(PropertyName = "value")]
		TypeNameMarker Value { get; set; }
	}

	public class TypeFilter : FilterBase, ITypeFilter
	{
		bool IFilterBase.IsConditionless
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
