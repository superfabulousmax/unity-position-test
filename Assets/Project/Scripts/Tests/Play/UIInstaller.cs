using JetBrains.Annotations;
using Project.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Tests.Play
{
    [UsedImplicitly]
    internal sealed class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            var playerInput = new GameObject("PlayerInput").AddComponent<TMP_InputField>();
            
            var playerSymbol = new GameObject("PlayerSymbol").AddComponent<Image>();
			var rockSprite = Resources.Load<Sprite>($"Images/rock");
			var paperSprite = Resources.Load<Sprite>($"Images/paper");
			var scissorsSprite = Resources.Load<Sprite>($"Images/scissors");
			var sprites = new Sprite [] { rockSprite, paperSprite, scissorsSprite };
			var opponentSymbol = new GameObject("OpponentSymbol").AddComponent<Image>();
            
            var countdownText = new GameObject("CountdownText").AddComponent<TextMeshProUGUI>();
            var resultText = new GameObject("ResultText").AddComponent<TextMeshProUGUI>();
            
            Container.BindInterfacesAndSelfTo<PlayerInputPresenter>().AsSingle().WithArguments(playerInput).NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerSymbolPresenter>().AsSingle().WithArguments(playerSymbol, sprites).NonLazy();
            Container.BindInterfacesAndSelfTo<OpponentSymbolPresenter>().AsSingle().WithArguments(opponentSymbol, sprites).NonLazy();
            
            Container.BindInterfacesAndSelfTo<CountdownPresenter>().AsSingle().WithArguments(countdownText).NonLazy();
            Container.BindInterfacesAndSelfTo<ResultTextPresenter>().AsSingle().WithArguments(resultText).NonLazy();
        }
    }
}