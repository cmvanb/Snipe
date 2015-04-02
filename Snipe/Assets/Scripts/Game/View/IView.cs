namespace Snipe
{
    public interface IView
    {
        void Update(GameModel gameModel);

        void CleanUp();
    }
}