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
using System.Runtime.Serialization;

namespace Examples.Models
{
	public class Tweet
	{
		public int? Counter { get; set; }

		public int? Id { get; set; }

		public string Name { get; set; }

		public string Message { get; set; }

		[DataMember(Name = "post_date")]
		public DateTime? PostDate { get; set; }

		public string[] Tags { get; set; }

		public string User { get; set; }

		[DataMember(Name = "user_name")]
		public string UserName { get; set; }
		public long? Likes { get; set; }
		public int? Age { get; set; }
		public string Email { get; set; }

		public string Title { get; set; }

		public DateTime? Date { get; set; }
	}
}
