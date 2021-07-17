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

		public void DisplayInput() => Debug.Log(inputField.text);

		public string GetInput() => inputField.text;

    }
}