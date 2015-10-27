using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IRepository
	{
		[JsonProperty("type")]
		string Type { get; }
	}

	public class SnapshotRepository : IRepository
	{
		public string Type { get; internal set; }
	}
}