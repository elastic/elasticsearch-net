using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	//public class MyRequestWithFluent
	//{
	//	public string? Something { get; internal set; }
	//	public MyRequest WrappedRequest { get; internal set; }

	//	public MyRequestWithFluent WithSomething(string? something)
	//	{
	//		Something = something;
	//		return this;
	//	}

	//	public MyRequestWithFluent WithWrappedRequest(MyRequest request)
	//	{
	//		WrappedRequest = request;
	//		return this;
	//	}
	//}

	public class MyRequest
	{
		public string? Something { get; set; }

		public MyRequest WrappedRequest { get; set; }

		public Thing Thing { get; set; }
	}

	public class Thing
	{
		public string Name {  get; set; }	
	}

	public class ThingDescriptor
	{
		internal Thing Thing { get; } = new();

		public ThingDescriptor Name(string name)
		{
			Thing.Name = name;
			return this;
		}
	}

	public class MyRequestDescriptor
	{
		//// Could be lazily created - any value?
		//internal MyRequestDescriptor() => _self = new MyRequest();

		//[JsonIgnore]
		//private readonly MyRequest _self;

		internal MyRequest Request { get; } = new();

		//internal MyRequest GetRequest() => _self;

		public MyRequestDescriptor Something(string? something)
		{
			Request.Something = something;
			return this;
		}

		public MyRequestDescriptor WrappedRequest(MyRequest anotherOne)
		{
			Request.WrappedRequest = anotherOne;
			return this;
		}

		public MyRequestDescriptor WrappedRequest(Action<MyRequestDescriptor> anotherOne)
		{
			var descriptor = new MyRequestDescriptor();
			anotherOne.Invoke(descriptor);
			Request.WrappedRequest = descriptor.Request;
			return this;
		}

		public MyRequestDescriptor Thing(Thing thing)
		{
			Request.Thing = thing;
			return this;
		}

		public MyRequestDescriptor Thing(Action<ThingDescriptor> thing)
		{
			var descriptor = new ThingDescriptor();
			thing.Invoke(descriptor);
			Request.Thing = descriptor.Thing;
			return this;
		}

		//public static implicit operator TRequest(MyRequestDescriptor<TRequest> d) => _self;

		//[MethodImpl(MethodImplOptions.AggressiveInlining)]
		//protected MyRequestDescriptor Assign<TValue>(TValue value, Action<MyRequestDescriptor, TValue> assigner)
		//{
		//	assigner(this, value);
		//	return this;
		//}
	}

	public class MyClient
	{
		public void MakeRequest(IndexName index, Action<MyRequestDescriptor> selector)
		{
			var descriptor = new MyRequestDescriptor();
			selector.Invoke(descriptor);
			var request = descriptor.Request;

			// SEND IT
		}

		public void MakeRequest(IndexName index, MyRequest request)
		{
			// SEND IT
		}

		public void MakeRequest(IndexName index, MyRequestDescriptor descriptor)
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

			client.MakeRequest("index", new MyRequest { Something = "Thing" });

			client.MakeRequest("index", a => a
				.Something("MainSomething")
				.WrappedRequest(r => r.Something("AnotherSomething"))
				.Thing(t => t.Name("Name")));
		}
	}

	public readonly partial struct PropertyName : IDictionaryKey
	{
		public string Key => Value;
	}

	// This is an incomplete stub implementation and should really be a struct
	public partial class Indices : IUrlParameter
	{
		public static readonly Indices All = new("_all");

		internal Indices(IndexName index) => _indexNameList.Add(index);

		public Indices(IEnumerable<IndexName> indices)
		{
			indices.ThrowIfEmpty(nameof(indices));
			_indexNameList.AddRange(indices);
		}

		public Indices(IEnumerable<string> indices)
		{
			indices.ThrowIfEmpty(nameof(indices));
			_indexNameList.AddRange(indices.Select(s => (IndexName)s));
		}

		public IReadOnlyCollection<IndexName> Values => _indexNameList.ToArray();

		public static Indices Parse(string names) => names.IsNullOrEmptyCommaSeparatedList(out var list) ? null : new Indices(list);

		public static Indices Single(string index) => new Indices((IndexName)index);

		public static implicit operator Indices(string names) => Parse(names);

		string IUrlParameter.GetString(ITransportConfiguration settings)
		{
			if (settings is not IElasticsearchClientSettings elasticsearchClientSettings)
				throw new Exception(
					"Tried to pass index names on query sting but it could not be resolved because no Elastic.Clients.Elasticsearch settings are available.");

			var indices = _indexNameList.Select(i => i.GetString(settings)).Distinct();

			return string.Join(",", indices);
		}
	}

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

	// TODO: Dictionary Examples
	public partial class IndexHealthStatsDictionary : Dictionary<IndexName, Cluster.Health.IndexHealthStats>
	{
		public Cluster.Health.IndexHealthStats GetStats(IndexName indexName) => base[indexName];
	}

	public partial class IndexHealthStatsDictionaryV2
	{
		private readonly Dictionary<IndexName, Cluster.Health.IndexHealthStats> _backingDictionary = new();

		public Cluster.Health.IndexHealthStats GetStats(IndexName indexName) => _backingDictionary[indexName];
	}

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



	public class Aggregate
	{
	}

	public class Property
	{
	}

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
