// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[ReadAs(typeof(TermVectorsResult))]
	public interface ITermVectors
	{
		bool Found { get; }
		string Id { get; }
		string Index { get; }
		IReadOnlyDictionary<Field, TermVector> TermVectors { get; }
		long Took { get; }
		long Version { get; }
	}

	public class TermVectorsResult : ITermVectors
	{
		[DataMember(Name ="found")]
		public bool Found { get; internal set; }

		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_index")]
		public string Index { get; internal set; }

		[DataMember(Name ="term_vectors")]
		[JsonFormatter(typeof(ResolvableReadOnlyDictionaryFormatter<Field, TermVector>))]
		public IReadOnlyDictionary<Field, TermVector> TermVectors { get; internal set; } = EmptyReadOnly<Field, TermVector>.Dictionary;

		[DataMember(Name ="took")]
		public long Took { get; internal set; }

		[DataMember(Name ="_version")]
		public long Version { get; internal set; }
	}
}
