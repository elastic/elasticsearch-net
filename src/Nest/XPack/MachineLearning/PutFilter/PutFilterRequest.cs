using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Creates a machine learning filter.
	/// </summary>
	public partial interface IPutFilterRequest
	{
		/// <summary>
		/// A description of the filter.
		/// </summary>
		[JsonProperty("description")]
		string Description { get; set; }

		/// <summary>
		/// The items of the filter. A wildcard * can be used at the beginning or
		/// the end of an item. Up to 10000 items are allowed in each filter.
		/// </summary>
		[JsonProperty("items")]
		IEnumerable<string> Items { get; set; }
	}

	/// <inheritdoc cref="PutFilterRequest" />
	public partial class PutFilterRequest : IPutFilterRequest
	{
		/// <inheritdoc />
		public string Description { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Items { get; set; }
	}

	[DescriptorFor("XpackMlPutFilter")]
	public partial class PutFilterDescriptor : IPutFilterRequest
	{
		string IPutFilterRequest.Description { get; set; }
		IEnumerable<string> IPutFilterRequest.Items { get; set; }

		/// <inheritdoc cref="IPutFilterRequest.Description" />
		public PutFilterDescriptor Description(string description) => Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc cref="IPutFilterRequest.Items" />
		public PutFilterDescriptor Items(params string[] items) => Assign(items, (a, v) => a.Items = v);

		/// <inheritdoc cref="IPutFilterRequest.Items" />
		public PutFilterDescriptor Items(IEnumerable<string> items) => Assign(items, (a, v) => a.Items = v);
	}
}
