using System;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Nest
{
	// Stubs until we generate these - Allows the code to compile so we can identify real errors.

	// TODO: Implement properly
	[JsonConverter(typeof(UnionConverter<EpochMillis>))]
	public partial class EpochMillis
	{
		public EpochMillis(string value) { }

		public EpochMillis(int value) { }
	}

	// TODO: Implement properly
	[JsonConverter(typeof(PercentageConverter))]
	public partial class Percentage
	{
		public Percentage(string value) { }

		public Percentage(float value) { }
	}

	public class NodeRoles
	{
	}

	// TODO - Implement array type aliases
	// TODO - Consider OneOrMany implementation to only allocate a list when more than one item
	public class StopWords
	{
	}

	public class GeoTilePrecision
	{
	}

	public class GeoHashPrecision
	{
	}


	public class Aggregate
	{
	}


	public class Property
	{
	}

	public class SortResults
	{
	}


	public class SuggestOption<TDocument>
	{
	}

	[JsonConverter(typeof(NumericAliasConverter<SequenceNumber>))]
	public class SequenceNumber
	{
		public SequenceNumber(long value) => Value = value;

		internal long Value { get; }
	}


	[JsonConverter(typeof(NumericAliasConverter<VersionNumber>))]
	public class VersionNumber
	{
		public VersionNumber(long value) => Value = value;

		internal long Value { get; }
	}

	public partial struct DataStreamName : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

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

	public class Index
	{
	}

	public class RollupIndex
	{
	}

	public class JobId : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

	public class Metrics : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

	public class ExpandWildcards
	{
	}

	//public class ScrollId
	//{
	//}

	public partial struct IndexAlias : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}


	//public class Refresh { }

	//public class WaitForActiveShards { }

	public class Types : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}


	public partial struct CategoryId : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}


	//	public partial class ErrorCause
	//	{
	//		[JsonPropertyName("bytes_limit")]
	//		public long? BytesLimit
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("bytes_wanted")]
	//		public long? BytesWanted
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("caused_by")]
	//		public ErrorCause? CausedBy
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("col")]
	//		public int? Col
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("column")]
	//		public int? Column
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("failed_shards")]
	//		public IReadOnlyCollection<ShardFailure>? FailedShards
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("grouped")]
	//		public bool? Grouped
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("header")]
	//		public HttpHeaders? Header
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("index")]
	//		public IndexName? Index
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("index_uuid")]
	//		public Uuid? IndexUuid
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("lang")]
	//		public string? Lang
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("language")]
	//		public string? Language
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("licensed_expired_feature")]
	//		public string? LicensedExpiredFeature
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("line")]
	//		public int? Line
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("max_buckets")]
	//		public int? MaxBuckets
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("phase")]
	//		public string? Phase
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("position")]
	//		public PainlessExecutionPosition? Position
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("processor_type")]
	//		public string? ProcessorType
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("property_name")]
	//		public string? PropertyName
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("reason")]
	//		public string Reason
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("resource_id")]
	//		public Ids? ResourceId
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("resource_type")]
	//		public string? ResourceType
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("root_cause")]
	//		public IReadOnlyCollection<ErrorCause>? RootCause
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("script")]
	//		public string? Script
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("script_stack")]
	//		public IReadOnlyCollection<string>? ScriptStack
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("shard")]
	//		public Union<int, string>? Shard
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("stack_trace")]
	//		public string? StackTrace
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}

	//		[JsonPropertyName("type")]
	//		public string Type
	//		{
	//			get;
	//#if NET5_0
	//			init;
	//#else
	//			internal set;
	//#endif
	//		}
	//	}
}
