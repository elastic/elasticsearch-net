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
		[JsonProperty("description")]
		string Description { get; set; }

		[JsonProperty("items")]
		IEnumerable<string> Items { get; set; }
	}

	/// <inheritdoc cref="PutFilterRequest" />
	public partial class PutFilterRequest : IPutFilterRequest
	{
		public string Description { get; set; }

		public IEnumerable<string> Items { get; set; }
	}

	[DescriptorFor("XpackMlPutFilter")]
	public partial class PutFilterDescriptor : IPutFilterRequest
	{
		string IPutFilterRequest.Description { get; set; }
		IEnumerable<string> IPutFilterRequest.Items { get; set; }

		/// <inheritdoc cref="IPutFilterRequest.Description" />
		public PutFilterDescriptor Description(string description) => Assign(a => a.Description = description);

		public PutFilterDescriptor Items(params string[] items) => Assign(a => a.Items = items);

		public PutFilterDescriptor Items(IEnumerable<string> items) => Assign(a => a.Items = items);
	}
}
