/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;

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
