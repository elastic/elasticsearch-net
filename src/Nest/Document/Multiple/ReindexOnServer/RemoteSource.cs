// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IRemoteSource
	{
		[DataMember(Name ="host")]
		Uri Host { get; set; }

		[DataMember(Name ="password")]
		string Password { get; set; }

		[DataMember(Name ="username")]
		string Username { get; set; }

		[DataMember(Name ="socket_timeout")]
		Time SocketTimeout { get; set; }

		[DataMember(Name ="connect_timeout")]
		Time ConnectTimeout { get; set; }
	}

	public class RemoteSource : IRemoteSource
	{
		public Uri Host { get; set; }

		public string Password { get; set; }

		public string Username { get; set; }

		public Time SocketTimeout { get; set; }

		public Time ConnectTimeout { get; set; }
	}

	public class RemoteSourceDescriptor : DescriptorBase<RemoteSourceDescriptor, IRemoteSource>, IRemoteSource
	{
		Uri IRemoteSource.Host { get; set; }
		string IRemoteSource.Password { get; set; }
		string IRemoteSource.Username { get; set; }

		Time IRemoteSource.SocketTimeout { get; set; }

		Time IRemoteSource.ConnectTimeout { get; set; }

		public RemoteSourceDescriptor Host(Uri host) => Assign(host, (a, v) => a.Host = v);

		public RemoteSourceDescriptor Username(string username) => Assign(username, (a, v) => a.Username = v);

		public RemoteSourceDescriptor Password(string password) => Assign(password, (a, v) => a.Password = v);

		public RemoteSourceDescriptor SocketTimeout(Time socketTimeout) => Assign(socketTimeout, (a, v) => a.SocketTimeout = v);

		public RemoteSourceDescriptor ConnectTimeout(Time connectTimeout) => Assign(connectTimeout, (a, v) => a.ConnectTimeout = v);
	}
}
