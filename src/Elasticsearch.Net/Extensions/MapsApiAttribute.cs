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
using System.Collections.ObjectModel;
using System.Linq;

namespace Elasticsearch.Net
{
	internal class MapsApiAttribute : Attribute
	{
		public MapsApiAttribute(string restSpecName) : this(restSpecName, null) { }

		public MapsApiAttribute(string restSpecName, string parametersCommaSeparated = null)
		{
			RestSpecName = restSpecName;
			Parameters = new OrderedHashSet();
			if (!string.IsNullOrWhiteSpace(parametersCommaSeparated))
			{
				var args = parametersCommaSeparated.Split(',').Select(s => s.Trim());
				foreach(var a in args) Parameters.Add(a);
			}
		}

		public string RestSpecName { get; }
		public KeyedCollection<string, string> Parameters { get; }

		public class OrderedHashSet : KeyedCollection<string, string>
		{
			protected override string GetKeyForItem(string item) => item;
		}

	}
}
