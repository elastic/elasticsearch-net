using System.Runtime.Serialization;

namespace Nest
{
	public interface ISnapshotRepository
	{
		[DataMember(Name ="type")]
		string Type { get; }
	}

	public interface IRepositoryWithSettings: ISnapshotRepository
	{
		[IgnoreDataMember]
		object DelegateSettings { get; }
	}

	public interface IRepository<TSettings> : IRepositoryWithSettings
		where TSettings : class, IRepositorySettings
	{
		[DataMember(Name ="settings")]
		TSettings Settings { get; set; }
	}

	public interface IRepositorySettings { }
}
