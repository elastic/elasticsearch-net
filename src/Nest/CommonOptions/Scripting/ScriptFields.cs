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
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
