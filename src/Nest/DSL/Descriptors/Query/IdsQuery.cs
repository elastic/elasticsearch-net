using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class IdsQuery : IQuery
	{
		[JsonProperty(PropertyName = "type")]
		public IEnumerable<string> Type { get; set; }
		[JsonProperty(PropertyName = "values")]
		public IEnumerable<string> Values { get; set; }

		public bool IsConditionless
		{
			get
			{
				return !this.Values.HasAny() || this.Values.All(s=>s.IsNullOrEmpty());
			}
		}
	}
}
