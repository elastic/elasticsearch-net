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

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("put_script.json")]
	public partial interface IPutScriptRequest
	{
		[DataMember(Name ="script")]
		IStoredScript Script { get; set; }
	}

	public partial class PutScriptRequest
	{
		public IStoredScript Script { get; set; }
	}

	public partial class PutScriptDescriptor
	{
		IStoredScript IPutScriptRequest.Script { get; set; }

		public PutScriptDescriptor Script(Func<StoredScriptDescriptor, IStoredScript> selector) =>
			Assign(selector, (a, v) => a.Script = v?.Invoke(new StoredScriptDescriptor()));

		/// <summary>
		/// A Painless language script
		/// </summary>
		public PutScriptDescriptor Painless(string source) => Assign(new PainlessScript(source), (a, v) => a.Script = v);

		/// <summary>
		/// A Lucene expression language script
		/// </summary>
		public PutScriptDescriptor LuceneExpression(string source) => Assign(new LuceneExpressionScript(source), (a, v) => a.Script = v);

		/// <summary>
		/// A Mustache template language script
		/// </summary>
		public PutScriptDescriptor Mustache(string source) => Assign(new MustacheScript(source), (a, v) => a.Script = v);
	}
}
