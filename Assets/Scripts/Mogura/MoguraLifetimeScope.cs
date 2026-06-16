using DIStudy.Mogura;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DIStudy.Mogura
{
    public class MoguraLifetimeScope : LifetimeScope
    {
        [SerializeField] private MoguraConfig _config = new MoguraConfig();

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_config);
            builder.Register<IScoreService, MoguraScoreService>(Lifetime.Singleton);
            builder.Register<ISaveService, MoguraPlayerSaveService>(Lifetime.Singleton);
            builder.Register<IGameService, MoguraGameService>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<MoguraSpawner>();
            builder.RegisterComponentInHierarchy<MoguraAudioManager>().As<IAudioService>();
            builder.RegisterComponentInHierarchy<MoguraHUDController>();
            builder.RegisterComponentInHierarchy<MoguraClickRouter>();
        }
    }
}



