using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetLifecycleResponse : IResponse
	{
		IReadOnlyDictionary<string, LifecyclePolicy> Policies { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetLifecycleResponse, string, LifecyclePolicy>))]
	public class GetLifecycleResponse : DictionaryResponseBase<string, LifecyclePolicy>, IGetLifecycleResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, LifecyclePolicy> Policies => Self.BackingDictionary;
	}

	public class LifecyclePolicy
	{
		[JsonProperty("version")]
		public int Version { get; internal set; }

		[JsonProperty("modified_date")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset ModifiedDate { get; internal set; }

		[JsonProperty("policy")]
		public Policy Policy { get; internal set; }
	}
}
