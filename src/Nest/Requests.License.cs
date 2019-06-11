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
using Elasticsearch.Net.Specification.LicenseApi;

// ReSharper disable RedundantBaseConstructorCall
// ReSharper disable UnusedTypeParameter
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
namespace Nest
{
	[InterfaceDataContract]
	public partial interface IDeleteLicenseRequest : IRequest<DeleteLicenseRequestParameters>
	{
	}

	///<summary>Request for Delete <pre>https://www.elastic.co/guide/en/x-pack/current/license-management.html</pre></summary>
	public partial class DeleteLicenseRequest : PlainRequestBase<DeleteLicenseRequestParameters>, IDeleteLicenseRequest
	{
		protected IDeleteLicenseRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseDelete;
	// values part of the url path
	// Request parameters
	}

	[InterfaceDataContract]
	public partial interface IGetLicenseRequest : IRequest<GetLicenseRequestParameters>
	{
	}

	///<summary>Request for Get <pre>https://www.elastic.co/guide/en/x-pack/current/license-management.html</pre></summary>
	public partial class GetLicenseRequest : PlainRequestBase<GetLicenseRequestParameters>, IGetLicenseRequest
	{
		protected IGetLicenseRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseGet;
		// values part of the url path
		// Request parameters
		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public bool? Local
		{
			get => Q<bool? >("local");
			set => Q("local", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IGetBasicLicenseStatusRequest : IRequest<GetBasicLicenseStatusRequestParameters>
	{
	}

	///<summary>Request for GetBasicStatus <pre>https://www.elastic.co/guide/en/x-pack/current/license-management.html</pre></summary>
	public partial class GetBasicLicenseStatusRequest : PlainRequestBase<GetBasicLicenseStatusRequestParameters>, IGetBasicLicenseStatusRequest
	{
		protected IGetBasicLicenseStatusRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseGetBasicStatus;
	// values part of the url path
	// Request parameters
	}

	[InterfaceDataContract]
	public partial interface IGetTrialLicenseStatusRequest : IRequest<GetTrialLicenseStatusRequestParameters>
	{
	}

	///<summary>Request for GetTrialStatus <pre>https://www.elastic.co/guide/en/x-pack/current/license-management.html</pre></summary>
	public partial class GetTrialLicenseStatusRequest : PlainRequestBase<GetTrialLicenseStatusRequestParameters>, IGetTrialLicenseStatusRequest
	{
		protected IGetTrialLicenseStatusRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseGetTrialStatus;
	// values part of the url path
	// Request parameters
	}

	[InterfaceDataContract]
	public partial interface IPostLicenseRequest : IRequest<PostLicenseRequestParameters>
	{
	}

	///<summary>Request for Post <pre>https://www.elastic.co/guide/en/x-pack/current/license-management.html</pre></summary>
	public partial class PostLicenseRequest : PlainRequestBase<PostLicenseRequestParameters>, IPostLicenseRequest
	{
		protected IPostLicenseRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicensePost;
		// values part of the url path
		// Request parameters
		///<summary>whether the user has acknowledged acknowledge messages (default: false)</summary>
		public bool? Acknowledge
		{
			get => Q<bool? >("acknowledge");
			set => Q("acknowledge", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IStartBasicLicenseRequest : IRequest<StartBasicLicenseRequestParameters>
	{
	}

	///<summary>Request for StartBasic <pre>https://www.elastic.co/guide/en/x-pack/current/license-management.html</pre></summary>
	public partial class StartBasicLicenseRequest : PlainRequestBase<StartBasicLicenseRequestParameters>, IStartBasicLicenseRequest
	{
		protected IStartBasicLicenseRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseStartBasic;
		// values part of the url path
		// Request parameters
		///<summary>whether the user has acknowledged acknowledge messages (default: false)</summary>
		public bool? Acknowledge
		{
			get => Q<bool? >("acknowledge");
			set => Q("acknowledge", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IStartTrialLicenseRequest : IRequest<StartTrialLicenseRequestParameters>
	{
	}

	///<summary>Request for StartTrial <pre>https://www.elastic.co/guide/en/x-pack/current/license-management.html</pre></summary>
	public partial class StartTrialLicenseRequest : PlainRequestBase<StartTrialLicenseRequestParameters>, IStartTrialLicenseRequest
	{
		protected IStartTrialLicenseRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.LicenseStartTrial;
		// values part of the url path
		// Request parameters
		///<summary>whether the user has acknowledged acknowledge messages (default: false)</summary>
		public bool? Acknowledge
		{
			get => Q<bool? >("acknowledge");
			set => Q("acknowledge", value);
		}

		///<summary>The type of trial license to generate (default: "trial")</summary>
		public string TypeQueryString
		{
			get => Q<string>("type");
			set => Q("type", value);
		}
	}
}