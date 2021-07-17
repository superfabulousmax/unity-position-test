using System.Collections.Generic;
using UnityEngine;

public abstract class SymbolOutcomeCalculator
{
	public enum Symbols
	{
		Rock = 0, Paper = 1, Scissors = 2
	}

	public Symbols id;

	public List<Symbols> winOutcomes;

	public virtual void DetermineOutcome(SymbolOutcomeCalculator other)
	{
		if(other.id == id)
		{
			// send draw
			Debug.Log("Draw");
		}
		else if(winOutcomes.Contains(other.id))
		{
			// send win
			Debug.Log("Win");
		}
		else
		{
			// send loss
			Debug.Log("Loss");
		}
	}
}

public class RockOutcomeCaculator : SymbolOutcomeCalculator
{
	public RockOutcomeCaculator()
	{
		id = Symbols.Rock;
		// rock beats scissors
		winOutcomes = new List<Symbols>(){ Symbols.Scissors };
	}
}

public class PaperOutcomeCaculator : SymbolOutcomeCalculator
{
	public PaperOutcomeCaculator()
	{
		id = Symbols.Paper;
		// paper beats rock
		winOutcomes = new List<Symbols>() { Symbols.Rock };
	}
}

public class ScissorsOutcomeCaculator : SymbolOutcomeCalculator
{
	public ScissorsOutcomeCaculator()
	{
		id = Symbols.Scissors;
		// scissors beats paper
		winOutcomes = new List<Symbols>() { Symbols.Paper };
	}
}
