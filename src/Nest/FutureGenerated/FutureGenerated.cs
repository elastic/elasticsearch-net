using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Nest
{
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


namespace Nest
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

	//public partial class Actions : Dictionary<IndexName, ActionStatus>
	//{
	//}

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

		//public class Types : IUrlParameter
	//{
	//	public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	//}
}
