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
namespace Nest.Monitoring
{
	[ConvertAs(typeof(BulkRequest<>))]
	public partial interface IBulkRequest<TSource> : IRequest<BulkRequestParameters>
	{
	}

	public partial class BulkRequest<TSource> : PlainRequestBase<BulkRequestParameters>, IBulkRequest<TSource>
	{
		protected IBulkRequest<TSource> Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.MonitoringBulk;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => true;

		///<summary>/_monitoring/bulk</summary>
        public BulkRequest() : base()
		{
		}

		[JsonIgnore]
		public string SystemId { get => Q<string>("system_id"); set => Q("system_id", value); }

		[JsonIgnore]
		public string SystemApiVersion { get => Q<string>("system_api_version"); set => Q("system_api_version", value); }

		[JsonIgnore]
		public Nest.Time Interval { get => Q<Nest.Time>("interval"); set => Q("interval", value); }
	}
}