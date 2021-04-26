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

namespace Nest
{
	[MapsApi("get_source.json")]
	[ResponseBuilderWithGeneric("SourceRequestResponseBuilder<TDocument>.Instance")]
	public partial interface ISourceRequest { }

	// ReSharper disable UnusedTypeParameter
	public partial interface ISourceRequest<TDocument> where TDocument : class { }

	public partial class SourceRequest { }

	// ReSharper disable UnusedTypeParameter
	public partial class SourceRequest<TDocument> where TDocument : class { }

	public partial class SourceDescriptor<TDocument> where TDocument : class
	{
		public SourceDescriptor<TDocument> ExecuteOnLocalShard() => Preference("_local");
	}
}
