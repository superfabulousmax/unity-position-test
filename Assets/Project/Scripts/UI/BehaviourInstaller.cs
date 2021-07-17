using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Behaviour
{
	internal sealed class BehaviourInstaller : MonoInstaller<BehaviourInstaller>
	{

		// Set up dependencies
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<SymbolOutcomeFactory>().AsSingle().NonLazy();
		}
	}

}