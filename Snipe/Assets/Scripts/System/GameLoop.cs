﻿using UnityEngine;

namespace Snipe
{
	public class GameLoop : MonoBehaviour
	{
		private static GameLoop instance;

        private GameState gameState;
		private GameView gameView;
		private GameController gameController;

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

            resourceManager.AddPath("Sprites/empty-rect");
            resourceManager.AddPath("Sprites/grass");
            resourceManager.LoadAll();

            // Use sprite manager to load textures into sprites.
            SpriteManager spriteManager = SpriteManager.Instance;

            spriteManager.AddSprite(SpriteID.EmptyRect, "Sprites/empty-rect");
            spriteManager.AddSprite(SpriteID.GrassRect, "Sprites/grass");

			// TODO: retrieve level data from file and/or scene
			LevelData levelData = new LevelData();

            // Build game state, view and controller objects.
			gameState = new GameState(levelData);

			gameView = new GameView();

			gameController = new GameController(gameState);

            // Initialize view.
            gameView.Update(gameState);

            // Start game.
			gameController.Start();
		}

		void Update()
		{
			gameController.Update();
            gameView.Update(gameState);
		}
	}
}