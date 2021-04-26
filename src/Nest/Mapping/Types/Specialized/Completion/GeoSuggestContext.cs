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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IGeoSuggestContext : ISuggestContext
	{
		[DataMember(Name = "precision")]
		Union<Distance, int> Precision { get; set; }
	}

	[DataContract]
	public class GeoSuggestContext : SuggestContextBase, IGeoSuggestContext
	{
		public Union<Distance, int> Precision { get; set; }
		public override string Type => "geo";
	}

	[DataContract]
	public class GeoSuggestContextDescriptor<T>
		: SuggestContextDescriptorBase<GeoSuggestContextDescriptor<T>, IGeoSuggestContext, T>, IGeoSuggestContext
		where T : class
	{
		protected override string Type => "geo";

		Union<Distance, int> IGeoSuggestContext.Precision { get; set; }

		public GeoSuggestContextDescriptor<T> Precision(Distance precision) => Assign(precision, (a, v) => a.Precision = v);

		public GeoSuggestContextDescriptor<T> Precision(int? precision) => Assign(precision, (a, v) => a.Precision = v);
	}
}
