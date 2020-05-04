// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class TermVector
	{
		[DataMember(Name ="field_statistics")]
		public FieldStatistics FieldStatistics { get; internal set; }

		[DataMember(Name ="terms")]
		public IReadOnlyDictionary<string, TermVectorTerm> Terms { get; internal set; } =
			EmptyReadOnly<string, TermVectorTerm>.Dictionary;
	}
}
