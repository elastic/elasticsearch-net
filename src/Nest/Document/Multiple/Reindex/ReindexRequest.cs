using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	/// <summary>
	/// A reindex implementation that uses <see cref="IElasticClient.ScrollAll{T}(Nest.IScrollAllRequest,System.Threading.CancellationToken)"/>
	/// and <see cref="IElasticClient.BulkAll{T}(IBulkAllRequest{T}, System.Threading.CancellationToken)"/> to compose a reindex pipeline.
	///
	/// <para> This differs from <see cref="IElasticClient.ReindexOnServer(System.Func{Nest.ReindexOnServerDescriptor,Nest.IReindexOnServerRequest})"/> in
	/// that documents are fetched over the wire before being send back.
	/// </para>
	///
	/// <para> This will create the target index if it doesn't exist already. If <see cref="CreateIndexRequest" /> is not specified
	/// and the source of the reindex  points to a single index we try and reuse the settings from source.
	/// You can completely opt out of all of this using <see cref="OmitIndexCreation"/>
	/// </para>
	///
	/// </summary>
	public interface IReindexRequest<T> where T : class
	{
		/// <summary>
		/// The scroll typically outperforms the bulk operations by a long shot. If we'd leave things unbounded you'd quickly have way t0o many pending scroll
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
		/// argument to create <see cref="BulkAllRequest{T}"> or <see cref="BulkAllDescriptor{T}"/>
		/// <para>
		/// Note that <see cref="IBulkAllRequest{T}.BufferToBulk"/> is always overriden as well as
		/// <see cref="IBulkAllRequest{T}.BackPressure"/>
		/// </para>
		/// </summary>
		Func<IEnumerable<IHit<T>>, IBulkAllRequest<IHit<T>>> BulkAll { get; set; }

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
	public class ReindexRequest<T> : IReindexRequest<T> where T : class
	{
		/// <inheritdoc/>
		IScrollAllRequest IReindexRequest<T>.ScrollAll { get; set; }
		/// <inheritdoc/>
		Func<IEnumerable<IHit<T>>,IBulkAllRequest<IHit<T>>> IReindexRequest<T>.BulkAll { get; set; }

		/// <inheritdoc/>
		public bool OmitIndexCreation { get; set; }
		/// <inheritdoc/>
		public ICreateIndexRequest CreateIndexRequest { get; set; }
		/// <inheritdoc/>
		public int? BackPressureFactor { get; set; }

		/// <inheritdoc/>
		/// <param name="scrollSource">The scroll operation yielding the source documents for the reindex operation</param>
		/// <param name="bulkAllTarget">A factory that instantiates the bulk all operation over the lazy stream of search result hits</param>
		public ReindexRequest(IScrollAllRequest scrollSource, Func<IEnumerable<IHit<T>>, IBulkAllRequest<IHit<T>>> bulkAllTarget)
		{
			scrollSource.ThrowIfNull(nameof(scrollSource), "without a scroll all request we don't know where to read from!");
			bulkAllTarget.ThrowIfNull(nameof(bulkAllTarget), "without a bulk all request we don't know where to send documents!");

			var i = (IReindexRequest<T>) this;
			i.ScrollAll = scrollSource;
			i.BulkAll = bulkAllTarget;
		}
	}

	public class ReindexDescriptor<T> : DescriptorBase<ReindexDescriptor<T>, IReindexRequest<T>>, IReindexRequest<T> where T : class
	{
		IScrollAllRequest IReindexRequest<T>.ScrollAll{ get; set; }

		private Func<BulkAllDescriptor<IHit<T>>, IBulkAllRequest<IHit<T>>> _createBulkAll;
		Func<IEnumerable<IHit<T>>, IBulkAllRequest<IHit<T>>> IReindexRequest<T>.BulkAll { get; set; }
		bool IReindexRequest<T>.OmitIndexCreation { get; set; }
		ICreateIndexRequest IReindexRequest<T>.CreateIndexRequest { get; set; }
		int? IReindexRequest<T>.BackPressureFactor { get; set; }

		public ReindexDescriptor()
		{
			((IReindexRequest<T>) this).BulkAll = (d) => this._createBulkAll.InvokeOrDefault(new BulkAllDescriptor<IHit<T>>(d));
		}

		/// <inheritdoc/>
		public ReindexDescriptor<T> ScrollAll(Time scrollTime, int slices, Func<ScrollAllDescriptor<T>,IScrollAllRequest> selector = null) =>
			Assign(a => a.ScrollAll = selector.InvokeOrDefault(new ScrollAllDescriptor<T>(scrollTime, slices)));

		/// <inheritdoc/>
		public ReindexDescriptor<T> BackPressureFactor(int? maximum) =>
			Assign(a => a.BackPressureFactor = maximum);

		/// <inheritdoc/>
		public ReindexDescriptor<T> BulkAll(Func<BulkAllDescriptor<IHit<T>>, IBulkAllRequest<IHit<T>>> selector) =>
			Assign(a => this._createBulkAll = selector);

		/// <inheritdoc/>
		public ReindexDescriptor<T> OmitIndexCreation(bool omit = true) => Assign(a => a.OmitIndexCreation = true);

		/// <inheritdoc/>
		public ReindexDescriptor<T> CreateIndex(Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector) =>
			Assign(a => a.CreateIndexRequest = createIndexSelector.InvokeOrDefault(new CreateIndexDescriptor("ignored")));
	}
}
