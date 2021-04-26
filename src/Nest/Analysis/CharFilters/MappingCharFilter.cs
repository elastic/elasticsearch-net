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
	/// <summary>
	/// A char filter of type mapping replacing characters of an analyzed text with given mapping.
	/// </summary>
	public interface IMappingCharFilter : ICharFilter
	{
		[DataMember(Name = "mappings")]
		IEnumerable<string> Mappings { get; set; }

		[DataMember(Name = "mappings_path")]
		string MappingsPath { get; set; }
	}

	/// <inheritdoc />
	public class MappingCharFilter : CharFilterBase, IMappingCharFilter
	{
		public MappingCharFilter() : base("mapping") { }

		/// <inheritdoc />
		public IEnumerable<string> Mappings { get; set; }

		/// <inheritdoc />
		public string MappingsPath { get; set; }
	}

	/// <inheritdoc />
	public class MappingCharFilterDescriptor
		: CharFilterDescriptorBase<MappingCharFilterDescriptor, IMappingCharFilter>, IMappingCharFilter
	{
		protected override string Type => "mapping";
		IEnumerable<string> IMappingCharFilter.Mappings { get; set; }
		string IMappingCharFilter.MappingsPath { get; set; }

		/// <inheritdoc />
		public MappingCharFilterDescriptor Mappings(params string[] mappings) =>
			Assign(mappings, (a, v) => a.Mappings = v);

		/// <inheritdoc />
		public MappingCharFilterDescriptor Mappings(IEnumerable<string> mappings) =>
			Assign(mappings, (a, v) => a.Mappings = v);

		/// <inheritdoc />
		public MappingCharFilterDescriptor MappingsPath(string path) =>
			Assign(path, (a, v) => a.MappingsPath = v);
	}
}
