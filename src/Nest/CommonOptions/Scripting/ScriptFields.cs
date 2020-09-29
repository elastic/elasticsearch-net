// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ScriptField))]
	public interface IScriptField
	{
		[DataMember(Name = "script")]
		IScript Script { get; set; }
	}

	public class ScriptField : IScriptField
	{
		public IScript Script { get; set; }
	}

	public class ScriptFieldDescriptor
		: DescriptorBase<ScriptFieldDescriptor, IScriptField>, IScriptField
	{
		IScript IScriptField.Script { get; set; }

		public ScriptFieldDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}

	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<ScriptFields, IScriptFields, string, IScriptField>))]
	public interface IScriptFields : IIsADictionary<string, IScriptField> { }

	public class ScriptFields : IsADictionaryBase<string, IScriptField>, IScriptFields
	{
		public ScriptFields() { }

		public ScriptFields(IDictionary<string, IScriptField> container) : base(container) { }

		public ScriptFields(Dictionary<string, IScriptField> container) : base(container) { }

		public void Add(string name, IScriptField script) => BackingDictionary.Add(name, script);

		public void Add(string name, IScript script) => BackingDictionary.Add(name, new ScriptField { Script = script });
	}

	public class ScriptFieldsDescriptor : IsADictionaryDescriptorBase<ScriptFieldsDescriptor, IScriptFields, string, IScriptField>
	{
		public ScriptFieldsDescriptor() : base(new ScriptFields()) { }

		public ScriptFieldsDescriptor ScriptField(string name, Func<ScriptDescriptor, IScript> selector) =>
			Assign(name, ToScript(selector?.Invoke(new ScriptDescriptor())));

		private static IScriptField ToScript(IScript script) => script != null ? new ScriptField { Script = script } : null;
	}
}
