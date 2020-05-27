using System.Collections.Generic;

namespace Nest
{
	// ReSharper disable UnusedTypeParameter
	public abstract partial class RequestDescriptorBase<TDescriptor, TParameters, TInterface>
	{
		///<summary>Include the stack trace of returned errors.</summary>
		public TDescriptor ErrorTrace(bool? errortrace = true) => Qs("error_trace", errortrace);
		///<summary>A comma-separated list of filters used to reduce the response.<para>Use of response filtering can result in a response from Elasticsearch that cannot be correctly deserialized to the respective response type for the request. In such situations, use the low level client to issue the request and handle response deserialization</para></summary>
		public TDescriptor FilterPath(params string[] filterpath) => Qs("filter_path", filterpath);
		///<summary>A comma-separated list of filters used to reduce the response.<para>Use of response filtering can result in a response from Elasticsearch that cannot be correctly deserialized to the respective response type for the request. In such situations, use the low level client to issue the request and handle response deserialization</para></summary>
		public TDescriptor FilterPath(IEnumerable<string> filterpath) => Qs("filter_path", filterpath);
		///<summary>Return human readable values for statistics.</summary>
		public TDescriptor Human(bool? human = true) => Qs("human", human);
		///<summary>Pretty format the returned JSON response.</summary>
		public TDescriptor Pretty(bool? pretty = true) => Qs("pretty", pretty);
		///<summary>The URL-encoded request definition. Useful for libraries that do not accept a request body for non-POST requests.</summary>
		public TDescriptor SourceQueryString(string sourcequerystring) => Qs("source", sourcequerystring);
	}
}