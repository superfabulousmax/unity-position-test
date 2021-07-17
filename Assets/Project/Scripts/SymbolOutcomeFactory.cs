public sealed class SymbolOutcomeFactory
{
    public SymbolOutcomeCalculator Create(uint id)
	{
		switch(id)
		{
			case (uint)SymbolOutcomeCalculator.Symbols.Rock:
				return new RockOutcomeCaculator();
			case (uint)SymbolOutcomeCalculator.Symbols.Paper:
				return new PaperOutcomeCaculator();
			case (uint)SymbolOutcomeCalculator.Symbols.Scissors:
				return new ScissorsOutcomeCaculator();
			default:
				return null;
		}
	}
}
