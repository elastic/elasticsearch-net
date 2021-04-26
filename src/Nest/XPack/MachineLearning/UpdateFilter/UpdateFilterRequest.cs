/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Updates the description of a machine learning filter, adds items, or removes items.
	/// </summary>
	[MapsApi("ml.update_filter")]
	public partial interface IUpdateFilterRequest
	{
		/// <summary>
		/// The items to add to the filter
		/// </summary>
		[DataMember(Name = "add_items")]
		IEnumerable<string> AddItems { get; set; }

		/// <summary>
		/// A description for the filter
		/// </summary>
		[DataMember(Name = "description")]
		string Description { get; set; }

		/// <summary>
		/// The items to remove from the filter
		/// </summary>
		[DataMember(Name = "remove_items")]
		IEnumerable<string> RemoveItems { get; set; }
	}

	/// <inheritdoc cref="UpdateFilterRequest" />
	public partial class UpdateFilterRequest : IUpdateFilterRequest
	{
		/// <inheritdoc />
		public IEnumerable<string> AddItems { get; set; }

		/// <inheritdoc />
		public string Description { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> RemoveItems { get; set; }
	}


	public partial class UpdateFilterDescriptor : IUpdateFilterRequest
	{
		IEnumerable<string> IUpdateFilterRequest.AddItems { get; set; }
		string IUpdateFilterRequest.Description { get; set; }
		IEnumerable<string> IUpdateFilterRequest.RemoveItems { get; set; }

		/// <inheritdoc cref="IUpdateFilterRequest.Description" />
		public UpdateFilterDescriptor Description(string description) => Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc cref="IUpdateFilterRequest.AddItems" />
		public UpdateFilterDescriptor AddItems(params string[] items) => Assign(items, (a, v) => a.AddItems = v);

		/// <inheritdoc cref="IUpdateFilterRequest.AddItems" />
		public UpdateFilterDescriptor AddItems(IEnumerable<string> items) => Assign(items, (a, v) => a.AddItems = v);

		/// <inheritdoc cref="IUpdateFilterRequest.RemoveItems" />
		public UpdateFilterDescriptor RemoveItems(params string[] items) => Assign(items, (a, v) => a.RemoveItems = v);

		/// <inheritdoc cref="IUpdateFilterRequest.RemoveItems" />
		public UpdateFilterDescriptor RemoveItems(IEnumerable<string> items) => Assign(items, (a, v) => a.RemoveItems = v);
	}
}
