// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Specification.IngestApi;

// ReSharper disable RedundantBaseConstructorCall
// ReSharper disable UnusedTypeParameter
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
namespace Nest
{
	[InterfaceDataContract]
	public partial interface IDeletePipelineRequest : IRequest<DeletePipelineRequestParameters>
	{
		[IgnoreDataMember]
		Id Id
		{
			get;
		}
	}

	///<summary>Request for DeletePipeline <pre>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html</pre></summary>
	public partial class DeletePipelineRequest : PlainRequestBase<DeletePipelineRequestParameters>, IDeletePipelineRequest
	{
		protected IDeletePipelineRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestDeletePipeline;
		///<summary>/_ingest/pipeline/{id}</summary>
		///<param name = "id">this parameter is required</param>
		public DeletePipelineRequest(Id id): base(r => r.Required("id", id))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected DeletePipelineRequest(): base()
		{
		}

		// values part of the url path
		[IgnoreDataMember]
		Id IDeletePipelineRequest.Id => Self.RouteValues.Get<Id>("id");
		// Request parameters
		///<summary>Explicit operation timeout for connection to master node</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public Time Timeout
		{
			get => Q<Time>("timeout");
			set => Q("timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IGetPipelineRequest : IRequest<GetPipelineRequestParameters>
	{
		[IgnoreDataMember]
		Id Id
		{
			get;
		}
	}

	///<summary>Request for GetPipeline <pre>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html</pre></summary>
	public partial class GetPipelineRequest : PlainRequestBase<GetPipelineRequestParameters>, IGetPipelineRequest
	{
		protected IGetPipelineRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestGetPipeline;
		///<summary>/_ingest/pipeline</summary>
		public GetPipelineRequest(): base()
		{
		}

		///<summary>/_ingest/pipeline/{id}</summary>
		///<param name = "id">Optional, accepts null</param>
		public GetPipelineRequest(Id id): base(r => r.Optional("id", id))
		{
		}

		// values part of the url path
		[IgnoreDataMember]
		Id IGetPipelineRequest.Id => Self.RouteValues.Get<Id>("id");
		// Request parameters
		///<summary>Explicit operation timeout for connection to master node</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IGrokProcessorPatternsRequest : IRequest<GrokProcessorPatternsRequestParameters>
	{
	}

	///<summary>Request for GrokProcessorPatterns <pre>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html</pre></summary>
	public partial class GrokProcessorPatternsRequest : PlainRequestBase<GrokProcessorPatternsRequestParameters>, IGrokProcessorPatternsRequest
	{
		protected IGrokProcessorPatternsRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestGrokProcessorPatterns;
	// values part of the url path
	// Request parameters
	}

	[InterfaceDataContract]
	public partial interface IPutPipelineRequest : IRequest<PutPipelineRequestParameters>
	{
		[IgnoreDataMember]
		Id Id
		{
			get;
		}
	}

	///<summary>Request for PutPipeline <pre>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html</pre></summary>
	public partial class PutPipelineRequest : PlainRequestBase<PutPipelineRequestParameters>, IPutPipelineRequest
	{
		protected IPutPipelineRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestPutPipeline;
		///<summary>/_ingest/pipeline/{id}</summary>
		///<param name = "id">this parameter is required</param>
		public PutPipelineRequest(Id id): base(r => r.Required("id", id))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected PutPipelineRequest(): base()
		{
		}

		// values part of the url path
		[IgnoreDataMember]
		Id IPutPipelineRequest.Id => Self.RouteValues.Get<Id>("id");
		// Request parameters
		///<summary>Explicit operation timeout for connection to master node</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public Time Timeout
		{
			get => Q<Time>("timeout");
			set => Q("timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface ISimulatePipelineRequest : IRequest<SimulatePipelineRequestParameters>
	{
		[IgnoreDataMember]
		Id Id
		{
			get;
		}
	}

	///<summary>Request for SimulatePipeline <pre>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html</pre></summary>
	public partial class SimulatePipelineRequest : PlainRequestBase<SimulatePipelineRequestParameters>, ISimulatePipelineRequest
	{
		protected ISimulatePipelineRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestSimulatePipeline;
		///<summary>/_ingest/pipeline/_simulate</summary>
		public SimulatePipelineRequest(): base()
		{
		}

		///<summary>/_ingest/pipeline/{id}/_simulate</summary>
		///<param name = "id">Optional, accepts null</param>
		public SimulatePipelineRequest(Id id): base(r => r.Optional("id", id))
		{
		}

		// values part of the url path
		[IgnoreDataMember]
		Id ISimulatePipelineRequest.Id => Self.RouteValues.Get<Id>("id");
		// Request parameters
		///<summary>Verbose mode. Display data output for each processor in executed pipeline</summary>
		public bool? Verbose
		{
			get => Q<bool? >("verbose");
			set => Q("verbose", value);
		}
	}
}