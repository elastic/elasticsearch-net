using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RolloverIndexRequest>))]
	public partial interface IRolloverIndexRequest
	{
		[JsonProperty("conditions")]
		IRolloverConditions Conditions { get; set; }

		[JsonProperty("settings")]
		IIndexSettings Settings { get; set; }

		[JsonProperty("aliases")]
		IAliases Aliases { get; set; }

		[JsonProperty("mappings")]
		IMappings Mappings { get; set; }
	}

	public partial class RolloverIndexRequest
	{
		public IRolloverConditions Conditions { get; set; }

		public IIndexSettings Settings { get; set; }

		public IAliases Aliases { get; set; }

		public IMappings Mappings { get; set; }
	}

	[DescriptorFor("IndicesRollover")]
	public partial class RolloverIndexDescriptor
	{
		IRolloverConditions IRolloverIndexRequest.Conditions { get; set; }
		IIndexSettings IRolloverIndexRequest.Settings { get; set; }
		IMappings IRolloverIndexRequest.Mappings { get; set; }
		IAliases IRolloverIndexRequest.Aliases { get; set; }

		public RolloverIndexDescriptor Conditions(Func<RolloverConditionsDescriptor, IRolloverConditions> selector) =>
			Assign(a => a.Conditions = selector?.Invoke(new RolloverConditionsDescriptor()));

		public RolloverIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(a => a.Settings = selector?.Invoke(new IndexSettingsDescriptor())?.Value);

		public RolloverIndexDescriptor Mappings(Func<MappingsDescriptor, IPromise<IMappings>> selector) =>
			Assign(a => a.Mappings = selector?.Invoke(new MappingsDescriptor())?.Value);

		public RolloverIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(a => a.Aliases = selector?.Invoke(new AliasesDescriptor())?.Value);
	}
}
