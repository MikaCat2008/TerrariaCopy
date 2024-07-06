using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Engine
{
    public abstract class IRenderScript
    {
        public Sprite sprite;

        public virtual void Initialize(Sprite sprite) 
        {
            this.sprite = sprite;
        }

        public abstract Texture2D Render();
    }

    public class RectangleRenderScript : IRenderScript
    {
        public int width;
        public int height;
        public Color color;

        public RectangleRenderScript(int width, int height, Color color) 
        {
            this.width = width;
            this.height = height;
            this.color = color;
        }

        public override Texture2D Render() 
        {
            Color[] data = new Color[this.width * this.height];
            Texture2D texture = new Texture2D(Sprite.graphicsDevice, this.width, this.height);

            for (int i = 0; i < this.width * this.height; i++)
            {
                data[i] = this.color;
            }

            texture.SetData(data);
            return texture;
        }
    }

    public class TextureRenderScript : IRenderScript
    {
        public Texture2D texture;

        public TextureRenderScript(Texture2D texture) 
        {
            this.texture = texture;
        }

        public override Texture2D Render() 
        {
            return this.texture;
        }
    }

    public class Sprite : BaseComponent
    {
        public Texture2D rendered;
        public IRenderScript renderScript;

        public static GraphicsDevice graphicsDevice;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            this.renderScript.Initialize(this);
        }

        public void Render()
        {
            this.rendered = this.renderScript.Render();
        }
    }
}
