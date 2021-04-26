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
	/// <summary>
	/// Allows inline and stored scripts to be executed within ingest pipelines.
	/// </summary>
	[InterfaceDataContract]
	public interface IScriptProcessor : IProcessor
	{
		/// <summary>
		/// The stored script id to refer to
		/// </summary>
		[DataMember(Name ="id")]
		string Id { get; set; }

		/// <summary>
		/// The scripting language. Defaults to painless
		/// </summary>
		[DataMember(Name ="lang")]
		string Lang { get; set; }

		/// <summary>
		/// Parameters for the script
		/// </summary>
		[DataMember(Name ="params")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<string, object>))]
		Dictionary<string, object> Params { get; set; }

		/// <summary>
		/// An inline script to be executed
		/// </summary>
		[DataMember(Name ="source")]
		string Source { get; set; }
	}

	/// <summary>
	/// Allows inline, stored, and file scripts to be executed within ingest pipelines.
	/// </summary>
	public class ScriptProcessor : ProcessorBase, IScriptProcessor
	{
		/// <summary>
		/// The stored script id to refer to
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// The scripting language. Defaults to painless
		/// </summary>
		public string Lang { get; set; }

		/// <summary>
		/// Parameters for the script
		/// </summary>
		public Dictionary<string, object> Params { get; set; }

		/// <summary> An inline script to be executed </summary>
		public string Source { get; set; }

		protected override string Name => "script";
	}

	/// <summary>
	/// Allows inline, stored, and file scripts to be executed within ingest pipelines.
	/// </summary>
	public class ScriptProcessorDescriptor
		: ProcessorDescriptorBase<ScriptProcessorDescriptor, IScriptProcessor>, IScriptProcessor
	{
		protected override string Name => "script";
		string IScriptProcessor.Id { get; set; }
		string IScriptProcessor.Lang { get; set; }
		Dictionary<string, object> IScriptProcessor.Params { get; set; }
		string IScriptProcessor.Source { get; set; }

		/// <summary>
		/// The scripting language. Defaults to painless
		/// </summary>
		public ScriptProcessorDescriptor Lang(string lang) => Assign(lang, (a, v) => a.Lang = v);

		/// <summary>
		/// The stored script id to refer to
		/// </summary>
		public ScriptProcessorDescriptor Id(string id) => Assign(id, (a, v) => a.Id = v);

		/// <summary>
		/// An inline script to be executed
		/// </summary>
		public ScriptProcessorDescriptor Source(string source) => Assign(source, (a, v) => a.Source = v);

		/// <summary>
		/// Parameters for the script
		/// </summary>
		public ScriptProcessorDescriptor Params(Dictionary<string, object> scriptParams) => Assign(scriptParams, (a, v) => a.Params = v);

		/// <summary>
		/// Parameters for the script
		/// </summary>
		public ScriptProcessorDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(paramsSelector, (a, v) => a.Params = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
