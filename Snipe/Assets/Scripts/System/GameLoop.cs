using UnityEngine;

namespace Snipe
{
	public class GameLoop : MonoBehaviour
	{
		private static GameLoop instance;

        private GameModel gameModel;
		private GameView gameView;
        private GameController gameController;
        private GUIModel guiModel;
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

            resourceManager.AddAllAtPath("Sprites/");
            resourceManager.AddPath("Sprites/RectTiles/empty_rect");
            resourceManager.AddPath("Sprites/RectTiles/grass");
            resourceManager.AddPath("Sprites/RectTiles/grass2");
            resourceManager.AddPath("Sprites/RectTiles/dirt");
            resourceManager.AddPath("Sprites/RectTiles/tile_stone_light");
            resourceManager.AddPath("Sprites/Units/a_soldier");
            resourceManager.AddPath("Sprites/Units/a_sniper");
            resourceManager.AddPath("Sprites/Units/a_medic");
            resourceManager.AddPath("Sprites/Units/b_soldier");
            resourceManager.AddPath("Sprites/Units/b_sniper");
            resourceManager.AddPath("Sprites/Units/b_medic");
            resourceManager.AddPath("Sprites/Interface/action_select");
            resourceManager.AddPath("Sprites/Interface/selected");
            resourceManager.AddPath("Sprites/Interface/action_walk");
            resourceManager.AddPath("Sprites/Interface/action_shoot");
            resourceManager.AddPath("Sprites/Interface/action_heal");
            resourceManager.AddPath("Sprites/Interface/IDcard/idcard_portrait_stock");
            resourceManager.AddPath("Sprites/Portraits/soldier_wounded");
            resourceManager.LoadAll();

            // Use sprite manager to load textures into sprites.
            SpriteManager spriteManager = SpriteManager.Instance;

            spriteManager.AddSprite(SpriteID.EmptyRect, "Sprites/RectTiles/empty_rect");
            spriteManager.AddSprite(SpriteID.GrassRect, "Sprites/RectTiles/grass");
            spriteManager.AddSprite(SpriteID.Grass2Rect, "Sprites/RectTiles/grass2");
            spriteManager.AddSprite(SpriteID.DirtRect, "Sprites/RectTiles/dirt");
            spriteManager.AddSprite(SpriteID.StoneLightRect, "Sprites/RectTiles/tile_stone_light");
            spriteManager.AddSprite(SpriteID.ASoldier, "Sprites/Units/a_soldier");
            spriteManager.AddSprite(SpriteID.ASniper, "Sprites/Units/a_sniper");
            spriteManager.AddSprite(SpriteID.AMedic, "Sprites/Units/a_medic");
            spriteManager.AddSprite(SpriteID.BSoldier, "Sprites/Units/b_soldier");
            spriteManager.AddSprite(SpriteID.BSniper, "Sprites/Units/b_sniper");
            spriteManager.AddSprite(SpriteID.BMedic, "Sprites/Units/b_medic");
            spriteManager.AddSprite(SpriteID.Selector, "Sprites/Interface/action_select");
            spriteManager.AddSprite(SpriteID.Selected, "Sprites/Interface/selected");
            spriteManager.AddSprite(SpriteID.Move, "Sprites/Interface/action_walk");
            spriteManager.AddSprite(SpriteID.Attack, "Sprites/Interface/action_shoot");
            spriteManager.AddSprite(SpriteID.Heal, "Sprites/Interface/action_heal");
            spriteManager.AddSprite(SpriteID.Portrait1Normal, "Sprites/Interface/IDcard/idcard_portrait_stock");
            spriteManager.AddSprite(SpriteID.Portrait1Wounded, "Sprites/Portraits/soldier_wounded");

            // Find camera.
            Camera camera = Camera.main;

            // Build game model, view and controller objects.
			gameModel = new GameModel();

            gameModel.AddPlayer(new Player("Casper", Faction.A));
            gameModel.AddPlayer(new Player("Thomas", Faction.B));

            gameView = new GameView(gameModel, camera);

			gameController = new GameController(gameModel);

            // Find interface view and link up relevant button events.
            InterfaceView interfaceView = GameObject.Find("InterfaceView").GetComponent<InterfaceView>();

            interfaceView.ClickedEndTurnButtonEvent += () => { gameController.EndTurn(); };
            interfaceView.ClickedNewGameButtonEvent += () => { gameController.NewGame(); };

            // Build gui model, view and controller objects.
            guiModel = new GUIModel();

            guiView = new GUIView(interfaceView);

            guiController = new GUIController(gameModel, guiModel);

            // Initialize view.
            gameView.Update(gameModel);
            guiView.Update(guiModel);

            // Start game.
            gameController.StartGame();
		}

		void Update()
        {
            guiController.Update();
			gameController.Update();
            gameView.Update(gameModel);
            guiView.Update(guiModel);
		}
	}
}