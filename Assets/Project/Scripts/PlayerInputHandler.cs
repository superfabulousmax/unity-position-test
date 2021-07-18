using Project.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

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

	[Inject]
	private ResultTextPresenter resultTextPresenter;

	[Inject]
	private SymbolOutcomeFactory SymbolOutcomeFactory;

	private int countDownValue = 3;

	private CompositeDisposable disposables = new CompositeDisposable();

	private IReadOnlyList<string> validInputs;

	public EventHandler<SymbolOutcomeCalculator.OutcomeDeterminedEventArgs> DisplayOutcome { get; private set; }

	void Start()
    {
		gameObject.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Return)).Subscribe(_ => HandleInput());
		Observable.FromEvent<ResultTextPresenter.ResetEventHandler, EventArgs>(
			   h => (sender, e) => h(e), 
			   h => resultTextPresenter.ResetEvent += playerSymbolPresenter.HandleResetEvent, 
			   h => resultTextPresenter.ResetEvent -= playerSymbolPresenter.HandleResetEvent)
		   .Subscribe()
		   .AddTo(disposables);
		Observable.FromEvent<ResultTextPresenter.ResetEventHandler, EventArgs>(
			   h => (sender, e) => h(e),
			   h => resultTextPresenter.ResetEvent += opponentSymbolPresenter.HandleResetEvent,
			   h => resultTextPresenter.ResetEvent -= opponentSymbolPresenter.HandleResetEvent)
		   .Subscribe()
		   .AddTo(disposables);
		Observable.FromEvent<ResultTextPresenter.ResetEventHandler, EventArgs>(
			   h => (sender, e) => h(e),
			   h => resultTextPresenter.ResetEvent += inputPresenter.HandleResetEvent,
			   h => resultTextPresenter.ResetEvent -= inputPresenter.HandleResetEvent)
		   .Subscribe()
		   .AddTo(disposables);
		validInputs = playerSymbolPresenter.GetValidInputs().ToList();
	}

	void LogInput()
	{
		inputPresenter.DisplayInput();
	}

	void HandleInput()
	{
		var input = inputPresenter.GetInput().ToLower().Trim();
		if (validInputs.ToList().Contains(input))
		{
			StartCoroutine(WaitThenDisplay(input));
		}
	}

	IEnumerator WaitThenDisplay(string input)
	{
		inputPresenter.DisableInput();
		yield return Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1), Scheduler.MainThreadIgnoreTimeScale)
			.TakeWhile(x => x <= countDownValue)
			.ForEachAsync(x => { countdownPresenter.SetCountDownText(countDownValue - x); })
			.ToYieldInstruction();
		HandleResults(input);
		inputPresenter.EnableInput();

	}

	async void HandleResults(string input)
	{
		var playerResult = (uint)validInputs.ToList().IndexOf(input);
		var aiResult = await opponentSymbolPresenter.GenerateRandomSymbol().Preserve();
		playerSymbolPresenter.ChangeImage(playerResult);
		opponentSymbolPresenter.ChangeImage(aiResult);
		var playerSymbolOutcome = SymbolOutcomeFactory.Create(playerResult);
		var aiSymbolOutcome = SymbolOutcomeFactory.Create(aiResult);
		playerSymbolOutcome.OutcomeDetermined += resultTextPresenter.HandleDisplayResults;
		playerSymbolOutcome.DetermineOutcome(aiSymbolOutcome);
	}

	void OnDestroy()
	{
		disposables.Dispose();	
	}
}
