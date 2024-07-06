using System;
using Microsoft.Xna.Framework;


namespace Engine
{
    public class BaseComponent : ICloneable
    {
        public Entity? entity;

        public static App app;

        public virtual void Update(GameTime gameTime) { }

        public virtual void Initialize(Entity entity) 
        {
            this.entity = entity;
        }

        public Object Clone() 
        {
            return this.MemberwiseClone();
        }
    }
}
