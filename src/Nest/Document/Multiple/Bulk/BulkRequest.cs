using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(BulkRequestJsonConverter))]
	public partial interface IBulkRequest
	{
		[JsonIgnore]
		IList<IBulkOperation> Operations { get; set; }
	}

	public partial class BulkRequest
	{
		public IList<IBulkOperation> Operations { get; set; }
	}

	public partial class BulkDescriptor
	{
		IList<IBulkOperation> IBulkRequest.Operations { get; set; } = new SynchronizedCollection<IBulkOperation>();

		private BulkDescriptor AddOperation(IBulkOperation operation) => Assign(a => a.Operations.AddIfNotNull(operation));

		public BulkDescriptor Create<T>(Func<BulkCreateDescriptor<T>, BulkCreateDescriptor<T>> bulkCreateSelector) where T : class =>
			Assign(a => AddOperation(bulkCreateSelector?.Invoke(new BulkCreateDescriptor<T>())));

		/// <summary>
		/// CreateMany, convenience method to create many documents at once.
		/// </summary>
		/// <param name="objects">the objects to create</param>
		/// <param name="bulkCreateSelector">A func called on each object to describe the individual create operation</param>
		public BulkDescriptor CreateMany<T>(IEnumerable<T> @objects, Func<BulkCreateDescriptor<T>, T, BulkCreateDescriptor<T>> bulkCreateSelector = null) where T : class =>
			Assign(a => @objects.ForEach(o => AddOperation(bulkCreateSelector.InvokeOrDefault(new BulkCreateDescriptor<T>().Document(o), o))));

		public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, BulkIndexDescriptor<T>> bulkIndexSelector) where T : class =>
			Assign(a => AddOperation(bulkIndexSelector?.Invoke(new BulkIndexDescriptor<T>())));

		/// <summary>
		/// IndexMany, convenience method to pass many objects at once.
		/// </summary>
		/// <param name="objects">the objects to index</param>
		/// <param name="bulkIndexSelector">A func called on each object to describe the individual index operation</param>
		public BulkDescriptor IndexMany<T>(IEnumerable<T> @objects, Func<BulkIndexDescriptor<T>, T, BulkIndexDescriptor<T>> bulkIndexSelector = null) where T : class =>
			Assign(a => @objects.ForEach(o => AddOperation(bulkIndexSelector.InvokeOrDefault(new BulkIndexDescriptor<T>().Document(o), o))));

		public BulkDescriptor Delete<T>(Func<BulkDeleteDescriptor<T>, BulkDeleteDescriptor<T>> bulkDeleteSelector) where T : class =>
			Assign(a => AddOperation(bulkDeleteSelector?.Invoke(new BulkDeleteDescriptor<T>())));

		/// <summary>
		/// DeleteMany, convenience method to delete many objects at once.
		/// </summary>
		/// <param name="objects">the objects to delete</param>
		/// <param name="bulkDeleteSelector">A func called on each object to describe the individual delete operation</param>
		public BulkDescriptor DeleteMany<T>(IEnumerable<T> @objects, Func<BulkDeleteDescriptor<T>, T, BulkDeleteDescriptor<T>> bulkDeleteSelector = null) where T : class =>
			Assign(a => @objects.ForEach(o => AddOperation(bulkDeleteSelector.InvokeOrDefault(new BulkDeleteDescriptor<T>().Document(o), o))));

		/// <summary>
		/// DeleteMany, convenience method to delete many objects at once.
		/// </summary>
		/// <param name="ids">Enumerable of string ids to delete</param>
		/// <param name="bulkDeleteSelector">A func called on each ids to describe the individual delete operation</param>
		public BulkDescriptor DeleteMany<T>(IEnumerable<string> ids, Func<BulkDeleteDescriptor<T>, string, BulkDeleteDescriptor<T>> bulkDeleteSelector = null) where T : class=>
			Assign(a => ids.ForEach(o => AddOperation(bulkDeleteSelector.InvokeOrDefault(new BulkDeleteDescriptor<T>().Id(o), o))));

		/// <summary>
		/// DeleteMany, convenience method to delete many objects at once.
		/// </summary>
		/// <param name="ids">Enumerable of int ids to delete</param>
		/// <param name="bulkDeleteSelector">A func called on each ids to describe the individual delete operation</param>
		public BulkDescriptor DeleteMany<T>(IEnumerable<long> ids, Func<BulkDeleteDescriptor<T>, long, BulkDeleteDescriptor<T>> bulkDeleteSelector = null) where T : class =>
			Assign(a => ids.ForEach(o => AddOperation(bulkDeleteSelector.InvokeOrDefault(new BulkDeleteDescriptor<T>().Id(o), o))));

		public BulkDescriptor Update<T>(Func<BulkUpdateDescriptor<T, T>, BulkUpdateDescriptor<T, T>> bulkUpdateSelector) where T : class => 
			this.Update<T, T>(bulkUpdateSelector);

		public BulkDescriptor Update<T, K>(Func<BulkUpdateDescriptor<T, K>, BulkUpdateDescriptor<T, K>> bulkUpdateSelector)
			where T : class
			where K : class =>
			Assign(a => AddOperation(bulkUpdateSelector?.Invoke(new BulkUpdateDescriptor<T, K>())));
	}
}
