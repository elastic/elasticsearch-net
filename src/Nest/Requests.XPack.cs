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
using Elasticsearch.Net.Specification.XPackApi;

// ReSharper disable RedundantBaseConstructorCall
// ReSharper disable UnusedTypeParameter
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
namespace Nest
{
	[InterfaceDataContract]
	public partial interface IXPackInfoRequest : IRequest<XPackInfoRequestParameters>
	{
	}

	///<summary>Request for Info <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/info-api.html</para></summary>
	public partial class XPackInfoRequest : PlainRequestBase<XPackInfoRequestParameters>, IXPackInfoRequest
	{
		protected IXPackInfoRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.XPackInfo;
		// values part of the url path
		// Request parameters
		///<summary>If this param is used it must be set to true</summary>
		[Obsolete("Scheduled to be removed in 8.0, Deprecated as of: 8.0.0, reason: Supported for backwards compatibility with 7.x")]
		public bool? AcceptEnterprise
		{
			get => Q<bool? >("accept_enterprise");
			set => Q("accept_enterprise", value);
		}

		///<summary>Comma-separated list of info categories. Can be any of: build, license, features</summary>
		public string[] Categories
		{
			get => Q<string[]>("categories");
			set => Q("categories", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IXPackUsageRequest : IRequest<XPackUsageRequestParameters>
	{
	}

	///<summary>Request for Usage <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/usage-api.html</para></summary>
	public partial class XPackUsageRequest : PlainRequestBase<XPackUsageRequestParameters>, IXPackUsageRequest
	{
		protected IXPackUsageRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.XPackUsage;
		// values part of the url path
		// Request parameters
		///<summary>Specify timeout for watch write operation</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}
	}
}