// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Specification.SecurityApi;

namespace Nest
{
	[MapsApi("ssl.certificates.json")]
	public partial interface IGetCertificatesRequest { }

	public partial class GetCertificatesRequest
	{
		protected sealed override void RequestDefaults(GetCertificatesRequestParameters parameters) =>
			parameters.CustomResponseBuilder = GetCertificatesResponseBuilder.Instance;
	}

	public partial class GetCertificatesDescriptor
	{
		protected sealed override void RequestDefaults(GetCertificatesRequestParameters parameters) =>
			parameters.CustomResponseBuilder = GetCertificatesResponseBuilder.Instance;
	}
}
