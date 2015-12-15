using System.Collections.Generic;

namespace Nest
{
	public partial interface IIndicesStatsRequest 
	{
		IEnumerable<TypeName> Types { get; set; }
	}

	public partial class IndicesStatsRequest 
	{
		private IEnumerable<TypeName> _types;
		public IEnumerable<TypeName> Types
		{
			get { return _types; }
			set
			{
				if (value.HasAny()) this.RequestState.RequestParameters.AddQueryString("types", value);
				else this.RequestState.RequestParameters.RemoveQueryString("types");
				this._types = value;
			}
		}
	}

	[DescriptorFor("IndicesStats")]
	public partial class IndicesStatsDescriptor 
	{
		private IEnumerable<TypeName> _types;
		IEnumerable<TypeName> IIndicesStatsRequest.Types 
		{
			get { return _types; }
			set
			{
				if (value.HasAny()) this.RequestState.RequestParameters.AddQueryString("types", value);
				else this.RequestState.RequestParameters.RemoveQueryString("types");
				this._types = value;
			}
		}

		//<summary>A comma-separated list of fields for `completion` metric (supports wildcards)</summary>
		public IndicesStatsDescriptor Types(params TypeName[] types) =>
			Assign(a => a.Types = types);

	}
}
