using JetBrains.Annotations;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace Project.UI
{
    public abstract class SymbolPresenter
    {
		private Image image;
		private Sprite defaultSprite;
		private readonly Sprite [] sprites;

        protected SymbolPresenter(Image image, Sprite [] sprites)
        {
			this.image = image;
			defaultSprite = image.sprite;
			this.sprites = sprites;
        }

		public virtual void ChangeImage(uint index)
		{
			if(sprites.Length > 0 && index < sprites.Length)
			{
				image.sprite = sprites[index];
			}
		}

		public void HandleResetEvent(object sender, EventArgs args)
		{
			image.sprite = defaultSprite;
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

		public async UniTask<uint> GenerateRandomSymbol()
		{
			var request = UnityWebRequest.Get("http://www.randomnumberapi.com/api/v1.0/random?min=0&max=2&count=1");
			var response = await request.SendWebRequest();
			var text = response.downloadHandler.text;
			return Convert.ToUInt32(text.Substring(1, text.Length - 2));
		}


    }
}