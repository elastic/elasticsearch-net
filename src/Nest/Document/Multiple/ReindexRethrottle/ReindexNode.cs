using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class ReindexNode
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }

		[JsonProperty("host")]
		public string Host { get; internal set; }

		[JsonProperty("ip")]
		public string Ip { get; internal set; }

		[JsonProperty("roles")]
		public IEnumerable<string> Roles { get; internal set; }

		[JsonProperty("attributes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, string>))]
		public IReadOnlyDictionary<string, string> Attributes { get; internal set; } =
			EmptyReadOnly<string, string>.Dictionary;

		[JsonProperty("tasks")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<TaskId, ReindexTask>))]
		public IReadOnlyDictionary<TaskId, ReindexTask> Tasks { get; internal set; } =
			EmptyReadOnly<TaskId, ReindexTask>.Dictionary;
	}


	public class ReindexTask
	{
		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("id")]
		public long Id { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }

		[JsonProperty("action")]
		public string Action { get; internal set; }

		[JsonProperty("status")]
		public ReindexStatus Status { get; internal set; }

		[JsonProperty("description")]
		public string Description { get; internal set; }

		[JsonProperty("start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[JsonProperty("running_time_in_nanos")]
		public long RunningTimeInNanoseconds { get; internal set; }

		[JsonProperty("cancellable")]
		public bool Cancellable { get; internal set; }
	}

	public class ReindexStatus
	{
		[JsonProperty("total")]
		public long Total { get; internal set; }

		[JsonProperty("updated")]
		public long Updated { get; internal set; }

		[JsonProperty("created")]
		public long Created { get; internal set; }

		[JsonProperty("deleted")]
		public long Deleted { get; internal set; }

		[JsonProperty("batches")]
		public long Batches { get; internal set; }

		[JsonProperty("version_conflicts")]
		public long VersionConflicts { get; internal set; }

		[JsonProperty("noops")]
		public long Noops { get; internal set; }

		[JsonProperty("retries")]
		public Retries Retries { get; internal set; }

		[JsonProperty("throttled_millis")]
		public long ThrottledInMilliseconds { get; internal set; }

		[JsonProperty("requests_per_second")]
		public float RequestsPerSecond { get; internal set; }

		[JsonProperty("throttled_until_millis")]
		public long ThrottledUntilInMilliseconds { get; internal set; }
	}
}

