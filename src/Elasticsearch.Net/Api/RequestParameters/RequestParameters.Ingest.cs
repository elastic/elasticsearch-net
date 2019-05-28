using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Elasticsearch.Net.Specification.IngestApi
{
	///<summary>Request options for DeletePipeline<pre>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html</pre></summary>
	public class DeletePipelineRequestParameters : RequestParameters<DeletePipelineRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.DELETE;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}
	}

	///<summary>Request options for GetPipeline<pre>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html</pre></summary>
	public class GetPipelineRequestParameters : RequestParameters<GetPipelineRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}
	}

	///<summary>Request options for GrokProcessorPatterns<pre>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html</pre></summary>
	public class GrokProcessorPatternsRequestParameters : RequestParameters<GrokProcessorPatternsRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
	}

	///<summary>Request options for PutPipeline<pre>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html</pre></summary>
	public class PutPipelineRequestParameters : RequestParameters<PutPipelineRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.PUT;
		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}
	}

	///<summary>Request options for SimulatePipeline<pre>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html</pre></summary>
	public class SimulatePipelineRequestParameters : RequestParameters<SimulatePipelineRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		///<summary>Verbose mode. Display data output for each processor in executed pipeline</summary>
		public bool? Verbose
		{
			get => Q<bool? >("verbose");
			set => Q("verbose", value);
		}
	}
}