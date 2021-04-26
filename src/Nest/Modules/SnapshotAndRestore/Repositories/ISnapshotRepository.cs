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

namespace Nest
{
	/// <summary>
	/// A snapshot repository
	/// </summary>
	public interface ISnapshotRepository
	{
		[DataMember(Name ="type")]
		string Type { get; }
	}

	/// <summary>
	/// A snapshot repository with settings
	/// </summary>
	public interface IRepositoryWithSettings: ISnapshotRepository
	{
		/// <summary>
		/// The repository settings
		/// </summary>
		[IgnoreDataMember]
		object DelegateSettings { get; }
	}

	/// <summary>
	/// A snapshot repository with typed settings
	/// </summary>
	public interface IRepository<TSettings> : IRepositoryWithSettings
		where TSettings : class, IRepositorySettings
	{
		/// <summary>
		/// The repository settings
		/// </summary>
		[DataMember(Name ="settings")]
		TSettings Settings { get; set; }
	}

	/// <summary>
	/// Snapshot repository settings
	/// </summary>
	public interface IRepositorySettings { }
}
