// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("clear_scroll.json")]
	public partial interface IClearScrollRequest
	{
		[DataMember(Name ="scroll_id")]
		IEnumerable<string> ScrollIds { get; set; }
	}

	public partial class ClearScrollRequest
	{
		public ClearScrollRequest(IEnumerable<string> scrollIds) => ScrollIds = scrollIds;

		public ClearScrollRequest(string scrollId) => ScrollIds = new[] { scrollId };

		public IEnumerable<string> ScrollIds { get; set; }
	}

	public partial class ClearScrollDescriptor
	{
		IEnumerable<string> IClearScrollRequest.ScrollIds { get; set; }

		public ClearScrollDescriptor ScrollId(params string[] scrollIds) => Assign(scrollIds, (a, v) => a.ScrollIds = v);
	}
}
