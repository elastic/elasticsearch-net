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
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Makes it explicit which API this request interface maps, the name of the interface informs
	/// The generator how to name related types
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface)]
	internal class MapsApiAttribute : Attribute
	{
		// ReSharper disable once UnusedParameter.Local
		public MapsApiAttribute(string restSpecName) { }
	}

	/// <summary>
	/// The preferred way to wire in a custom response formatter is for requests to override
	/// <see cref="RequestBase{TParameters}.RequestDefaults"/> however sometimes a request does not have
	/// access to enough type information. This attribute will set up the <see cref="RequestParameters{T}.CustomResponseBuilder"/>
	/// in the generated client methods instead.
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface)]
	internal class ResponseBuilderWithGeneric : Attribute
	{
		// ReSharper disable once UnusedParameter.Local
		public ResponseBuilderWithGeneric(string pathToBuilder) { }
	}
}
