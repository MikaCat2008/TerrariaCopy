using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;


namespace Engine
{
    public class EntityManager
    {
        private Dictionary<string, List<Entity>> entities;

        public EntityManager()
        {
            this.entities = new Dictionary<string, List<Entity>>();
        }

        public Entity Add(Entity entity)
        {
            List<Entity> entities = this.GetEntitiesByTag(entity.tag);

            entities.Add(entity);

            return entity;
        }

        public Entity Create()
        {
            return this.Create("Entity");
        }

        public Entity Create(string tag)
        {
            return this.Create(tag, new List<BaseComponent>());
        }

        public Entity Create(string tag, List<BaseComponent> components) 
        {
            Entity entity = new Entity(tag, this);

            this.Add(entity);
            entity.entityManager = this;

            foreach (BaseComponent component in components)
            {
                entity.AddComponent(component);
            }

            return entity;
        }

        public List<Entity> GetEntitiesByTag(string tag)
        {
            List<Entity>? entities;

            if (!this.entities.TryGetValue(tag, out entities))
            {
                entities = new List<Entity>();

                this.entities.Add(tag, entities);
            }

            return entities;
        }

        public Entity? GetEntityByTag(string tag)
        {
            List<Entity> entities = this.GetEntitiesByTag(tag);

            if (entities.Count > 0)
            {
                return entities[0];
            }

            return null;
        }

        public IEnumerable<Entity> GetEntities()
        {
            IEnumerable<Entity> entities = new List<Entity>();

            foreach (string tag in this.entities.Keys)
            {
                entities = entities.Concat(this.entities[tag]);
            }

            return entities;
        }

        public void UpdateEntities(GameTime gameTime)
        {
            foreach (Entity entity in this.GetEntities())
            {
                entity.UpdateComponents(gameTime);
            }
        }

        public void InitializeEntities()
        {
            foreach (Entity entity in this.GetEntities())
            {
                entity.InitializeComponents();
            }
        }
    }
}
