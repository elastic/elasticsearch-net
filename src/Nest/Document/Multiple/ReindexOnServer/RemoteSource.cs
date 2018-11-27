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
	}

	public class RemoteSource : IRemoteSource
	{
		public Uri Host { get; set; }

		public string Password { get; set; }

		public string Username { get; set; }
	}

	public class RemoteSourceDescriptor : DescriptorBase<RemoteSourceDescriptor, IRemoteSource>, IRemoteSource
	{
		Uri IRemoteSource.Host { get; set; }
		string IRemoteSource.Password { get; set; }
		string IRemoteSource.Username { get; set; }

		public RemoteSourceDescriptor Host(Uri host) => Assign(a => a.Host = host);

		public RemoteSourceDescriptor Username(string username) => Assign(a => a.Username = username);

		public RemoteSourceDescriptor Password(string password) => Assign(a => a.Password = password);
	}
}
