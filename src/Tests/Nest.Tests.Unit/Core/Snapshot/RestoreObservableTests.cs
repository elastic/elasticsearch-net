using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Moq;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Snapshot
{
	[TestFixture]
	public class RestoreObservableTests : BaseJsonTests
	{
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RequestIsNull_Exception()
		{
			var elastickClientMock = new Mock<IElasticClient>();

			var sut = new RestoreObservable(elastickClientMock.Object, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ClientIsNull_Exception()
		{
			var sut = new RestoreObservable(null, new RestoreRequest("repository", "snapshot"));
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void IncorrectTimeInterval_Exception()
		{
			var elastickClientMock = new Mock<IElasticClient>();
			var sut = new RestoreObservable(elastickClientMock.Object, new RestoreRequest("repository", "snapshot"),
				TimeSpan.FromMilliseconds(-1));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ObserverIsNull_Exception()
		{
			var elastickClientMock = new Mock<IElasticClient>();

			var sut = new RestoreObservable(elastickClientMock.Object, new RestoreRequest("repository", "snapshot"));

			using (sut.Subscribe(null))
			{

			}
		}

		[Test]
		public void ShouldCallRestoreMethodOnce()
		{
			var elastickClientMock = new Mock<IElasticClient>();

			var sut = new RestoreObservable(elastickClientMock.Object, new RestoreRequest("repository", "snapshot"));

			sut.Subscribe(new RestoreObserver());

			elastickClientMock.Verify(x => x
				.Restore(
					It.Is<IRestoreRequest>(
						request =>
							request.Repository == "repository" && request.Snapshot == "snapshot" &&
							request.RequestParameters.ContainsKey("wait_for_completion") == true &&
							request.RequestParameters.GetQueryStringValue<bool>("wait_for_completion") == false)),
				Times.Once);
		}


		[Test]
		public void OnNext_RestoreObservableShouldNotify()
		{
			var onNextWasCalled = false;
			var done = false;
			var onNextCounter = 0;
			var elastickClientMock = ElastickClientMock(RecoveryStatusFixture("test", 1, 2));

			var sut = new RestoreObservable(elastickClientMock.Object,
				RestoreRequestFixture(), TimeSpan.FromMilliseconds(200));

			sut.Subscribe(new RestoreObserver(response =>
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
			var elastickClientMock = ElastickClientMock(RecoveryStatusFixture("test", 1, 2));

			var sut = new RestoreObservable(elastickClientMock.Object,
				RestoreRequestFixture(), TimeSpan.FromMilliseconds(5));

			Action<IRecoveryStatusResponse> onNext = response =>
			{
				done = true;
				if (disposed) onNextCalledAfterDispose = true;
			};
			using (sut.Subscribe(new RestoreObserver(onNext, exception => done = true, () => done = true)))
			{
				while (!done) Thread.Sleep(10);
			}

			disposed = true;

			Thread.Sleep(100);

			Assert.AreEqual(false, onNextCalledAfterDispose);
		}

		[Test]
		public void OnCompleted_RestoreObservableShouldNotify()
		{
			var onCompletedWasCalled = false;
			var done = false;
			var elastickClientMock = ElastickClientMock(RecoveryStatusFixture("test", 1, 1));

			var sut = new RestoreObservable(elastickClientMock.Object,
				RestoreRequestFixture());

			sut.Subscribe(new RestoreObserver(response => done = true, exception => done = true,
				() => onCompletedWasCalled = done = true));

			while (!done) Thread.Sleep(10);

			Assert.AreEqual(true, onCompletedWasCalled);
		}

		[Test]
		public void OnError_RestoreObservableShouldNotify()
		{
			var onErrorWasCalled = false;
			var done = false;
			var recoveryStatusReponseMock = new Mock<IRecoveryStatusResponse>();
			recoveryStatusReponseMock.SetupGet(x => x.IsValid).Returns(false);
			var elastickClientMock = new Mock<IElasticClient>();
			elastickClientMock.Setup(x => x.Restore(It.IsAny<IRestoreRequest>()))
				.Returns(new RestoreResponse());
			elastickClientMock.Setup(x => x.RecoveryStatus(It.IsAny<IRecoveryStatusRequest>()))
				.Returns(recoveryStatusReponseMock.Object);

			var sut = new RestoreObservable(elastickClientMock.Object,
				RestoreRequestFixture());

			sut.Subscribe(new RestoreObserver(response => done = true, exception => onErrorWasCalled = done = true, () => done = true));

			while (!done) Thread.Sleep(10);

			Assert.AreEqual(true, onErrorWasCalled);
		}

		[Test]
		[ExpectedException(typeof(RestoreException))]
		public void RestoreResponseInvalid_Exception()
		{
			var elastickClientMock = new Mock<IElasticClient>();
			var recoveryResponseMock = new Mock<IRecoveryStatusResponse>();

			recoveryResponseMock.Setup(x => x.IsValid).Returns(false);

			elastickClientMock.Setup(x => x.RecoveryStatus(It.IsAny<IRecoveryStatusRequest>()))
				.Returns(() => recoveryResponseMock.Object);

			var sut = new RestoreStatusHumbleObject(elastickClientMock.Object,
				RestoreRequestFixture());

			sut.Next += (sender, args) => Assert.Fail("");
			sut.Completed += (sender, args) => Assert.Fail("Can't complete");
			sut.Error += (sender, args) =>
			{
				throw args.Exception;
			};

			sut.CheckStatus();
		}

		[Test]
		public void ShouldCallRecoveryStatus()
		{
			var elastickClientMock = new Mock<IElasticClient>();

			var sut = new RestoreStatusHumbleObject(elastickClientMock.Object,
				RestoreRequestFixture());

			sut.CheckStatus();

			elastickClientMock.Verify(x => x
				.RecoveryStatus(
					It.Is<RecoveryStatusRequest>(
						request =>
							request.Indices.Count() == 1 &&
							request.Indices.ElementAt(0).Name == "test" &&
							request.Indices.ElementAt(0).Type == typeof(object) &&
							request.Detailed)),
				Times.AtLeastOnce);
		}

		[Test]
		public void ShouldCallRecoveryStatus_Rename()
		{
			var elastickClientMock = new Mock<IElasticClient>();

			RestoreRequest restoreRequestFixture = RestoreRequestFixture("test_tmp");

			restoreRequestFixture.RenamePattern = "_(.+)";
			restoreRequestFixture.RenameReplacement = "_restored_$1";

			var sut = new RestoreStatusHumbleObject(elastickClientMock.Object,
				restoreRequestFixture);

			sut.CheckStatus();

			elastickClientMock.Verify(x => x
				.RecoveryStatus(
					It.Is<RecoveryStatusRequest>(
						request =>
							request.Indices.Count() == 1 &&
							request.Indices.ElementAt(0).Name == Regex.Replace("test_tmp", restoreRequestFixture.RenamePattern, restoreRequestFixture.RenameReplacement) &&
							request.Indices.ElementAt(0).Type == typeof(object) &&
							request.Detailed)),
				Times.AtLeastOnce);
		}

		[Test]
		public void OnNext_WhenRecoveredLessThanTotalForAllFiles()
		{
			var elastickClientMock = ElastickClientMock(RecoveryStatusFixture("test", 1, 2));

			var sut = new RestoreStatusHumbleObject(elastickClientMock.Object,
				RestoreRequestFixture());

			sut.Next += (sender, args) => Assert.AreEqual(1, args.RecoveryStatusResponse.Indices.Count);
			sut.Completed += (sender, args) => Assert.Fail("Can't complete");
			sut.Error += (sender, args) => Assert.Fail(args.Exception.Message);

			sut.CheckStatus();
		}

		[Test]
		public void OnCompleted_WhenRecoveredEqualsToTotalForAllFiles()
		{
			var elastickClientMock = ElastickClientMock(RecoveryStatusFixture("test", 1, 1));

			var sut = new RestoreStatusHumbleObject(elastickClientMock.Object,
				RestoreRequestFixture());

			sut.Next += (sender, args) => Assert.Fail();
			sut.Completed += (sender, args) => Assert.NotNull(args.RecoveryStatusResponse);
			sut.Error += (sender, args) => Assert.Fail(args.Exception.Message);

			sut.CheckStatus();
		}

		private static RestoreRequest RestoreRequestFixture(string indexName = null)
		{
			if (string.IsNullOrEmpty(indexName)) indexName = "test";

			var indexNameMarkers = new List<IndexNameMarker>
            {
                new IndexNameMarker {Name = indexName, Type = typeof (object)}
            };
			return new RestoreRequest("repository", "snapshot")
			{
				Indices = indexNameMarkers
			};
		}

		private static Mock<IElasticClient> ElastickClientMock(Dictionary<string, RecoveryStatus> recoveryStatuses)
		{
			var elastickClientMock = new Mock<IElasticClient>();
			var restoreResponseMock = new Mock<IRestoreResponse>();
			var recoveryStatusResponseMock = new Mock<IRecoveryStatusResponse>();

			restoreResponseMock.Setup(x => x.IsValid).Returns(true);

			elastickClientMock.Setup(x => x.Restore(It.IsAny<IRestoreRequest>()))
				.Returns(() => restoreResponseMock.Object);

			recoveryStatusResponseMock.Setup(x => x.IsValid).Returns(true);
			recoveryStatusResponseMock.Setup(x => x.Indices)
				.Returns(() => recoveryStatuses);

			elastickClientMock.Setup(x => x.RecoveryStatus(It.IsAny<IRecoveryStatusRequest>()))
				.Returns(() => recoveryStatusResponseMock.Object);

			return elastickClientMock;
		}

		private Dictionary<string, RecoveryStatus> RecoveryStatusFixture(string indexName, long recovered, long total)
		{
			return new Dictionary<string, RecoveryStatus>
			{
				{ indexName, new RecoveryStatusWrapper(
					new List<ShardRecovery> 
					{ 
						new ShardRecoveryWrapper(
							new RecoveryIndexStatusWrapper(
								new RecoveryFilesWrapper(recovered, total)
							)
						) 
					}) 
				}
			};
		}

		class RecoveryFilesWrapper : RecoveryFiles
		{
			public RecoveryFilesWrapper(long recovered, long total)
			{
				PropertyInfo propertyInfo = this.GetType().GetProperty("Recovered");
				propertyInfo.SetValue(this, recovered);

				propertyInfo = this.GetType().GetProperty("Total");
				propertyInfo.SetValue(this, total);
			}
		}

		class RecoveryIndexStatusWrapper : RecoveryIndexStatus
		{
			public RecoveryIndexStatusWrapper(RecoveryFiles files)
			{
				PropertyInfo propertyInfo = this.GetType().GetProperty("Files");
				propertyInfo.SetValue(this, files);
			}
		}

		class ShardRecoveryWrapper : ShardRecovery
		{
			public ShardRecoveryWrapper(RecoveryIndexStatus recoveryIndexStatus)
			{
				PropertyInfo propertyInfo = this.GetType().GetProperty("Index");
				propertyInfo.SetValue(this, recoveryIndexStatus);
			}
		}

		class RecoveryStatusWrapper : RecoveryStatus
		{
			public RecoveryStatusWrapper(IEnumerable<ShardRecovery> shardsRecoveryWrapper)
			{
				PropertyInfo propertyInfo = this.GetType().GetProperty("Shards");
				propertyInfo.SetValue(this, shardsRecoveryWrapper);
			}
		}
	}
}