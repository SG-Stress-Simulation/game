namespace Zinnia.Rule
{
    using UnityEngine;
    using Zinnia.Data.Collection.List;

    /// <summary>
    /// Determines whether a <see cref="GameObject"/>'s <see cref="GameObject.tag"/> is part of a list.
    /// </summary>
    public class ExactGameObjectRule : GameObjectRule
    {
        [Tooltip("The gameObject to check against.")]
        public GameObject acceptedObject;

        /// <inheritdoc />
        protected override bool Accepts(GameObject targetGameObject)
        {
            return targetGameObject == acceptedObject;
        }
    }
}