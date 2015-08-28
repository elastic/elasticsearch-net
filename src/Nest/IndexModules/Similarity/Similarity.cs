using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface ISimilarity
	{
		[JsonProperty("type")]
		string Type { get; }
	}
}
