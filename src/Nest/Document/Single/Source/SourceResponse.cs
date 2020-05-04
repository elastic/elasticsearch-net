// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	public interface ISourceResponse<out TDocument> : IResponse
	{
		TDocument Body { get; }
	}

	public class SourceResponse<TDocument> : ResponseBase, ISourceResponse<TDocument>
	{
		public TDocument Body { get; internal set; }
	}
}
