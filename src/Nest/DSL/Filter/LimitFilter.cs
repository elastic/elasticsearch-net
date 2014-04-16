using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public interface ILimitFilter : IFilterBase
	{
		[JsonProperty(PropertyName = "value")]
		int? Value { get; set; }
	}

	public class LimitFilter : FilterBase, ILimitFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				return !((ILimitFilter)this).Value.HasValue;
			}

		}

		int? ILimitFilter.Value { get; set;}
	}
}
