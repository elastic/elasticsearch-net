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
