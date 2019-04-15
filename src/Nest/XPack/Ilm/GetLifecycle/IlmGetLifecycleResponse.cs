using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmGetLifecycleResponse : IResponse
	{
		IReadOnlyDictionary<string, LifecyclePolicy> Policies { get; }
	}

	public class IlmGetLifecycleResponse : ResponseBase, IIlmGetLifecycleResponse
	{
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, LifecyclePolicy>))]
		public IReadOnlyDictionary<string, LifecyclePolicy> Policies { get; internal set; } = EmptyReadOnly<string, LifecyclePolicy>.Dictionary;
	}

	public class LifecyclePolicy
	{
		[JsonProperty("version")]
		public int Version { get; internal set; }

		[JsonProperty("modified_date")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTime ModifiedDate { get; internal set; }

		[JsonProperty("policy")]
		public Policy Policy { get; internal set; }
	}
}
