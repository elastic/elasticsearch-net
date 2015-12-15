using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(CustomJsonConverter))]
	public partial interface IPutWarmerRequest : ICustomJson
	{
		ISearchRequest Search { get; set; }
	}

	public partial class PutWarmerRequest 
	{
		public ISearchRequest Search { get; set; }

		object ICustomJson.GetCustomJson() { return this.Search; }

	}
	[DescriptorFor("IndicesPutWarmer")]
	public partial class PutWarmerDescriptor 
	{
		ISearchRequest IPutWarmerRequest.Search { get; set; }

		public PutWarmerDescriptor Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector) where T : class =>
			Assign(a => a.Search = selector?.Invoke(new SearchDescriptor<T>()));

		object ICustomJson.GetCustomJson() => ((IPutWarmerRequest)this).Search;
	}
}
