// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
