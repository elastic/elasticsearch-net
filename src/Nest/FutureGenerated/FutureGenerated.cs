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

	public class Metrics : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

	public class ExpandWildcards
	{
	}

	public class Types : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}
}
