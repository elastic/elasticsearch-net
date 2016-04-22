using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<CountRequest>))]
	public partial interface ICountRequest 
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }
	}
	public partial interface ICountRequest<T> : ICountRequest
		where T :class
	{

	}

	public partial class CountRequest 
	{
		private CountRequestParameters QueryString => ((IRequest<CountRequestParameters>)this).RequestParameters;
		protected override HttpMethod HttpMethod =>
			this.QueryString.ContainsKey("_source") || this.QueryString.ContainsKey("q") ? HttpMethod.GET : HttpMethod.POST;

		public QueryContainer Query { get; set; }
	}

	public partial class CountRequest<T>
	{
		private CountRequestParameters QueryString => ((IRequest<CountRequestParameters>)this).RequestParameters;
		protected override HttpMethod HttpMethod =>
			this.QueryString.ContainsKey("_source") || this.QueryString.ContainsKey("q") ? HttpMethod.GET : HttpMethod.POST;

		public QueryContainer Query { get; set; }
	}

	[DescriptorFor("Count")]
	public partial class CountDescriptor<T> where T : class
	{
		private CountRequestParameters QueryString => ((IRequest<CountRequestParameters>)this).RequestParameters;
		protected override HttpMethod HttpMethod =>
			this.QueryString.ContainsKey("_source") || this.QueryString.ContainsKey("q") ? HttpMethod.GET : HttpMethod.POST;
		
		QueryContainer ICountRequest.Query { get; set; }

		public CountDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
