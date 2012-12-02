using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class LimitFilter : FilterBase
	{
		internal override bool IsConditionless
		{
			get
			{
				return !this.Value.HasValue;
			}

		}

		[JsonProperty(PropertyName = "value")]
		public int? Value { get; set;}
	}
}
