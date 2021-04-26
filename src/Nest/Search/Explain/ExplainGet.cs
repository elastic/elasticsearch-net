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
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(InlineGet<>))]
	public interface IInlineGet<out TDocument> where TDocument : class
	{
		[DataMember(Name = "fields")]
		FieldValues Fields { get; }

		[DataMember(Name = "found")]
		bool Found { get; }

		[DataMember(Name = "_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		TDocument Source { get; }
	}


	public class InlineGet<TDocument> : IInlineGet<TDocument>
		where TDocument : class
	{
		public FieldValues Fields { get; internal set; }

		public bool Found { get; internal set; }

		public TDocument Source { get; internal set; }
	}
}
