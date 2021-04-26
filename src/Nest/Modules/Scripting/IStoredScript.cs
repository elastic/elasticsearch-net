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
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A Stored script
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(StoredScript))]
	public interface IStoredScript
	{
		/// <summary>
		/// The script language
		/// </summary>
		[DataMember(Name = "lang")]
		string Lang { get; set; }

		/// <summary>
		/// The script source
		/// </summary>
		[DataMember(Name = "source")]
		string Source { get; set; }
	}

	/// <inheritdoc />
	public class StoredScript : IStoredScript
	{
		//used for deserialization
		internal StoredScript() { }

		/// <summary>
		/// Instantiates a new instance of <see cref="StoredScript" />
		/// </summary>
		/// <param name="lang">Script language</param>
		/// <param name="source">Script source</param>
		protected StoredScript(string lang, string source)
		{
			IStoredScript self = this;
			self.Lang = lang;
			self.Source = source;
		}

		[DataMember(Name = "lang")]
		string IStoredScript.Lang { get; set; }

		[DataMember(Name = "source")]
		string IStoredScript.Source { get; set; }
	}

	public class PainlessScript : StoredScript
	{
		private static readonly string Lang = ScriptLang.Painless.GetStringValue();

		public PainlessScript(string source) : base(Lang, source) { }
	}

	public class LuceneExpressionScript : StoredScript
	{
		private static readonly string Lang = ScriptLang.Expression.GetStringValue();

		public LuceneExpressionScript(string source) : base(Lang, source) { }
	}

	public class MustacheScript : StoredScript
	{
		private static readonly string Lang = ScriptLang.Mustache.GetStringValue();

		public MustacheScript(string source) : base(Lang, source) { }
	}

	public class StoredScriptDescriptor : DescriptorBase<StoredScriptDescriptor, IStoredScript>, IStoredScript
	{
		string IStoredScript.Lang { get; set; }
		string IStoredScript.Source { get; set; }

		public StoredScriptDescriptor Source(string source) => Assign(source, (a, v) => a.Source = v);

		public StoredScriptDescriptor Lang(string lang) => Assign(lang, (a, v) => a.Lang = v);

		public StoredScriptDescriptor Lang(ScriptLang lang) => Assign(lang.GetStringValue(), (a, v) => a.Lang = v);
	}
}
