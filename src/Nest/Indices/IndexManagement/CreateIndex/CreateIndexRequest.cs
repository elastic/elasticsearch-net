using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("indices.create.json")]
	[ReadAs(typeof(CreateIndexRequest))]
	public partial interface ICreateIndexRequest : IIndexState { }

	public partial class CreateIndexRequest
	{
		private static readonly string[] ReadOnlySettings =
		{
			"index.creation_date",
			"index.uuid",
			"index.version.created",
			"index.provided_name"
		};

		public CreateIndexRequest(IndexName index, IIndexState state) : this(index)
		{
			Settings = state.Settings;
			Mappings = state.Mappings;
			RemoveReadOnlySettings(Settings);
		}

		public IAliases Aliases { get; set; }

		public ITypeMapping Mappings { get; set; }

		public IIndexSettings Settings { get; set; }

		internal static void RemoveReadOnlySettings(IIndexSettings settings)
		{
			if (settings == null) return;

			foreach (var bad in ReadOnlySettings)
			{
				if (settings.ContainsKey(bad))
					settings.Remove(bad);
			}
		}
	}

	public partial class CreateIndexDescriptor
	{
		IAliases IIndexState.Aliases { get; set; }

		ITypeMapping IIndexState.Mappings { get; set; }
		IIndexSettings IIndexState.Settings { get; set; }

		public CreateIndexDescriptor InitializeUsing(IIndexState indexSettings) => Assign(a =>
		{
			a.Settings = indexSettings.Settings;
			a.Mappings = indexSettings.Mappings;
			a.Aliases = indexSettings.Aliases;
			CreateIndexRequest.RemoveReadOnlySettings(a.Settings);
		});

		public CreateIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(a => a.Settings = selector?.Invoke(new IndexSettingsDescriptor())?.Value);

		public CreateIndexDescriptor Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			Assign(a => a.Mappings = selector?.Invoke(new TypeMappingDescriptor<T>()));

		public CreateIndexDescriptor Map(Func<TypeMappingDescriptor<object>, ITypeMapping> selector) =>
			Assign(a => a.Mappings = selector?.Invoke(new TypeMappingDescriptor<object>()));

		[Obsolete("Mappings is no longer a dictionary in 7.x, please use the simplified Map() method on this descriptor instead")]
		public CreateIndexDescriptor Mappings(Func<MappingsDescriptor, ITypeMapping> selector) =>
			Assign(a => a.Mappings = selector?.Invoke(new MappingsDescriptor()));

		public CreateIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(a => a.Aliases = selector?.Invoke(new AliasesDescriptor())?.Value);
	}
}
