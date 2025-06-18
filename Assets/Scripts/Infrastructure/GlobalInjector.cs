using VContainer;

namespace Infrastructure
{
    public class GlobalInjector
    {
        private IObjectResolver _resolver;
        private static GlobalInjector _globalInjector;

        // private GlobalInjector() { }

        [Inject]
        private void Constructor(IObjectResolver resolver)
        {
            _resolver = resolver;

            _globalInjector = this;
        }

        public static void Inject(object obj) =>
            _globalInjector._resolver.Inject(obj);
    }
}