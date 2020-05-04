// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	public class PutRoleResponse : ResponseBase
	{
		[DataMember(Name ="role")]
		public PutRoleStatus Role { get; internal set; }
	}

	public class PutRoleStatus
	{
		[DataMember(Name ="created")]
		public bool Created { get; internal set; }
	}
}
