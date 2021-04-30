// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.XPack.Eql.Events;

namespace Nest
{
	public class EqlSearchResponse<TDocument> : ResponseBase where TDocument : class
	{
		private IReadOnlyCollection<IEvent<TDocument>> _events;
		private IReadOnlyCollection<ISequence<TDocument>> _sequences;

		/// <inheritdoc />
		[IgnoreDataMember]
		public IReadOnlyCollection<IEvent<TDocument>> Events =>
			_events ??= EventHitsMetadata?.Events ?? EmptyReadOnly<IEvent<TDocument>>.Collection;

		/// <inheritdoc />
		[IgnoreDataMember]
		public IReadOnlyCollection<ISequence<TDocument>> Sequences =>
			_sequences ??= EventHitsMetadata?.Sequences ?? EmptyReadOnly<ISequence<TDocument>>.Collection;

		/// <inheritdoc />
		[DataMember(Name = "hits")]
		public IEventHitsMetadata<TDocument> EventHitsMetadata { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "id")]
		public Id Id { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "is_partial")]
		public bool? IsPartial { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "is_running")]
		public bool? IsRunning { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "took")]
		public int Took { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "timed_out")]
		public bool? TimedOut { get; internal set; }

		/// <inheritdoc />
		[IgnoreDataMember]
		public long Total => EventHitsMetadata?.Total.Value ?? -1;
	}
}
