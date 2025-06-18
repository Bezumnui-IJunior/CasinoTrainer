using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using Unity.VisualScripting;

namespace Infrastructure
{
    public static class WorldExtension
    {
        public static List<Entity> GetAllEntities(this World world)
        {
            // TODO: resolve without reflection (if possible)
            List<Entity> result = new List<Entity>();

            foreach (EntityData entityData in world.GetEntities())
            {
                FieldInfo fieldInfo = typeof(EntityData).GetField("currentArchetype", BindingFlags.Instance | BindingFlags.NonPublic);
                Archetype currentArchetype = (Archetype)new ReflectionFieldAccessor(fieldInfo).GetValue(entityData);

                if (currentArchetype == null)
                    continue;

                fieldInfo = typeof(Archetype).GetField("entities", BindingFlags.Instance | BindingFlags.NonPublic);
                EntityPinnedArray entities = (EntityPinnedArray)new ReflectionFieldAccessor(fieldInfo).GetValue(currentArchetype);
                Entity[] data = entities.data;

                foreach (Entity entity in data.Where(entity => !world.IsDisposed(entity)))
                {
                    if (result.Contains(entity) == false)
                        result.Add(entity);
                }
            }

            return result;
        }

        private static EntityData[] GetEntities(this World world)
        {
            FieldInfo entitiesFieldInfo = world.GetType().GetField("entities", BindingFlags.Instance | BindingFlags.NonPublic);

            return (EntityData[])new ReflectionFieldAccessor(entitiesFieldInfo).GetValue(world);
        }
    }
}