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
				if (value.HasAny()) RequestState.RequestParameters.AddQueryString("types", value);
				else RequestState.RequestParameters.RemoveQueryString("types");
				_types = value;
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
				if (value.HasAny()) RequestState.RequestParameters.AddQueryString("types", value);
				else RequestState.RequestParameters.RemoveQueryString("types");
				_types = value;
			}
		}

		//<summary>A comma-separated list of fields for `completion` metric (supports wildcards)</summary>
		public IndicesShardStoresDescriptor Types(params TypeName[] types) =>
			Assign(a => a.Types = types);
	}
}
