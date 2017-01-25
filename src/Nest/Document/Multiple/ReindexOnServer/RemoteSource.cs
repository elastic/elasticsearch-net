using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IRemoteSource
	{
		[JsonProperty("host")]
		Uri Host { get; set; }

		[JsonProperty("username")]
		string Username { get; set; }

		[JsonProperty("password")]
		string Password { get; set; }

    }

	public class RemoteSource : IRemoteSource
	{
		public Uri Host { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

	public class RemoteSourceDescriptor : DescriptorBase<RemoteSourceDescriptor, IRemoteSource>, IRemoteSource
	{
		Uri IRemoteSource.Host { get; set; }
		string IRemoteSource.Username { get; set; }
		string IRemoteSource.Password { get; set; }

		public RemoteSourceDescriptor Host(Uri host) => Assign(a => a.Host = host);

		public RemoteSourceDescriptor Username(string username) => Assign(a => a.Username = username);

        public RemoteSourceDescriptor Password(string password) => Assign(a => a.Password = password);
    }
}
