using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ShrinkIndexRequest>))]
	public partial interface IShrinkIndexRequest
	{
		[JsonProperty("settings")]
		IIndexSettings Settings { get; set; }

		[JsonProperty("aliases")]
		IAliases Aliases { get; set; }
	}

	public partial class ShrinkIndexRequest
	{
		// For ReadAsType()
		internal ShrinkIndexRequest() { }

		public IIndexSettings Settings { get; set; }

		public IAliases Aliases { get; set; }
	}

	[DescriptorFor("IndicesShrink")]
	public partial class ShrinkIndexDescriptor
	{
		IIndexSettings IShrinkIndexRequest.Settings { get; set; }

		IAliases IShrinkIndexRequest.Aliases { get; set; }

		public ShrinkIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(a => a.Settings = selector?.Invoke(new IndexSettingsDescriptor())?.Value);

		public ShrinkIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(a => a.Aliases = selector?.Invoke(new AliasesDescriptor())?.Value);
	}
}
