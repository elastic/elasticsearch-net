using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Elasticsearch.Net;
using FakeItEasy.Core;
using Moq;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Snapshot
{
	[TestFixture]
	public class SnapshotObservableTests : BaseJsonTests
	{
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RequestIsNull_Exception()
		{
			var elastickClientMock = new Mock<IElasticClient>();

			var sut = new SnapshotObservable(elastickClientMock.Object, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ClientIsNull_Exception()
		{
			var sut = new SnapshotObservable(null, new SnapshotRequest("repository", "snapshot"));
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void IncorrectTimeInterval_Exception()
		{
			var elastickClientMock = new Mock<IElasticClient>();
			var sut = new SnapshotObservable(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"),
				TimeSpan.FromMilliseconds(-1));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ObserverIsNull_Exception()
		{
			var elastickClientMock = new Mock<IElasticClient>();

			var sut = new SnapshotObservable(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

			using (sut.Subscribe(null))
			{

			}
		}

		[Test]
		public void ShouldCallSnapshotMethodOnce()
		{
			var elastickClientMock = new Mock<IElasticClient>();
			elastickClientMock.Setup(x => x.Snapshot(It.IsAny<ISnapshotRequest>()))
				.Returns(new SnapshotResponse());

			var sut = new SnapshotObservable(elastickClientMock.Object,
				new SnapshotRequest("repository", "snapshot"));

			sut.Subscribe(new SnapshotObserver());

			elastickClientMock.Verify(x => x
				.Snapshot(
					It.Is<ISnapshotRequest>(
						request =>
							request.Repository == "repository" && request.Snapshot == "snapshot" &&
							request.RequestParameters.ContainsKey("wait_for_completion") == true &&
							request.RequestParameters.GetQueryStringValue<bool>("wait_for_completion") == false)),
				Times.Once);
		}

		[Test]
		public void OnNext_SnapshotObservableShouldNotify()
		{
			var onNextWasCalled = false;
			var done = false;
			var onNextCounter = 0;
			var elastickClientMock = ElastickClientMock(SnapshotStatusFixture(1, 1, 2));

			var sut = new SnapshotObservable(elastickClientMock.Object,
				new SnapshotRequest("repository", "snapshot"), TimeSpan.FromMilliseconds(100));

			sut.Subscribe(new SnapshotObserver(response =>
			{
				onNextCounter++;
				onNextWasCalled = done = true;
			}, exception => done = true, () => done = true));

			while (!done) Thread.Sleep(10);

			Assert.AreEqual(true, onNextWasCalled);
			Assert.AreEqual(1, onNextCounter);
		}

		[Test]
		public void ShouldNotNotifyOnNextWhenDisposed()
		{
			var disposed = false;
			var onNextCalledAfterDispose = false;
			var done = true;
			var elastickClientMock = ElastickClientMock(SnapshotStatusFixture(1, 1, 2));

			var sut = new SnapshotObservable(elastickClientMock.Object,
				new SnapshotRequest("repository", "snapshot"), TimeSpan.FromMilliseconds(5));

			Action<ISnapshotStatusResponse> onNext = response =>
			{
				done = true;
				if (disposed) onNextCalledAfterDispose = true;
			};
			using (sut.Subscribe(new SnapshotObserver(onNext, exception => done = true, () => done = true)))
			{
				while (!done) Thread.Sleep(10);
			}

			disposed = true;

			Thread.Sleep(100);

			Assert.AreEqual(false, onNextCalledAfterDispose);
		}

		[Test]
		public void OnCompleted_SnapshotObservableShouldNotify()
		{
			var onCompletedWasCalled = false;
			var done = false;
			var elastickClientMock = ElastickClientMock(SnapshotStatusFixture(1, 1, 1));

			var sut = new SnapshotObservable(elastickClientMock.Object,
				new SnapshotRequest("repository", "snapshot"));

			sut.Subscribe(new SnapshotObserver(response => done = true, exception => done = true,
				() => onCompletedWasCalled = done = true));

			while (!done) Thread.Sleep(10);

			Assert.AreEqual(true, onCompletedWasCalled);
		}

		[Test]
		public void OnError_SnapshotObservableShouldNotify()
		{
			var onErrorWasCalled = false;
			var done = false;
			var snapshotStatusReponseMock = new Mock<ISnapshotStatusResponse>();
			snapshotStatusReponseMock.SetupGet(x => x.IsValid).Returns(false);
			var elastickClientMock = new Mock<IElasticClient>();
			elastickClientMock.Setup(x => x.Snapshot(It.IsAny<ISnapshotRequest>()))
				.Returns(new SnapshotResponse());
			elastickClientMock.Setup(x => x.SnapshotStatus(It.IsAny<ISnapshotStatusRequest>()))
				.Returns(snapshotStatusReponseMock.Object);

			var sut = new SnapshotObservable(elastickClientMock.Object,
				new SnapshotRequest("repository", "snapshot"));

			sut.Subscribe(new SnapshotObserver(response => done = true, exception => onErrorWasCalled = done = true, () => done = true));

			while (!done) Thread.Sleep(10);

			Assert.AreEqual(true, onErrorWasCalled);
		}

		[Test]
		[ExpectedException(typeof(SnapshotException))]
		public void SnapshotResponseInvalid_Exception()
		{
			var elastickClientMock = new Mock<IElasticClient>();
			var snapshotResponseMock = new Mock<ISnapshotStatusResponse>();

			snapshotResponseMock.Setup(x => x.IsValid).Returns(false);

			elastickClientMock.Setup(x => x.SnapshotStatus(It.IsAny<ISnapshotStatusRequest>()))
				.Returns(() => snapshotResponseMock.Object);

			var sut = new SnapshotStatusHumbleObject(elastickClientMock.Object,
				new SnapshotRequest("repository", "snapshot"));

			sut.Next += (sender, args) => Assert.Fail("");
			sut.Completed += (sender, args) => Assert.Fail("Can't complete");
			sut.Error += (sender, args) =>
			{
				throw args.Exception;
			};

			sut.CheckStatus();
		}

		[Test]
		[ExpectedException(typeof(SnapshotException))]
		public void SnapshotStatusResponseInvalid_Exception()
		{
			var elastickClientMock = new Mock<IElasticClient>();
			var snapshotStatusResponseMock = new Mock<ISnapshotStatusResponse>();

			snapshotStatusResponseMock.Setup(x => x.IsValid).Returns(false);

			elastickClientMock.Setup(
				x => x.SnapshotStatus(It.IsAny<ISnapshotStatusRequest>()))
				.Returns(() => snapshotStatusResponseMock.Object);

			var sut = new SnapshotStatusHumbleObject(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

			sut.Next += (sender, args) => Assert.Fail("");
			sut.Completed += (sender, args) => Assert.Fail("Can't complete");
			sut.Error += (sender, args) => { throw args.Exception; };

			sut.CheckStatus();
		}

		[Test]
		public void ShouldCallSnapshotStatus()
		{
			var elastickClientMock = new Mock<IElasticClient>();

			var sut = new SnapshotStatusHumbleObject(elastickClientMock.Object,
				new SnapshotRequest("repository", "snapshot"));

			sut.CheckStatus();

			elastickClientMock.Verify(x => x
				.SnapshotStatus(
					It.Is<ISnapshotStatusRequest>(
						request => request.Repository == "repository" && request.Snapshot == "snapshot")),
						Times.AtLeastOnce);
		}

		[Test]
		public void OnNext_WhenDoneLessThanTotalForAllSnapshotShardStats()
		{
			var elastickClientMock = ElastickClientMock(SnapshotStatusFixture(1, 1, 2));

			var sut = new SnapshotStatusHumbleObject(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

			sut.Next += (sender, args) => Assert.AreEqual(1, args.SnapshotStatusResponse.Snapshots.Count());
			sut.Completed += (sender, args) => Assert.Fail("Can't complete");
			sut.Error += (sender, args) => Assert.Fail(args.Exception.Message);

			sut.CheckStatus();
		}

		[Test]
		public void OnNext_WhenDoneLessThanTotalForAtLeastOneSnapshotShardStats()
		{
			var snapshotStatusFixture = SnapshotStatusFixture(1, 1, 2).ToList();
			snapshotStatusFixture.Insert(1, new SnapshotStatusWrapper(new SnapshotShardsStatsWrapper(2, 2)));
			var elastickClientMock = ElastickClientMock(snapshotStatusFixture);

			var sut = new SnapshotStatusHumbleObject(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

			sut.Next += (sender, args) => Assert.AreEqual(2, args.SnapshotStatusResponse.Snapshots.Count());
			sut.Completed += (sender, args) => Assert.Fail("Can't complete");
			sut.Error += (sender, args) => Assert.Fail(args.Exception.Message);

			sut.CheckStatus();
		}

		[Test]
		public void OnCompleted_WhenDoneEqualsToTotalForAllSnapshotShardStats()
		{
			var elastickClientMock = ElastickClientMock(SnapshotStatusFixture(1, 2, 2));

			var sut = new SnapshotStatusHumbleObject(elastickClientMock.Object,
				new SnapshotRequest("repository", "snapshot"));

			sut.Next += (sender, args) => Assert.Fail();
			sut.Completed += (sender, args) => Assert.NotNull(args.SnapshotStatusResponse);
			sut.Error += (sender, args) => Assert.Fail(args.Exception.Message);

			sut.CheckStatus();
		}

		private static Mock<IElasticClient> ElastickClientMock(IEnumerable<SnapshotStatus> snapshotStatuses)
		{
			var elastickClientMock = new Mock<IElasticClient>();
			var snapshotResponseMock = new Mock<ISnapshotResponse>();
			var snapshotStatusResponseMock = new Mock<ISnapshotStatusResponse>();

			snapshotResponseMock.Setup(x => x.IsValid).Returns(true);

			elastickClientMock.Setup(x => x.Snapshot(It.IsAny<ISnapshotRequest>()))
				.Returns(() => snapshotResponseMock.Object);

			snapshotStatusResponseMock.Setup(x => x.IsValid).Returns(true);
			snapshotStatusResponseMock.Setup(x => x.Snapshots)
				.Returns(() => snapshotStatuses);

			elastickClientMock.Setup(x => x.SnapshotStatus(It.IsAny<ISnapshotStatusRequest>()))
				.Returns(() => snapshotStatusResponseMock.Object);
			return elastickClientMock;
		}

		private IEnumerable<SnapshotStatus> SnapshotStatusFixture(int count, long done, long total)
		{
			for (int i = 0; i < count; i++)
			{
				yield return new SnapshotStatusWrapper(new SnapshotShardsStatsWrapper(done, total));
			}
		}

		class SnapshotShardsStatsWrapper : SnapshotShardsStats
		{
			public SnapshotShardsStatsWrapper(long done, long total)
			{
				PropertyInfo donePropertyInfo = this.GetType().GetProperty("Done");
				donePropertyInfo.SetValue(this, done);
				PropertyInfo totalPropertyInfo = this.GetType().GetProperty("Total");
				totalPropertyInfo.SetValue(this, total);
			}
		}

		class SnapshotStatusWrapper : SnapshotStatus
		{
			public SnapshotStatusWrapper(SnapshotShardsStatsWrapper shardsStatsWrapper)
			{
				PropertyInfo propertyInfo = this.GetType().GetProperty("ShardsStats");
				propertyInfo.SetValue(this, shardsStatsWrapper);
			}
		}
	}
}