// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetTaskResponse : ResponseBase
	{
		[DataMember(Name = "completed")]
		public bool Completed { get; internal set; }

		[DataMember(Name = "task")]
		public TaskInfo Task { get; internal set; }

		[DataMember(Name = "response")]
		internal LazyDocument Response { get; set; }

		/// <summary>
		/// Gets the response for the request that the task represents, if available.
		/// Because the response will have no associated <see cref="ApiCallDetails"/>, the value
		/// of <see cref="IResponse.IsValid"/> should not be used.
		/// </summary>
		public TResponse GetResponse<TResponse>() where TResponse : class, IResponse => Response?.AsUsingRequestResponseSerializer<TResponse>();
	}
}
