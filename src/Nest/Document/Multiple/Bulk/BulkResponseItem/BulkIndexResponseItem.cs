// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(ConcreteBulkIndexResponseItemFormatter<BulkIndexResponseItem>))]
	public class BulkIndexResponseItem : BulkResponseItemBase
	{
		/// <summary>
		/// The _ids that matched (if any) for the Percolate API.
		/// Will be null if the operation is not in response to Percolate API.
		/// </summary>
		[DataMember(Name = "matches")]
		public IEnumerable<string> Matches { get; internal set; }

		public override string Operation { get; } = "index";
	}
}
