using System;
using Newtonsoft.Json;

namespace Nest
{
	public class NestedObjectMapping : ObjectMapping
	{
		[JsonProperty("type")]
		public override string Type { get { return "nested"; } }

		[JsonProperty("include_in_parent")]
		public bool? IncludeInParent { get; set; }

		[JsonProperty("include_in_root")]
		public bool? IncludeInRoot { get; set; }

	}
}