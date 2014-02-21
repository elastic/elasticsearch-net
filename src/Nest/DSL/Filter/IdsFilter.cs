using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public class IdsFilter : FilterBase
	{
		internal override bool IsConditionless
		{
			get
			{
				return !this.Values.HasAny() || this.Values.All(v=>v.IsNullOrEmpty());
			}

		}

		[JsonProperty(PropertyName = "type")]
		public IEnumerable<string> Type { get; set; }
		[JsonProperty(PropertyName = "values")]
		public IEnumerable<string> Values { get; set; }
	}
}
