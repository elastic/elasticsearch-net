using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IRestoreResponse : IResponse
	{

		[JsonProperty("snapshot")]
		SnapshotRestore Snapshot { get; set; }
	}

	[JsonObject]
	public class RestoreResponse : BaseResponse, IRestoreResponse
	{

		[JsonProperty("snapshot")]
		public SnapshotRestore Snapshot { get; set; }

	}
}
