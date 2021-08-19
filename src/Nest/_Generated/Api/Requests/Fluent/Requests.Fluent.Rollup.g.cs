// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using Elastic.Transport;

#nullable restore
namespace Nest.Rollup
{
	public partial class DeleteJobDescriptor : RequestDescriptorBase<DeleteJobDescriptor, DeleteJobRequestParameters, IDeleteJobRequest>, IDeleteJobRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.RollupDeleteJob;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		///<summary>/_rollup/job/{id}</summary>
        public DeleteJobDescriptor(Nest.Id id) : base(r => r.Required("id", id))
		{
		}
	}

	public partial class GetJobsDescriptor : RequestDescriptorBase<GetJobsDescriptor, GetJobsRequestParameters, IGetJobsRequest>, IGetJobsRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.RollupGetJobs;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/_rollup/job/{id}</summary>
        public GetJobsDescriptor(Nest.Id? id) : base(r => r.Optional("id", id))
		{
		}

		///<summary>/_rollup/job/</summary>
        public GetJobsDescriptor() : base()
		{
		}
	}

	public partial class GetRollupCapsDescriptor : RequestDescriptorBase<GetRollupCapsDescriptor, GetRollupCapsRequestParameters, IGetRollupCapsRequest>, IGetRollupCapsRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.RollupGetRollupCaps;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/_rollup/data/{id}</summary>
        public GetRollupCapsDescriptor(Nest.Id? id) : base(r => r.Optional("id", id))
		{
		}

		///<summary>/_rollup/data/</summary>
        public GetRollupCapsDescriptor() : base()
		{
		}
	}

	public partial class GetRollupIndexCapsDescriptor : RequestDescriptorBase<GetRollupIndexCapsDescriptor, GetRollupIndexCapsRequestParameters, IGetRollupIndexCapsRequest>, IGetRollupIndexCapsRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.RollupGetRollupIndexCaps;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/{index}/_rollup/data</summary>
        public GetRollupIndexCapsDescriptor(Nest.Id index) : base(r => r.Required("index", index))
		{
		}
	}

	public partial class PutJobDescriptor : RequestDescriptorBase<PutJobDescriptor, PutJobRequestParameters, IPutJobRequest>, IPutJobRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.RollupPutJob;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		///<summary>/_rollup/job/{id}</summary>
        public PutJobDescriptor(Nest.Id id) : base(r => r.Required("id", id))
		{
		}
	}

	public partial class RollupDescriptor : RequestDescriptorBase<RollupDescriptor, RollupRequestParameters, IRollupRequest>, IRollupRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.RollupRollup;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
	}

	public partial class RollupSearchDescriptor : RequestDescriptorBase<RollupSearchDescriptor, RollupSearchRequestParameters, IRollupSearchRequest>, IRollupSearchRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.RollupRollupSearch;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		///<summary>/{index}/_rollup_search</summary>
        public RollupSearchDescriptor(Nest.Indices index) : base(r => r.Required("index", index))
		{
		}
	}

	public partial class StartJobDescriptor : RequestDescriptorBase<StartJobDescriptor, StartJobRequestParameters, IStartJobRequest>, IStartJobRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.RollupStartJob;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		///<summary>/_rollup/job/{id}/_start</summary>
        public StartJobDescriptor(Nest.Id id) : base(r => r.Required("id", id))
		{
		}
	}

	public partial class StopJobDescriptor : RequestDescriptorBase<StopJobDescriptor, StopJobRequestParameters, IStopJobRequest>, IStopJobRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.RollupStopJob;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		///<summary>/_rollup/job/{id}/_stop</summary>
        public StopJobDescriptor(Nest.Id id) : base(r => r.Required("id", id))
		{
		}
	}
}