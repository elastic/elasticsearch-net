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
using System.Collections.Concurrent;

namespace Nest
{
	public class RelationNameResolver
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly ConcurrentDictionary<Type, string> _relationNames = new ConcurrentDictionary<Type, string>();

		public RelationNameResolver(IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			_connectionSettings = connectionSettings;
		}

		public string Resolve<T>() where T : class => Resolve(typeof(T));

		public string Resolve(RelationName t) => t?.Name ?? ResolveType(t?.Type);

		private string ResolveType(Type type)
		{
			if (type == null) return null;

			string typeName;

			if (_relationNames.TryGetValue(type, out typeName))
				return typeName;

			if (_connectionSettings.DefaultRelationNames.TryGetValue(type, out typeName))
			{
				_relationNames.TryAdd(type, typeName);
				return typeName;
			}

			var att = ElasticsearchTypeAttribute.From(type);
			if (att != null && !att.RelationName.IsNullOrEmpty())
				typeName = att.RelationName;
			else
				typeName = type.Name.ToLowerInvariant();

			_relationNames.TryAdd(type, typeName);
			return typeName;
		}
	}
}
