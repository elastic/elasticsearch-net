using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Configures the source for a reindex API request
	/// </summary>
	public interface IReindexSource
	{
		/// <summary>
		/// The indices to target
		/// </summary>
		[DataMember(Name ="index")]
		Indices Index { get; set; }

		/// <summary>
		/// Search query to execute to match documents for reindexing
		/// </summary>
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		/// <summary>
		/// Reindex from a remote Elasticsearch cluster
		/// </summary>
		[DataMember(Name ="remote")]
		IRemoteSource Remote { get; set; }

		/// <summary>
		/// Limit the number of processed documents
		/// </summary>
		[DataMember(Name ="size")]
		int? Size { get; set; }

		/// <summary>
		/// Manually parallelize the reindexing process.
		/// This parallelization can improve efficiency and provide a convenient
		/// way to break the request down into smaller parts.
		/// </summary>
		/// <remarks>
		/// Automatic slicing can be performed using <see cref="ReindexOnServerRequest.Slices" />
		/// </remarks>
		[DataMember(Name ="slice")]
		ISlicedScroll Slice { get; set; }

		/// <summary>
		/// Sort the documents to be reindexed. Sorting makes the operation less
		/// efficient but allows targeting a specific set of documents when used
		/// in conjunction with <see cref="Size" />
		/// </summary>
		[DataMember(Name ="sort")]
		IList<ISort> Sort { get; set; }

		/// <summary>
		/// Individual fields from _source to reindex
		/// </summary>
		[DataMember(Name ="_source")]
		Fields Source { get; set; }
	}

	/// <inheritdoc />
	public class ReindexSource : IReindexSource
	{
		/// <inheritdoc />
		public Indices Index { get; set; }

		/// <inheritdoc />
		public QueryContainer Query { get; set; }

		/// <inheritdoc />
		public IRemoteSource Remote { get; set; }

		/// <inheritdoc />
		public int? Size { get; set; }

		/// <inheritdoc />
		public ISlicedScroll Slice { get; set; }

		/// <inheritdoc />
		public IList<ISort> Sort { get; set; }

		/// <inheritdoc />
		public Fields Source { get; set; }
	}

	/// <inheritdoc cref="IReindexSource" />
	public class ReindexSourceDescriptor : DescriptorBase<ReindexSourceDescriptor, IReindexSource>, IReindexSource
	{
		Indices IReindexSource.Index { get; set; }
		QueryContainer IReindexSource.Query { get; set; }
		IRemoteSource IReindexSource.Remote { get; set; }
		int? IReindexSource.Size { get; set; }
		ISlicedScroll IReindexSource.Slice { get; set; }
		IList<ISort> IReindexSource.Sort { get; set; }
		Fields IReindexSource.Source { get; set; }

		/// <inheritdoc cref="IReindexSource.Query" />
		public ReindexSourceDescriptor Query<T>(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) where T : class =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc cref="IReindexSource.Sort" />
		public ReindexSourceDescriptor Sort<T>(Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector) where T : class =>
			Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<T>())?.Value);

		/// <inheritdoc cref="IReindexSource.Remote" />
		public ReindexSourceDescriptor Remote(Func<RemoteSourceDescriptor, IRemoteSource> selector) =>
			Assign(a => a.Remote = selector?.Invoke(new RemoteSourceDescriptor()));

		/// <inheritdoc cref="IReindexSource.Index" />
		public ReindexSourceDescriptor Index(Indices indices) => Assign(a => a.Index = indices);

		/// <inheritdoc cref="IReindexSource.Size" />
		public ReindexSourceDescriptor Size(int? size) => Assign(a => a.Size = size);

		/// <inheritdoc cref="IReindexSource.Slice" />
		public ReindexSourceDescriptor Slice<T>(Func<SlicedScrollDescriptor<T>, ISlicedScroll> selector) where T : class =>
			Assign(a => a.Slice = selector?.Invoke(new SlicedScrollDescriptor<T>()));

		/// <inheritdoc cref="IReindexSource.Source" />
		public ReindexSourceDescriptor Source<T>(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) where T : class =>
			Assign(a => a.Source = fields?.Invoke(new FieldsDescriptor<T>())?.Value);
	}
}
