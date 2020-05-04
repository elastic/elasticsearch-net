// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class Explanation
	{
		[DataMember(Name = "description")]
		public string Description { get; internal set; }

		[DataMember(Name = "details")]
		public IReadOnlyCollection<ExplanationDetail> Details { get; internal set; } = EmptyReadOnly<ExplanationDetail>.Collection;

		[DataMember(Name = "value")]
		public float Value { get; internal set; }
	}
}
