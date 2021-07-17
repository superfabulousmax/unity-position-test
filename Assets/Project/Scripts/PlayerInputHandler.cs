using Project.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using UniRx;

public class PlayerInputHandler : MonoBehaviour
{
	[Inject]
	private PlayerInputPresenter inputPresenter;

	[Inject]
	private CountdownPresenter countdownPresenter;

	[Inject]
	private PlayerSymbolPresenter playerSymbolPresenter;

	[Inject]
	private OpponentSymbolPresenter opponentSymbolPresenter;

	private int countDownValue = 3;

	private List<string> validInputs = new List<string>{ "rock", "paper", "scissors" };

    void Start()
    {
		gameObject.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Return)).Subscribe(_ => HandleInput());
    }

	void LogInput()
	{
		inputPresenter.DisplayInput();
	}

	void HandleInput()
	{
		var input = inputPresenter.GetInput().ToLower().Trim();
		if (validInputs.Contains(input))
		{
			StartCoroutine(WaitThenDisplay(input));
		}
	}

	IEnumerator WaitThenDisplay(string input)
	{
		yield return Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1), Scheduler.MainThreadIgnoreTimeScale)
			.TakeWhile(x => x <= countDownValue)
			.ForEachAsync(x => { countdownPresenter.SetCountDownText(countDownValue - x); })
			.ToYieldInstruction();
		var index = validInputs.IndexOf(input);
		playerSymbolPresenter.ChangeImage((uint)index);
		opponentSymbolPresenter.GenerateRandomSymbol();

	}
}
