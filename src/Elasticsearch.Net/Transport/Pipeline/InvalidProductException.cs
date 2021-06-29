// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elasticsearch.Net
{
	public class InvalidProductException : Exception
	{
		public InvalidProductException()
			: base(@"The client noticed that the server is not Elasticsearch and we do not support this unknown product.") { }
	}
}
