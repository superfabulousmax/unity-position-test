using JetBrains.Annotations;
using TMPro;
using static SymbolOutcomeCalculator;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class ResultTextPresenter
    {
		private TMP_Text textLabel;

        public ResultTextPresenter(TMP_Text text)
        {
			this.textLabel = text;
        }

		public void HandleDisplayResults(object sender, OutcomeDeterminedEventArgs args)
		{
			textLabel.text = args.outcomeMessage;
		}
    }
}