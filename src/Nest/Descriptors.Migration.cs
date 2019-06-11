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
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Specification.MigrationApi;

// ReSharper disable RedundantBaseConstructorCall
// ReSharper disable UnusedTypeParameter
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
namespace Nest
{
	///<summary>descriptor for DeprecationInfo <pre>http://www.elastic.co/guide/en/migration/current/migration-api-deprecation.html</pre></summary>
	public partial class DeprecationInfoDescriptor : RequestDescriptorBase<DeprecationInfoDescriptor, DeprecationInfoRequestParameters, IDeprecationInfoRequest>, IDeprecationInfoRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.MigrationDeprecationInfo;
		///<summary>/_migration/deprecations</summary>
		public DeprecationInfoDescriptor(): base()
		{
		}

		///<summary>/{index}/_migration/deprecations</summary>
		///<param name = "index">Optional, accepts null</param>
		public DeprecationInfoDescriptor(IndexName index): base(r => r.Optional("index", index))
		{
		}

		// values part of the url path
		IndexName IDeprecationInfoRequest.Index => Self.RouteValues.Get<IndexName>("index");
		///<summary>Index pattern</summary>
		public DeprecationInfoDescriptor Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Optional("index", v));
		///<summary>a shortcut into calling Index(typeof(TOther))</summary>
		public DeprecationInfoDescriptor Index<TOther>()
			where TOther : class => Assign(typeof(TOther), (a, v) => a.RouteValues.Optional("index", (IndexName)v));
	// Request parameters
	}

	///<summary>descriptor for Assistance <pre>https://www.elastic.co/guide/en/elasticsearch/reference/current/migration-api-assistance.html</pre></summary>
	public partial class MigrationAssistanceDescriptor : RequestDescriptorBase<MigrationAssistanceDescriptor, MigrationAssistanceRequestParameters, IMigrationAssistanceRequest>, IMigrationAssistanceRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.MigrationAssistance;
		///<summary>/_migration/assistance</summary>
		public MigrationAssistanceDescriptor(): base()
		{
		}

		///<summary>/_migration/assistance/{index}</summary>
		///<param name = "index">Optional, accepts null</param>
		public MigrationAssistanceDescriptor(Indices index): base(r => r.Optional("index", index))
		{
		}

		// values part of the url path
		Indices IMigrationAssistanceRequest.Index => Self.RouteValues.Get<Indices>("index");
		///<summary>A comma-separated list of index names; use the special string `_all` or Indices.All to perform the operation on all indices</summary>
		public MigrationAssistanceDescriptor Index(Indices index) => Assign(index, (a, v) => a.RouteValues.Optional("index", v));
		///<summary>a shortcut into calling Index(typeof(TOther))</summary>
		public MigrationAssistanceDescriptor Index<TOther>()
			where TOther : class => Assign(typeof(TOther), (a, v) => a.RouteValues.Optional("index", (Indices)v));
		///<summary>A shortcut into calling Index(Indices.All)</summary>
		public MigrationAssistanceDescriptor AllIndices() => Index(Indices.All);
		// Request parameters
		///<summary>Whether to ignore if a wildcard indices expression resolves into no concrete indices. (This includes `_all` string or when no indices have been specified)</summary>
		public MigrationAssistanceDescriptor AllowNoIndices(bool? allownoindices = true) => Qs("allow_no_indices", allownoindices);
		///<summary>Whether to expand wildcard expression to concrete indices that are open, closed or both.</summary>
		public MigrationAssistanceDescriptor ExpandWildcards(ExpandWildcards? expandwildcards) => Qs("expand_wildcards", expandwildcards);
		///<summary>Whether specified concrete indices should be ignored when unavailable (missing or closed)</summary>
		public MigrationAssistanceDescriptor IgnoreUnavailable(bool? ignoreunavailable = true) => Qs("ignore_unavailable", ignoreunavailable);
	}

	///<summary>descriptor for Upgrade <pre>https://www.elastic.co/guide/en/elasticsearch/reference/current/migration-api-upgrade.html</pre></summary>
	public partial class MigrationUpgradeDescriptor : RequestDescriptorBase<MigrationUpgradeDescriptor, MigrationUpgradeRequestParameters, IMigrationUpgradeRequest>, IMigrationUpgradeRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.MigrationUpgrade;
		///<summary>/_migration/upgrade/{index}</summary>
		///<param name = "index">this parameter is required</param>
		public MigrationUpgradeDescriptor(IndexName index): base(r => r.Required("index", index))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected MigrationUpgradeDescriptor(): base()
		{
		}

		// values part of the url path
		IndexName IMigrationUpgradeRequest.Index => Self.RouteValues.Get<IndexName>("index");
		///<summary>The name of the index</summary>
		public MigrationUpgradeDescriptor Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));
		///<summary>a shortcut into calling Index(typeof(TOther))</summary>
		public MigrationUpgradeDescriptor Index<TOther>()
			where TOther : class => Assign(typeof(TOther), (a, v) => a.RouteValues.Required("index", (IndexName)v));
		// Request parameters
		///<summary>Should the request block until the upgrade operation is completed</summary>
		public MigrationUpgradeDescriptor WaitForCompletion(bool? waitforcompletion = true) => Qs("wait_for_completion", waitforcompletion);
	}
}