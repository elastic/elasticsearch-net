// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	public partial interface IGetRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial interface IGetRequest<TDocument> where TDocument : class { }

	public partial class GetRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial class GetRequest<TDocument> where TDocument : class { }

	public partial class GetDescriptor<TDocument> where TDocument : class
	{
		public GetDescriptor<TDocument> ExecuteOnLocalShard() => Preference("_local");
	}
}
