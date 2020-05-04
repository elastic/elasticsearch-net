// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetEnrichPolicyResponse : ResponseBase
	{
		[DataMember(Name = "policies")]
		public IReadOnlyCollection<NamedPolicyMetadata> Policies { get; internal set; } = EmptyReadOnly<NamedPolicyMetadata>.Collection;
	}
}
