using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmGetLifecycleResponse : IResponse
	{
		IReadOnlyDictionary<string, LifecyclePolicies> Policies { get; }
	}

	public class IlmGetLifecycleResponse : ResponseBase, IIlmGetLifecycleResponse
	{
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ShardHealthStats>))]
		public IReadOnlyDictionary<string, LifecyclePolicies> Policies { get; internal set; } = EmptyReadOnly<string, LifecyclePolicies>.Dictionary;
	}

	public class LifecyclePolicies
	{
		[JsonProperty("version")]
		public int Version { get; internal set; }

		[JsonProperty("modified_date")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTime ModifiedDate { get; internal set; }

		[JsonProperty("policy")]
		public LifecyclePolicy Policy { get; internal set; }
	}

	public class LifecyclePolicy
	{
		[JsonProperty("phases")]
		public IReadOnlyDictionary<string, Phase> Phases { get; internal set; } = EmptyReadOnly<string, Phase>.Dictionary;
	}

	public class Phase
	{
		[JsonProperty("min_age")]
		public Time MinimumAge { get; set; }

		[JsonProperty("actions")]
		public List<ILifecycleAction> Actions { get; set; }
	}
}
