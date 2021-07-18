using NUnit.Framework;
using Project.UI;
using System.Collections.Generic;
using Zenject;

namespace Project.Tests.Play
{
	internal sealed class ApiTest : ZenjectIntegrationTestFixture
	{
		[Inject]
		private OpponentSymbolPresenter opponentSymbolPresenter;

		[SetUp]
		public void CommonInstall()
		{
			PreInstall();
			UIInstaller.Install(Container);
			PostInstall();
			Container.Inject(this);
		}

		[Test]
		public async void TestRandomApi()
		{
			List<uint> expectedResults = new List<uint>() { 0, 1, 2 };

			for(int i = 0; i < 100; ++i)
			{
				var result = await opponentSymbolPresenter.GenerateRandomSymbol();
				Assert.That(expectedResults.Contains(result));
			}
		}
	}
}
