// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	internal static class RelationNameExtensions
	{
		internal static bool IsConditionless(this RelationName marker) => marker == null || marker.Name.IsNullOrEmpty() && marker.Type == null;
	}
}
