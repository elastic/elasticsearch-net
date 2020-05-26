using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class AsyncSearch<TDocument> where TDocument : class
	{
		private IReadOnlyCollection<TDocument> _documents;
		private IReadOnlyCollection<FieldValues> _fields;
		private IReadOnlyCollection<IHit<TDocument>> _hits;

		/// <inheritdoc cref="ISearchResponse{TDocument}.Aggregations" />
		[DataMember(Name ="aggregations")]
		public AggregateDictionary Aggregations { get; internal set; } = AggregateDictionary.Default;

		/// <inheritdoc cref="ISearchResponse{TDocument}.Clusters" />
		[DataMember(Name = "_clusters")]
		public ClusterStatistics Clusters { get; internal set; }

		/// <inheritdoc cref="ISearchResponse{TDocument}.Documents" />
		[IgnoreDataMember]
		public IReadOnlyCollection<TDocument> Documents =>
			_documents ??= Hits
				.Select(h => h.Source)
				.ToList();

		/// <inheritdoc cref="ISearchResponse{TDocument}.Fields" />
		[IgnoreDataMember]
		public IReadOnlyCollection<FieldValues> Fields =>
			_fields ??= Hits
				.Select(h => h.Fields)
				.ToList();

		/// <inheritdoc cref="ISearchResponse{TDocument}.Hits" />
		[IgnoreDataMember]
		public IReadOnlyCollection<IHit<TDocument>> Hits =>
			_hits ??= HitsMetadata?.Hits ?? EmptyReadOnly<IHit<TDocument>>.Collection;

		/// <inheritdoc cref="ISearchResponse{TDocument}.HitsMetadata" />
		[DataMember(Name ="hits")]
		public IHitsMetadata<TDocument> HitsMetadata { get; internal set; }

		/// <inheritdoc cref="ISearchResponse{TDocument}.MaxScore" />
		[IgnoreDataMember]
		public double MaxScore => HitsMetadata?.MaxScore ?? 0;

		/// <inheritdoc cref="ISearchResponse{TDocument}.NumberOfReducePhases" />
		[DataMember(Name ="num_reduce_phases")]
		public long NumberOfReducePhases { get; internal set; }

		/// <inheritdoc cref="ISearchResponse{TDocument}.Profile" />
		[DataMember(Name ="profile")]
		public Profile Profile { get; internal set; }

		/// <inheritdoc cref="ISearchResponse{TDocument}.ScrollId" />
		[DataMember(Name = "_scroll_id")]
		public string ScrollId { get; internal set; }

		/// <inheritdoc cref="ISearchResponse{TDocument}.Shards" />
		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }

		/// <inheritdoc cref="ISearchResponse{TDocument}.Suggest" />
		[DataMember(Name ="suggest")]
		public ISuggestDictionary<TDocument> Suggest { get; internal set; } = SuggestDictionary<TDocument>.Default;

		/// <inheritdoc cref="ISearchResponse{TDocument}.TerminatedEarly" />
		[DataMember(Name ="terminated_early")]
		public bool TerminatedEarly { get; internal set; }

		/// <inheritdoc cref="ISearchResponse{TDocument}.TimedOut" />
		[DataMember(Name ="timed_out")]
		public bool TimedOut { get; internal set; }

		/// <inheritdoc cref="ISearchResponse{TDocument}.Took" />
		[DataMember(Name ="took")]
		public long Took { get; internal set; }
	}
}