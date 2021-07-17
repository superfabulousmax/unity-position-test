using JetBrains.Annotations;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public abstract class SymbolPresenter
    {
		private Image image;
		private readonly Sprite [] sprites;

        protected SymbolPresenter(Image image, Sprite [] sprites)
        {
			this.image = image;
			this.sprites = sprites;
        }

		public virtual void ChangeImage(uint index)
		{
			if(sprites.Length > 0 && index < sprites.Length)
			{
				image.sprite = sprites[index];
			}
		}
    }

    [UsedImplicitly]
    public sealed class PlayerSymbolPresenter : SymbolPresenter
    {
        public PlayerSymbolPresenter(Image image, Sprite [] sprites) : base(image, sprites)
        {
        }
    }

    [UsedImplicitly]
    public sealed class OpponentSymbolPresenter : SymbolPresenter
    {
        public OpponentSymbolPresenter(Image image, Sprite [] sprites) : base(image, sprites)
        {
        }

		public void GenerateRandomSymbol()
		{
			ObservableWWW.Get("http://www.randomnumberapi.com/api/v1.0/random?min=0&max=2&count=1")
			.Subscribe(
				x => ChangeImage(Convert.ToUInt32(x.Substring(1, x.Length - 2))), // onSuccess
				ex => Debug.LogException(ex)); // onError
		}


    }
}