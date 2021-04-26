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

namespace Nest
{
	[MapsApi("indices.create.json")]
	[ReadAs(typeof(CreateIndexRequest))]
	public partial interface ICreateIndexRequest : IIndexState { }

	public partial class CreateIndexRequest
	{
		private static readonly string[] ReadOnlySettings =
		{
			"index.creation_date",
			"index.uuid",
			"index.version.created",
			"index.provided_name"
		};

		public CreateIndexRequest(IndexName index, IIndexState state) : this(index)
		{
			Settings = state.Settings;
			Mappings = state.Mappings;
			RemoveReadOnlySettings(Settings);
		}

		public IAliases Aliases { get; set; }

		public ITypeMapping Mappings { get; set; }

		public IIndexSettings Settings { get; set; }

		internal static void RemoveReadOnlySettings(IIndexSettings settings)
		{
			if (settings == null) return;

			foreach (var bad in ReadOnlySettings)
			{
				if (settings.ContainsKey(bad))
					settings.Remove(bad);
			}
		}
	}

	public partial class CreateIndexDescriptor
	{
		IAliases IIndexState.Aliases { get; set; }

		ITypeMapping IIndexState.Mappings { get; set; }
		IIndexSettings IIndexState.Settings { get; set; }

		public CreateIndexDescriptor InitializeUsing(IIndexState indexSettings) => Assign(indexSettings, (a, v) =>
		{
			a.Settings = v.Settings;
			a.Mappings = v.Mappings;
			a.Aliases = v.Aliases;
			CreateIndexRequest.RemoveReadOnlySettings(a.Settings);
		});

		public CreateIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(selector, (a, v) => a.Settings = v?.Invoke(new IndexSettingsDescriptor())?.Value);

		public CreateIndexDescriptor Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			Assign(selector, (a, v) => a.Mappings = v?.Invoke(new TypeMappingDescriptor<T>()));

		public CreateIndexDescriptor Map(Func<TypeMappingDescriptor<object>, ITypeMapping> selector) =>
			Assign(selector, (a, v) => a.Mappings = v?.Invoke(new TypeMappingDescriptor<object>()));

		[Obsolete("Mappings is no longer a dictionary in 7.x, please use the simplified Map() method on this descriptor instead")]
		public CreateIndexDescriptor Mappings(Func<MappingsDescriptor, ITypeMapping> selector) =>
			Assign(selector, (a, v) => a.Mappings = v?.Invoke(new MappingsDescriptor()));

		public CreateIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(selector, (a, v) => a.Aliases = v?.Invoke(new AliasesDescriptor())?.Value);
	}
}
