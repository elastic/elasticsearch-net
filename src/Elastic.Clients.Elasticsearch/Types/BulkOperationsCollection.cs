// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	//public interface BulkOperationBase
	//{
	//	//BulkOperationBase Operation { get; set; }

	//	//TDocument Document { get; set; }
	//}

	//public sealed class BulkOperation<TDocument> : BulkOperationBase<TDocument>
	//{
	//	public BulkOperationBase Operation { get; set; }

	//	public TDocument Document { get; set; }
	//}

	/// <summary>
	/// Marker interface for types which can be serialised as an operation of a bulk API request.
	/// </summary>
	public interface IBulkOperation
	{
	}

	internal sealed class BulkResponseItemConverter : JsonConverter<IReadOnlyList<ResponseItem>>
	{
		public override IReadOnlyList<ResponseItem>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartArray)
				throw new JsonException("Unexpected token");

			var responseItems = new List<ResponseItem>();

			while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
			{
				if (reader.TokenType != JsonTokenType.StartObject)
					throw new JsonException("Unexpected token");

				reader.Read();

				if (reader.TokenType != JsonTokenType.PropertyName)
					throw new JsonException("Unexpected token");

				ResponseItem responseItem;

				if (reader.ValueTextEquals("index"))
				{
					responseItem = JsonSerializer.Deserialize<IndexResponseItem>(ref reader, options);
				}
				else if (reader.ValueTextEquals("delete"))
				{
					responseItem = JsonSerializer.Deserialize<DeleteResponseItem>(ref reader, options);
				}
				else if (reader.ValueTextEquals("create"))
				{
					responseItem = JsonSerializer.Deserialize<CreateResponseItem>(ref reader, options);
				}
				else if (reader.ValueTextEquals("update"))
				{
					responseItem = JsonSerializer.Deserialize<UpdateResponseItem>(ref reader, options);
				}
				else
				{
					throw new JsonException("Unexpected operation type");
				}

				responseItems.Add(responseItem);

				reader.Read();

				if (reader.TokenType != JsonTokenType.EndObject)
					throw new JsonException("Unexpected token");
			}

			return responseItems;
		}

		public override void Write(Utf8JsonWriter writer, IReadOnlyList<ResponseItem> value, JsonSerializerOptions options) => throw new NotImplementedException();
	}


	public sealed class IndexResponseItem : ResponseItem
	{
		public string Operation { get; } = "index";
	}

	public sealed class DeleteResponseItem : ResponseItem
	{
		public string Operation { get; } = "delete";
	}

	public sealed class CreateResponseItem : ResponseItem
	{
		public string Operation { get; } = "create";
	}

	public sealed class UpdateResponseItem : ResponseItem
	{
		public string Operation { get; } = "update";
	}

	public sealed class BulkIndexOperation<T> : BulkOperationBase
	{
		private static byte _newline => (byte)'\n';

		public BulkIndexOperation(T document) => Document = document;

		[JsonPropertyName("pipeline")]
		public string? Pipeline { get; set; }

		[JsonPropertyName("require_alias")]
		public bool? RequireAlias { get; set; }

		[JsonPropertyName("dynamic_templates")]
		public Dictionary<string, string>? DynamicTemplates { get; set; }

		[JsonIgnore]
		public T Document { get; set; }

		protected override Type ClrType => typeof(T);

		protected override string Operation => "index";

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			settings.SourceSerializer.Serialize(Document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			stream.WriteByte(_newline);

			await settings.SourceSerializer.SerializeAsync(Document, stream, formatting).ConfigureAwait(false);
		}
	}

	public sealed class BulkCreateOperation<T> : BulkOperationBase
	{
		private static byte _newline => (byte)'\n';

		public BulkCreateOperation(T document) => Document = document;

		[JsonPropertyName("pipeline")]
		public string? Pipeline { get; set; }

		[JsonPropertyName("require_alias")]
		public bool? RequireAlias { get; set; }

		[JsonPropertyName("dynamic_templates")]
		public Dictionary<string, string>? DynamicTemplates { get; set; }

		[JsonIgnore]
		public T Document { get; set; }

		protected override Type ClrType => typeof(T);

		protected override string Operation => "create";

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkCreateOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkCreateOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			settings.SourceSerializer.Serialize(Document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkCreateOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkCreateOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			stream.WriteByte(_newline);

			await settings.SourceSerializer.SerializeAsync(Document, stream, formatting).ConfigureAwait(false);
		}
	}

	public sealed class BulkUpdateOperation<TDocument, TPartialDocument> : BulkOperationBase
	{
		private static byte _newline => (byte)'\n';

		[JsonPropertyName("retry_on_conflict")]
		public bool? RetryOnConflict { get; set; }

		[JsonPropertyName("require_alias")]
		public bool? RequireAlias { get; set; }

		[JsonIgnore]
		public TDocument IdFrom { get; set; }

		[JsonIgnore]
		public TPartialDocument PartialDocument { get; set; }

		protected override Type ClrType => typeof(TDocument);

		protected override string Operation => "update";

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			if (Id is null && IdFrom is not null)
				Id = settings.Inferrer.Id<TDocument>(IdFrom);

			if (Index is null)
				Index = settings.Inferrer.IndexName<TDocument>();

			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkUpdateOperation<TDocument, TPartialDocument>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkUpdateOperation<TDocument, TPartialDocument>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			var body = new BulkUpdateBody<TDocument, TPartialDocument>()
			{
				Upsert = IdFrom,
				PartialUpdate = PartialDocument
			};

			settings.SourceSerializer.Serialize(body, stream, formatting);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			if (Id is null && IdFrom is not null)
				Id = settings.Inferrer.Id<TDocument>(IdFrom);

			if (Index is null)
				Index = settings.Inferrer.IndexName<TDocument>();

			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkUpdateOperation<TDocument, TPartialDocument>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkUpdateOperation<TDocument, TPartialDocument>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			stream.WriteByte(_newline);

			var body = new BulkUpdateBody<TDocument, TPartialDocument>()
			{
				Upsert = IdFrom,
				PartialUpdate = PartialDocument
			};

			await settings.SourceSerializer.SerializeAsync(body, stream, formatting).ConfigureAwait(false);
		}
	}

	internal class BulkUpdateBody<TDocument, TPartialUpdate>
	{
		[JsonPropertyName("doc_as_upsert")]
		public bool? DocAsUpsert { get; set; }

		[JsonPropertyName("doc")]
		//[JsonFormatter(typeof(CollapsedSourceFormatter<>))]
		public TPartialUpdate PartialUpdate { get; set; }

		[JsonPropertyName("script")]
		public ScriptBase Script { get; set; }

		[JsonPropertyName("scripted_upsert")]
		public bool? ScriptedUpsert { get; set; }

		[JsonPropertyName("upsert")]
		//[JsonFormatter(typeof(CollapsedSourceFormatter<>))]
		public TDocument Upsert { get; set; }

		[JsonPropertyName("if_seq_no")]
		public long? IfSequenceNumber { get; set; }

		[JsonPropertyName("if_primary_term")]
		public long? IfPrimaryTerm { get; set; }

		//[DataMember(Name = "_source")]
		//internal Union<bool, ISourceFilter> Source { get; set; }
	}
	

	public sealed class BulkDeleteOperation<T> : BulkOperationBase
	{
		protected override Type ClrType => typeof(T);

		protected override string Operation => "delete";

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkDeleteOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkDeleteOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			//stream.WriteByte(_newline);

			//settings.SourceSerializer.Serialize(Document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkDeleteOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkDeleteOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			//stream.WriteByte(_newline);

			//await settings.SourceSerializer.SerializeAsync(Document, stream, formatting).ConfigureAwait(false);
		}
	}

	public sealed class BulkIndexOperationDescriptor<TSource> : BulkOperationDescriptorBase<BulkIndexOperationDescriptor<TSource>, TSource>
	{
		private string _pipeline;
		private bool? _requireAlias;
		private Dictionary<string, string> _dynamicTemplates;

		private static byte _newline => (byte)'\n';

		private readonly TSource _document;

		public BulkIndexOperationDescriptor(TSource source) => _document = source;

		public BulkIndexOperationDescriptor<TSource> Pipeline(string pipeline) => Assign(pipeline, (a, v) => a._pipeline = v);

		public BulkIndexOperationDescriptor<TSource> RequireAlias(bool? requireAlias = true) => Assign(requireAlias, (a, v) => a._requireAlias = v);

		public BulkIndexOperationDescriptor<TSource> DynamicTemplates(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector) => Assign(selector, (a, v) => a._dynamicTemplates = v?.Invoke(new FluentDictionary<string, string>()));

		protected override string Operation => "index";

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			settings.SourceSerializer.Serialize(_document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			stream.WriteByte(_newline);

			await settings.SourceSerializer.SerializeAsync(_document, stream).ConfigureAwait(false);
		}

		protected override void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (!string.IsNullOrEmpty(_pipeline))
			{
				writer.WritePropertyName("pipeline");
				JsonSerializer.Serialize(writer, _pipeline, options);
			}

			if (_requireAlias.HasValue)
			{
				writer.WritePropertyName("require_alias");
				JsonSerializer.Serialize(writer, _requireAlias.Value, options);
			}

			if (_dynamicTemplates is not null)
			{
				writer.WritePropertyName("dynamic_templates");
				JsonSerializer.Serialize(writer, _dynamicTemplates, options);
			}
		}
	}

	public sealed class BulkCreateOperationDescriptor<TSource> : BulkOperationDescriptorBase<BulkCreateOperationDescriptor<TSource>, TSource>
	{
		private string _pipeline;
		private bool? _requireAlias;
		private Dictionary<string, string> _dynamicTemplates;

		private static byte _newline => (byte)'\n';

		private readonly TSource _document;

		public BulkCreateOperationDescriptor(TSource source) => _document = source;

		public BulkCreateOperationDescriptor<TSource> Pipeline(string pipeline) => Assign(pipeline, (a, v) => a._pipeline = v);

		public BulkCreateOperationDescriptor<TSource> RequireAlias(bool? requireAlias = true) => Assign(requireAlias, (a, v) => a._requireAlias = v);

		public BulkCreateOperationDescriptor<TSource> DynamicTemplates(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector) => Assign(selector, (a, v) => a._dynamicTemplates = v?.Invoke(new FluentDictionary<string, string>()));

		protected override string Operation => "create";

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkCreateOperationDescriptor<TSource>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkCreateOperationDescriptor<TSource>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			settings.SourceSerializer.Serialize(_document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkCreateOperationDescriptor<TSource>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkCreateOperationDescriptor<TSource>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			stream.WriteByte(_newline);

			await settings.SourceSerializer.SerializeAsync(_document, stream).ConfigureAwait(false);
		}

		protected override void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (!string.IsNullOrEmpty(_pipeline))
			{
				writer.WritePropertyName("pipeline");
				JsonSerializer.Serialize(writer, _pipeline, options);
			}

			if (_requireAlias.HasValue)
			{
				writer.WritePropertyName("require_alias");
				JsonSerializer.Serialize(writer, _requireAlias.Value, options);
			}

			if (_dynamicTemplates is not null)
			{
				writer.WritePropertyName("dynamic_templates");
				JsonSerializer.Serialize(writer, _dynamicTemplates, options);
			}
		}
	}

	public sealed class BulkDeleteOperationDescriptor<TSource> : BulkOperationDescriptorBase<BulkDeleteOperationDescriptor<TSource>, TSource>
	{
		public BulkDeleteOperationDescriptor() { }

		public BulkDeleteOperationDescriptor(Id id) => Id(id);

		protected override string Operation => "delete";

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkDeleteOperationDescriptor<TSource>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkDeleteOperationDescriptor<TSource>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkDeleteOperationDescriptor<TSource>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkDeleteOperationDescriptor<TSource>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);
		}

		protected override void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}

	public abstract class BulkOperationDescriptorBase<TDescriptor, TSource> : DescriptorBase<TDescriptor>, IBulkOperation, IStreamSerializable where TDescriptor : BulkOperationDescriptorBase<TDescriptor, TSource>
	{
		private Id _id;
		private long? _version;
		private IndexName _index;
		private Routing _routing;
		private VersionType? _versionType;
		private long? _ifSequenceNo;
		private long? _ifPrimaryTerm;

		protected abstract string Operation { get; }

		public TDescriptor Id(Id id) => Assign(id, (a, v) => a._id = v);

		public TDescriptor IfSequenceNumber(long? ifSequenceNumber) => Assign(ifSequenceNumber, (a, v) => a._ifSequenceNo = v);

		public TDescriptor IfPrimaryTerm(long? ifPrimaryTerm) => Assign(ifPrimaryTerm, (a, v) => a._ifPrimaryTerm = v);

		public TDescriptor Index(IndexName index) => Assign(index, (a, v) => a._index = v);

		public TDescriptor Index<T>() => Assign(typeof(T), (a, v) => a._index = v);

		public TDescriptor Routing(Routing routing) => Assign(routing, (a, v) => a._routing = v);

		public TDescriptor Version(long version) => Assign(version, (a, v) => a._version = v);

		public TDescriptor VersionType(VersionType? versionType) => Assign(versionType, (a, v) => a._versionType = v);

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();

			SerializeInternal(writer, options, settings);

			if (_id is not null)
			{
				writer.WritePropertyName("_id");
				JsonSerializer.Serialize(writer, _id, options);
			}

			if (_ifPrimaryTerm.HasValue)
			{
				writer.WritePropertyName("if_primary_term");
				JsonSerializer.Serialize(writer, _ifPrimaryTerm.Value, options);
			}

			if (_ifSequenceNo.HasValue)
			{
				writer.WritePropertyName("if_seq_no");
				JsonSerializer.Serialize(writer, _ifSequenceNo.Value, options);
			}

			if (_index is not null)
			{
				writer.WritePropertyName("_index");
				JsonSerializer.Serialize(writer, _index, options);
			}
			else
			{
				writer.WritePropertyName("_index");
				var index = settings.Inferrer.IndexName<TSource>();
				JsonSerializer.Serialize(writer, index, options);
			}

			if (_routing is not null)
			{
				writer.WritePropertyName("routing");
				JsonSerializer.Serialize(writer, _routing, options);
			}

			if (_version.HasValue)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, _version.Value, options);
			}

			if (_versionType.HasValue)
			{
				writer.WritePropertyName("version_type");
				JsonSerializer.Serialize(writer, _versionType.Value, options);
			}

			writer.WriteEndObject();
		}

		protected abstract void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);

		protected abstract void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting);

		protected abstract Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default);

		void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) => Serialize(stream, settings, formatting);

		Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) => SerializeAsync(stream, settings, formatting);
	}

	public abstract class BulkOperationBase : IBulkOperation, IStreamSerializable
	{
		[JsonPropertyName("_id")]
		public Id Id { get; set; }

		[JsonPropertyName("if_primary_term")]
		public long? IfPrimaryTerm { get; set; }

		[JsonPropertyName("if_seq_no")]
		public long? IfSequenceNumber { get; set; }

		[JsonPropertyName("_index")]
		public IndexName Index { get; set; }

		[JsonPropertyName("routing")]
		public Routing Routing { get; set; }

		[JsonPropertyName("version")]
		public long? Version { get; set; }

		[JsonPropertyName("version_type")]
		public VersionType? VersionType { get; set; }

		protected abstract Type ClrType { get; }

		protected abstract string Operation { get; }

		protected abstract void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None);

		protected abstract Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None);

		void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) =>
			Serialize(stream, settings, formatting);

		Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) =>
			SerializeAsync(stream, settings, formatting);

		//Type BulkOperationBase.ClrType => ClrType;

		//string BulkOperationBase.Operation => Operation;

		//object BulkOperationBase.GetBody() => GetBody();

		//Id BulkOperationBase.GetIdForOperation(Inferrer inferrer) => GetIdForOperation(inferrer);

		//Routing BulkOperationBase.GetRoutingForOperation(Inferrer inferrer) => GetRoutingForOperation(inferrer);

		//protected abstract object GetBody();

		//protected virtual Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(GetBody());

		//protected virtual Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(GetBody());
	}

	///// <summary>
	///// This class is used by <see cref="IBulkRequest.Operations" /> which needs thread safe adding <see cref="ICollection{T}.Add" /> as well as expose
	///// an equivalent of <see cref="List{T}.AddRange"/>. Because operations from Elasticsearch are executed in order none of the types in
	///// System.Collection.Concurrent can't be used for this. We need to preserve insert order and exposed indexed index because <see cref="BulkResponse.Items"/>
	///// is ordered and lines up with <see cref="BulkRequest.Operations"/> allowing one to zip the two together.
	///// </summary>
	///// <typeparam name="TOperation"></typeparam>
	public sealed class BulkOperationsCollection : IList<IBulkOperation>, IList, IStreamSerializable
	//where TOperation : IBulkOperation
	{
		private readonly object _lock = new();

		public BulkOperationsCollection() => Items = new List<IBulkOperation>();

		public BulkOperationsCollection(IEnumerable<IBulkOperation> operations)
		{
			Items = new List<IBulkOperation>();
			Items.AddRange(operations);
		}

		public int Count
		{
			get
			{
				lock (_lock)
					return Items.Count;
			}
		}

		public IBulkOperation this[int index]
		{
			get
			{
				lock (_lock)
					return Items[index];
			}
			set
			{
				lock (_lock)
				{
					if (index < 0 || index >= Items.Count)
						throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {Items.Count}");

					Items[index] = value;
				}
			}
		}

		bool IList.IsFixedSize => false;

		bool ICollection<IBulkOperation>.IsReadOnly => false;

		bool IList.IsReadOnly => false;

		bool ICollection.IsSynchronized => true;

		object IList.this[int index]
		{
			get => this[index];
			set
			{
				VerifyValueType(value);
				this[index] = (IBulkOperation)value;
			}
		}

		private List<IBulkOperation> Items { get; }

		object ICollection.SyncRoot => _lock;

		void ICollection.CopyTo(Array array, int index)
		{
			lock (_lock)
				((IList)Items).CopyTo(array, index);
		}

		public void Add(IBulkOperation item)
		{
			lock (_lock)
				Items.Add(item);
		}

		public void Clear()
		{
			lock (_lock)
				Items.Clear();
		}

		public bool Contains(IBulkOperation item)
		{
			lock (_lock)
				return Items.Contains(item);
		}

		public void CopyTo(IBulkOperation[] array, int index)
		{
			lock (_lock)
				Items.CopyTo(array, index);
		}

		public bool Remove(IBulkOperation item)
		{
			lock (_lock)
			{
				var index = InternalIndexOf(item);
				if (index < 0)
					return false;

				RemoveItem(index);
				return true;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => ((IList)Items).GetEnumerator();

		public IEnumerator<IBulkOperation> GetEnumerator()
		{
			lock (_lock)
				return Items.GetEnumerator();
		}

		int IList.Add(object value)
		{
			VerifyValueType(value);

			lock (_lock)
			{
				Add((IBulkOperation)value);
				return Count - 1;
			}
		}

		bool IList.Contains(object value)
		{
			VerifyValueType(value);
			return Contains((IBulkOperation)value);
		}

		int IList.IndexOf(object value)
		{
			VerifyValueType(value);
			return IndexOf((IBulkOperation)value);
		}

		void IList.Insert(int index, object value)
		{
			VerifyValueType(value);
			Insert(index, (IBulkOperation)value);
		}

		void IList.Remove(object value)
		{
			VerifyValueType(value);
			Remove((IBulkOperation)value);
		}

		public static implicit operator BulkOperationsCollection(List<IBulkOperation> items) => new(items);

		public int IndexOf(IBulkOperation item)
		{
			lock (_lock)
				return InternalIndexOf(item);
		}

		public void Insert(int index, IBulkOperation item)
		{
			lock (_lock)
			{
				if (index < 0 || index > Items.Count)
					throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {Items.Count}");

				InsertItem(index, item);
			}
		}

		public void RemoveAt(int index)
		{
			lock (_lock)
			{
				if (index < 0 || index >= Items.Count)
					throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {Items.Count}");

				RemoveItem(index);
			}
		}

		public void AddRange(IEnumerable<IBulkOperation> items)
		{
			lock (_lock)
				Items.AddRange(items);
		}

		private int InternalIndexOf(IBulkOperation item)
		{
			var count = Items.Count;

			for (var i = 0; i < count; i++)
			{
				if (Equals(Items[i], item))
					return i;
			}
			return -1;
		}

		private void InsertItem(int index, IBulkOperation item) => Items.Insert(index, item);

		private void RemoveItem(int index) => Items.RemoveAt(index);

		private static void VerifyValueType(object value)
		{
			if (value == null)
			{
				if (typeof(IBulkOperation).IsValueType)
					throw new ArgumentException("value is null and a value type");
			}
			else if (value is not IBulkOperation)
				throw new ArgumentException($"object is of type {value.GetType().FullName} but collection is of {typeof(IBulkOperation).FullName}");
		}

		public void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			foreach (var op in this)
			{
				if (op is not IStreamSerializable serializable)
					throw new InvalidOperationException("");

				serializable.Serialize(stream, settings, formatting);
				stream.WriteByte((byte)'\n');
			}
		}

		public async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			foreach (var op in this)
			{
				if (op is not IStreamSerializable serializable)
					throw new InvalidOperationException("");

				await serializable.SerializeAsync(stream, settings, formatting).ConfigureAwait(false);
				stream.WriteByte((byte)'\n');
			}
		}
	}
}
