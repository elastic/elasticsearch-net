// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class ResolveIndexResponse : ResponseBase
	{
		[DataMember(Name = "indices")]
		public IReadOnlyCollection<ResolvedIndex> Indices { get; internal set; } = EmptyReadOnly<ResolvedIndex>.Collection;

		[DataMember(Name = "aliases")]
		public IReadOnlyCollection<ResolvedAlias> Aliases { get; internal set; } = EmptyReadOnly<ResolvedAlias>.Collection;

		[DataMember(Name = "data_streams")]
		public IReadOnlyCollection<ResolvedDataStream> DataStreams { get; internal set; } = EmptyReadOnly<ResolvedDataStream>.Collection;
	}

	public class ResolvedIndex
	{
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "aliases")]
		public IReadOnlyCollection<string> Aliases { get; internal set; }

		[DataMember(Name = "attributes")]
		public IReadOnlyCollection<string> Attributes { get; internal set; }

		[DataMember(Name = "data_stream")]
		public string DataStream { get; internal set; }
	}

	public class ResolvedAlias
	{
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "indices")]
		public IReadOnlyCollection<string> Indices { get; internal set; }
	}

	public class ResolvedDataStream
	{
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "backing_indices")]
		public IReadOnlyCollection<string> BackingIndices { get; internal set; }

		[DataMember(Name = "timestamp_field")]
		public string TimestampField { get; internal set; }
	}
}
