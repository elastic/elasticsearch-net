using System;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public class TaskId : IUrlParameter, IEquatable<TaskId>
	{

		public string NodeId { get; }
		public long TaskNumber { get; }
		public string FullyQualifiedId => $"{NodeId}:{TaskNumber.ToString(CultureInfo.InvariantCulture)}";

		/// <summary>
		/// A task id exists in the form <node_id>:<task_id>
		/// </summary>
		/// <param name="taskId"></param>
		public TaskId(string taskId)
		{
			if (string.IsNullOrWhiteSpace(taskId))
				throw new ArgumentException("TaskId can not be an empty string", nameof(taskId));

			var tokens = taskId.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length != 2)
				throw new ArgumentException($"TaskId:{taskId} not in expected format of <node_id>:<task_id>", nameof(taskId));

			this.NodeId = tokens[0];
			long t;
			if (!long.TryParse(tokens[1], NumberStyles.Any, CultureInfo.InvariantCulture, out t) || t < -1 || t == 0)
				throw new ArgumentException($"TaskId task component:{tokens[1]} could not be parsed to long or is out of range", nameof(taskId));
			this.TaskNumber = t;
		}

		public override string ToString() => FullyQualifiedId;

		public string GetString(IConnectionConfigurationValues settings) => FullyQualifiedId;

		public static implicit operator TaskId(string taskId) => new TaskId(taskId);

		public bool Equals(TaskId other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(NodeId, other.NodeId) && TaskNumber == other.TaskNumber;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((TaskId) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (NodeId.GetHashCode()*397) ^ TaskNumber.GetHashCode();
			}
		}

		public static bool operator ==(TaskId left, TaskId right) => Equals(left, right);

		public static bool operator !=(TaskId left, TaskId right) => !Equals(left, right);
	}
}
