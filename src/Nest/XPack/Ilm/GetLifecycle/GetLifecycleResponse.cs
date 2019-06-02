using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetLifecycleResponse, string, LifecyclePolicy>))]
	public class GetLifecycleResponse : DictionaryResponseBase<string, LifecyclePolicy>
	{
		public IReadOnlyDictionary<string, LifecyclePolicy> Policies => Self.BackingDictionary;
	}

	public class LifecyclePolicy
	{
		[DataMember(Name = "modified_date")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset ModifiedDate { get; internal set; }

		[DataMember(Name = "policy")]
		public Policy Policy { get; internal set; }

		[DataMember(Name = "version")]
		public int Version { get; internal set; }
	}
}
