using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Configures the source for a reindex API request
	/// </summary>
	public interface IReindexSource
	{
		/// <summary>
		/// Search query to execute to match documents for reindexing
		/// </summary>
		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		/// <summary>
		/// Sort the documents to be reindexed. Sorting makes the operation less
		/// efficient but allows targeting a specific set of documents when used
		/// in conjunction with <see cref="Size"/>
		/// </summary>
		[JsonProperty("sort")]
		IList<ISort> Sort { get; set; }

		/// <summary>
		/// The indices to target
		/// </summary>
		[JsonProperty("index")]
		Indices Index { get; set; }

		/// <summary>
		/// The types of documents to target
		/// </summary>
		[JsonProperty("type")]
		Types Type { get; set; }

		/// <summary>
		/// Limit the number of processed documents
		/// </summary>
        [JsonProperty("size")]
        int? Size { get; set; }

		/// <summary>
		/// Reindex from a remote Elasticsearch cluster
		/// </summary>
        [JsonProperty("remote")]
        IRemoteSource Remote { get; set; }

		/// <summary>
		/// Manually parallelize the reindexing process.
		/// This parallelization can improve efficiency and provide a convenient
		/// way to break the request down into smaller parts.
		/// </summary>
		/// <remarks>
		/// Automatic slicing can be performed using <see cref="ReindexOnServerRequest.Slices"/>
		/// </remarks>
		[JsonProperty("slice")]
		ISlicedScroll Slice { get; set; }

		/// <summary>
		/// Individual fields from _source to reindex
		/// </summary>
		[JsonProperty("_source")]
		Fields Source { get; set; }
    }

	/// <inheritdoc />
	public class ReindexSource : IReindexSource
	{
		/// <inheritdoc />
		public QueryContainer Query { get; set; }

		/// <inheritdoc />
		public IList<ISort> Sort { get; set; }

		/// <inheritdoc />
		public Indices Index { get; set; }

		/// <inheritdoc />
		public Types Type { get; set; }

		/// <inheritdoc />
        public int? Size { get; set; }

		/// <inheritdoc />
        public IRemoteSource Remote { get; set; }

		/// <inheritdoc />
		public ISlicedScroll Slice { get; set; }

		/// <inheritdoc />
		public Fields Source { get; set; }
	}

	/// <inheritdoc cref="IReindexSource"/>
	public class ReindexSourceDescriptor : DescriptorBase<ReindexSourceDescriptor, IReindexSource>, IReindexSource
	{
		QueryContainer IReindexSource.Query { get; set; }
		IList<ISort> IReindexSource.Sort { get; set; }
		Indices IReindexSource.Index { get; set; }
		Types IReindexSource.Type { get; set; }
        int? IReindexSource.Size { get; set; }
        IRemoteSource IReindexSource.Remote { get; set; }
		ISlicedScroll IReindexSource.Slice { get; set; }
		Fields IReindexSource.Source { get; set; }

		/// <inheritdoc cref="IReindexSource.Query"/>
        public ReindexSourceDescriptor Query<T>(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) where T : class =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc cref="IReindexSource.Sort"/>
		public ReindexSourceDescriptor Sort<T>(Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector) where T : class =>
			Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<T>())?.Value);

		/// <inheritdoc cref="IReindexSource.Remote"/>
		public ReindexSourceDescriptor Remote(Func<RemoteSourceDescriptor, IRemoteSource> selector) =>
			Assign(a => a.Remote = selector?.Invoke(new RemoteSourceDescriptor()));

		/// <inheritdoc cref="IReindexSource.Index"/>
		public ReindexSourceDescriptor Index(Indices indices) => Assign(a => a.Index = indices);

		/// <inheritdoc cref="IReindexSource.Type"/>
		public ReindexSourceDescriptor Type(Types types) => Assign(a => a.Type = types);

		/// <inheritdoc cref="IReindexSource.Size"/>
        public ReindexSourceDescriptor Size(int? size) => Assign(a => a.Size = size);

		/// <inheritdoc cref="IReindexSource.Slice"/>
		public ReindexSourceDescriptor Slice<T>(Func<SlicedScrollDescriptor<T>, ISlicedScroll> selector) where T : class =>
			Assign(a => a.Slice = selector?.Invoke(new SlicedScrollDescriptor<T>()));

		/// <inheritdoc cref="IReindexSource.Source"/>
		public ReindexSourceDescriptor Source<T>(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) where T : class =>
			Assign(a => a.Source = fields?.Invoke(new FieldsDescriptor<T>())?.Value);
    }
}
