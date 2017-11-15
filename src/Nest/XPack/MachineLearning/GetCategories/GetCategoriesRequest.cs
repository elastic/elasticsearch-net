using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Retrieve job results for one or more categories.
	/// </summary>
	public partial interface IGetCategoriesRequest
	{
		/// <summary>
		/// Specifies pagination for the categories
		/// </summary>
		[JsonProperty("page")]
		IPage Page { get; set; }
	}

	/// <inheritdoc />
	public partial class GetCategoriesRequest
	{
		/// <inheritdoc />
		public IPage Page { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlGetCategories")]
	public partial class GetCategoriesDescriptor
	{
		/// <inheritdoc />
		IPage IGetCategoriesRequest.Page { get; set; }

		/// <inheritdoc />
		public GetCategoriesDescriptor Page(Func<PageDescriptor, IPage> selector) => Assign(a => a.Page = selector?.Invoke(new PageDescriptor()));
	}
}
