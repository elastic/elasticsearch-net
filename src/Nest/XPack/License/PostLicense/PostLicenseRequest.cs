// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("license.post.json")]
	public partial interface IPostLicenseRequest
	{
		[DataMember(Name ="license")]
		License License { get; set; }
	}

	public partial class PostLicenseRequest
	{
		public License License { get; set; }

		public static implicit operator PostLicenseRequest(License license) => new PostLicenseRequest { License = license };
	}

	public partial class PostLicenseDescriptor
	{
		License IPostLicenseRequest.License { get; set; }

		public PostLicenseDescriptor License(License license) =>
			Assign(license, (a, v) => a.License = v);
	}
}
