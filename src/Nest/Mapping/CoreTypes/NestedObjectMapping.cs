using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class NestedObjectMapping : ObjectMapping
	{
		[JsonProperty("type")]
		public override TypeNameMarker Type
		{
			get { return new TypeNameMarker { Name = "nested" }; }
		}

		[JsonProperty("include_in_parent")]
		public bool? IncludeInParent { get; set; }

		[JsonProperty("include_in_root")]
		public bool? IncludeInRoot { get; set; }

	}
}