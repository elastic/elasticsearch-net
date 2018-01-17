using System;
using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// A reindex implementation that uses ScrollAll() BulkAll() to compose a reindex pipeline.
	///
	/// <para> This differs from ReindexOnServer() in that documents are fetched from Elasticsearch, transformed on the client side,
	/// then sent back to Elasticsearch.
	/// </para>
	///
	/// <para> This will create the target index if it doesn't exist already. If <see cref="CreateIndexRequest" /> is not specified
	/// and the source of the reindex  points to a single index we try and reuse the settings from source.
	/// You can completely opt out of all of this using <see cref="OmitIndexCreation"/>
	/// </para>
	///
	/// </summary>
	public interface IReindexRequest<TSource, TTarget>
		where TSource : class
		where TTarget : class
	{
		/// <summary>
		/// The scroll typically outperforms the bulk operations by a long shot. If we'd leave things unbounded you'd quickly have way too many pending scroll
		/// requests. What this property allows you to express is that for each bucket in the max concurrency of the minimum max concurrency between
		/// producer and consumer amply the maximum overal pending of the other side by this factor. Typically the concurrency of the consumer (bulkall) will
		/// be lower and with this factor we can dampen the overall pending scroll requests while we are still processing bulk requests.
		/// <para>defaults to 4 if not provided</para>
		/// </summary>
		int? BackPressureFactor { get; set; }

		/// <summary>
		/// Describes the scroll operation where we need to fetch the documents from.
		/// <para>
		/// Note that <see cref="IScrollAllRequest.BackPressure"/> can be overriden by our own.
		/// </para>
		/// </summary>
		IScrollAllRequest ScrollAll{ get; set; }

		/// <summary>
		/// Provide a factory for the bulk all request, the first argument is the lazy collection of scroll results which is a mandatory
		/// argument to create <see cref="BulkAllRequest{T}" /> or <see cref="BulkAllDescriptor{T}"/>
		/// <para>
		/// Note that <see cref="IBulkAllRequest{T}.BufferToBulk"/> is always overriden as well as
		/// <see cref="IBulkAllRequest{T}.BackPressure"/>
		/// </para>
		/// </summary>
		Func<IEnumerable<IHitMetadata<TTarget>>, IBulkAllRequest<IHitMetadata<TTarget>>> BulkAll { get; set; }

		Func<TSource, TTarget> Map { get; set; }

		/// <summary>
		/// Do not send a create index call on the target index, assume the index has been created outside of the reindex.
		/// Reindex will never create the index if it already exists however this will also omit the IndexExists call.
		/// </summary>
		bool OmitIndexCreation { get; set; }

		/// <summary>
		/// Describe how the newly created index should be created. Remember you can also register Index Templates for more dynamic usecases.
		/// </summary>
		ICreateIndexRequest CreateIndexRequest { get; set; }
	}

	/// <inheritdoc/>
	public interface IReindexRequest<TSource> : IReindexRequest<TSource, TSource> where TSource : class { }

	/// <inheritdoc/>
	public class ReindexRequest<TSource, TTarget> : IReindexRequest<TSource, TTarget>
		where TSource : class
		where TTarget : class
	{
		/// <inheritdoc/>
		IScrollAllRequest IReindexRequest<TSource, TTarget>.ScrollAll { get; set; }
		/// <inheritdoc/>
		Func<IEnumerable<IHitMetadata<TTarget>>,IBulkAllRequest<IHitMetadata<TTarget>>> IReindexRequest<TSource, TTarget>.BulkAll { get; set; }
		/// <inheritdoc/>
		Func<TSource, TTarget> IReindexRequest<TSource, TTarget>.Map { get; set; }

		/// <inheritdoc/>
		public bool OmitIndexCreation { get; set; }
		/// <inheritdoc/>
		public ICreateIndexRequest CreateIndexRequest { get; set; }
		/// <inheritdoc/>
		public int? BackPressureFactor { get; set; }

		/// <inheritdoc/>
		/// <param name="scrollSource">The scroll operation yielding the source documents for the reindex operation</param>
		/// <param name="map">A function that converts from a source document to a target document</param>
		/// <param name="bulkAllTarget">A factory that instantiates the bulk all operation over the lazy stream of search result hits</param>
		public ReindexRequest(IScrollAllRequest scrollSource, Func<TSource, TTarget> map, Func<IEnumerable<IHitMetadata<TTarget>>, IBulkAllRequest<IHitMetadata<TTarget>>> bulkAllTarget)
		{
			scrollSource.ThrowIfNull(nameof(scrollSource), "scrollSource must be set in order to get the source of a Reindex operation");
			bulkAllTarget.ThrowIfNull(nameof(bulkAllTarget), "bulkAllTarget must set in order to get the target of a Reindex operation");
			map.ThrowIfNull(nameof(map), "map must be set to know how to take TSource and transform it into TTarget");

			var i = (IReindexRequest<TSource, TTarget>) this;
			i.ScrollAll = scrollSource;
			i.BulkAll = bulkAllTarget;
			i.Map = map;
		}
	}

	public class ReindexRequest<TSource> : ReindexRequest<TSource, TSource>
		where TSource : class
	{
		public ReindexRequest(IScrollAllRequest scrollSource, Func<IEnumerable<IHitMetadata<TSource>>, IBulkAllRequest<IHitMetadata<TSource>>> bulkAllTarget)
			: base(scrollSource, s=>s, bulkAllTarget)
		{
		}
	}

	public class ReindexDescriptor<TSource, TTarget> : DescriptorBase<ReindexDescriptor<TSource, TTarget>, IReindexRequest<TSource, TTarget>>, IReindexRequest<TSource, TTarget>
		where TSource : class
		where TTarget : class
	{
		IScrollAllRequest IReindexRequest<TSource, TTarget>.ScrollAll{ get; set; }

		private Func<BulkAllDescriptor<IHitMetadata<TTarget>>, IBulkAllRequest<IHitMetadata<TTarget>>> _createBulkAll;
		Func<IEnumerable<IHitMetadata<TTarget>>, IBulkAllRequest<IHitMetadata<TTarget>>> IReindexRequest<TSource, TTarget>.BulkAll { get; set; }
		bool IReindexRequest<TSource, TTarget>.OmitIndexCreation { get; set; }
		ICreateIndexRequest IReindexRequest<TSource, TTarget>.CreateIndexRequest { get; set; }
		int? IReindexRequest<TSource, TTarget>.BackPressureFactor { get; set; }
		Func<TSource, TTarget> IReindexRequest<TSource, TTarget>.Map { get; set; }

		public ReindexDescriptor(Func<TSource, TTarget> mapper)
		{
			var i = (IReindexRequest<TSource, TTarget>) this;
			i.BulkAll = d => this._createBulkAll.InvokeOrDefault(new BulkAllDescriptor<IHitMetadata<TTarget>>(d));
			i.Map = mapper;
		}

		/// <inheritdoc/>
		public ReindexDescriptor<TSource, TTarget> ScrollAll(Time scrollTime, int slices, Func<ScrollAllDescriptor<TSource>,IScrollAllRequest> selector = null) =>
			Assign(a => a.ScrollAll = selector.InvokeOrDefault(new ScrollAllDescriptor<TSource>(scrollTime, slices)));

		/// <inheritdoc/>
		public ReindexDescriptor<TSource, TTarget> BackPressureFactor(int? maximum) =>
			Assign(a => a.BackPressureFactor = maximum);

		/// <inheritdoc/>
		public ReindexDescriptor<TSource, TTarget> BulkAll(Func<BulkAllDescriptor<IHitMetadata<TTarget>>, IBulkAllRequest<IHitMetadata<TTarget>>> selector) =>
			Assign(a => this._createBulkAll = selector);

		/// <inheritdoc/>
		public ReindexDescriptor<TSource, TTarget> OmitIndexCreation(bool omit = true) => Assign(a => a.OmitIndexCreation = omit);

		/// <inheritdoc/>
		public ReindexDescriptor<TSource, TTarget> CreateIndex(Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector) =>
			Assign(a => a.CreateIndexRequest = createIndexSelector.InvokeOrDefault(new CreateIndexDescriptor("ignored")));
	}
}
