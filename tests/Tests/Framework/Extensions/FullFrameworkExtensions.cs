// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace Tests.Framework.Extensions
{
	public static class FullFrameworkExtensions
	{
		internal static void Deconstruct<T, TValue>(this KeyValuePair<T, TValue> pair, out T key, out TValue value)
		{
			key = pair.Key;
			value = pair.Value;
		}
	}
}
