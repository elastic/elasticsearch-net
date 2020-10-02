// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ListDanglingIndicesResponse : ResponseBase
	{
		[DataMember(Name = "dangling_indices")]
		public IReadOnlyCollection<AggregatedDanglingIndexInfo> DanglingIndices { get; internal set; } =
			EmptyReadOnly<AggregatedDanglingIndexInfo>.Collection;
	}

	public class AggregatedDanglingIndexInfo
	{
		private DateTimeOffset? _creationDate;

		[DataMember(Name = "index_name")]
		public string IndexName { get; internal set; }

		[DataMember(Name = "index_uuid")]
		public string IndexUUID { get; internal set; }

		[DataMember(Name = "creation_date_millis")]
		public long CreationDateInMilliseconds { get; internal set; }

		[DataMember(Name = "creation_date")]
		public DateTimeOffset CreationDate
		{
			get
			{
				_creationDate ??= DateTimeOffset.FromUnixTimeMilliseconds(CreationDateInMilliseconds);
				return _creationDate.Value;
			}
			internal set => _creationDate = value;
		}

		[DataMember(Name = "node_ids")]
		public IReadOnlyCollection<string> NodeIds { get; internal set; }
	}
}
