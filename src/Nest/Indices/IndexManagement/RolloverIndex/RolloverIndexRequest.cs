using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RolloverIndexRequest>))]
	public partial interface IRolloverIndexRequest : IIndexState
	{
		[JsonProperty("conditions")]
		IRolloverConditions Conditions { get; set; }
	}

	public partial class RolloverIndexRequest
	{
		public IAliases Aliases { get; set; }
		public IRolloverConditions Conditions { get; set; }

		public IMappings Mappings { get; set; }

		public IIndexSettings Settings { get; set; }
	}

	[DescriptorFor("IndicesRollover")]
	public partial class RolloverIndexDescriptor
	{
		IAliases IIndexState.Aliases { get; set; }
		IRolloverConditions IRolloverIndexRequest.Conditions { get; set; }
		IMappings IIndexState.Mappings { get; set; }
		IIndexSettings IIndexState.Settings { get; set; }

		public RolloverIndexDescriptor Conditions(Func<RolloverConditionsDescriptor, IRolloverConditions> selector) =>
			Assign(selector, (a, v) => a.Conditions = v?.Invoke(new RolloverConditionsDescriptor()));

		public RolloverIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(selector, (a, v) => a.Settings = v?.Invoke(new IndexSettingsDescriptor())?.Value);

		public RolloverIndexDescriptor Mappings(Func<MappingsDescriptor, IPromise<IMappings>> selector) =>
			Assign(selector, (a, v) => a.Mappings = v?.Invoke(new MappingsDescriptor())?.Value);

		public RolloverIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(selector, (a, v) => a.Aliases = v?.Invoke(new AliasesDescriptor())?.Value);
	}
}
