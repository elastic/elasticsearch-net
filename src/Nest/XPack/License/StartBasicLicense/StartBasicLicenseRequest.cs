// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
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
