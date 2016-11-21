using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<CreateIndexRequest>))]
	public partial interface ICreateIndexRequest : IIndexState
	{
	}

	public partial class CreateIndexRequest
	{
		//TODO Only here for ReadAsType new() constraint needs to be updated
		public CreateIndexRequest() { }

		public CreateIndexRequest(IndexName index, IndexState state) : this(index)
		{
			this.Settings = state.Settings;
			this.Mappings = state.Mappings;
			this.Aliases = state.Aliases;
			this.Similarity = state.Similarity;
			CreateIndexRequest.RemoveReadOnlySettings(this.Settings);
		}

		private static string[] ReadOnlySettings =
		{
			"index.creation_date",
			"index.uuid",
			"index.version.created",
		};

		internal static void RemoveReadOnlySettings (IIndexSettings settings)
		{
			if (settings == null) return;
			foreach(var bad in ReadOnlySettings)
			{
				if (settings.ContainsKey(bad))
					settings.Remove(bad);
			}
		}

		public IIndexSettings Settings { get; set; }

		public IMappings Mappings { get; set; }

		public IAliases Aliases { get; set; }

		public ISimilarities Similarity { get; set; }
	}

	[DescriptorFor("IndicesCreate")]
	public partial class CreateIndexDescriptor
	{
		IIndexSettings IIndexState.Settings { get; set; }

		IMappings IIndexState.Mappings { get; set; }

		IAliases IIndexState.Aliases { get; set; }

		ISimilarities IIndexState.Similarity { get; set; }

		public CreateIndexDescriptor InitializeUsing(IIndexState indexSettings) => Assign(a =>
		{
			a.Settings = indexSettings.Settings;
			a.Mappings = indexSettings.Mappings;
			a.Aliases = indexSettings.Aliases;
			a.Similarity = indexSettings.Similarity;
			CreateIndexRequest.RemoveReadOnlySettings(a.Settings);
		});

		public CreateIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(a => a.Settings = selector?.Invoke(new IndexSettingsDescriptor())?.Value);

		public CreateIndexDescriptor Mappings(Func<MappingsDescriptor, IPromise<IMappings>> selector) =>
			Assign(a => a.Mappings = selector?.Invoke(new MappingsDescriptor())?.Value);

		public CreateIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(a => a.Aliases = selector?.Invoke(new AliasesDescriptor())?.Value);

		public CreateIndexDescriptor Similarity(Func<SimilaritiesDescriptor, IPromise<ISimilarities>> selector) =>
			Assign(a => a.Similarity = selector?.Invoke(new SimilaritiesDescriptor())?.Value);
	}
}
