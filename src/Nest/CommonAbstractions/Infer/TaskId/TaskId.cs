using System;
using System.Diagnostics;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class TaskId : IUrlParameter, IEquatable<TaskId>
	{
		public string NodeId { get; }
		public long TaskNumber { get; }
		public string FullyQualifiedId { get; }

		private string DebugDisplay => FullyQualifiedId;

		/// <summary>
		/// A task id exists in the form [node_id]:[task_id]
		/// </summary>
		/// <param name="taskId">the task identifier</param>
		public TaskId(string taskId)
		{
			if (string.IsNullOrWhiteSpace(taskId))
				throw new ArgumentException("TaskId can not be an empty string", nameof(taskId));

			var tokens = taskId.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length != 2)
				throw new ArgumentException($"TaskId:{taskId} not in expected format of <node_id>:<task_id>", nameof(taskId));

			this.NodeId = tokens[0];
			this.FullyQualifiedId = taskId;

			if (!long.TryParse(tokens[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var t) || t < -1 || t == 0)
				throw new ArgumentException($"TaskId task component:{tokens[1]} could not be parsed to long or is out of range", nameof(taskId));

			this.TaskNumber = t;
		}

		public override string ToString() => FullyQualifiedId;

		public string GetString(IConnectionConfigurationValues settings) => FullyQualifiedId;

		public static implicit operator TaskId(string taskId) => taskId.IsNullOrEmpty() ? null : new TaskId(taskId);

		public static bool operator ==(TaskId left, TaskId right) => Equals(left, right);

		public static bool operator !=(TaskId left, TaskId right) => !Equals(left, right);

		public bool Equals(TaskId other) => EqualsString(other?.FullyQualifiedId);

		public override bool Equals(object obj) =>
			obj != null && obj is string s ? this.EqualsString(s) : (obj is TaskId i) && this.EqualsString(i.FullyQualifiedId);

		private bool EqualsString(string other) => !other.IsNullOrEmpty() && other == this.FullyQualifiedId;

		public override int GetHashCode()
		{
			unchecked
			{
				return (NodeId.GetHashCode()*397) ^ TaskNumber.GetHashCode();
			}
		}
	}
}
