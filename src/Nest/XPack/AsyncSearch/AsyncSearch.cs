/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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