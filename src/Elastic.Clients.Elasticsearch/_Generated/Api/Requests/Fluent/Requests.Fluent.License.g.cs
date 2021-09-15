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
using System.Collections.Generic;

#nullable restore
namespace Elastic.Clients.Elasticsearch.License
{
	public partial class DeleteDescriptor : RequestDescriptorBase<DeleteDescriptor, DeleteRequestParameters, IDeleteRequest>, IDeleteRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseDelete;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		///<summary>/_license</summary>
        public DeleteDescriptor() : base()
		{
		}
	}

	public partial class GetDescriptor : RequestDescriptorBase<GetDescriptor, GetRequestParameters, IGetRequest>, IGetRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseGet;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/_license</summary>
        public GetDescriptor() : base()
		{
		}

		public GetDescriptor AcceptEnterprise(bool? acceptEnterprise = true) => Qs("accept_enterprise", acceptEnterprise);
		public GetDescriptor Local(bool? local = true) => Qs("local", local);
	}

	public partial class GetBasicStatusDescriptor : RequestDescriptorBase<GetBasicStatusDescriptor, GetBasicStatusRequestParameters, IGetBasicStatusRequest>, IGetBasicStatusRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseGetBasicStatus;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/_license/basic_status</summary>
        public GetBasicStatusDescriptor() : base()
		{
		}
	}

	public partial class GetTrialStatusDescriptor : RequestDescriptorBase<GetTrialStatusDescriptor, GetTrialStatusRequestParameters, IGetTrialStatusRequest>, IGetTrialStatusRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseGetTrialStatus;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/_license/trial_status</summary>
        public GetTrialStatusDescriptor() : base()
		{
		}
	}

	public partial class PostDescriptor : RequestDescriptorBase<PostDescriptor, PostRequestParameters, IPostRequest>, IPostRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicensePost;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		///<summary>/_license</summary>
        public PostDescriptor() : base()
		{
		}

		Elastic.Clients.Elasticsearch.License.ILicense? IPostRequest.License { get; set; }

		IEnumerable<Elastic.Clients.Elasticsearch.License.ILicense>? IPostRequest.Licenses { get; set; }

		public PostDescriptor Acknowledge(bool? acknowledge = true) => Qs("acknowledge", acknowledge);
		public PostDescriptor License(Elastic.Clients.Elasticsearch.License.ILicense? license) => Assign(license, (a, v) => a.License = v);
		public PostDescriptor Licenses(IEnumerable<Elastic.Clients.Elasticsearch.License.ILicense>? licenses) => Assign(licenses, (a, v) => a.Licenses = v);
	}

	public partial class PostStartBasicDescriptor : RequestDescriptorBase<PostStartBasicDescriptor, PostStartBasicRequestParameters, IPostStartBasicRequest>, IPostStartBasicRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicensePostStartBasic;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		///<summary>/_license/start_basic</summary>
        public PostStartBasicDescriptor() : base()
		{
		}

		public PostStartBasicDescriptor Acknowledge(bool? acknowledge = true) => Qs("acknowledge", acknowledge);
	}

	public partial class PostStartTrialDescriptor : RequestDescriptorBase<PostStartTrialDescriptor, PostStartTrialRequestParameters, IPostStartTrialRequest>, IPostStartTrialRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicensePostStartTrial;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		///<summary>/_license/start_trial</summary>
        public PostStartTrialDescriptor() : base()
		{
		}

		public PostStartTrialDescriptor Acknowledge(bool? acknowledge = true) => Qs("acknowledge", acknowledge);
		public PostStartTrialDescriptor TypeQueryString(string? typeQueryString) => Qs("type_query_string", typeQueryString);
	}
}