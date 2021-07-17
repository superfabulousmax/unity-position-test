using JetBrains.Annotations;
using TMPro;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class CountdownPresenter
    {
		private TMP_Text text;

        public CountdownPresenter(TMP_Text text)
        {
			this.text = text;
        }

		public void SetCountDownText(long value)
		{
			text.text = value.ToString();
		}
    }
}