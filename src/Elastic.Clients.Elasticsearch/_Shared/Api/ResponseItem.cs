// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public abstract partial class ResponseItem
{
	public abstract string Operation { get; }

	public bool IsValid
	{
		get
		{
			if (Error is not null)
				return false;

			var operation = Operation;

			if (operation.Equals("delete", StringComparison.OrdinalIgnoreCase))
				return Status is 200 or 404;

			if (operation.Equals("create", StringComparison.OrdinalIgnoreCase) ||
				operation.Equals("update", StringComparison.OrdinalIgnoreCase) ||
				operation.Equals("index", StringComparison.OrdinalIgnoreCase))
			{
				return Status is 200 or 201;
			}

			return false;
		}
	}
}
