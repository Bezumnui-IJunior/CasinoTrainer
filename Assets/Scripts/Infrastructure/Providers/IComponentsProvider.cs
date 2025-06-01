using Scellecs.Morpeh;

namespace Infrastructure.Providers
{
    public interface IComponentsProvider
    {
        void Initialize(Entity entity, World world);
    }
}