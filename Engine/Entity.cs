using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace TerrariaCopy.Engine
{
    public class Entity
    {
        public string tag;
        public Dictionary<string, BaseComponent> components;
        public EntityManager entityManager;
        private bool initialized;

        public Entity(string tag, EntityManager entityManager)
        {
            this.tag = tag;
            this.components = new Dictionary<string, BaseComponent>();
            this.entityManager = entityManager;
            this.initialized = false;
        }

        public T AddComponent<T>() where T : BaseComponent, new()
        {
            return this.AddComponent(new T());
        }

        public T AddComponent<T>(T component) where T : BaseComponent
        {
            this.components.Add(component.GetType().Name, component);

            if (this.initialized)
            {
                component.Initialize(this);
            }

            return component;
        }

        public void UpdateComponents(GameTime gameTime)
        {
            foreach (BaseComponent component in this.GetComponents())
            {
                component.Update(gameTime);
            }
        }

        public void InitializeComponents()
        {
            this.initialized = true;

            foreach (BaseComponent component in this.GetComponents())
            {
                component.Initialize(this);
            }
        }

        public T? GetComponent<T>() where T : BaseComponent, new()
        {
            string componentName = typeof(T).Name;

            this.components.TryGetValue(componentName, out BaseComponent? component);

            return (T?)component;
        }

        public List<BaseComponent> GetComponents()
        {
            return this.components.Values.ToList();
        }
    }
}
