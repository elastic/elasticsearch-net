// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	[MapsApi("migration.deprecations.json")]
	public partial interface IDeprecationInfoRequest { }

	public partial class DeprecationInfoRequest { }

	public partial class DeprecationInfoDescriptor : IDeprecationInfoRequest { }
}
