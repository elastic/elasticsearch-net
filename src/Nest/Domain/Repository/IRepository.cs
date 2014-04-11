using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IRepository
	{
		[JsonProperty("type")]
		string Type { get; }
		[JsonProperty("settings")]
		IDictionary<string, object> Settings { get; }
	}
}