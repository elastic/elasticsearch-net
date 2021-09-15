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
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.License
{
	[ConvertAs(typeof(DeleteRequest))]
	public partial interface IDeleteRequest : IRequest<DeleteRequestParameters>
	{
	}

	public partial class DeleteRequest : PlainRequestBase<DeleteRequestParameters>, IDeleteRequest
	{
		protected IDeleteRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseDelete;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => true;

		///<summary>/_license</summary>
        public DeleteRequest() : base()
		{
		}
	}

	[ConvertAs(typeof(GetRequest))]
	public partial interface IGetRequest : IRequest<GetRequestParameters>
	{
	}

	public partial class GetRequest : PlainRequestBase<GetRequestParameters>, IGetRequest
	{
		protected IGetRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseGet;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => true;

		///<summary>/_license</summary>
        public GetRequest() : base()
		{
		}

		[JsonIgnore]
		public bool? AcceptEnterprise { get => Q<bool?>("accept_enterprise"); set => Q("accept_enterprise", value); }

		[JsonIgnore]
		public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }
	}

	[ConvertAs(typeof(GetBasicStatusRequest))]
	public partial interface IGetBasicStatusRequest : IRequest<GetBasicStatusRequestParameters>
	{
	}

	public partial class GetBasicStatusRequest : PlainRequestBase<GetBasicStatusRequestParameters>, IGetBasicStatusRequest
	{
		protected IGetBasicStatusRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseGetBasicStatus;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => true;

		///<summary>/_license/basic_status</summary>
        public GetBasicStatusRequest() : base()
		{
		}
	}

	[ConvertAs(typeof(GetTrialStatusRequest))]
	public partial interface IGetTrialStatusRequest : IRequest<GetTrialStatusRequestParameters>
	{
	}

	public partial class GetTrialStatusRequest : PlainRequestBase<GetTrialStatusRequestParameters>, IGetTrialStatusRequest
	{
		protected IGetTrialStatusRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseGetTrialStatus;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => true;

		///<summary>/_license/trial_status</summary>
        public GetTrialStatusRequest() : base()
		{
		}
	}

	[ConvertAs(typeof(PostRequest))]
	public partial interface IPostRequest : IRequest<PostRequestParameters>
	{
		Elastic.Clients.Elasticsearch.License.ILicense? License { get; set; }

		IEnumerable<Elastic.Clients.Elasticsearch.License.ILicense>? Licenses { get; set; }
	}

	public partial class PostRequest : PlainRequestBase<PostRequestParameters>, IPostRequest
	{
		protected IPostRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicensePost;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => License is null && Licenses is null;

		///<summary>/_license</summary>
        public PostRequest() : base()
		{
		}

		[JsonIgnore]
		public bool? Acknowledge { get => Q<bool?>("acknowledge"); set => Q("acknowledge", value); }

		[JsonInclude]
		[JsonPropertyName("license")]
		public Elastic.Clients.Elasticsearch.License.ILicense? License { get; set; }

		[JsonInclude]
		[JsonPropertyName("licenses")]
		public IEnumerable<Elastic.Clients.Elasticsearch.License.ILicense>? Licenses { get; set; }
	}

	[ConvertAs(typeof(PostStartBasicRequest))]
	public partial interface IPostStartBasicRequest : IRequest<PostStartBasicRequestParameters>
	{
	}

	public partial class PostStartBasicRequest : PlainRequestBase<PostStartBasicRequestParameters>, IPostStartBasicRequest
	{
		protected IPostStartBasicRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicensePostStartBasic;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => true;

		///<summary>/_license/start_basic</summary>
        public PostStartBasicRequest() : base()
		{
		}

		[JsonIgnore]
		public bool? Acknowledge { get => Q<bool?>("acknowledge"); set => Q("acknowledge", value); }
	}

	[ConvertAs(typeof(PostStartTrialRequest))]
	public partial interface IPostStartTrialRequest : IRequest<PostStartTrialRequestParameters>
	{
	}

	public partial class PostStartTrialRequest : PlainRequestBase<PostStartTrialRequestParameters>, IPostStartTrialRequest
	{
		protected IPostStartTrialRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicensePostStartTrial;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => true;

		///<summary>/_license/start_trial</summary>
        public PostStartTrialRequest() : base()
		{
		}

		[JsonIgnore]
		public bool? Acknowledge { get => Q<bool?>("acknowledge"); set => Q("acknowledge", value); }

		[JsonIgnore]
		public string? TypeQueryString { get => Q<string?>("type_query_string"); set => Q("type_query_string", value); }
	}
}