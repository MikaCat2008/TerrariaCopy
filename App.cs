using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using TerrariaCopy.Engine;
using Engine;


namespace TerrariaCopy
{
    public class App : Game
    {
        public int frames;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private EntityManager entityManager;

        public App()
        {
            this.frames = 0;
            this.graphics = new GraphicsDeviceManager(this);
            this.IsMouseVisible = true;
            this.entityManager = new EntityManager();
            this.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsFixedTimeStep = false; 
            Tools.currentManager = this.entityManager;
            Sprite.graphicsDevice = this.GraphicsDevice;
            BaseComponent.app = this;
            
            Dictionary<string, TileType> tileMapTypes = new Dictionary<string, TileType>();
            tileMapTypes.Add(
                "Stone", new TileType(
                    new Sprite2DRenderScript(Textures.Stone)
                )
            );

            Prefab myPrefab = new Prefab(
                "TileMap", 
                new List<BaseComponent>() 
                {
                    new TileMap()
                    {
                        new TileMapTypes(tileMapTypes)
                    }
                }
            );

            Tools.Instantiate(myPrefab, new Vector2(200, 200));
            
            this.entityManager.InitializeEntities();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            Textures.Initialize(this.Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            this.entityManager.UpdateEntities(gameTime);

            this.frames++;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();

            foreach (Entity entity in this.entityManager.GetEntities())
            {
                Sprite? sprite = entity.GetComponent<Sprite>();
                Transition? transition = entity.GetComponent<Transition>();
            
                if (sprite != null && transition != null) 
                {
                    sprite.Render();
                    this.spriteBatch.Draw(sprite.rendered, transition.position, Color.White);
                }
            }

            this.spriteBatch.End();

            if (this.frames % 5 == 0) 
            {
                this.Window.Title = $"TerrariaCopy ({Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds)} fps)";
            }

            base.Draw(gameTime);
        }
    }
}
