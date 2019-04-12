using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(BulkRequestFormatter))]
	public partial interface IBulkRequest
	{
		[IgnoreDataMember]
		BulkOperationsCollection<IBulkOperation> Operations { get; set; }
	}

	public partial class BulkRequest
	{
		public BulkOperationsCollection<IBulkOperation> Operations { get; set; }
	}

	public partial class BulkDescriptor
	{
		BulkOperationsCollection<IBulkOperation> IBulkRequest.Operations { get; set; } = new BulkOperationsCollection<IBulkOperation>();

		public BulkDescriptor AddOperation(IBulkOperation operation) => Assign(operation, (a, v) => a.Operations.AddIfNotNull(v));

		public BulkDescriptor Create<T>(Func<BulkCreateDescriptor<T>, IBulkCreateOperation<T>> bulkCreateSelector) where T : class =>
			AddOperation(bulkCreateSelector?.Invoke(new BulkCreateDescriptor<T>()));

		/// <summary>
		/// CreateMany, convenience method to create many documents at once.
		/// </summary>
		/// <param name="objects">the objects to create</param>
		/// <param name="bulkCreateSelector">A func called on each object to describe the individual create operation</param>
		public BulkDescriptor CreateMany<T>(IEnumerable<T> @objects,
			Func<BulkCreateDescriptor<T>, T, IBulkCreateOperation<T>> bulkCreateSelector = null
		) where T : class
		{
			var operations = @objects
				.Select(o => bulkCreateSelector.InvokeOrDefault(new BulkCreateDescriptor<T>().Document(o), o))
				.Where(o => o != null)
				.ToList();
			return Assign(operations, (a, v) => a.Operations.AddRange(v));
		}

		public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, IBulkIndexOperation<T>> bulkIndexSelector) where T : class =>
			AddOperation(bulkIndexSelector?.Invoke(new BulkIndexDescriptor<T>()));

		/// <summary>
		/// IndexMany, convenience method to pass many objects at once.
		/// </summary>
		/// <param name="objects">the objects to index</param>
		/// <param name="bulkIndexSelector">A func called on each object to describe the individual index operation</param>
		public BulkDescriptor IndexMany<T>(IEnumerable<T> @objects, Func<BulkIndexDescriptor<T>, T, IBulkIndexOperation<T>> bulkIndexSelector = null)
			where T : class
		{
			var operations = @objects
				.Select(o => bulkIndexSelector.InvokeOrDefault(new BulkIndexDescriptor<T>().Document(o), o))
				.Where(o => o != null)
				.ToList();
			return Assign(operations, (a, v) => a.Operations.AddRange(v));
		}

		public BulkDescriptor Delete<T>(T obj, Func<BulkDeleteDescriptor<T>, IBulkDeleteOperation<T>> bulkDeleteSelector = null) where T : class =>
			AddOperation(bulkDeleteSelector.InvokeOrDefault(new BulkDeleteDescriptor<T>().Document(obj)));

		public BulkDescriptor Delete<T>(Func<BulkDeleteDescriptor<T>, IBulkDeleteOperation<T>> bulkDeleteSelector) where T : class =>
			AddOperation(bulkDeleteSelector?.Invoke(new BulkDeleteDescriptor<T>()));

		/// <summary>
		/// DeleteMany, convenience method to delete many objects at once.
		/// </summary>
		/// <param name="objects">the objects to delete</param>
		/// <param name="bulkDeleteSelector">A func called on each object to describe the individual delete operation</param>
		public BulkDescriptor DeleteMany<T>(IEnumerable<T> @objects,
			Func<BulkDeleteDescriptor<T>, T, IBulkDeleteOperation<T>> bulkDeleteSelector = null
		) where T : class
		{
			var operations = @objects
				.Select(o => bulkDeleteSelector.InvokeOrDefault(new BulkDeleteDescriptor<T>().Document(o), o))
				.Where(o => o != null)
				.ToList();
			return Assign(operations, (a, v) => a.Operations.AddRange(v));
		}

		/// <summary>
		/// DeleteMany, convenience method to delete many objects at once.
		/// </summary>
		/// <param name="ids">Enumerable of string ids to delete</param>
		/// <param name="bulkDeleteSelector">A func called on each ids to describe the individual delete operation</param>
		public BulkDescriptor DeleteMany<T>(IEnumerable<string> ids,
			Func<BulkDeleteDescriptor<T>, string, IBulkDeleteOperation<T>> bulkDeleteSelector = null
		) where T : class
		{
			var operations = ids
				.Select(id => bulkDeleteSelector.InvokeOrDefault(new BulkDeleteDescriptor<T>().Id(id), id))
				.Where(id => id != null)
				.ToList();
			return Assign(operations, (a, v) => a.Operations.AddRange(v));
		}

		/// <summary>
		/// DeleteMany, convenience method to delete many objects at once.
		/// </summary>
		/// <param name="ids">Enumerable of int ids to delete</param>
		/// <param name="bulkDeleteSelector">A func called on each ids to describe the individual delete operation</param>
		public BulkDescriptor DeleteMany<T>(IEnumerable<long> ids,
			Func<BulkDeleteDescriptor<T>, long, IBulkDeleteOperation<T>> bulkDeleteSelector = null
		) where T : class
		{
			var operations = ids
				.Select(id => bulkDeleteSelector.InvokeOrDefault(new BulkDeleteDescriptor<T>().Id(id), id))
				.Where(id => id != null)
				.ToList();
			return Assign(operations, (a, v) => a.Operations.AddRange(v));
		}

		/// <summary>
		/// Updatemany, convenience method to pass many objects at once to do multiple updates.
		/// </summary>
		/// <param name="objects">the objects to update</param>
		/// <param name="bulkUpdateSelector">An func called on each object to describe the individual update operation</param>
		public BulkDescriptor UpdateMany<T>(IEnumerable<T> @objects,
			Func<BulkUpdateDescriptor<T, T>, T, IBulkUpdateOperation<T, T>> bulkUpdateSelector
		) where T : class
		{
			var operations = @objects
				.Select(o => bulkUpdateSelector.InvokeOrDefault(new BulkUpdateDescriptor<T, T>().IdFrom(o), o))
				.Where(o => o != null)
				.ToList();
			return Assign(operations, (a, v) => a.Operations.AddRange(v));
		}

		/// <summary>
		/// Update many, convenience method to pass many objects at once to do multiple updates.
		/// </summary>
		/// <param name="objects">the objects to update</param>
		/// <param name="bulkUpdateSelector">An func called on each object to describe the individual update operation</param>
		public BulkDescriptor UpdateMany<T, TPartialDocument>(IEnumerable<T> @objects,
			Func<BulkUpdateDescriptor<T, TPartialDocument>, T, IBulkUpdateOperation<T, TPartialDocument>> bulkUpdateSelector
		) where T : class
		  where TPartialDocument : class
		{
			var operations = @objects
				.Select(o => bulkUpdateSelector.InvokeOrDefault(new BulkUpdateDescriptor<T, TPartialDocument>().IdFrom(o), o))
				.Where(o => o != null)
				.ToList();
			return Assign(operations, (a, v) => a.Operations.AddRange(v));
		}

		public BulkDescriptor Update<T>(Func<BulkUpdateDescriptor<T, T>, IBulkUpdateOperation<T, T>> bulkUpdateSelector) where T : class =>
			Update<T, T>(bulkUpdateSelector);

		public BulkDescriptor Update<T, TPartialDocument>(
			Func<BulkUpdateDescriptor<T, TPartialDocument>, IBulkUpdateOperation<T, TPartialDocument>> bulkUpdateSelector
		) where T : class
		  where TPartialDocument : class => AddOperation(bulkUpdateSelector?.Invoke(new BulkUpdateDescriptor<T, TPartialDocument>()));
	}
}
