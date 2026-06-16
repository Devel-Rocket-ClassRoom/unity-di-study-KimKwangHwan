using VContainer;
using VContainer.Unity;

public class HelloWorldLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<GreetingService>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GreetingEntryPoint>();
    }
}
