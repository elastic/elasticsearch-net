// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	public interface IExplainResponse<out TDocument> : IResponse
		where TDocument : class
	{
		IInlineGet<TDocument> Get { get; }
	}

	[DataContract]
	public class ExplainResponse<TDocument> : ResponseBase, IExplainResponse<TDocument>
		where TDocument : class
	{
		[DataMember(Name ="explanation")]
		public ExplanationDetail Explanation { get; internal set; }

		[DataMember(Name ="get")]
		public IInlineGet<TDocument> Get { get; internal set; }

		[DataMember(Name ="matched")]
		public bool Matched { get; internal set; }
	}
}
