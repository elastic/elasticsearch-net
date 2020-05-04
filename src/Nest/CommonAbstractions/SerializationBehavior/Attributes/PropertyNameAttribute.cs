// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyNameAttribute : Attribute, IJsonProperty
	{
		public PropertyNameAttribute(string name) => Name = name;

		public string Name { get; set; }
		public int Order { get; } = -1;
		public bool Ignore { get; set; }
		public bool? AllowPrivate { get; set; } = true;
	}
}
