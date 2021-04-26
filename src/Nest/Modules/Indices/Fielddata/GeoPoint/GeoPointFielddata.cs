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
	public interface IGeoPointFielddata : IFielddata
	{
		[DataMember(Name ="format")]
		GeoPointFielddataFormat? Format { get; set; }

		[DataMember(Name ="precision")]
		Distance Precision { get; set; }
	}

	public class GeoPointFielddata : FielddataBase, IGeoPointFielddata
	{
		public GeoPointFielddataFormat? Format { get; set; }
		public Distance Precision { get; set; }
	}

	public class GeoPointFielddataDescriptor
		: FielddataDescriptorBase<GeoPointFielddataDescriptor, IGeoPointFielddata>, IGeoPointFielddata
	{
		GeoPointFielddataFormat? IGeoPointFielddata.Format { get; set; }
		Distance IGeoPointFielddata.Precision { get; set; }

		public GeoPointFielddataDescriptor Format(GeoPointFielddataFormat? format) => Assign(format, (a, v) => a.Format = v);

		public GeoPointFielddataDescriptor Precision(Distance distance) => Assign(distance, (a, v) => a.Precision = v);
	}
}
