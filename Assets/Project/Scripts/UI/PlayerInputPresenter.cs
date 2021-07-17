using JetBrains.Annotations;
using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class PlayerInputPresenter
    {
		private TMP_InputField inputField;

		public PlayerInputPresenter(TMP_InputField field)
        {
			inputField = field;
		}

		public void HandleResetEvent(object sender, EventArgs args)
		{
			inputField.text = default;
		}

		public void DisableInput() => inputField.enabled = false;
		public void EnableInput() => inputField.enabled = true;

		public void DisplayInput() => Debug.Log(inputField.text);

		public string GetInput() => inputField.text;

    }
}