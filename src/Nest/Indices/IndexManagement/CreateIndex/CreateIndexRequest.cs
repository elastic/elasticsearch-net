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
	public interface ICreateIndexRequest : IIndexPath<CreateIndexRequestParameters>
	{
		[JsonProperty("settings")]
		IIndexSettings Settings { get; set; }

		[JsonProperty("mappings")]
		IMappings Mappings { get; set; }
		
		[JsonProperty("warmers")]
		IWarmers Warmers { get; set; }
		
	}

	internal static class CreateIndexPathInfo
	{
		public static void Update(ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo, ICreateIndexRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.POST;
		}
	}

	public partial class CreateIndexRequest : IndexPathBase<CreateIndexRequestParameters>, ICreateIndexRequest
	{
		internal CreateIndexRequest() : base(null) { } 

		public CreateIndexRequest(IndexName index) : base(index) { }

		public IIndexSettings Settings { get; set; }

		public IMappings Mappings { get; set; }

		public IWarmers Warmers { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo)
		{
			CreateIndexPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesCreate")]
	public partial class CreateIndexDescriptor : IndexPathDescriptorBase<CreateIndexDescriptor, CreateIndexRequestParameters>, ICreateIndexRequest
	{
		protected CreateIndexDescriptor Assign(Action<ICreateIndexRequest> assigner) => Fluent.Assign(this, assigner);

		IIndexSettings ICreateIndexRequest.Settings { get; set; }

		IMappings ICreateIndexRequest.Mappings { get; set; }

		IWarmers ICreateIndexRequest.Warmers { get; set; }

		public CreateIndexDescriptor InitializeUsing(IIndexState indexSettings)
		{
			//TODO make this work again
			return this;
		}

		public CreateIndexDescriptor Settings(Func<IndexSettingsDescriptor, IIndexSettings> selector) =>
			Assign(a => a.Settings = selector?.Invoke(new IndexSettingsDescriptor()));

		public CreateIndexDescriptor Mappings(Func<MappingsDescriptor, IMappings> selector) =>
			Assign(a => a.Mappings = selector?.Invoke(new MappingsDescriptor()));

		public CreateIndexDescriptor Warmers(Func<WarmersDescriptor, IWarmers> selector) =>
			Assign(a => a.Warmers = selector?.Invoke(new WarmersDescriptor()));

		/// <summary>
		/// Add an alias for this index upon index creation
		/// </summary>
		public CreateIndexDescriptor AddAlias(string aliasName, Func<CreateAliasDescriptor, CreateAliasDescriptor> addAliasSelector = null)
		{
			return this;
		}


		public CreateIndexDescriptor AddWarmer(Func<CreateWarmerDescriptor, CreateWarmerDescriptor> warmerSelector)
		{
			return this;
		}

		public CreateIndexDescriptor DeleteWarmer(string warmerName)
		{
			return this;
		}

		public CreateIndexDescriptor Similarity(Func<SimilarityDescriptor, SimilarityDescriptor> similaritySelector)
		{
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo)
		{
			CreateIndexPathInfo.Update(pathInfo, this);
		}
	}
}
