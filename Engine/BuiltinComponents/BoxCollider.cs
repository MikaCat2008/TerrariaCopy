using System;
using System.DirectoryServices.ActiveDirectory;
using Microsoft.Xna.Framework;


namespace Engine
{
    public abstract class ICollisionScript
    {
        public BoxCollider? boxCollider;
        public abstract Rectangle[] GetRectangles();
    }

    public class RactangleCollisionScript : ICollisionScript
    {
        public int width;
        public int height;

        public RactangleCollisionScript(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public override Rectangle[] GetRectangles()
        {
            Vector2 position = boxCollider.transition.position;

            return new Rectangle[1] { 
                new Rectangle((int)position.X, (int)position.Y - this.height / 2, this.width, this.height) 
            };
        }
    }

    public class BoxCollider : BaseComponent
    {
        public bool collision;
        public Transition transition;
        public ICollisionScript collisionScript;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            this.transition = entity.GetComponent<Transition>();
            this.collisionScript.boxCollider = this;
        }

        public Tuple<bool, bool> UpdateCollision(Vector2 move)
        {
            int mx = (int)Math.Round(move.X),
                my = (int)Math.Round(move.Y);
            bool topCollision = false, bottomCollision = false;

            foreach (Entity entity in this.entity.entityManager.GetEntities())
            {
                BoxCollider? boxCollider = entity.GetComponent<BoxCollider>();

                if (boxCollider == null || boxCollider == this)
                {
                    continue;
                }

                foreach (Rectangle rectangle in this.collisionScript.GetRectangles())
                {
                    foreach (Rectangle otherRectangle in boxCollider.collisionScript.GetRectangles())
                    {
                        Rectangle offsetXRectangle = new Rectangle(
                            rectangle.X + mx, rectangle.Y, 
                            rectangle.Width, rectangle.Height
                        );
                        Rectangle offsetYRectangle = new Rectangle(
                            rectangle.X, rectangle.Y + my, 
                            rectangle.Width, rectangle.Height
                        );

                        if (otherRectangle.Intersects(offsetXRectangle))
                        {
                            mx = 0;
                        }
                        else if (otherRectangle.Intersects(offsetYRectangle))
                        {
                            if (my < 0)
                            {
                                my = otherRectangle.Bottom - rectangle.Top;

                                bottomCollision = true;
                            }
                            else if (my > 0)
                            {
                                my = otherRectangle.Top - rectangle.Bottom;
                            
                                topCollision = true;
                            }
                        }
                    }
                }
            }

            this.transition.position += new Vector2(mx, my);

            return new Tuple<bool, bool>(topCollision, bottomCollision);
        }
    }
}
