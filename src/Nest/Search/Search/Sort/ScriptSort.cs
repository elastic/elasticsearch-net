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
using Nest.Utf8Json;

namespace Nest
{
	[ReadAs(typeof(ScriptSort))]
	[InterfaceDataContract]
	public interface IScriptSort : ISort
	{
		[DataMember(Name ="script")]
		IScript Script { get; set; }

		[DataMember(Name ="type")]
		string Type { get; set; }
	}

	public class ScriptSort : SortBase, IScriptSort
	{
		public string Language { get; set; }
		public IScript Script { get; set; }

		public string Type { get; set; }
		protected override Field SortKey => "_script";
	}

	public class ScriptSortDescriptor<T> : SortDescriptorBase<ScriptSortDescriptor<T>, IScriptSort, T>, IScriptSort
		where T : class
	{
		protected override Field SortKey => "_script";

		IScript IScriptSort.Script { get; set; }

		string IScriptSort.Type { get; set; }

		public virtual ScriptSortDescriptor<T> Type(string type) => Assign(type, (a, v) => a.Type = v);

		public ScriptSortDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
