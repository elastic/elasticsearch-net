using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class ExistsFilter : FilterBase
	{
		internal override bool IsConditionless
		{
			get
			{
				return this.Field.IsConditionless();
			}

		}

		[JsonProperty(PropertyName = "field")]
		public PropertyPathMarker Field { get; set;}
		
	}
}
