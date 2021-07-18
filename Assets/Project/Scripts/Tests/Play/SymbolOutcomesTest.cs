using NUnit.Framework;
using System;
using Zenject;
using static SymbolOutcomeCalculator;

namespace Project.Tests.Play
{
	internal sealed class SymbolOutcomesTest : ZenjectIntegrationTestFixture
	{
		[Inject]
		private SymbolOutcomeFactory symbolOutcomeFactory;

		[SetUp]
		public void CommonInstall()
		{
			PreInstall();
			BehaviourInstaller.Install(Container);
			PostInstall();
			Container.Inject(this);
		}

		[Test]
		public void TestRockOutCome()
		{
			TestOutcome(Symbols.Rock, typeof(RockOutcomeCaculator));
		}

		[Test]
		public void TestPaperOutCome()
		{
			TestOutcome(Symbols.Paper, typeof(PaperOutcomeCaculator));
		}

		[Test]
		public void TestScissorsOutCome()
		{
			TestOutcome(Symbols.Scissors, typeof(ScissorsOutcomeCaculator));
		}

		public void TestOutcome(Symbols symbol, Type expectedType)
		{
			var outcomeCalculator = symbolOutcomeFactory.Create((uint)symbol);
			Assert.True(outcomeCalculator != null, $"Check that the {symbol} symbol is not null");
			Assert.That(outcomeCalculator.GetType(), Is.EqualTo(expectedType));
		}

		[Test]
		public void TestRockVsScissors()
		{
			TestVersus(Symbols.Rock, Symbols.Scissors, "Player Win!");
		}

		[Test]
		public void TestScissorsVsPaper()
		{
			TestVersus(Symbols.Scissors, Symbols.Paper, "Player Win!");
		}

		[Test]
		public void TestPaperVsRock()
		{
			TestVersus(Symbols.Paper, Symbols.Rock, "Player Win!");
		}

		[Test]
		public void TestScissorsVsRock()
		{
			TestVersus(Symbols.Scissors, Symbols.Rock, "AI Win!");
		}

		[Test]
		public void TestPaperVsScissors()
		{
			TestVersus(Symbols.Paper, Symbols.Scissors, "AI Win!");
		}

		[Test]
		public void TestRockVsPaper()
		{
			TestVersus(Symbols.Rock, Symbols.Paper, "AI Win!");
		}

		[Test]
		public void RockVsRock()
		{
			TestVersus(Symbols.Rock, Symbols.Rock, "Tie!");
		}

		[Test]
		public void PaperVsPaper()
		{
			TestVersus(Symbols.Paper, Symbols.Paper, "Tie!");
		}

		[Test]
		public void ScissorsVsScissors()
		{
			TestVersus(Symbols.Scissors, Symbols.Scissors, "Tie!");
		}

		public void TestVersus(Symbols symbol1, Symbols symbol2, string message)
		{
			var outcomeCalculator1 = symbolOutcomeFactory.Create((uint)symbol1);
			var outcomeCalculator2 = symbolOutcomeFactory.Create((uint)symbol2);
			outcomeCalculator1.OutcomeDetermined += delegate (object sender, OutcomeDeterminedEventArgs args)
			{
				Assert.That(message, Is.EqualTo(args.outcomeMessage));
			};
			outcomeCalculator1.DetermineOutcome(outcomeCalculator2);
		}
	}
}
