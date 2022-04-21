// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public partial struct OpType : IStringable
	{
		public static OpType Index = new("index");
		public static OpType Create = new("create");

		public OpType(string value) => Value = value;

		public string Value { get; }

		public static implicit operator OpType(string v) => new(v);

		public string GetString() => Value ?? string.Empty;
	}
}
