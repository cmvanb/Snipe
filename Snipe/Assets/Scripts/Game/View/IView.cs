namespace Snipe
{
    public interface IView
    {
        void Update(GameState gameState);

        void CleanUp();
    }
}