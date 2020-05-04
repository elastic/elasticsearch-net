// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
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
