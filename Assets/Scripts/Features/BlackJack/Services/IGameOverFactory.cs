using Scellecs.Morpeh;

namespace Features.BlackJack.Services
{
    public interface IGameOverFactory
    {
        Entity CreateGameOver(Entity winner);
        Entity CreateDraw();
    }
}