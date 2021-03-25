using System;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Nest
{
	// Stubs until we generate these - Allows the code to compile so we can identify real errors.

	public abstract partial class PlainRequestBase<TParameters>
	{
		///<summary>Include the stack trace of returned errors.</summary>
		[JsonIgnore]
		public bool? ErrorTrace
		{
			get => Q<bool?>("error_trace");
			set => Q("error_trace", value);
		}

		///<summary>
		/// A comma-separated list of filters used to reduce the response.
		/// <para>Use of response filtering can result in a response from Elasticsearch
		/// that cannot be correctly deserialized to the respective response type for the request.
		/// In such situations, use the low level client to issue the request and handle response deserialization.</para>
		///</summary>
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

		///<summary>The URL-encoded request definition. Useful for libraries that do not accept a request body for non-POST requests.</summary>
		[JsonIgnore]
		public string SourceQueryString
		{
			get => Q<string>("source");
			set => Q("source", value);
		}
	}

	public class Index { }

	public class RollupIndex { }

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

	public class ScrollId
	{
	}

	public class IndexAlias : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}


	public class DateString
	{
	}

	public class NodeId { }

	//public class Refresh { }

	public class WaitForActiveShards { }

	public class Types : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

	public class Alias : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

	public class CategoryId : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

	//// TODO: Recently removed from spec during validation
	//public enum TimeUnit
	//{
	//	a
	//}
}
