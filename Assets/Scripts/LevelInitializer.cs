using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fizz6.LudumDare45
{
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField]
        GameObject playerPrefab = null;

        [SerializeField]
        GameObject playerSpawn = null;

        private void Awake()
        {
            Instantiate(playerPrefab);

            if (Player.Sprite != null)
            {
                SpriteRenderer playerSpriteRenderer = playerPrefab.GetComponent<SpriteRenderer>();
                playerSpriteRenderer.sprite = Player.Sprite;
            }

            playerPrefab.transform.position = playerSpawn.transform.position;
        }
    }
}
