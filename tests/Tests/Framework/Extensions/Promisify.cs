// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Framework.Extensions
{
	public static class Promisify
	{
		public static PromiseValue<T> Promise<T>(T o) where T : class => new PromiseValue<T>(o);

		public class PromiseValue<T> : IPromise<T> where T : class
		{
			private readonly T _o;

			public PromiseValue(T o) => _o = o;

			T IPromise<T>.Value => _o;
		}
	}
}
