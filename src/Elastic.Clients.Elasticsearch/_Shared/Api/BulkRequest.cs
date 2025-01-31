// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Elastic.Transport;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Clients.Elasticsearch.Requests;

namespace Elastic.Clients.Elasticsearch;

public partial class BulkRequest : IStreamSerializable
{
	private static readonly IRequestConfiguration RequestConfigSingleton = new RequestConfiguration
	{
		Accept = "application/json",
		ContentType = "application/x-ndjson"
	};

	internal Request Self => this;

	protected internal override IRequestConfiguration RequestConfig => RequestConfigSingleton;

	public BulkOperationsCollection Operations { get; set; }

	public void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
	{
		if (Operations is null)
			return;

		var index = RouteValues.Get<IndexName>("index");

		foreach (var op in Operations)
		{
			if (op is not IStreamSerializable serializable)
			{
				ThrowHelper.ThrowInvalidOperationForBulkWhenNotIStreamSerializable();
				return;
			}

			op.PrepareIndex(index);

			serializable.Serialize(stream, settings, SerializationFormatting.None);
			stream.WriteByte((byte)'\n');
		}
	}

	public async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
	{
		if (Operations is null)
			return;

		var index = RouteValues.Get<IndexName>("index");

		foreach (var op in Operations)
		{
			if (op is not IStreamSerializable serializable)
			{
				ThrowHelper.ThrowInvalidOperationForBulkWhenNotIStreamSerializable();
				return;
			}

			op.PrepareIndex(index);

			await serializable.SerializeAsync(stream, settings, SerializationFormatting.None).ConfigureAwait(false);
			stream.WriteByte((byte)'\n');
		}
	}
}

public sealed partial class BulkRequestDescriptor : IStreamSerializable
{
	private static readonly IRequestConfiguration RequestConfigSingleton = new RequestConfiguration
	{
		Accept = "application/json",
		ContentType = "application/x-ndjson"
	};

	protected internal override IRequestConfiguration RequestConfig => RequestConfigSingleton;

	private readonly BulkOperationsCollection _operations = new();

	public BulkRequestDescriptor Index(string index)
	{
		RouteValues.Optional("index", IndexName.Parse(index));
		return Self;
	}

	public BulkRequestDescriptor Create<TSource>(TSource document, Action<BulkCreateOperationDescriptor<TSource>> configure = null)
	{
		var descriptor = new BulkCreateOperationDescriptor<TSource>(document);
		configure?.Invoke(descriptor);
		_operations.Add(descriptor);
		return this;
	}

	public BulkRequestDescriptor Create<TSource>(TSource document, IndexName index, Action<BulkCreateOperationDescriptor<TSource>> configure = null)
	{
		var descriptor = new BulkCreateOperationDescriptor<TSource>(document, index);
		configure?.Invoke(descriptor);
		_operations.Add(descriptor);
		return this;
	}

	public BulkRequestDescriptor Index<TSource>(TSource document, Action<BulkIndexOperationDescriptor<TSource>> configure = null)
	{
		var descriptor = new BulkIndexOperationDescriptor<TSource>(document);
		configure?.Invoke(descriptor);
		_operations.Add(descriptor);
		return this;
	}

	public BulkRequestDescriptor Index<TSource>(TSource document, IndexName index, Action<BulkIndexOperationDescriptor<TSource>> configure = null)
	{
		var descriptor = new BulkIndexOperationDescriptor<TSource>(document, index);
		configure?.Invoke(descriptor);
		_operations.Add(descriptor);
		return this;
	}

	public BulkRequestDescriptor Update(BulkUpdateOperation update)
	{
		_operations.Add(update);
		return this;
	}

	public BulkRequestDescriptor Update<TSource, TPartialDocument>(Action<BulkUpdateOperationDescriptor<TSource, TPartialDocument>> configure)
	{
		var descriptor = new BulkUpdateOperationDescriptor<TSource, TPartialDocument>();
		configure?.Invoke(descriptor);
		_operations.Add(descriptor);
		return this;
	}

	public BulkRequestDescriptor Update<T>(Action<BulkUpdateOperationDescriptor<T, T>> configure) =>
		Update<T, T>(configure);

	public BulkRequestDescriptor Delete(Id id, Action<BulkDeleteOperationDescriptor> configure = null)
	{
		var descriptor = new BulkDeleteOperationDescriptor(id);
		configure?.Invoke(descriptor);
		_operations.Add(descriptor);
		return this;
	}

	public BulkRequestDescriptor Delete(string id, Action<BulkDeleteOperationDescriptor> configure = null)
	{
		var descriptor = new BulkDeleteOperationDescriptor(id);
		configure?.Invoke(descriptor);
		_operations.Add(descriptor);
		return this;
	}

	public BulkRequestDescriptor Delete(Action<BulkDeleteOperationDescriptor> configure)
	{
		var descriptor = new BulkDeleteOperationDescriptor();
		configure?.Invoke(descriptor);
		_operations.Add(descriptor);
		return this;
	}

	public BulkRequestDescriptor Delete<TSource>(TSource documentToDelete, Action<BulkDeleteOperationDescriptor> configure = null)
	{
		var descriptor = new BulkDeleteOperationDescriptor(new Id(documentToDelete));
		configure?.Invoke(descriptor);
		_operations.Add(descriptor);
		return this;
	}

	public BulkRequestDescriptor Delete<TSource>(Action<BulkDeleteOperationDescriptor> configure) => Delete(configure);

	public BulkRequestDescriptor CreateMany<TSource>(IEnumerable<TSource> documents, Action<BulkCreateOperationDescriptor<TSource>, TSource> bulkCreateSelector) =>
		AddOperations(documents, bulkCreateSelector, o => new BulkCreateOperationDescriptor<TSource>(o));

	public BulkRequestDescriptor CreateMany<TSource>(IEnumerable<TSource> documents) =>
		AddOperations(documents, null, o => new BulkCreateOperationDescriptor<TSource>(o));

	public BulkRequestDescriptor IndexMany<TSource>(IEnumerable<TSource> documents, Action<BulkIndexOperationDescriptor<TSource>, TSource> bulkIndexSelector) =>
		AddOperations(documents, bulkIndexSelector, o => new BulkIndexOperationDescriptor<TSource>(o));

	public BulkRequestDescriptor IndexMany<TSource>(IEnumerable<TSource> documents) =>
		AddOperations(documents, null, o => new BulkIndexOperationDescriptor<TSource>(o));

	public BulkRequestDescriptor UpdateMany<TSource>(IEnumerable<TSource> objects, Action<BulkUpdateOperationDescriptor<TSource, TSource>, TSource> bulkIndexSelector) =>
		AddOperations(objects, bulkIndexSelector, o => new BulkUpdateOperationDescriptor<TSource, TSource>().IdFrom(o));

	public BulkRequestDescriptor UpdateMany<TSource>(IEnumerable<TSource> objects) =>
		AddOperations(objects, null, o => new BulkUpdateOperationDescriptor<TSource, TSource>().IdFrom(o));

	public BulkRequestDescriptor DeleteMany<T>(IEnumerable<T> objects, Action<BulkDeleteOperationDescriptor, T> bulkDeleteSelector) =>
		AddOperations(objects, bulkDeleteSelector, obj => new BulkDeleteOperationDescriptor(new Id(obj)));

	public BulkRequestDescriptor DeleteMany(IEnumerable<Id> ids, Action<BulkDeleteOperationDescriptor, Id> bulkDeleteSelector) =>
		AddOperations(ids, bulkDeleteSelector, id => new BulkDeleteOperationDescriptor(id));

	public BulkRequestDescriptor DeleteMany<T>(IEnumerable<T> objects) =>
		AddOperations(objects, null, obj => new BulkDeleteOperationDescriptor<T>(obj));

	public BulkRequestDescriptor DeleteMany(IndexName index, IEnumerable<Id> ids) =>
		AddOperations(ids, null, id => new BulkDeleteOperationDescriptor(id).Index(index));

	public void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
	{
		if (_operations is null)
			return;

		var index = Self.RouteValues.Get<IndexName>("index");

		foreach (var op in _operations)
		{
			if (op is not IStreamSerializable serializable)
			{
				ThrowHelper.ThrowInvalidOperationForBulkWhenNotIStreamSerializable();
				return;
			}

			op.PrepareIndex(index);

			serializable.Serialize(stream, settings, SerializationFormatting.None);
			stream.WriteByte((byte)'\n');
		}
	}

	public async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
	{
		if (_operations is null)
			return;

		var index = Self.RouteValues.Get<IndexName>("index");

		foreach (var op in _operations)
		{
			if (op is not IStreamSerializable serializable)
			{
				ThrowHelper.ThrowInvalidOperationForBulkWhenNotIStreamSerializable();
				return;
			}

			op.PrepareIndex(index);

			await serializable.SerializeAsync(stream, settings, SerializationFormatting.None).ConfigureAwait(false);
			stream.WriteByte((byte)'\n');
		}
	}

	private BulkRequestDescriptor AddOperations<TSource, TDescriptor>(
		IEnumerable<TSource> objects,
		Action<TDescriptor, TSource> configureDescriptor,
		Func<TSource, TDescriptor> createDescriptor
	) where TDescriptor : IBulkOperation
	{
		if (@objects == null)
			return this;

		var objectsList = @objects.ToList();
		var operations = new List<IBulkOperation>(objectsList.Count());

		foreach (var o in objectsList)
		{
			var descriptor = createDescriptor(o);

			if (configureDescriptor is not null)
			{
				configureDescriptor(descriptor, o);
			}

			operations.Add(descriptor);
		}

		_operations.AddRange(operations);
		return Self;
	}
}
