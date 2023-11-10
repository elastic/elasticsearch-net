// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

internal static class ExceptionExtensions
{
	internal static T ThrowWhen<T>(this T @object, Func<T, bool> predicate, string exceptionMessage)
	{
		var x = predicate?.Invoke(@object);
		if (x.GetValueOrDefault(false))
			throw new ArgumentException(exceptionMessage);

		return @object;
	}
}
