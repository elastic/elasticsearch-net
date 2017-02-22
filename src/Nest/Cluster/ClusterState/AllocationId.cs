using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class AllocationId
	{
		[JsonProperty("id")]
		public string Id { get; internal set; }
	}
}