using System;
using System.Collections;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateSettingsRequest : IIndicesOptionalPath<UpdateSettingsRequestParameters>, IIndexSettings, IHasADictionary
	{
	}

	public partial class UpdateSettingsRequest : IndicesOptionalPathBase<UpdateSettingsRequestParameters>, IUpdateSettingsRequest
	{
		public UpdateSettingsRequest() { }
		public UpdateSettingsRequest(IndexName index) { this.Indices = new []{ index }; }

		IDictionary IHasADictionary.Dictionary => this.AnySettings;

		public Dictionary<string, object> AnySettings { get; set; }

		/// <inheritdoc />
		public int? NumberOfShards { get; set; }

		/// <inheritdoc />
		public FileSystemStorageImplementation? FileSystemStorageImplementation { get; set; }

		/// <inheritdoc/ >
		public string AutoExpandReplicas { get; set; }

		/// <inheritdoc/ >
		public bool? BlocksMetadata { get; set; }

		/// <inheritdoc/ >
		public bool? BlocksRead { get; set; }

		/// <inheritdoc/ >
		public bool? BlocksReadOnly { get; set; }
		
		/// <inheritdoc/ >
		public bool? BlocksWrite { get; set; }
		
		/// <inheritdoc/ >
		public int? Priority { get; set; }
		
		/// <inheritdoc/ >
		public bool? WarmersEnabled { get; set; }
		
		/// <inheritdoc/ >
		public bool? RequestCacheEnabled { get; set; }
		
		/// <inheritdoc/ >
		public IMergeSettings Merge { get; set; }
		
		/// <inheritdoc/ >
		public int? NumberOfReplicas { get; set; }
		
		/// <inheritdoc/ >
		public Union<int, RecoveryInitialShards> RecoveryInitialShards { get; set; }
		
		/// <inheritdoc/ >
		public TimeUnitExpression RefreshInterval { get; set; }
		
		/// <inheritdoc/ >
		public int? RoutingAllocationTotalShardsPerNode { get; set; }
		
		/// <inheritdoc/ >
		public ISlowLog SlowLog { get; set; }
		
		/// <inheritdoc/ >
		public ITranslogSettings Translog { get; set; }
		
		/// <inheritdoc/ >
		public TimeUnitExpression UnassignedNodeLeftDelayedTimeout { get; set; }

		/// <inheritdoc/ >
		public IAnalysis Analysis { get; set; }
	}

	[DescriptorFor("IndicesPutSettings")]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public partial class UpdateSettingsDescriptor
		: IndicesOptionalPathDescriptor<UpdateSettingsDescriptor, UpdateSettingsRequestParameters>, IUpdateSettingsRequest
	{
		UpdateSettingsDescriptor Assign(Action<IIndexSettings> assigner) => Fluent.Assign(this, assigner);

		int? IIndexSettings.NumberOfShards { get; set; }
		FileSystemStorageImplementation? IIndexSettings.FileSystemStorageImplementation { get; set; }
		int? IDynamicIndexSettings.NumberOfReplicas { get; set; }
		string IDynamicIndexSettings.AutoExpandReplicas { get; set; }
		bool? IDynamicIndexSettings.BlocksMetadata { get; set; }
		bool? IDynamicIndexSettings.BlocksRead { get; set; }
		bool? IDynamicIndexSettings.BlocksReadOnly { get; set; }
		bool? IDynamicIndexSettings.BlocksWrite { get; set; }
		int? IDynamicIndexSettings.Priority { get; set; }
		bool? IDynamicIndexSettings.WarmersEnabled { get; set; }
		bool? IDynamicIndexSettings.RequestCacheEnabled { get; set; }
		IMergeSettings IDynamicIndexSettings.Merge { get; set; }
		Union<int, RecoveryInitialShards> IDynamicIndexSettings.RecoveryInitialShards { get; set; }
		TimeUnitExpression IDynamicIndexSettings.RefreshInterval { get; set; }
		int? IDynamicIndexSettings.RoutingAllocationTotalShardsPerNode { get; set; }
		ISlowLog IDynamicIndexSettings.SlowLog { get; set; }
		ITranslogSettings IDynamicIndexSettings.Translog { get; set; }
		TimeUnitExpression IDynamicIndexSettings.UnassignedNodeLeftDelayedTimeout { get; set; }
		IAnalysis IDynamicIndexSettings.Analysis { get; set; }

		protected Dictionary<string, object> BackingDictionary { get; set; } = new Dictionary<string, object>();
		IDictionary IHasADictionary.Dictionary => this.BackingDictionary;

		/// <summary>
		/// Add any setting that we might have missed or is introduced by a plugin
		/// </summary>
		public UpdateSettingsDescriptor Add(string setting, object value)
		{
			this.BackingDictionary.Add(setting, value);
			return this;
		}

		/// <inheritdoc />
		public UpdateSettingsDescriptor NumberOfShards(int? numberOfShards) => Assign(a => a.NumberOfShards = numberOfShards);

		/// <inheritdoc />
		public UpdateSettingsDescriptor FileSystemStorageImplementation(FileSystemStorageImplementation? fs) =>
			Assign(a => a.FileSystemStorageImplementation = fs);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor NumberOfReplicas(int? numberOfReplicas) =>
			Assign(a => a.NumberOfReplicas = numberOfReplicas);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor AutoExpandReplicas(string AutoExpandReplicas) =>
			Assign(a => a.AutoExpandReplicas = AutoExpandReplicas);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor BlocksMetadata(bool? blocksMetadata = true) =>
			Assign(a => a.BlocksMetadata = blocksMetadata);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor BlocksRead(bool? blocksRead = true) =>
			Assign(a => a.BlocksRead = blocksRead);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor BlocksReadOnly(bool? blocksReadOnly = true) =>
			Assign(a => a.BlocksReadOnly = blocksReadOnly);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor BlocksWrite(bool? blocksWrite = true) =>
			Assign(a => a.BlocksWrite = blocksWrite);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor Priority(int? priority) =>
			Assign(a => a.Priority = priority);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor WarmersEnabled(bool enabled = true) =>
			Assign(a => a.WarmersEnabled = enabled);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor RequestCacheEnabled(bool enabled = true) =>
			Assign(a => a.RequestCacheEnabled = enabled);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor Merge(Func<MergeSettingsDescriptor, IMergeSettings> merge) =>
			Assign(a => a.Merge = merge?.Invoke(new MergeSettingsDescriptor()));

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor RecoveryInitialShards(Union<int, RecoveryInitialShards> initialShards) =>
			Assign(a => a.RecoveryInitialShards = initialShards);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor RefreshInterval(TimeUnitExpression time) =>
			Assign(a => a.RefreshInterval = time);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor TotalShardsPerNode(int? totalShardsPerNode) =>
			Assign(a => a.RoutingAllocationTotalShardsPerNode = totalShardsPerNode);

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor SlowLog(Func<SlowLogDescriptor, ISlowLog> slowLogSelector) =>
			Assign(a => a.SlowLog = slowLogSelector?.Invoke(new SlowLogDescriptor()));

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor Translog(Func<TranslogSettingsDescriptor, ITranslogSettings> translogSelector) =>
			Assign(a => a.Translog = translogSelector?.Invoke(new TranslogSettingsDescriptor()));

		/// <inheritdoc/ >
		public UpdateSettingsDescriptor UnassignedNodeLeftDelayedTimeout(TimeUnitExpression time) =>
			Assign(a => a.UnassignedNodeLeftDelayedTimeout = time);

		public UpdateSettingsDescriptor Analysis(Func<AnalysisDescriptor, IAnalysis> selector) =>
			Assign(a => a.Analysis = selector?.Invoke(new AnalysisDescriptor()));
	}
}
