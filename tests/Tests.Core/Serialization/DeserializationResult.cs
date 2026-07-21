// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Tests.Core.Serialization
{
	public class DeserializationResult<T> : SerializationResult
	{
		public T Result { get; set; }

		public override string ToString()
		{
			var s = $"Deserialization has result: {Result != null}";
			s += Environment.NewLine;
			s += base.ToString();
			return s;
		}
	}
}
