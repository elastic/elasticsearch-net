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

namespace Nest
{
	/// <summary>
	/// The start basic API enables you to initiate an indefinite basic license, which gives access to all
	/// the basic features. If the basic license does not support all of the features that are
	/// available with your current license, however, you are notified in the response. You must then
	/// re-submit the API request with the acknowledge parameter set to true.
	/// </summary>
	[MapsApi("license.post_start_basic.json")]
	public partial interface IStartBasicLicenseRequest { }

	/// <inheritdoc cref="IStartBasicLicenseRequest"/>
	public partial class StartBasicLicenseRequest { }

	/// <inheritdoc cref="IStartBasicLicenseRequest"/>
	public partial class StartBasicLicenseDescriptor { }
}
