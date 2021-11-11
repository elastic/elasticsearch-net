// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading;
using Elastic.Clients.Elasticsearch.Analysis;
using Elastic.Transport;
using System.Text.Json;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch
{
	public partial struct WaitForActiveShards : IStringable
	{
		public static WaitForActiveShards All = new("all");

		public WaitForActiveShards(string value) => Value = value;

		public string Value { get; }

		public static implicit operator WaitForActiveShards(int v) => new(v.ToString());
		public static implicit operator WaitForActiveShards(string v) => new(v);

		public string GetString() => Value ?? string.Empty;
	}

	// COULD ALSO BE AN ENUM AS IN EXISTING NEST?
	public partial struct Refresh : IStringable
	{
		public static Refresh WaitFor = new("wait_for");
		public static Refresh True = new("true");
		public static Refresh False = new("false");

		public Refresh(string value) => Value = value;

		public string Value { get; }

		public string GetString() => Value ?? string.Empty;
	}


	public class DocType { }

	public partial interface IElasticClient
	{
		DeleteResponse Delete<TDocument>(Id id, Action<DeleteRequestDescriptor<TDocument>> configureRequest);

		Task<DeleteResponse> DeleteAsync<TDocument>(Id id, Action<DeleteRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default);

		CreateResponse Create<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest);

		Task<CreateResponse> CreateAsync<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default);

		IndexResponse Index<TDocument>(TDocument document, Action<IndexRequestDescriptor<TDocument>> configureRequest);

		Task<IndexResponse> IndexAsync<TDocument>(TDocument document, Action<IndexRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default);

		Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest = null, CancellationToken cancellationToken = default);

		UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest = null);
	}

	public partial class ElasticClient
{
		public IndexResponse Index<TDocument>(TDocument document, Action<IndexRequestDescriptor<TDocument>> configureRequest)
		{
			var descriptor = new IndexRequestDescriptor<TDocument>(documentWithId: document);
			configureRequest?.Invoke(descriptor);
			return DoRequest<IndexRequestDescriptor<TDocument>, IndexResponse>(descriptor);
		}

		public Task<IndexResponse> IndexAsync<TDocument>(TDocument document, Action<IndexRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new IndexRequestDescriptor<TDocument>(documentWithId: document);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<IndexRequestDescriptor<TDocument>, IndexResponse>(descriptor);
		}

		public CreateResponse Create<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest)
		{
			var descriptor = new CreateRequestDescriptor<TDocument>(documentWithId: document);
			configureRequest?.Invoke(descriptor);
			return DoRequest<CreateRequestDescriptor<TDocument>, CreateResponse>(descriptor);
		}

		public Task<CreateResponse> CreateAsync<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new CreateRequestDescriptor<TDocument>(documentWithId: document);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<CreateRequestDescriptor<TDocument>, CreateResponse>(descriptor);
		}

		public DeleteResponse Delete<TDocument>(Id id, Action<DeleteRequestDescriptor<TDocument>> configureRequest)
		{
			var descriptor = new DeleteRequestDescriptor<TDocument>(id);
			configureRequest?.Invoke(descriptor);
			return DoRequest<DeleteRequestDescriptor<TDocument>, DeleteResponse>(descriptor);
		}

		public Task<DeleteResponse> DeleteAsync<TDocument>(Id id, Action<DeleteRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new DeleteRequestDescriptor<TDocument>(id);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<DeleteRequestDescriptor<TDocument>, DeleteResponse>(descriptor);
		}

		public Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest = null, CancellationToken cancellationToken = default)
{
			var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(descriptor);
		}

		public UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest = null)
		{
			var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
			configureRequest?.Invoke(descriptor);
			return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(descriptor);
		}
	}

	public sealed partial class DeleteRequestDescriptor<TDocument> : RequestDescriptorBase<DeleteRequestDescriptor<TDocument>, DeleteRequestParameters>
	{
		public DeleteRequestDescriptor(IndexName index, Id id) : base(r => r.Required("index", index).Required("id", id))
		{
		}

		public DeleteRequestDescriptor(Id id) : this(typeof(TDocument), id)
		{
		}

		public DeleteRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) { }

		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceDelete;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		public DeleteRequestDescriptor<TDocument> IfPrimaryTerm(long? ifPrimaryTerm) => Qs("if_primary_term", ifPrimaryTerm);
		public DeleteRequestDescriptor<TDocument> IfSeqNo(long? ifSeqNo) => Qs("if_seq_no", ifSeqNo);
		public DeleteRequestDescriptor<TDocument> Refresh(Refresh? refresh) => Qs("refresh", refresh);
		public DeleteRequestDescriptor<TDocument> Routing(string? routing) => Qs("routing", routing);
		public DeleteRequestDescriptor<TDocument> Timeout(Time? timeout) => Qs("timeout", timeout);
		public DeleteRequestDescriptor<TDocument> Version(long? version) => Qs("version", version);
		public DeleteRequestDescriptor<TDocument> VersionType(VersionType? versionType) => Qs("version_type", versionType);
		public DeleteRequestDescriptor<TDocument> WaitForActiveShards(WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

		public DeleteRequestDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));
	}

	public sealed partial class CountRequestDescriptor
	{
		//public CountRequestDescriptor Query(Action<QueryContainerDescriptor> configureContainer) => Assign(query, (a, v) => a._query = v);
	}

	public sealed partial class CreateRequest<TDocument>
	{

		public CreateRequest(Id id) : this(typeof(TDocument), id)
		{
		}

		public CreateRequest(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) => Document = documentWithId;
	}

	public sealed partial class CreateRequestDescriptor<TDocument> : ICustomJsonWriter
	{
		public CreateRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Elasticsearch.Id.From(documentWithId)) => DocumentFromPath(documentWithId);

		private void DocumentFromPath(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);

		public CreateRequestDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));

		public void WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialisation.Serialize(DocumentValue, writer, sourceSerializer);

		// TODO: We should be able to generate these for optional params
		public CreateRequestDescriptor<TDocument> Id(Id id)
		{
			RouteValues.Optional("id", id);
			return this;
		}
	}

	public sealed partial class CreateRequestDescriptor<TDocument>
	{
		// TODO: Codegen
		public CreateRequestDescriptor<TDocument> Document(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);
	}

	public sealed partial class UpdateRequestDescriptor<TDocument, TPartialDocument>
	{
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Document(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> PartialDocument(TPartialDocument document) => this;  // TODO
	}

	public sealed partial class DeleteRequest<TDocument> : DeleteRequest
	{
		public DeleteRequest(IndexName index, Id id) : base(index, id) { }

		public DeleteRequest(Id id) : this(typeof(TDocument), id)
		{
		}

		public DeleteRequest(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) { }
	}

	public sealed partial class SearchRequestDescriptor
	{
		public SearchRequestDescriptor Query(Func<QueryContainerDescriptor, QueryContainer> configure)
		{
			var container = configure?.Invoke(new QueryContainerDescriptor());
			return Assign(container, (a, v) => a.QueryValue = v);
		}
	}

	public sealed partial class CountRequestDescriptor
	{
		public CountRequestDescriptor Query(Func<QueryContainerDescriptor, QueryContainer> configure)
		{
			var container = configure?.Invoke(new QueryContainerDescriptor());
			return Assign(container, (a, v) => a.QueryValue = v);
		}
	}
}

namespace Elastic.Clients.Elasticsearch.Analysis
{
	// TODO: Generator should handle these

	public sealed partial class ShingleTokenFilterDescriptor : ITokenFiltersVariant { }

	public sealed partial class TokenFiltersDescriptor : IsADictionaryDescriptorBase<TokenFiltersDescriptor, TokenFilters, string, ITokenFiltersVariant>
	{
		public TokenFiltersDescriptor() : base(new TokenFilters()) { }

		public TokenFiltersDescriptor UserDefined(string name, ITokenFiltersVariant analyzer) => Assign(name, analyzer);

		public TokenFiltersDescriptor Shingle(string name, Action<ShingleTokenFilterDescriptor> configure)
		{
			var descriptor = new ShingleTokenFilterDescriptor();
			configure?.Invoke(descriptor);
			return Assign(name, descriptor);
		}
	}

	// TODO: IndexRequestDescriptorConverter - As per https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-5-0#sample-factory-pattern-converter
}

namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public sealed partial class IndexSettingsAnalysisDescriptor
	{
		internal TokenFilters _tokenFilters;

		public IndexSettingsAnalysisDescriptor TokenFilters(Func<TokenFiltersDescriptor, IPromise<TokenFilters>> selector) =>
			Assign(selector, (a, v) => _tokenFilters = v?.Invoke(new TokenFiltersDescriptor())?.Value);
	}
}

namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	//public sealed partial class BoolQueryDescriptor
	//{
	//	internal BoolQuery ToQuery()
	//	{
	//		var query = new BoolQuery();

	//		if (_filter is not null)
	//			query.Filter = _filter;

	//		// TODO - More

	//		return query;
	//	}
	//}

	//public sealed partial class MatchQueryDescriptor
	//{
	//	public MatchQueryDescriptor Query(string query) => Assign(query, (a, v) => a._query = v);

	//	internal MatchQuery ToQuery()
	//	{
	//		var query = new MatchQuery();

	//		if (_field is not null)
	//			query.Field = _field;

	//		if (_query is not null)
	//			query.Query = _query;

	//		return query;
	//	}
	//}

	[JsonConverter(typeof(MatchQueryConverter))]
	public sealed partial class MatchQuery
	{

	}

	internal sealed class MatchQueryConverter : FieldNameQueryConverterBase<MatchQuery>
	{
		internal override MatchQuery ReadInternal(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
			{
				throw new JsonException();
			}

			string queryValue = default;

			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				var property = reader.GetString();

				if (property == "query")
				{
					reader.Read();
					queryValue = reader.GetString();
				}
			}

			var query = new MatchQuery()
			{
				Query = queryValue
			};

			return query;
		}

		internal override void WriteInternal(Utf8JsonWriter writer, MatchQuery value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(value.Query))
			{
				writer.WritePropertyName("query");
				writer.WriteStringValue(value.Query);
			}
			writer.WriteEndObject();
		}
	}

	

	internal sealed class MatchQueryDescriptorConverter : FieldNameQueryDescriptorConverterBase<MatchQueryDescriptor>
	{
		internal override void WriteInternal(Utf8JsonWriter writer, MatchQueryDescriptor value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			if (!string.IsNullOrEmpty(value.QueryValue))
			{
				writer.WritePropertyName("query");
				writer.WriteStringValue(value.QueryValue);
			}

			writer.WriteEndObject();
		}
	}

	internal sealed partial class QueryContainerDescriptorConverter : JsonConverter<QueryContainerDescriptor>
	{
		public override QueryContainerDescriptor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, QueryContainerDescriptor value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			if (value.ContainedVariant is not null)
			{
				writer.WritePropertyName(value._containedVariant);
				JsonSerializer.Serialize(writer, value.ContainedVariant, options);
			}

			if (value.QueryContainerDescriptorAction is not null)
			{
				writer.WritePropertyName(value._containedVariant);

				if (value._containedVariant == "match")
				{
					var descriptor = new MatchQueryDescriptor();
					((Action<MatchQueryDescriptor>)value.QueryContainerDescriptorAction).Invoke(descriptor);
					JsonSerializer.Serialize(writer, descriptor, options);
				}
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class QueryContainerDescriptor
	{
		private bool _containsVariant;
		internal string _containedVariant;

		internal QueryContainer ContainedVariant { get; private set; }
		internal object QueryContainerDescriptorAction { get; private set; }

		//public QueryContainerDescriptor QueryString(Action<BoolQueryDescriptor> configure)
		//{
		//	var descriptor = new BoolQueryDescriptor();
		//	configure?.Invoke(descriptor);
		//	return Assign(descriptor, (d, v) => d._boolQueryDescriptor = v);
		//}

		//internal QueryContainerDescriptor(Action<QueryContainerDescriptor> configure) => configure.Invoke(this);

		//public void Bool(Action<BoolQueryDescriptor> configure) => Set((Action<IQueryContainerVariantDescriptor>)configure, "bool");
		//{
			//if (_containsVariant)
			//	throw new Exception("TODO");

			//QueryContainerDescriptorAction = (Action<IQueryContainerVariantDescriptor>)configure;

			//_containedVariant = "bool";
			//_containsVariant = true;

			//if (configure is null)
			//	return new QueryContainer(new BoolQuery());

			//var descriptor = new BoolQueryDescriptor();
			//configure.Invoke(descriptor);
			//Assign(descriptor, (d, v) => d._variantDescriptor = v);

			//return ToQueryContainer();
		//}

		private void Set(object descriptorAction, string variantName)
		{
			if (_containsVariant)
				throw new Exception("TODO");

			QueryContainerDescriptorAction = descriptorAction;

			_containedVariant = variantName;
			_containsVariant = true;
		}

		public void Match(Action<MatchQueryDescriptor> configure) => Set(configure, "match");

		//internal QueryContainer ToQueryContainer()
		//{
		//	if (!_containsQuery)
		//		throw new Exception("TODO");

		//	if (ContainedVariant is not null)
		//		return ContainedVariant;

		//	if (_descriptorType == "bool")
		//	{
		//		var descriptor = new BoolQueryDescriptor();
		//		QueryContainerDescriptorAction.Invoke(descriptor);

		//	}

		//	ContainedVariant = _descriptorType switch
		//	{
		//		"bool" => new QueryContainer(variant.ToQuery()),
		//		MatchQueryDescriptor variant => new QueryContainer(variant.ToQuery()),
		//		_ => null,
		//	};

		//	return ContainedVariant;
		//}
	}
}


namespace Elastic.Clients.Elasticsearch
{
	public class MyRequest
	{
		public string? Something { get; set; }

		public MyRequest WrappedRequest { get; set; }

		public Thing Thing { get; set; }
	}

	public class Thing
	{
		public string Name { get; set; }
	}

	// Main downside is we always allocate a MyRequest per descriptor instead of relying on casting the descriptor to
	// the interface.
	public class MyRequestDescriptor : TestDescriptorBase<MyRequestDescriptor, MyRequest>
	{
		public MyRequestDescriptor() : base(new MyRequest()) { }

		public MyRequest Request => Target;

		public MyRequestDescriptor Something(string? something) => Assign(something, (r, v) => r.Something = v);

		public MyRequestDescriptor WrappedRequest(MyRequest request) => Assign(request, (r, v) => r.WrappedRequest = v);

		// Could be added to the partial class by hand at a later date if we then determine a descriptor is needed.
		public MyRequestDescriptor WrappedRequest(Action<MyRequestDescriptor> anotherOne)
		{
			var descriptor = new MyRequestDescriptor();
			anotherOne.Invoke(descriptor);
			Target.WrappedRequest = descriptor.Request;
			return this;
		}

		public MyRequestDescriptor Thing(Thing thing)
		{
			Target.Thing = thing;
			return this;
		}

		public MyRequestDescriptor Thing(Action<ThingDescriptor> thing)
		{
			var descriptor = new ThingDescriptor();
			thing.Invoke(descriptor);
			Target.Thing = descriptor.Thing;
			return this;
		}
	}

	public class ThingDescriptor : TestDescriptorBase<ThingDescriptor, Thing>
	{
		public ThingDescriptor() : base(new Thing()) { }

		public Thing Thing => Target;

		public ThingDescriptor Something(string name) => Assign(name, (r, v) => r.Name = v);

		// We could prefer no base class to optimise which avoids the extra field required for _self.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected ThingDescriptor LocalAssign<TValue>(TValue value, Action<Thing, TValue> assigner)
		{
			assigner(Target, value);
			return this;
		}
	}

	public abstract class TestDescriptorBase<TDescriptor, TTarget> where TDescriptor : TestDescriptorBase<TDescriptor, TTarget>
	{
		private readonly TDescriptor _self;

		protected TestDescriptorBase(TTarget target)
		{
			Target = target;
			_self = (TDescriptor)this;
		}

		protected TTarget Target { get; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected TDescriptor Assign<TValue>(TValue value, Action<TTarget, TValue> assigner)
		{
			assigner(Target, value);
			return _self;
		}
	}

	public class MyClient
	{
		public void DoRequest(IndexName index, Action<MyRequestDescriptor> selector)
		{
			var descriptor = new MyRequestDescriptor();
			selector.Invoke(descriptor);
			var request = descriptor.Request;

			// SEND IT
		}

		public void DoRequest(IndexName index, MyRequest request)
		{
			// SEND IT
		}

		public void DoRequest(IndexName index, MyRequestDescriptor descriptor)
		{
			var request = descriptor.Request;

			// SEND IT
		}
	}

	public class Testing
	{
		public void DoStuff()
		{
			var client = new MyClient();

			client.DoRequest("index", new MyRequest { Something = "Thing" });

			client.DoRequest("index", a => a
				.Something("MainSomething")
				.WrappedRequest(r => r.Something("AnotherSomething"))
				.Thing(t => t.Something("Name")));
		}
	}

	//public readonly partial struct PropertyName : IDictionaryKey
	//{
	//	public string Key => Value;
	//}

	//// This is an incomplete stub implementation and should really be a struct
	//public partial class Indices : IUrlParameter
	//{
	//	public static readonly Indices All = new("_all");

	//	internal Indices(IndexName index) => _indexNameList.Add(index);

	//	public Indices(IEnumerable<IndexName> indices)
	//	{
	//		indices.ThrowIfEmpty(nameof(indices));
	//		_indexNameList.AddRange(indices);
	//	}

	//	public Indices(IEnumerable<string> indices)
	//	{
	//		indices.ThrowIfEmpty(nameof(indices));
	//		_indexNameList.AddRange(indices.Select(s => (IndexName)s));
	//	}

	//	public IReadOnlyCollection<IndexName> Values => _indexNameList.ToArray();

	//	public static Indices Parse(string names) => names.IsNullOrEmptyCommaSeparatedList(out var list) ? null : new Indices(list);

	//	public static Indices Single(string index) => new Indices((IndexName)index);

	//	public static implicit operator Indices(string names) => Parse(names);

	//	string IUrlParameter.GetString(ITransportConfiguration settings)
	//	{
	//		if (settings is not IElasticsearchClientSettings elasticsearchClientSettings)
	//			throw new Exception(
	//				"Tried to pass index names on query sting but it could not be resolved because no Elastic.Clients.Elasticsearch settings are available.");

	//		var indices = _indexNameList.Select(i => i.GetString(settings)).Distinct();

	//		return string.Join(",", indices);
	//	}
	//}

	//public partial struct IndicesList : IUrlParameter
	//{
	//	//public static readonly IndicesList All = new("_all");

	//	private readonly List<IndexName> _indices = new();

	//	internal IndicesList(IndexName index) => _indices.Add(index);

	//	public IndicesList(IEnumerable<IndexName> indices)
	//	{
	//		indices.ThrowIfEmpty(nameof(indices));

	//		// De-duplicating during creation avoids cost when accessing the values.
	//		foreach (var index in indices)
	//			if (!_indices.Contains(index))
	//				_indices.Add(index);
	//	}

	//	public IndicesList(string[] indices)
	//	{
	//		indices.ThrowIfEmpty(nameof(indices));

	//		foreach (var index in indices)
	//			if (!_indices.Contains(index))
	//				_indices.Add(index);
	//	}

	//	public IReadOnlyCollection<IndexName> Values => _indices;

	//	public static IndicesList Parse(string names) => names.IsNullOrEmptyCommaSeparatedList(out var list) ? null : new IndicesList(list);

	//	public static implicit operator IndicesList(string names) => Parse(names);
	//}

	//public partial struct IndicesList { string IUrlParameter.GetString(ITransportConfiguration settings) => ""; }


	public abstract partial class PlainRequestBase<TParameters>
	{
		///<summary>Include the stack trace of returned errors.</summary>
		[JsonIgnore]
		public bool? ErrorTrace
		{
			get => Q<bool?>("error_trace");
			set => Q("error_trace", value);
		}

		/// <summary>
		///     A comma-separated list of filters used to reduce the response.
		///     <para>
		///         Use of response filtering can result in a response from Elasticsearch
		///         that cannot be correctly deserialized to the respective response type for the request.
		///         In such situations, use the low level client to issue the request and handle response deserialization.
		///     </para>
		/// </summary>
		[JsonIgnore]
		public string[] FilterPath
		{
			get => Q<string[]>("filter_path");
			set => Q("filter_path", value);
		}

		///<summary>Return human readable values for statistics.</summary>
		[JsonIgnore]
		public bool? Human
		{
			get => Q<bool?>("human");
			set => Q("human", value);
		}

		///<summary>Pretty format the returned JSON response.</summary>
		[JsonIgnore]
		public bool? Pretty
		{
			get => Q<bool?>("pretty");
			set => Q("pretty", value);
		}

		/// <summary>
		///     The URL-encoded request definition. Useful for libraries that do not accept a request body for non-POST
		///     requests.
		/// </summary>
		[JsonIgnore]
		public string SourceQueryString
		{
			get => Q<string>("source");
			set => Q("source", value);
		}
	}
}

namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	//public partial class Like
	//{

	//}

	//public partial class QueryContainerDescriptor : DescriptorBase<QueryContainerDescriptor, IQueryContainer>, IQueryContainer
	//{
	//	private QueryContainer WrapInContainer<TQuery, TQueryInterface>(
	//		Func<TQuery, TQueryInterface> create
	//	)
	//		where TQuery : class, TQueryInterface, new()
	//		where TQueryInterface : class
	//	{
	//		// Invoke the create delegate before assigning container; the create delegate
	//		// may mutate the current QueryContainerDescriptor<T> instance such that it
	//		// contains a query. See https://github.com/elastic/elasticsearch-net/issues/2875
	//		var query = create.InvokeOrDefault(new TQuery());

	//		if (query is not IQueryContainerVariant variant)
	//		{
	//			throw new Exception();
	//		}

	//		return variant.ToQueryContainer();

	//		//var container = ContainedQuery == null
	//		//	? this
	//		//	: new QueryContainerDescriptor();

	//		////c.IsVerbatim = query.IsVerbatim;
	//		////c.IsStrict = query.IsStrict;

	//		//assign(query, container);
	//		//container.ContainedQuery = query;

	//		//return container;

	//		//if query is writable (not conditionless or verbatim): return a container that holds the query
	//		//if (query.IsWritable)
	//		//	return container;

	//		//query is conditionless but marked as strict, throw exception
	//		//if (query.IsStrict)
	//		//	throw new ArgumentException("Query is conditionless but strict is turned on");

	//		//query is conditionless return an empty container that can later be rewritten
	//		//return null;
	//	}

	//	//[JsonIgnore]
	//	//internal IQuery ContainedQuery { get; set; }

	//	public QueryContainer QueryString(Func<QueryStringQueryDescriptor, IQueryStringQuery> selector) =>
	//		WrapInContainer(selector);
	//}

	//public partial class QueryStringQueryDescriptor : IQueryContainerVariant
	//{
	//	string IUnionVariant.VariantType => "query_string";

	//	public QueryContainer ToQueryContainer() => new(this);

	//	public QueryStringQueryDescriptor DefaultField(string field) => Assign(field, (a, v) => a.DefaultField = v);
	//}

	//public partial class DistanceFeatureQuery : IQueryContainerVariant
	//{
	//	public void WrapInContainer(IQueryContainer container) => throw new NotImplementedException();
	//}

	//public partial interface ISpanGapQuery : QueryDsl.IQueryContainerVariant, QueryDsl.ISpanQueryVariant
	//{
	//}

	//public partial class SpanGapQuery : Dictionary<string, int>, ISpanGapQuery
	//{
	//	public void WrapInContainer(IQueryContainer container) => throw new NotImplementedException();
	//	public void WrapInContainer(ISpanQuery container) => throw new NotImplementedException();
	//}

	//public partial class PinnedIdsQuery : IPinnedQueryVariant
	//{
	//	public IEnumerable<string> Ids { get; set; }

	//	public void WrapInContainer(IPinnedQuery container) => throw new NotImplementedException();
	//}

	//public partial class PinnedDocsQuery : IPinnedQueryVariant
	//{
	//	public IEnumerable<PinnedDoc> Docs { get; set; }

	//	public void WrapInContainer(IPinnedQuery container) => throw new NotImplementedException();
	//}
}

namespace Elastic.Clients.Elasticsearch
{
	// Stubs until we generate these - Allows the code to compile so we can identify real errors.

	public partial class HttpHeaders : Dictionary<string, Union<string, IReadOnlyCollection<string>>>
	{
	}

	public partial class Metadata : Dictionary<string, object>
	{
	}

	//public partial class RuntimeFields : Dictionary<Field, RuntimeField>
	//{
	//}

	public partial class ApplicationsPrivileges : Dictionary<Name, ResourcePrivileges>
	{
	}

	public partial class Privileges : Dictionary<string, bool>
	{
	}

	public partial class ResourcePrivileges : Dictionary<Name, Privileges>
	{
	}

	//// TODO: Dictionary Examples
	//public partial class IndexHealthStatsDictionary : Dictionary<IndexName, Cluster.Health.IndexHealthStats>
	//{
	//	public Cluster.Health.IndexHealthStats GetStats(IndexName indexName) => base[indexName];
	//}

	//public partial class IndexHealthStatsDictionaryV2
	//{
	//	private readonly Dictionary<IndexName, Cluster.Health.IndexHealthStats> _backingDictionary = new();

	//	public Cluster.Health.IndexHealthStats GetStats(IndexName indexName) => _backingDictionary[indexName];
	//}

	//public partial class Actions : Dictionary<IndexName, ActionStatus>
	//{
	//}

	// TODO: Implement properly
	[JsonConverter(typeof(UnionConverter<EpochMillis>))]
	public partial class EpochMillis
	{
		public EpochMillis() : base(1) { } // TODO: This is temp
	}

	// TODO: Implement properly
	[JsonConverter(typeof(PercentageConverter))]
	public partial class Percentage
	{
	}



	/// <summary>
	///     Block type for an index.
	/// </summary>
	//public readonly struct IndicesBlockOptions : IUrlParameter
	//{
	//	 TODO - This is currently generated as an enum by the code generator
	//	 ?? Should all enums be generated this way, or just those used in Url parameters

	//	private IndicesBlockOptions(string value) => Value = value;

	//	public string Value { get; }

	//	public string GetString(ITransportConfiguration settings) => Value;

	//	/ <summary>
	//	/     Disable metadata changes, such as closing the index.
	//	/ </summary>
	//	public static IndicesBlockOptions Metadata { get; } = new("metadata");

	//	/ <summary>
	//	/     Disable read operations.
	//	/ </summary>
	//	public static IndicesBlockOptions Read { get; } = new("read");

	//	/ <summary>
	//	/     Disable write operations and metadata changes.
	//	/ </summary>
	//	public static IndicesBlockOptions ReadOnly { get; } = new("read_only");

	//	/ <summary>
	//	/     Disable write operations. However, metadata changes are still allowed.
	//	/ </summary>
	//	public static IndicesBlockOptions Write { get; } = new("write");
	//}



	//public class Aggregate
	//{
	//}

	//public class Property
	//{
	//}

	namespace Global.Search
	{
		public class SortResults
		{
		}
	}




	//[JsonConverter(typeof(NumericAliasConverter<VersionNumber>))]
	//public class VersionNumber
	//{
	//	public VersionNumber(long value) => Value = value;

	//	internal long Value { get; }
	//}

	//public class Types : IUrlParameter
	//{
	//	public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	//}
}
