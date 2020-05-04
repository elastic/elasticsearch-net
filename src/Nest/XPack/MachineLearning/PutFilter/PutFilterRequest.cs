// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Creates a machine learning filter.
	/// </summary>
	[MapsApi("ml.put_filter")]
	public partial interface IPutFilterRequest
	{
		/// <summary>
		/// A description of the filter.
		/// </summary>
		[DataMember(Name = "description")]
		string Description { get; set; }

		/// <summary>
		/// The items of the filter. A wildcard * can be used at the beginning or
		/// the end of an item. Up to 10000 items are allowed in each filter.
		/// </summary>
		[DataMember(Name = "items")]
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
