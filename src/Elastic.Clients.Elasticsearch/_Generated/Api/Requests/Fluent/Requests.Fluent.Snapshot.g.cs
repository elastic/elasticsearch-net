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
namespace Elastic.Clients.Elasticsearch.Snapshot
{
	public partial class CleanupRepositoryDescriptor : RequestDescriptorBase<CleanupRepositoryDescriptor, CleanupRepositoryRequestParameters, ICleanupRepositoryRequest>, ICleanupRepositoryRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotCleanupRepository;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot/{repository}/_cleanup</summary>
        public CleanupRepositoryDescriptor(Elastic.Clients.Elasticsearch.Name repository) : base(r => r.Required("repository", repository))
		{
		}

		public CleanupRepositoryDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public CleanupRepositoryDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
	}

	public partial class CloneDescriptor : RequestDescriptorBase<CloneDescriptor, CloneRequestParameters, ICloneRequest>, ICloneRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotClone;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot/{repository}/{snapshot}/_clone/{target_snapshot}</summary>
        public CloneDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot, Elastic.Clients.Elasticsearch.Name target_snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot).Required("target_snapshot", target_snapshot))
		{
		}

		string ICloneRequest.Indices { get; set; }

		public CloneDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public CloneDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		public CloneDescriptor Indices(string indices) => Assign(indices, (a, v) => a.Indices = v);
	}

	public partial class CreateDescriptor : RequestDescriptorBase<CreateDescriptor, CreateRequestParameters, ICreateRequest>, ICreateRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotCreate;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot/{repository}/{snapshot}</summary>
        public CreateDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
		{
		}

		bool? ICreateRequest.IgnoreUnavailable { get; set; }

		bool? ICreateRequest.IncludeGlobalState { get; set; }

		Elastic.Clients.Elasticsearch.Indices? ICreateRequest.Indices { get; set; }

		IEnumerable<string>? ICreateRequest.FeatureStates { get; set; }

		Elastic.Clients.Elasticsearch.Metadata? ICreateRequest.Metadata { get; set; }

		bool? ICreateRequest.Partial { get; set; }

		public CreateDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public CreateDescriptor WaitForCompletion(bool? waitForCompletion = true) => Qs("wait_for_completion", waitForCompletion);
		public CreateDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Assign(ignoreUnavailable, (a, v) => a.IgnoreUnavailable = v);
		public CreateDescriptor IncludeGlobalState(bool? includeGlobalState = true) => Assign(includeGlobalState, (a, v) => a.IncludeGlobalState = v);
		public CreateDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices) => Assign(indices, (a, v) => a.Indices = v);
		public CreateDescriptor FeatureStates(IEnumerable<string>? featureStates) => Assign(featureStates, (a, v) => a.FeatureStates = v);
		public CreateDescriptor Metadata(Elastic.Clients.Elasticsearch.Metadata? metadata) => Assign(metadata, (a, v) => a.Metadata = v);
		public CreateDescriptor Partial(bool? partial = true) => Assign(partial, (a, v) => a.Partial = v);
	}

	public partial class CreateRepositoryDescriptor : RequestDescriptorBase<CreateRepositoryDescriptor, CreateRepositoryRequestParameters, ICreateRepositoryRequest>, ICreateRepositoryRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotCreateRepository;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot/{repository}</summary>
        public CreateRepositoryDescriptor(Elastic.Clients.Elasticsearch.Name repository) : base(r => r.Required("repository", repository))
		{
		}

		string ICreateRepositoryRequest.Type { get; set; }

		Elastic.Clients.Elasticsearch.Snapshot.IRepositorySettings ICreateRepositoryRequest.Settings { get; set; }

		public CreateRepositoryDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public CreateRepositoryDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		public CreateRepositoryDescriptor Verify(bool? verify = true) => Qs("verify", verify);
		public CreateRepositoryDescriptor Type(string type) => Assign(type, (a, v) => a.Type = v);
		public CreateRepositoryDescriptor Settings(Elastic.Clients.Elasticsearch.Snapshot.IRepositorySettings settings) => Assign(settings, (a, v) => a.Settings = v);
	}

	public partial class DeleteDescriptor : RequestDescriptorBase<DeleteDescriptor, DeleteRequestParameters, IDeleteRequest>, IDeleteRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotDelete;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot/{repository}/{snapshot}</summary>
        public DeleteDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
		{
		}

		public DeleteDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
	}

	public partial class DeleteRepositoryDescriptor : RequestDescriptorBase<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters, IDeleteRepositoryRequest>, IDeleteRepositoryRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotDeleteRepository;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot/{repository}</summary>
        public DeleteRepositoryDescriptor(Elastic.Clients.Elasticsearch.Names repository) : base(r => r.Required("repository", repository))
		{
		}

		public DeleteRepositoryDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public DeleteRepositoryDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
	}

	public partial class GetDescriptor : RequestDescriptorBase<GetDescriptor, GetRequestParameters, IGetRequest>, IGetRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotGet;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot/{repository}/{snapshot}</summary>
        public GetDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Names snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
		{
		}

		public GetDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
		public GetDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public GetDescriptor Verbose(bool? verbose = true) => Qs("verbose", verbose);
		public GetDescriptor IndexDetails(bool? indexDetails = true) => Qs("index_details", indexDetails);
	}

	public partial class GetRepositoryDescriptor : RequestDescriptorBase<GetRepositoryDescriptor, GetRepositoryRequestParameters, IGetRepositoryRequest>, IGetRepositoryRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotGetRepository;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot</summary>
        public GetRepositoryDescriptor() : base()
		{
		}

		///<summary>/_snapshot/{repository}</summary>
        public GetRepositoryDescriptor(Elastic.Clients.Elasticsearch.Names? repository) : base(r => r.Optional("repository", repository))
		{
		}

		public GetRepositoryDescriptor Local(bool? local = true) => Qs("local", local);
		public GetRepositoryDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
	}

	public partial class RestoreDescriptor : RequestDescriptorBase<RestoreDescriptor, RestoreRequestParameters, IRestoreRequest>, IRestoreRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotRestore;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot/{repository}/{snapshot}/_restore</summary>
        public RestoreDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
		{
		}

		IEnumerable<string>? IRestoreRequest.IgnoreIndexSettings { get; set; }

		bool? IRestoreRequest.IgnoreUnavailable { get; set; }

		bool? IRestoreRequest.IncludeAliases { get; set; }

		bool? IRestoreRequest.IncludeGlobalState { get; set; }

		Elastic.Clients.Elasticsearch.IndexManagement.PutSettingsRequest? IRestoreRequest.IndexSettings { get; set; }

		Elastic.Clients.Elasticsearch.Indices? IRestoreRequest.Indices { get; set; }

		bool? IRestoreRequest.Partial { get; set; }

		string? IRestoreRequest.RenamePattern { get; set; }

		string? IRestoreRequest.RenameReplacement { get; set; }

		public RestoreDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public RestoreDescriptor WaitForCompletion(bool? waitForCompletion = true) => Qs("wait_for_completion", waitForCompletion);
		public RestoreDescriptor IgnoreIndexSettings(IEnumerable<string>? ignoreIndexSettings) => Assign(ignoreIndexSettings, (a, v) => a.IgnoreIndexSettings = v);
		public RestoreDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Assign(ignoreUnavailable, (a, v) => a.IgnoreUnavailable = v);
		public RestoreDescriptor IncludeAliases(bool? includeAliases = true) => Assign(includeAliases, (a, v) => a.IncludeAliases = v);
		public RestoreDescriptor IncludeGlobalState(bool? includeGlobalState = true) => Assign(includeGlobalState, (a, v) => a.IncludeGlobalState = v);
		public RestoreDescriptor IndexSettings(Elastic.Clients.Elasticsearch.IndexManagement.PutSettingsRequest? indexSettings) => Assign(indexSettings, (a, v) => a.IndexSettings = v);
		public RestoreDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices) => Assign(indices, (a, v) => a.Indices = v);
		public RestoreDescriptor Partial(bool? partial = true) => Assign(partial, (a, v) => a.Partial = v);
		public RestoreDescriptor RenamePattern(string? renamePattern) => Assign(renamePattern, (a, v) => a.RenamePattern = v);
		public RestoreDescriptor RenameReplacement(string? renameReplacement) => Assign(renameReplacement, (a, v) => a.RenameReplacement = v);
	}

	public partial class StatusDescriptor : RequestDescriptorBase<StatusDescriptor, StatusRequestParameters, IStatusRequest>, IStatusRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotStatus;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot/_status</summary>
        public StatusDescriptor() : base()
		{
		}

		///<summary>/_snapshot/{repository}/_status</summary>
        public StatusDescriptor(Elastic.Clients.Elasticsearch.Name? repository) : base(r => r.Optional("repository", repository))
		{
		}

		///<summary>/_snapshot/{repository}/{snapshot}/_status</summary>
        public StatusDescriptor(Elastic.Clients.Elasticsearch.Name? repository, Elastic.Clients.Elasticsearch.Names? snapshot) : base(r => r.Optional("repository", repository).Optional("snapshot", snapshot))
		{
		}

		public StatusDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
		public StatusDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
	}

	public partial class VerifyRepositoryDescriptor : RequestDescriptorBase<VerifyRepositoryDescriptor, VerifyRepositoryRequestParameters, IVerifyRepositoryRequest>, IVerifyRepositoryRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotVerifyRepository;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		///<summary>/_snapshot/{repository}/_verify</summary>
        public VerifyRepositoryDescriptor(Elastic.Clients.Elasticsearch.Name repository) : base(r => r.Required("repository", repository))
		{
		}

		public VerifyRepositoryDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public VerifyRepositoryDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
	}
}