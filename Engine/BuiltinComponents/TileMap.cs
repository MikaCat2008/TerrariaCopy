using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TerrariaCopy.Engine
{
    public class Tile
    {
        public TileType tileType;

        public Tile(TileType tileType) 
        {
            this.tileType = tileType;
        }
    }

    public class TileType
    {
        public string name;
        public IRenderScript renderScript;

        public TileType(string name, IRenderScript renderScript)
        {
            this.name = name;
            this.renderScript = renderScript;
        }
    }

    public class TileMap : BaseComponent
    {
        public int? minX;
        public int? maxX;
        public int? minY;
        public int? maxY;

        public Dictionary<Tuple<int, int>, Tile> tiles;
        public Dictionary<string, TileType> types;

        public override void Initialize(Entity entity)
        {
            this.tiles = new Dictionary<Tuple<int, int>, Tile>();
        }

        public void Add(string name, Vector2 position) 
        {
            TileType? tileType;

            if (!this.types.TryGetValue(name, out tileType))
            {
                throw new Exception("This tile type is not exists");
            }

            Tile tile = new Tile(tileType);
            Tuple<int, int> positionTuple = new Tuple<int, int>((int)position.X, (int)position.Y);

            if (this.minX == null)
            {
                this.minX = positionTuple.Item1;
                this.maxX = positionTuple.Item1;
            }
            else
            {
                this.minX = Math.Min((int)minX, positionTuple.Item1);
                this.maxX = Math.Max((int)maxX, positionTuple.Item1);
            }

            if (this.minY == null)
            {
                this.minY = positionTuple.Item2;
                this.maxY = positionTuple.Item2;
            }
            else
            {
                this.minY = Math.Min((int)minY, positionTuple.Item2);
                this.maxY = Math.Max((int)maxY, positionTuple.Item2);
            }

            this.tiles.Add(positionTuple, tile);
        }

        public Tile? Get(Vector2 position)
        {   
            Tuple<int, int> positionTuple = new Tuple<int, int>((int)position.X, (int)position.Y);

            Tile? tile;

            this.tiles.TryGetValue(positionTuple, out tile);

            return tile;
        }

        public int GetWidth()
        {
            if (this.maxX == null) 
            {
                return 0;
            }

            int minX = (int)this.minX;
            int maxX = (int)this.maxX;

            return maxX - minX + 1;
        }
    
        public int GetHeight()
        {
            if (this.maxY == null) 
            {
                return 0;
            }

            int minY = (int)this.minY;
            int maxY = (int)this.maxY;

            return maxY - minY + 1;
        }
    }

    public class TileMapRenderScript : IRenderScript 
        {
            public int tileSize;
            public TileMap tileMap;
            public Transition transition;

            public TileMapRenderScript(int tileSize)
            {
                this.tileSize = tileSize;
            }

            public override void Initialize(Sprite sprite)
            {
                base.Initialize(sprite);

                this.tileMap = sprite.entity.GetComponent<TileMap>();
                this.transition = sprite.entity.GetComponent<Transition>();
            }

            public override Texture2D Render() 
            {
                Texture2D texture = new Texture2D(
                    BaseComponent.app.GraphicsDevice, 
                    this.tileMap.GetWidth() * this.tileSize, 
                    this.tileMap.GetHeight() * this.tileSize
                );

                foreach (Tuple<int, int> position in this.tileMap.tiles.Keys)
                {
                    int x = position.Item1, y = position.Item2;
                    Tile tile = this.tileMap.tiles[position];
                    Color[] data = new Color[this.tileSize * this.tileSize];
                    Texture2D tileTexture = tile.tileType.renderScript.Render();

                    tileTexture.GetData(data);

                    texture.SetData(
                        0, 
                        new Rectangle(
                            x * this.tileSize, 
                            y * this.tileSize, 
                            this.tileSize, 
                            this.tileSize
                        ),
                        data,
                        0,
                        this.tileSize * this.tileSize
                    );
                }

                return texture;
            }
        }
}
