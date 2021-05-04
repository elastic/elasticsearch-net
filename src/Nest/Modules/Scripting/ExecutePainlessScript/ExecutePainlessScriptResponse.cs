// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public interface IExecutePainlessScriptResponse<out TResult> : IResponse
	{
		TResult Result { get; }
	}

	public class ExecutePainlessScriptResponse<TResult> : ResponseBase, IExecutePainlessScriptResponse<TResult>
	{
		[DataMember(Name ="result")]
		public TResult Result { get; set; }
	}
}
