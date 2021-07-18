using System.Collections;
using Cysharp.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.Networking;
using UnityEngine.TestTools;
using Zenject;

namespace Project.Tests.Edit
{
    internal sealed class Example : ZenjectUnitTestFixture
    {
        [Inject] private IBoundary boundary;

        [SetUp]
        public void CommonInstall()
        {
            Container.BindInstance(Substitute.For<IBoundary>());
            Container.Inject(this);
        }

        [Test]
        public void Boundary_Receives_Call()
        {
            boundary.Do();
            boundary.Received(1).Do();
        }

        [UnityTest]
        public IEnumerator Get_Google_Succeeds() => UniTask.ToCoroutine(async () =>
        {
            var request = await UnityWebRequest.Get("www.google.com").SendWebRequest();
            Assert.That(request.result, Is.EqualTo(UnityWebRequest.Result.Success));
        });

        public interface IBoundary
        {
            void Do();
        }
    }
}
