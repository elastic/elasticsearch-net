// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IBulkAllRequest<T> where T : class
	{
		/// <summary> In case of a HTTP 429 (Too Many Requests) response status code, how many times should we automatically back off before failing</summary>
		int? BackOffRetries { get; set; }

		/// <summary> In case of a HTTP 429 (Too Many Requests) response status code, how long should we wait before retrying</summary>
		Time BackOffTime { get; set; }

		/// <summary>
		/// Simple back pressure implementation that makes sure the minimum max concurrency between producer and consumer
		/// is not amplified by the greedier of the two by more then a given back pressure factor
		/// When set each bulk request will call <see cref="ProducerConsumerBackPressure.Release" />
		/// </summary>
		ProducerConsumerBackPressure BackPressure { get; set; }

		/// <summary>
		/// By default, <see cref="BulkAllObservable{T}" /> calls <see cref="BulkDescriptor.IndexMany{T}" /> on the buffer.
		/// There might be case where you'd like more control over the bulk operation. By setting this callback, you are in complete control
		/// of describing how the buffer should be translated to a bulk operation.
		/// </summary>
		Action<BulkDescriptor, IList<T>> BufferToBulk { get; set; }

		/// <summary>
		/// Halt the bulk all request if any of the documents returned is a failure that can not be retried.
		/// When true, will feed dropped documents to <see cref="DroppedDocumentCallback" />
		/// </summary>
		bool ContinueAfterDroppedDocuments { get; set; }

		/// <summary>
		///  The documents to send to Elasticsearch, ideally lazily evaluated by using <see langword="yield" />
		/// return to provide each document.
		/// <see cref="BulkAllObservable{T}" /> will eager evaluate each partitioned page when operating on it, using
		/// <see cref="Enumerable.ToList{T}" />.
		/// </summary>
		IEnumerable<T> Documents { get; }

		/// <summary>
		/// If a bulk operation fails because it receives documents it can not retry they will be fed to this callback.
		/// If <see cref="ContinueAfterDroppedDocuments" /> is set to <c>true</c> processing will continue, so this callback can be used
		/// to feed into a dead letter queue. Otherwise bulk all indexing will be halted.
		/// </summary>
		Action<BulkResponseItemBase, T> DroppedDocumentCallback { get; set; }

		///<summary>The index to use for items that don't specify one. By default, will be inferred from <typeparamref name="T"/>.
		/// If no default index has been mapped for <typeparamref name="T" />
		/// using <see cref="ConnectionSettingsBase{TConnectionSettings}.DefaultMappingFor{TDocument}"/>
		/// on <see cref="Nest.ConnectionSettings"/>, an exception will be thrown.
		/// </summary>
		IndexName Index { get; set; }

		///<summary>The maximum number of bulk operations we want to have in flight at a time</summary>
		int? MaxDegreeOfParallelism { get; set; }

		///<summary>The pipeline id to preprocess all the incoming documents with</summary>
		string Pipeline { get; set; }

		/// <summary>The indices you wish to refresh after the bulk all completes, defaults to <see cref="Index" /> </summary>
		Indices RefreshIndices { get; set; }

		/// <summary>
		///  Refresh the index after performing ALL the bulk operations (NOTE this is an additional request)
		/// </summary>
		bool RefreshOnCompleted { get; set; }

		/// <summary>
		/// A predicate to control which documents should be retried.
		/// Defaults to failed bulk items with a HTTP 429 (Too Many Requests) response status code.
		/// </summary>
		Func<BulkResponseItemBase, T, bool> RetryDocumentPredicate { get; set; }

		///<summary>Specific per bulk operation routing value</summary>
		Routing Routing { get; set; }

		/// <summary> The number of documents to send per bulk</summary>
		int? Size { get; set; }

		///<summary>Explicit per operation timeout</summary>
		Time Timeout { get; set; }

		/// <summary>
		/// Sets the number of shard copies that must be active before proceeding with the bulk operation.
		/// Defaults to <c>1</c>, meaning the primary shard only. Set to `all` for all shard copies, otherwise set to any
		/// non-negative value less than or equal to the total number of copies for the shard (number of replicas + 1)
		/// </summary>
		int? WaitForActiveShards { get; set; }

		/// <summary>
		/// Be notified every time a bulk response returns, this includes retries.
		/// <see cref="IObserver{T}.OnNext"/> is only called for successful batches.
		/// </summary>
		Action<BulkResponse> BulkResponseCallback { get; set; }
	}

	public class BulkAllRequest<T> : IBulkAllRequest<T>
		where T : class
	{
		public BulkAllRequest(IEnumerable<T> documents)
		{
			Documents = documents;
			Index = typeof(T);
		}

		/// <inheritdoc />
		public int? BackOffRetries { get; set; }

		/// <inheritdoc />
		public Time BackOffTime { get; set; }

		/// <inheritdoc />
		public ProducerConsumerBackPressure BackPressure { get; set; }

		/// <inheritdoc />
		public Action<BulkDescriptor, IList<T>> BufferToBulk { get; set; }

		/// <inheritdoc />
		public bool ContinueAfterDroppedDocuments { get; set; }

		/// <inheritdoc />
		public IEnumerable<T> Documents { get; }

		/// <inheritdoc />
		public Action<BulkResponseItemBase, T> DroppedDocumentCallback { get; set; }

		/// <inheritdoc />
		public IndexName Index { get; set; }

		/// <inheritdoc />
		public int? MaxDegreeOfParallelism { get; set; }

		/// <inheritdoc />
		public string Pipeline { get; set; }

		/// <inheritdoc />
		public Indices RefreshIndices { get; set; }

		/// <inheritdoc />
		public bool RefreshOnCompleted { get; set; }

		/// <inheritdoc />
		public Func<BulkResponseItemBase, T, bool> RetryDocumentPredicate { get; set; }

		/// <inheritdoc />
		public Routing Routing { get; set; }

		/// <inheritdoc />
		public int? Size { get; set; }

		/// <inheritdoc />
		public Time Timeout { get; set; }

		/// <inheritdoc />
		public int? WaitForActiveShards { get; set; }

		/// <inheritdoc />
		public Action<BulkResponse> BulkResponseCallback { get; set; }
	}

	public class BulkAllDescriptor<T> : DescriptorBase<BulkAllDescriptor<T>, IBulkAllRequest<T>>, IBulkAllRequest<T>
		where T : class
	{
		private readonly IEnumerable<T> _documents;

		public BulkAllDescriptor(IEnumerable<T> documents)
		{
			_documents = documents;
			((IBulkAllRequest<T>)this).Index = typeof(T);
		}

		int? IBulkAllRequest<T>.BackOffRetries { get; set; }

		Time IBulkAllRequest<T>.BackOffTime { get; set; }
		ProducerConsumerBackPressure IBulkAllRequest<T>.BackPressure { get; set; }
		Action<BulkDescriptor, IList<T>> IBulkAllRequest<T>.BufferToBulk { get; set; }
		bool IBulkAllRequest<T>.ContinueAfterDroppedDocuments { get; set; }
		IEnumerable<T> IBulkAllRequest<T>.Documents => _documents;
		Action<BulkResponseItemBase, T> IBulkAllRequest<T>.DroppedDocumentCallback { get; set; }
		IndexName IBulkAllRequest<T>.Index { get; set; }
		int? IBulkAllRequest<T>.MaxDegreeOfParallelism { get; set; }
		string IBulkAllRequest<T>.Pipeline { get; set; }
		Indices IBulkAllRequest<T>.RefreshIndices { get; set; }
		bool IBulkAllRequest<T>.RefreshOnCompleted { get; set; }
		Func<BulkResponseItemBase, T, bool> IBulkAllRequest<T>.RetryDocumentPredicate { get; set; }
		Routing IBulkAllRequest<T>.Routing { get; set; }
		int? IBulkAllRequest<T>.Size { get; set; }
		Time IBulkAllRequest<T>.Timeout { get; set; }
		int? IBulkAllRequest<T>.WaitForActiveShards { get; set; }
		Action<BulkResponse> IBulkAllRequest<T>.BulkResponseCallback { get; set; }

		/// <inheritdoc cref="IBulkAllRequest{T}.MaxDegreeOfParallelism" />
		public BulkAllDescriptor<T> MaxDegreeOfParallelism(int? parallelism) =>
			Assign(parallelism, (a, v) => a.MaxDegreeOfParallelism = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.Size" />
		public BulkAllDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.BackOffRetries" />
		public BulkAllDescriptor<T> BackOffRetries(int? backoffs) =>
			Assign(backoffs, (a, v) => a.BackOffRetries = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.BackOffTime" />
		public BulkAllDescriptor<T> BackOffTime(Time time) => Assign(time, (a, v) => a.BackOffTime = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.Index" />
		public BulkAllDescriptor<T> Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.Index" />
		public BulkAllDescriptor<T> Index<TOther>() where TOther : class => Assign(typeof(TOther), (a, v) => a.Index = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.RefreshOnCompleted" />
		public BulkAllDescriptor<T> RefreshOnCompleted(bool refresh = true) => Assign(refresh, (a, v) => a.RefreshOnCompleted = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.RefreshIndices" />
		public BulkAllDescriptor<T> RefreshIndices(Indices indicesToRefresh) => Assign(indicesToRefresh, (a, v) => a.RefreshIndices = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.Routing" />
		public BulkAllDescriptor<T> Routing(Routing routing) => Assign(routing, (a, v) => a.Routing = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.Timeout" />
		public BulkAllDescriptor<T> Timeout(Time timeout) => Assign(timeout, (a, v) => a.Timeout = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.Pipeline" />
		public BulkAllDescriptor<T> Pipeline(string pipeline) => Assign(pipeline, (a, v) => a.Pipeline = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.BufferToBulk" />
		public BulkAllDescriptor<T> BufferToBulk(Action<BulkDescriptor, IList<T>> modifier) => Assign(modifier, (a, v) => a.BufferToBulk = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.RetryDocumentPredicate" />
		public BulkAllDescriptor<T> RetryDocumentPredicate(Func<BulkResponseItemBase, T, bool> predicate) =>
			Assign(predicate, (a, v) => a.RetryDocumentPredicate = v);

		/// <summary>
		/// Simple back pressure implementation that makes sure the minimum max concurrency between producer and consumer
		/// is not amplified by the greedier of the two by more then a given back pressure factor
		/// When set each scroll request will additionally wait on <see cref="ProducerConsumerBackPressure.WaitAsync" /> as well as
		/// <see cref="MaxDegreeOfParallelism" /> if set. Not that the consumer has to call <see cref="ProducerConsumerBackPressure.Release" />
		/// on the same instance every time it is done.
		/// </summary>
		/// <param name="maxConcurrency">The minimum maximum concurrency which would be the bottleneck of the producer consumer pipeline</param>
		/// <param name="backPressureFactor">The maximum amplification back pressure of the greedier part of the producer consumer pipeline</param>
		public BulkAllDescriptor<T> BackPressure(int maxConcurrency, int? backPressureFactor = null) =>
			Assign(new ProducerConsumerBackPressure(backPressureFactor, maxConcurrency), (a, v) => a.BackPressure = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.ContinueAfterDroppedDocuments" />
		public BulkAllDescriptor<T> ContinueAfterDroppedDocuments(bool proceed = true) => Assign(proceed, (a, v) => a.ContinueAfterDroppedDocuments = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.DroppedDocumentCallback" />
		public BulkAllDescriptor<T> DroppedDocumentCallback(Action<BulkResponseItemBase, T> callback) =>
			Assign(callback, (a, v) => a.DroppedDocumentCallback = v);

		/// <inheritdoc cref="IBulkAllRequest{T}.BulkResponseCallback" />
		public BulkAllDescriptor<T> BulkResponseCallback(Action<BulkResponse> callback) =>
			Assign(callback, (a, v) => a.BulkResponseCallback = v);
	}
}
