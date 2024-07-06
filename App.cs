using Engine;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace TerrariaCopy
{
    public class App : Game
    {
        public int frames;
        public Vector2 cameraPosition;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private EntityManager entityManager;

        public App()
        {
            this.frames = 0;
            this.cameraPosition = new Vector2(0, 0);  // new Vector2(1920, 1080) / 4;
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;
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

            Dictionary<string, TileType> tileMapTypes = new Dictionary<string, TileType>()
            {
                {
                    "Stone",
                    new TileType(
                        "Stone",
                        new TextureRenderScript(Textures.Stone)
                    )
                }
            };

            Prefab playerPrefab = new Prefab(
                "Player",
                new List<BaseComponent>()
                {
                    new Sprite() { renderScript=new RectangleRenderScript(32, 64, Color.Red) }
                }
            );
            Prefab tileMapPrefab = new Prefab(
                "TileMap", 
                new List<BaseComponent>() 
                {
                    new Sprite() { renderScript=new TileMapRenderScript(32) },
                    new TileMap() { types=tileMapTypes },
                    new Transition() { position=new Vector2(0, 0) }
                }
            );

            Tools.Instantiate(playerPrefab, new Vector2(0, 64));
            Entity tileMapEntity = Tools.Instantiate(tileMapPrefab, new Vector2(0, 0));

            this.entityManager.InitializeEntities();

            TileMap tileMap = tileMapEntity.GetComponent<TileMap>();
            tileMap.Add("Stone", new Vector2(0, 0));
            tileMap.Add("Stone", new Vector2(0, 1));
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
                Sprite sprite = entity.GetComponent<Sprite>();
                Transition transition = entity.GetComponent<Transition>();
            
                if (sprite != null && transition != null) 
                {
                    sprite.Render();

                    DisplayMode mode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
                    Vector2 position = new Vector2(0, 0);
                    position += new Vector2(transition.position.X, mode.Height - transition.position.Y);
                    position += new Vector2(mode.Width, -mode.Height) / 2;
                    position -= this.cameraPosition;
                    position -= sprite.rendered.Bounds.Size.ToVector2() / 2;

                    this.spriteBatch.Draw(
                        sprite.rendered,
                        position, 
                        Color.White
                    );
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
