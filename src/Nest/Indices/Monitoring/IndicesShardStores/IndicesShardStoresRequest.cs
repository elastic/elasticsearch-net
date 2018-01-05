using System.Collections.Generic;

namespace Nest
{
	public partial interface IIndicesShardStoresRequest
	{
		IEnumerable<TypeName> Types { get; set; }
	}

	public partial class IndicesShardStoresRequest
	{
		private IEnumerable<TypeName> _types;
		public IEnumerable<TypeName> Types
		{
			get => _types;
			set
			{
				this.RequestState.RequestParameters.SetQueryString("types", value.HasAny() ? value :  null);
				this._types = value;
			}
		}
	}

	[DescriptorFor("IndicesShardStores")]
	public partial class IndicesShardStoresDescriptor
	{
		private IEnumerable<TypeName> _types;
		IEnumerable<TypeName> IIndicesShardStoresRequest.Types
		{
			get => _types;
			set
			{
				this.RequestState.RequestParameters.SetQueryString("types", value.HasAny() ? value :  null);
				this._types = value;
			}
		}

		//<summary>A comma-separated list of fields for `completion` metric (supports wildcards)</summary>
		public IndicesShardStoresDescriptor Types(params TypeName[] types) =>
			Assign(a => a.Types = types);

	}
}
