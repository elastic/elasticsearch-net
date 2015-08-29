using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<CreateIndexDescriptor>))]
	public interface ICreateIndexRequest : IIndexPath<CreateIndexRequestParameters>, IIndexState
	{

	}

	internal static class CreateIndexPathInfo
	{
		public static void Update(ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo, ICreateIndexRequest request) =>
			pathInfo.HttpMethod = HttpMethod.POST;
	}

	public partial class CreateIndexRequest : IndexPathBase<CreateIndexRequestParameters>, ICreateIndexRequest
	{
		internal CreateIndexRequest() : base(null) { }

		public CreateIndexRequest(IndexName index) : base(index) { }

		public IIndexSettings Settings { get; set; }

		public IMappings Mappings { get; set; }

		public IWarmers Warmers { get; set; }

		public IAliases Aliases { get; set; }

		public ISimilarities Similarity { get; set; }

		public IAnalysis Analysis { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo) =>
			CreateIndexPathInfo.Update(pathInfo, this);
	}

	[DescriptorFor("IndicesCreate")]
	public partial class CreateIndexDescriptor : IndexPathDescriptorBase<CreateIndexDescriptor, CreateIndexRequestParameters>, ICreateIndexRequest
	{
		protected CreateIndexDescriptor Assign(Action<ICreateIndexRequest> assigner) => Fluent.Assign(this, assigner);

		IIndexSettings IIndexState.Settings { get; set; }

		IMappings IIndexState.Mappings { get; set; }

		IWarmers IIndexState.Warmers { get; set; }

		IAliases IIndexState.Aliases { get; set; }

		ISimilarities IIndexState.Similarity { get; set; }

		public CreateIndexDescriptor InitializeUsing(IIndexState indexSettings) => Assign(a =>
		{
			a.Settings = indexSettings.Settings;
			a.Mappings = indexSettings.Mappings;
			a.Warmers = indexSettings.Warmers;
			a.Aliases = indexSettings.Aliases;
		});

		public CreateIndexDescriptor Settings(Func<IndexSettingsDescriptor, IIndexSettings> selector) =>
			Assign(a => a.Settings = selector?.Invoke(new IndexSettingsDescriptor()));

		public CreateIndexDescriptor Mappings(Func<MappingsDescriptor, IMappings> selector) =>
			Assign(a => a.Mappings = selector?.Invoke(new MappingsDescriptor()));

		public CreateIndexDescriptor Warmers(Func<WarmersDescriptor, IWarmers> selector) =>
			Assign(a => a.Warmers = selector?.Invoke(new WarmersDescriptor()));

		public CreateIndexDescriptor Aliases(Func<AliasesDescriptor, IAliases> selector) =>
			Assign(a => a.Aliases = selector?.Invoke(new AliasesDescriptor()));

		public CreateIndexDescriptor Similarity(Func<SimilaritiesDescriptor, ISimilarities> selector) =>
			Assign(a => a.Similarity = selector?.Invoke(new SimilaritiesDescriptor()));


		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo) =>
			CreateIndexPathInfo.Update(pathInfo, this);
	}
}
