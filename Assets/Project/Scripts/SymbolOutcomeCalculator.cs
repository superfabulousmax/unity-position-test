using System;
using System.Collections.Generic;

public abstract class SymbolOutcomeCalculator
{
	public enum Symbols
	{
		Rock = 0, Paper = 1, Scissors = 2
	}

	public class OutcomeDeterminedEventArgs : EventArgs
	{
		public string outcomeMessage { get; set; }
	}

	public Symbols id;

	public List<Symbols> winOutcomes;

	public delegate void OutcomeEventHandler(object source, OutcomeDeterminedEventArgs args);
	public event OutcomeEventHandler OutcomeDetermined;

	public virtual void DetermineOutcome(SymbolOutcomeCalculator other)
	{
		var message = "";

		if(other.id == id)
		{
			message = "Tie!";
		}
		else if(winOutcomes.Contains(other.id))
		{
			message = "Player Win!";
		}
		else
		{
			message = "AI Win!";
		}

		OutcomeDetermined?.Invoke(this, new OutcomeDeterminedEventArgs { outcomeMessage = message });
	}
}

public class RockOutcomeCaculator : SymbolOutcomeCalculator
{
	public RockOutcomeCaculator() : base()
	{
		id = Symbols.Rock;
		// rock beats scissors
		winOutcomes = new List<Symbols>(){ Symbols.Scissors };
	}
}

public class PaperOutcomeCaculator : SymbolOutcomeCalculator
{
	public PaperOutcomeCaculator() : base()
	{
		id = Symbols.Paper;
		// paper beats rock
		winOutcomes = new List<Symbols>() { Symbols.Rock };
	}
}

public class ScissorsOutcomeCaculator : SymbolOutcomeCalculator
{
	public ScissorsOutcomeCaculator() : base()
	{
		id = Symbols.Scissors;
		// scissors beats paper
		winOutcomes = new List<Symbols>() { Symbols.Paper };
	}
}
