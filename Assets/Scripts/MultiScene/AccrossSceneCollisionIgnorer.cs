using System.Collections.Generic;
using UnityEngine;

public class AccrossSceneCollisionIgnorer : MonoBehaviour
{
  public GameObject[] objectsInThisSceneToIgnore;
  public GuidReference[] objectsInOtherScenesToIgnore;

  // Start is called before the first frame update
  void Start()
  {
    // recursively find all colliders in each object
    List<Collider> thisSceneColliders = new List<Collider>();
    foreach (GameObject obj in objectsInThisSceneToIgnore) {
      thisSceneColliders.AddRange(obj.GetComponentsInChildren<Collider>());
    }
    List<Collider> otherSceneColliders = new List<Collider>();
    foreach (GuidReference obj in objectsInOtherScenesToIgnore) {
      otherSceneColliders.AddRange(obj.gameObject.GetComponentsInChildren<Collider>());
    }
    // ignore collisions between all colliders
    foreach (Collider collider in thisSceneColliders) {
      foreach (Collider otherCollider in otherSceneColliders) {
        Physics.IgnoreCollision(collider, otherCollider);
      }
    }
  }
}
