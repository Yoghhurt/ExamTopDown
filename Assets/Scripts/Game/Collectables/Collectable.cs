using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
   private CollectableBehavior _collectableBehavior;

   private void Awake()
   {
       _collectableBehavior = GetComponent<CollectableBehavior>();
   }
   
   private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            _collectableBehavior.OnCollected(player.gameObject);
            Destroy(gameObject);
        }
    }
}
