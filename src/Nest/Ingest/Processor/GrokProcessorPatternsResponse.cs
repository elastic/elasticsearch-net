// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GrokProcessorPatternsResponse : ResponseBase
	{
		[DataMember(Name ="patterns")]
		public IReadOnlyDictionary<string, string> Patterns { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;
	}
}
