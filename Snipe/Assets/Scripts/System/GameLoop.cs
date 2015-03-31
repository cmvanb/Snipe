using UnityEngine;

namespace Snipe
{
	public class GameLoop : MonoBehaviour
	{
		private static GameLoop instance;

        private GameState gameState;
		private GameView gameView;
        private GameController gameController;
        private GUIState guiState;
        private GUIView guiView;
        private GUIController guiController;

		void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Debug.LogError("Multiple game loop objects.");

				return;
			}

            // Identify and load all dynamic resources.
            ResourceManager resourceManager = ResourceManager.Instance;

            resourceManager.AddPath("Sprites/empty_rect");
            resourceManager.AddPath("Sprites/grass");
            resourceManager.AddPath("Sprites/dirt");
            resourceManager.AddPath("Sprites/Units/a_soldier");
            resourceManager.AddPath("Sprites/Units/a_sniper");
            resourceManager.AddPath("Sprites/Units/a_medic");
            resourceManager.AddPath("Sprites/Units/b_soldier");
            resourceManager.AddPath("Sprites/Units/b_sniper");
            resourceManager.AddPath("Sprites/Units/b_medic");
            resourceManager.AddPath("Sprites/Interface/selector");
            resourceManager.LoadAll();

            // Use sprite manager to load textures into sprites.
            SpriteManager spriteManager = SpriteManager.Instance;

            spriteManager.AddSprite(SpriteID.EmptyRect, "Sprites/empty_rect");
            spriteManager.AddSprite(SpriteID.GrassRect, "Sprites/grass");
            spriteManager.AddSprite(SpriteID.DirtRect, "Sprites/dirt");
            spriteManager.AddSprite(SpriteID.ASoldier, "Sprites/Units/a_soldier");
            spriteManager.AddSprite(SpriteID.ASniper, "Sprites/Units/a_sniper");
            spriteManager.AddSprite(SpriteID.AMedic, "Sprites/Units/a_medic");
            spriteManager.AddSprite(SpriteID.BSoldier, "Sprites/Units/b_soldier");
            spriteManager.AddSprite(SpriteID.BSniper, "Sprites/Units/b_sniper");
            spriteManager.AddSprite(SpriteID.BMedic, "Sprites/Units/b_medic");
            spriteManager.AddSprite(SpriteID.Selector, "Sprites/Interface/selector");

            // Find camera.
            Camera camera = Camera.main;

			// TODO: retrieve level data from file and/or scene
			LevelData levelData = new LevelData();

            // Build game state, view and controller objects.
			gameState = new GameState(levelData);

            gameState.AddPlayer(new Player("One", Faction.A));
            gameState.AddPlayer(new Player("Two", Faction.B));

            gameView = new GameView(camera);

			gameController = new GameController(gameState);
            
            // Build gui state, view and controller objects.
            guiState = new GUIState();

            guiView = new GUIView(gameView);

            guiController = new GUIController(gameState, guiState);

            // Initialize view.
            gameView.Update(gameState);
            guiView.Update(guiState);

            // Start game.
            gameController.Start();
		}

		void Update()
        {
            guiController.Update();
			gameController.Update();
            gameView.Update(gameState);
            guiView.Update(guiState);
		}
	}
}