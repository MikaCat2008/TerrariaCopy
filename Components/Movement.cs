using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Engine
{
    public class Movement : BaseComponent
    {
        float xSpeed;
        float ySpeed;
        float gSpeed;
        Entity player;
        float yVelocity;
        Tuple<bool, bool> lastCollision;
        Transition playerTransition;
        BoxCollider playerBoxCollider;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            this.xSpeed = 200;
            this.ySpeed = 500;
            this.gSpeed = 20;
            this.player = entity.entityManager.GetEntityByTag("Player");
            this.lastCollision = new Tuple<bool, bool>(false, false);
            this.playerBoxCollider = player.GetComponent<BoxCollider>();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 move = new Vector2(0, 0);
            KeyboardState state = Keyboard.GetState();
            float xSpeed = this.xSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            float ySpeed = this.ySpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            float gSpeed = this.gSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (state.IsKeyDown(Keys.A))
            {
                move.X -= xSpeed;
            }
            if (state.IsKeyDown(Keys.D))
            {
                move.X += xSpeed;
            }

            if (this.lastCollision.Item1)
            {
                this.yVelocity = 0;
            }
            else if (this.lastCollision.Item2)
            {
                this.yVelocity = 0;

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    this.yVelocity = ySpeed;
                }
            }
            else 
            {
                this.yVelocity -= gSpeed;
            }

            if (this.yVelocity != 0)
            {
                move.Y += this.yVelocity;
            }

            this.lastCollision = this.playerBoxCollider.UpdateCollision(move);
        }
    }
}
