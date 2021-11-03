// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Analysis;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{

	public class DocType { }
}

namespace Elastic.Clients.Elasticsearch.Analysis
{
	// TODO: Generator should handle these

	public partial class ShingleTokenFilterDescriptor : ITokenFiltersVariant { }

	public class TokenFiltersDescriptor : IsADictionaryDescriptorBase<TokenFiltersDescriptor, TokenFilters, string, ITokenFiltersVariant>
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

	
}

namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public partial class IndexSettingsAnalysisDescriptor
	{
		internal TokenFilters _tokenFilters;

		public IndexSettingsAnalysisDescriptor TokenFilters(Func<TokenFiltersDescriptor, IPromise<TokenFilters>> selector) =>
			Assign(selector, (a, v) => _tokenFilters = v?.Invoke(new TokenFiltersDescriptor())?.Value);
	}
}

namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	//[JsonConverter(typeof(QueryContainerConverter))]
	//public partial class QueryContainer
	//{
	//}

	//public partial class QueryContainerConverter : JsonConverter<QueryContainer>
	//{
	//	public override QueryContainer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

	//	public override void Write(Utf8JsonWriter writer, QueryContainer value, JsonSerializerOptions options)
	//	{
	//		//var variant = value.ContainedVariant;

	//		//if (variant is null)
	//		//	writer.WriteNullValue();

	//		// TODO - Use serialiser from settings
	//		//JsonSerializer.Serialize(writer, variant, options);

	//		var container = value as QueryContainer; // This gives us access to the properties

	//		writer.WriteStartObject();

	//		if (container.Bool is not null)
	//		{
	//			writer.WritePropertyName("bool");

	//			// TODO - The options here are valid as they come for the initiating serialiser which is the DefaultRequestResponseSerialiser
	//			// Arguably, we want to have access to the transport settings to access the registered serialiser to use for this.
	//			// Considerations:
	//			//   a) Requires a converter factory approach which has costs on the CanConvert method for each factory which is attempted.
	//			//   b) The default serialiser for our types is known so we can be 'safe' in knowing we're using STJ.
	//			//   c) We know this converter would only get called by STJ internally, so if it's called, we're using STJ.
	//			JsonSerializer.Serialize(writer, container.Bool, options);
	//		}

	//		writer.WriteEndObject();
	//	}
	//}
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




	[JsonConverter(typeof(NumericAliasConverter<VersionNumber>))]
	public class VersionNumber
	{
		public VersionNumber(long value) => Value = value;

		internal long Value { get; }
	}

	//public class Types : IUrlParameter
	//{
	//	public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	//}
}
