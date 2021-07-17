using JetBrains.Annotations;
using TMPro;
using System;
using static SymbolOutcomeCalculator;
using UniRx;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class ResultTextPresenter
    {
		private TMP_Text textLabel;
		private string defaultLabel;

		private float showResultsDelay = 0.5f;
		private float resetResultsDelay = 2f;

		public delegate void ResetEventHandler(object source, EventArgs args);
		public event ResetEventHandler ResetEvent;

		public ResultTextPresenter(TMP_Text text)
        {
			this.textLabel = text;
			defaultLabel = textLabel.text;
		}

		public async void HandleDisplayResults(object sender, OutcomeDeterminedEventArgs args)
		{
			await WaitThenAction(ChangeResult, args.outcomeMessage, showResultsDelay);
			await WaitThenAction(ChangeResult, defaultLabel, resetResultsDelay);
			ResetEvent?.Invoke(this, new EventArgs());
		}

		IObservable<Unit> WaitThenAction(Action<string> action, string result, float delay)
		{
			return Observable.Timer(TimeSpan.FromSeconds(delay), Scheduler.MainThreadIgnoreTimeScale)
				.TakeWhile(x => x <= delay)
				.ForEachAsync(x => { action?.Invoke(result); });
			
		}

		private void ChangeResult(string result)
		{
			textLabel.text = result;
		}
	}
}