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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface ITemplateMapping
	{
		[DataMember(Name ="aliases")]
		IAliases Aliases { get; set; }

		[DataMember(Name ="index_patterns")]
		IReadOnlyCollection<string> IndexPatterns { get; set; }

		[DataMember(Name = "mappings")]
		ITypeMapping Mappings { get; set; }

		[DataMember(Name ="order")]
		int? Order { get; set; }

		[DataMember(Name ="settings")]
		IIndexSettings Settings { get; set; }

		[DataMember(Name ="version")]
		int? Version { get; set; }
	}

	public class TemplateMapping : ITemplateMapping
	{
		public IAliases Aliases { get; set; }
		public IReadOnlyCollection<string> IndexPatterns { get; set; } = EmptyReadOnly<string>.Collection;

		public ITypeMapping Mappings { get; set; }

		public int? Order { get; set; }

		public IIndexSettings Settings { get; set; }

		public int? Version { get; set; }
	}
}
