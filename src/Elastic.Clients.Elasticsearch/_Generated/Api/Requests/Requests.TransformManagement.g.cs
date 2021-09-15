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
namespace Elastic.Clients.Elasticsearch.TransformManagement
{
	[ConvertAs(typeof(DeleteTransformRequest))]
	public partial interface IDeleteTransformRequest : IRequest<DeleteTransformRequestParameters>
	{
	}

	public partial class DeleteTransformRequest : PlainRequestBase<DeleteTransformRequestParameters>, IDeleteTransformRequest
	{
		protected IDeleteTransformRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.TransformManagementDeleteTransform;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => false;
		protected override bool IsEmpty => false;

		///<summary>/_transform/{transform_id}</summary>
        public DeleteTransformRequest(Elastic.Clients.Elasticsearch.Name transform_id) : base(r => r.Required("transform_id", transform_id))
		{
		}

		[JsonIgnore]
		public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }
	}

	[ConvertAs(typeof(GetTransformRequest))]
	public partial interface IGetTransformRequest : IRequest<GetTransformRequestParameters>
	{
	}

	public partial class GetTransformRequest : PlainRequestBase<GetTransformRequestParameters>, IGetTransformRequest
	{
		protected IGetTransformRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.TransformManagementGetTransform;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => true;

		///<summary>/_transform/{transform_id}</summary>
        public GetTransformRequest(Elastic.Clients.Elasticsearch.Name? transform_id) : base(r => r.Optional("transform_id", transform_id))
		{
		}

		///<summary>/_transform</summary>
        public GetTransformRequest() : base()
		{
		}

		[JsonIgnore]
		public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

		[JsonIgnore]
		public int? From { get => Q<int?>("from"); set => Q("from", value); }

		[JsonIgnore]
		public int? Size { get => Q<int?>("size"); set => Q("size", value); }

		[JsonIgnore]
		public bool? ExcludeGenerated { get => Q<bool?>("exclude_generated"); set => Q("exclude_generated", value); }
	}

	[ConvertAs(typeof(GetTransformStatsRequest))]
	public partial interface IGetTransformStatsRequest : IRequest<GetTransformStatsRequestParameters>
	{
	}

	public partial class GetTransformStatsRequest : PlainRequestBase<GetTransformStatsRequestParameters>, IGetTransformStatsRequest
	{
		protected IGetTransformStatsRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.TransformManagementGetTransformStats;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => false;
		protected override bool IsEmpty => false;

		///<summary>/_transform/{transform_id}/_stats</summary>
        public GetTransformStatsRequest(Elastic.Clients.Elasticsearch.Name transform_id) : base(r => r.Required("transform_id", transform_id))
		{
		}

		[JsonIgnore]
		public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

		[JsonIgnore]
		public long? From { get => Q<long?>("from"); set => Q("from", value); }

		[JsonIgnore]
		public long? Size { get => Q<long?>("size"); set => Q("size", value); }
	}

	[ConvertAs(typeof(PreviewTransformRequest))]
	public partial interface IPreviewTransformRequest : IRequest<PreviewTransformRequestParameters>
	{
		Elastic.Clients.Elasticsearch.Global.Reindex.IDestination? Dest { get; set; }

		string? Description { get; set; }

		Elastic.Clients.Elasticsearch.Time? Frequency { get; set; }

		Elastic.Clients.Elasticsearch.TransformManagement.IPivot? Pivot { get; set; }

		Elastic.Clients.Elasticsearch.Global.Reindex.ISource? Source { get; set; }

		Elastic.Clients.Elasticsearch.TransformManagement.ISettings? Settings { get; set; }

		Elastic.Clients.Elasticsearch.TransformManagement.SyncContainer? Sync { get; set; }

		Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyContainer? RetentionPolicy { get; set; }

		Elastic.Clients.Elasticsearch.TransformManagement.ILatest? Latest { get; set; }
	}

	public partial class PreviewTransformRequest : PlainRequestBase<PreviewTransformRequestParameters>, IPreviewTransformRequest
	{
		protected IPreviewTransformRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.TransformManagementPreviewTransform;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => Dest is null && Description is null && Frequency is null && Pivot is null && Source is null && Settings is null && Sync is null && RetentionPolicy is null && Latest is null;

		///<summary>/_transform/{transform_id}/_preview</summary>
        public PreviewTransformRequest(Elastic.Clients.Elasticsearch.Id? transform_id) : base(r => r.Optional("transform_id", transform_id))
		{
		}

		///<summary>/_transform/_preview</summary>
        public PreviewTransformRequest() : base()
		{
		}

		[JsonInclude]
		[JsonPropertyName("dest")]
		public Elastic.Clients.Elasticsearch.Global.Reindex.IDestination? Dest { get; set; }

		[JsonInclude]
		[JsonPropertyName("description")]
		public string? Description { get; set; }

		[JsonInclude]
		[JsonPropertyName("frequency")]
		public Elastic.Clients.Elasticsearch.Time? Frequency { get; set; }

		[JsonInclude]
		[JsonPropertyName("pivot")]
		public Elastic.Clients.Elasticsearch.TransformManagement.IPivot? Pivot { get; set; }

		[JsonInclude]
		[JsonPropertyName("source")]
		public Elastic.Clients.Elasticsearch.Global.Reindex.ISource? Source { get; set; }

		[JsonInclude]
		[JsonPropertyName("settings")]
		public Elastic.Clients.Elasticsearch.TransformManagement.ISettings? Settings { get; set; }

		[JsonInclude]
		[JsonPropertyName("sync")]
		public Elastic.Clients.Elasticsearch.TransformManagement.SyncContainer? Sync { get; set; }

		[JsonInclude]
		[JsonPropertyName("retention_policy")]
		public Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyContainer? RetentionPolicy { get; set; }

		[JsonInclude]
		[JsonPropertyName("latest")]
		public Elastic.Clients.Elasticsearch.TransformManagement.ILatest? Latest { get; set; }
	}

	[ConvertAs(typeof(PutTransformRequest))]
	public partial interface IPutTransformRequest : IRequest<PutTransformRequestParameters>
	{
	}

	public partial class PutTransformRequest : PlainRequestBase<PutTransformRequestParameters>, IPutTransformRequest
	{
		protected IPutTransformRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.TransformManagementPutTransform;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => false;
		protected override bool IsEmpty => false;

		///<summary>/_transform/{transform_id}</summary>
        public PutTransformRequest(Elastic.Clients.Elasticsearch.Id transform_id) : base(r => r.Required("transform_id", transform_id))
		{
		}

		[JsonIgnore]
		public bool? DeferValidation { get => Q<bool?>("defer_validation"); set => Q("defer_validation", value); }

		[JsonInclude]
		[JsonPropertyName("dest")]
		public Elastic.Clients.Elasticsearch.Global.Reindex.IDestination? Dest
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("description")]
		public string? Description
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("frequency")]
		public Elastic.Clients.Elasticsearch.Time? Frequency
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("pivot")]
		public Elastic.Clients.Elasticsearch.TransformManagement.IPivot? Pivot
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("source")]
		public Elastic.Clients.Elasticsearch.Global.Reindex.ISource? Source
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("settings")]
		public Elastic.Clients.Elasticsearch.TransformManagement.ISettings? Settings
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("sync")]
		public Elastic.Clients.Elasticsearch.TransformManagement.SyncContainer? Sync
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("retention_policy")]
		public Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyContainer? RetentionPolicy
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("latest")]
		public Elastic.Clients.Elasticsearch.TransformManagement.ILatest? Latest
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	[ConvertAs(typeof(StartTransformRequest))]
	public partial interface IStartTransformRequest : IRequest<StartTransformRequestParameters>
	{
	}

	public partial class StartTransformRequest : PlainRequestBase<StartTransformRequestParameters>, IStartTransformRequest
	{
		protected IStartTransformRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.TransformManagementStartTransform;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => false;
		protected override bool IsEmpty => false;

		///<summary>/_transform/{transform_id}/_start</summary>
        public StartTransformRequest(Elastic.Clients.Elasticsearch.Name transform_id) : base(r => r.Required("transform_id", transform_id))
		{
		}

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }
	}

	[ConvertAs(typeof(StopTransformRequest))]
	public partial interface IStopTransformRequest : IRequest<StopTransformRequestParameters>
	{
	}

	public partial class StopTransformRequest : PlainRequestBase<StopTransformRequestParameters>, IStopTransformRequest
	{
		protected IStopTransformRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.TransformManagementStopTransform;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => false;
		protected override bool IsEmpty => false;

		///<summary>/_transform/{transform_id}/_stop</summary>
        public StopTransformRequest(Elastic.Clients.Elasticsearch.Name transform_id) : base(r => r.Required("transform_id", transform_id))
		{
		}

		[JsonIgnore]
		public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

		[JsonIgnore]
		public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public bool? WaitForCheckpoint { get => Q<bool?>("wait_for_checkpoint"); set => Q("wait_for_checkpoint", value); }

		[JsonIgnore]
		public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
	}

	[ConvertAs(typeof(UpdateTransformRequest))]
	public partial interface IUpdateTransformRequest : IRequest<UpdateTransformRequestParameters>
	{
	}

	public partial class UpdateTransformRequest : PlainRequestBase<UpdateTransformRequestParameters>, IUpdateTransformRequest
	{
		protected IUpdateTransformRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.TransformManagementUpdateTransform;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		protected override bool CanBeEmpty => true;
		protected override bool IsEmpty => true;
		[JsonIgnore]
		public bool? DeferValidation { get => Q<bool?>("defer_validation"); set => Q("defer_validation", value); }
	}
}