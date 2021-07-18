using JetBrains.Annotations;
using Zenject;

namespace Project.Tests.Play
{
	[UsedImplicitly]
	internal sealed class BehaviourInstaller : Installer<BehaviourInstaller>
	{
		// Set up dependencies
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<SymbolOutcomeFactory>().AsSingle().NonLazy();
		}
	}
}