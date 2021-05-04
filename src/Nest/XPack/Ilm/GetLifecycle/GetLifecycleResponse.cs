// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
