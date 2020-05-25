// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;

namespace Nest
{
	public class MetadataRule : FieldRuleBase
	{
		public MetadataRule(string key, object value) => Metadata = Tuple.Create(key, value);
	}
}
