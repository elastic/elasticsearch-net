using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class ExistsFilter : FilterBase
	{
		internal override bool IsConditionless
		{
			get
			{
				return this.Field.IsNullOrEmpty();
			}

		}

		[JsonProperty(PropertyName = "field")]
		public string Field { get; set;}
		
	}
}
