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

using System.IO;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A factory for creating memory streams
	/// </summary>
	public interface IMemoryStreamFactory
	{
		/// <summary>
		/// Creates a memory stream
		/// </summary>
		MemoryStream Create();

		/// <summary>
		/// Creates a memory stream with the bytes written to the stream
		/// </summary>
		MemoryStream Create(byte[] bytes);

		/// <summary>
		/// Creates a memory stream with the bytes written to the stream
		/// </summary>
		MemoryStream Create(byte[] bytes, int index, int count);
	}
}
