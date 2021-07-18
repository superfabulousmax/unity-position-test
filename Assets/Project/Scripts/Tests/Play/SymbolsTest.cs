using NUnit.Framework;
using Project.UI;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.Tests.Play
{
	internal sealed class SymbolsTest : ZenjectIntegrationTestFixture
	{
		[Inject]
		private PlayerSymbolPresenter playerSymbolPresenter;

		[SetUp]
		public void CommonInstall()
		{
			PreInstall();
			UIInstaller.Install(Container);
			PostInstall();
			Container.Inject(this);
		}

		[Test]
		public void TestRockSpriteNotNull()
		{
			TestSpriteNotNull("rock");
		}

		[Test]
		public void TestPaperSpriteNotNull()
		{
			TestSpriteNotNull("paper");
		}

		[Test]
		public void TestScissorsSpriteNotNull()
		{
			TestSpriteNotNull("scissors");
		}

		public void TestSpriteNotNull(string input)
		{
			var sprite = Resources.Load<Sprite>($"Images/{input}");
			Assert.False(sprite == null, $"Test that {input} sprite is not null");
		}

		[Test]
		public void TestRockInputReturnsCorrectSymbol()
		{
			TestInputCorrect("rock");
		}

		[Test]
		public void TestPaperInputReturnsCorrectSymbol()
		{
			TestInputCorrect("paper");
		}

		[Test]
		public void TestScissorsInputReturnsCorrectSymbol()
		{
			TestInputCorrect("scissors");
		}

		public void TestInputCorrect(string input)
		{
			var sprite = Resources.Load<Sprite>($"Images/{input}");
			var index = (uint)playerSymbolPresenter.GetValidInputs().ToList().IndexOf(input);
			playerSymbolPresenter.ChangeImage(index);
			Assert.True(playerSymbolPresenter.Image.sprite != null);
			Assert.That(playerSymbolPresenter.Image.sprite, Is.EqualTo(sprite));
		}
	}
}