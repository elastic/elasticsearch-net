// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	public class TermVectorsResponse : ResponseBase
	{
		/// <summary>
		/// TermVector API returns 200 even if <see cref="Found"/>;
		/// </summary>
		public override bool IsValid => base.IsValid && Found;

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
