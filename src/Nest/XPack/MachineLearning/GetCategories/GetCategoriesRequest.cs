// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Retrieve job results for one or more categories.
	/// </summary>
	[MapsApi("ml.get_categories.json")]
	public partial interface IGetCategoriesRequest
	{
		/// <summary>
		/// Specifies pagination for the categories
		/// </summary>
		[DataMember(Name ="page")]
		IPage Page { get; set; }
	}

	/// <inheritdoc />
	public partial class GetCategoriesRequest
	{
		/// <inheritdoc cref="IGetCategoriesRequest.Page" />
		public IPage Page { get; set; }
	}

	public partial class GetCategoriesDescriptor
	{
		/// <inheritdoc cref="IGetCategoriesRequest.Page" />
		IPage IGetCategoriesRequest.Page { get; set; }


		/// <inheritdoc cref="IGetCategoriesRequest.Page" />
		public GetCategoriesDescriptor Page(Func<PageDescriptor, IPage> selector) => Assign(selector, (a, v) => a.Page = v?.Invoke(new PageDescriptor()));
	}
}
