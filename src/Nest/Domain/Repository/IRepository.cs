using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IRepository
	{

		[JsonProperty("settings")]
		IDictionary<string, object> Settings { get; set; }

		[JsonProperty("type")]
		string Type { get; }
	}
}