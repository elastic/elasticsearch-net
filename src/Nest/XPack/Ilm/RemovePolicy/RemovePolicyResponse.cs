// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class RemovePolicyResponse : ResponseBase
	{
		[DataMember(Name = "failed_indexes")]
		public IReadOnlyCollection<string> FailedIndexes { get; internal set; } = EmptyReadOnly<string>.Collection;
		[DataMember(Name = "has_failures")]
		public bool HasFailures { get; internal set; }
	}
}
