// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch
{
	public abstract partial class BulkResponseItemBase
	{
		public abstract string Operation { get; }

		public bool IsValid
		{
			get
			{
				if (Error is not null)
					return false;

				return Operation.ToLowerInvariant() switch
				{
					"delete" => Status == 200 || Status == 404,
					"update" or "index" or "create" => Status == 200 || Status == 201,
					_ => false,
				};
			}
		}
	}
}
