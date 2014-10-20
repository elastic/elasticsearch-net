using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<SnapshotRepository>))]
	public interface IRepository
	{

		[JsonProperty("settings")]
		IDictionary<string, object> Settings { get; set; }

		[JsonProperty("type")]
		string Type { get; }
	}


	public class SnapshotRepository : IRepository
	{
		public IDictionary<string, object> Settings { get; set; }
		public string Type { get; internal set; }
	}

}