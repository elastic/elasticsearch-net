using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(PutWarmerRequestJsonConverter))]
	public partial interface IPutWarmerRequest 
	{
		ISearchRequest Search { get; set; }
	}

	public partial class PutWarmerRequest 
	{
		public ISearchRequest Search { get; set; }


	}
	[DescriptorFor("IndicesPutWarmer")]
	public partial class PutWarmerDescriptor 
	{
		ISearchRequest IPutWarmerRequest.Search { get; set; }

		public PutWarmerDescriptor Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector) where T : class =>
			Assign(a => a.Search = selector?.Invoke(new SearchDescriptor<T>()));

	}
}
