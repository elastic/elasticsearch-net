// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class RecoveryFiles
	{
		[DataMember(Name ="details")]
		public IEnumerable<RecoveryFileDetails> Details { get; internal set; }

		[DataMember(Name ="percent")]
		public string Percent { get; internal set; }

		[DataMember(Name ="recovered")]
		public long Recovered { get; internal set; }

		[DataMember(Name ="reused")]
		public long Reused { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }
	}
}
