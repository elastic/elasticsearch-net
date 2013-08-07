using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class TypeFilter : FilterBase
	{
		internal override bool IsConditionless
		{
			get
			{
				return this.Value.IsConditionless();
			}

		}

		[JsonProperty(PropertyName = "value")]
		public TypeNameMarker Value { get; set; }
	}
}
