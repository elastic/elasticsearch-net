// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.


using System;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

internal static class RequestParametersExtensions
{
	internal static void SetRequestMetaData(this RequestParameters parameters, RequestMetaData requestMetaData)
	{
		if (parameters is null)
			throw new ArgumentNullException(nameof(parameters));

		if (requestMetaData is null)
			throw new ArgumentNullException(nameof(requestMetaData));

		parameters.RequestConfiguration ??= new RequestConfiguration();

		parameters.RequestConfiguration.RequestMetaData = requestMetaData;
	}
}
