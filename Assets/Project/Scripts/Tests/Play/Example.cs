using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Project.UI;
using UnityEngine.Networking;
using UnityEngine.TestTools;
using Zenject;

namespace Project.Tests.Play
{
    internal sealed class Example : ZenjectIntegrationTestFixture
    {
        [Inject] private PlayerInputPresenter playerInputPresenter;
        
        [Inject] private PlayerSymbolPresenter playerSymbolPresenter;
        [Inject] private OpponentSymbolPresenter opponentSymbolPresenter;

        [Inject] private CountdownPresenter countdownPresenter;
        [Inject] private ResultTextPresenter resultPresenter;
        
        [SetUp]
        public void CommonInstall()
        {
            PreInstall();
            UIInstaller.Install(Container);
            PostInstall();
            Container.Inject(this);
        }

        [UnityTest]
        public IEnumerator Installation_Succeeds()
        {
            yield break;
        }

        [UnityTest]
        public IEnumerator Get_Google_Succeeds() => UniTask.ToCoroutine(async () =>
        {
            var request = await UnityWebRequest.Get("www.google.com").SendWebRequest();
            Assert.That(request.result, Is.EqualTo(UnityWebRequest.Result.Success));
        });
    }
}