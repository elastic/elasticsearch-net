// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Thrown when the client pre-flight check determines that the server is not a supported Elasticsearch product.
/// </summary>
public class UnsupportedProductException : Exception
{
	internal const string InvalidProductError =
		"The client noticed that the server is not a supported distribution of Elasticsearch or an unknown product.";

	internal UnsupportedProductException(string error)
		: base(error) { }
}
