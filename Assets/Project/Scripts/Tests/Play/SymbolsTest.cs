using NUnit.Framework;
using Project.UI;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.Tests.Play
{
	internal sealed class SymbolsTest : ZenjectIntegrationTestFixture
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

		[Test]
		public void TestRockSpriteNotNull()
		{
			var name = "rock";
			var rockSprite = Resources.Load<Sprite>($"Images/{name}");
			Assert.False(rockSprite == null, $"Test that {name} sprite is not null");
		}

		[Test]
		public void TestPaperSpriteNotNull()
		{
			var name = "paper";
			var paperSprite = Resources.Load<Sprite>($"Images/{name}");
			Assert.False(paperSprite == null, $"Test that {name} sprite is not null");
		}

		[Test]
		public void TestScissorsSpriteNotNull()
		{
			var name = "scissors";
			var scissorsSprite = Resources.Load<Sprite>($"Images/{name}");
			Assert.False(scissorsSprite == null, $"Test that {name} sprite is not null");
		}

		[Test]
		public void TestRockInputReturnsCorrectSymbol()
		{
			var name = "rock";
			var rockSprite = Resources.Load<Sprite>($"Images/{name}");
			var index = (uint)playerSymbolPresenter.GetValidInputs().ToList().IndexOf(name);
			playerSymbolPresenter.ChangeImage(index);
			Assert.True(playerSymbolPresenter.Image.sprite != null);
			Assert.That(playerSymbolPresenter.Image.sprite, Is.EqualTo(rockSprite));
		}

		[Test]
		public void TestPaperInputReturnsCorrectSymbol()
		{
			var name = "paper";
			var paperSprite = Resources.Load<Sprite>($"Images/{name}");
			var index = (uint)playerSymbolPresenter.GetValidInputs().ToList().IndexOf(name);
			playerSymbolPresenter.ChangeImage(index);
			Assert.True(playerSymbolPresenter.Image.sprite != null);
			Assert.That(playerSymbolPresenter.Image.sprite, Is.EqualTo(paperSprite));
		}

		[Test]
		public void TestScissorsInputReturnsCorrectSymbol()
		{
			var name = "scissors";
			var scissorsSprite = Resources.Load<Sprite>($"Images/{name}");
			var index = (uint)playerSymbolPresenter.GetValidInputs().ToList().IndexOf(name);
			playerSymbolPresenter.ChangeImage(index);
			Assert.True(playerSymbolPresenter.Image.sprite != null);
			Assert.That(playerSymbolPresenter.Image.sprite, Is.EqualTo(scissorsSprite));
		}
	}
}