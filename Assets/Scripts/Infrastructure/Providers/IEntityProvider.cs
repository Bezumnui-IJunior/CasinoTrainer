using UnityEngine;

namespace Infrastructure.Providers
{
    public interface IEntityProvider
    {
        // ReSharper disable once InconsistentNaming
        public GameObject gameObject { get; }
    }
}