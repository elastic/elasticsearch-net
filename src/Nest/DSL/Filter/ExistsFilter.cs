using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public interface IExistsFilter : IFilterBase
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }
	}

	public class ExistsFilter : FilterBase, IExistsFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IExistsFilter)this).Field.IsConditionless();
			}
		}

		PropertyPathMarker IExistsFilter.Field { get; set;}
		
	}
}
