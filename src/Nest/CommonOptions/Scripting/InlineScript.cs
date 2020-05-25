// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IInlineScript : IScript
	{
		[DataMember(Name ="source")]
		string Source { get; set; }
	}

	public class InlineScript : ScriptBase, IInlineScript
	{
		public InlineScript(string script) => Source = script;

		public string Source { get; set; }

		public static implicit operator InlineScript(string script) => new InlineScript(script);
	}

	public class InlineScriptDescriptor
		: ScriptDescriptorBase<InlineScriptDescriptor, IInlineScript>, IInlineScript
	{
		public InlineScriptDescriptor() { }

		public InlineScriptDescriptor(string script) => Self.Source = script;

		string IInlineScript.Source { get; set; }

		public InlineScriptDescriptor Source(string script) => Assign(script, (a, v) => a.Source = v);
	}
}
