using NUnit.Framework;
using System;
using Zenject;

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
			TestOutcome(SymbolOutcomeCalculator.Symbols.Rock, typeof(RockOutcomeCaculator));
		}

		[Test]
		public void TestPaperOutCome()
		{
			TestOutcome(SymbolOutcomeCalculator.Symbols.Paper, typeof(PaperOutcomeCaculator));
		}

		[Test]
		public void TestScissorsOutCome()
		{
			TestOutcome(SymbolOutcomeCalculator.Symbols.Scissors, typeof(ScissorsOutcomeCaculator));
		}

		public void TestOutcome(SymbolOutcomeCalculator.Symbols symbol, Type expectedType)
		{
			var outcomeCalculator = symbolOutcomeFactory.Create((uint)symbol);
			Assert.True(outcomeCalculator != null, $"Check that the {symbol} symbol is not null");
			Assert.That(outcomeCalculator.GetType(), Is.EqualTo(expectedType));
		}
	}
}
