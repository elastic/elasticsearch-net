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

namespace Nest
{
	/// <summary>
	/// An analyzer tailored for japanese that is bootstrapped with defaults.
	/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
	/// </summary>
	public interface IKuromojiAnalyzer : IAnalyzer
	{
		[DataMember(Name ="mode")]
		KuromojiTokenizationMode? Mode { get; set; }

		[DataMember(Name ="user_dictionary")]
		string UserDictionary { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiAnalyzer : AnalyzerBase, IKuromojiAnalyzer
	{
		public KuromojiAnalyzer() : base("kuromoji") { }

		public KuromojiTokenizationMode? Mode { get; set; }

		public string UserDictionary { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiAnalyzerDescriptor : AnalyzerDescriptorBase<KuromojiAnalyzerDescriptor, IKuromojiAnalyzer>, IKuromojiAnalyzer
	{
		protected override string Type => "kuromoji";

		KuromojiTokenizationMode? IKuromojiAnalyzer.Mode { get; set; }
		string IKuromojiAnalyzer.UserDictionary { get; set; }

		public KuromojiAnalyzerDescriptor Mode(KuromojiTokenizationMode? mode) => Assign(mode, (a, v) => a.Mode = v);

		public KuromojiAnalyzerDescriptor UserDictionary(string userDictionary) => Assign(userDictionary, (a, v) => a.UserDictionary = v);
	}
}
