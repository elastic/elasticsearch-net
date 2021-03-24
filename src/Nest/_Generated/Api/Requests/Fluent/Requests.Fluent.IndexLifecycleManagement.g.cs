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
// Run the following in the root of the repository:
//
// TODO - RUN INSTRUCTIONS
//
// ------------------------------------------------

using Elastic.Transport;

#nullable restore
namespace Nest
{
	public class DeleteLifecycleDescriptor : RequestDescriptorBase<DeleteLifecycleDescriptor, DeleteLifecycleRequestParameters, IDeleteLifecycleRequest>, IDeleteLifecycleRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexLifecycleManagementDeleteLifecycle;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
	}

	public class ExplainLifecycleDescriptor : RequestDescriptorBase<ExplainLifecycleDescriptor, ExplainLifecycleRequestParameters, IExplainLifecycleRequest>, IExplainLifecycleRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexLifecycleManagementExplainLifecycle;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
	}

	public class GetLifecycleDescriptor : RequestDescriptorBase<GetLifecycleDescriptor, GetLifecycleRequestParameters, IGetLifecycleRequest>, IGetLifecycleRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexLifecycleManagementGetLifecycle;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
	}

	public class GetIlmStatusDescriptor : RequestDescriptorBase<GetIlmStatusDescriptor, GetIlmStatusRequestParameters, IGetIlmStatusRequest>, IGetIlmStatusRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexLifecycleManagementGetStatus;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
	}

	public class MoveToStepDescriptor : RequestDescriptorBase<MoveToStepDescriptor, MoveToStepRequestParameters, IMoveToStepRequest>, IMoveToStepRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexLifecycleManagementMoveToStep;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
	}

	public class PutLifecycleDescriptor : RequestDescriptorBase<PutLifecycleDescriptor, PutLifecycleRequestParameters, IPutLifecycleRequest>, IPutLifecycleRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexLifecycleManagementPutLifecycle;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
	}

	public class RemovePolicyDescriptor : RequestDescriptorBase<RemovePolicyDescriptor, RemovePolicyRequestParameters, IRemovePolicyRequest>, IRemovePolicyRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexLifecycleManagementRemovePolicy;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
	}

	public class RetryIlmDescriptor : RequestDescriptorBase<RetryIlmDescriptor, RetryIlmRequestParameters, IRetryIlmRequest>, IRetryIlmRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexLifecycleManagementRetry;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
	}

	public class StartIlmDescriptor : RequestDescriptorBase<StartIlmDescriptor, StartIlmRequestParameters, IStartIlmRequest>, IStartIlmRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexLifecycleManagementStart;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
	}

	public class StopIlmDescriptor : RequestDescriptorBase<StopIlmDescriptor, StopIlmRequestParameters, IStopIlmRequest>, IStopIlmRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexLifecycleManagementStop;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
	}
}