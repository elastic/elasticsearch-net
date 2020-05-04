// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{

	public interface IPreviewDatafeedResponse<out TDocument> : IResponse where TDocument : class
	{
		IReadOnlyCollection<TDocument> Data { get; }
	}

	public class PreviewDatafeedResponse<TDocument> : ResponseBase, IPreviewDatafeedResponse<TDocument> where TDocument : class
	{
		public IReadOnlyCollection<TDocument> Data { get; internal set; } = EmptyReadOnly<TDocument>.Collection;
	}
}
