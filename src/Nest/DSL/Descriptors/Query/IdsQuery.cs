using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class IdsQuery
	{
		[JsonProperty(PropertyName = "type")]
		public IEnumerable<string> Type { get; set; }
		[JsonProperty(PropertyName = "values")]
		public IEnumerable<string> Values { get; set; }

		internal bool IsConditionless
		{
			get
			{
				return !this.Values.HasAny() || this.Values.All(s=>s.IsNullOrEmpty());
			}
		}
	}
}
