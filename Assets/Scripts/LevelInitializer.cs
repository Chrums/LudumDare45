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

            Dimension2DManager.CurrentFrame = 0;
        }

        public void Update()
        {
            if (Input.GetButton("Jump"))
            {
                Dimension2DManager.CurrentFrame--;
            }
            else if (Dimension2DManager.HorizontalAxis != Dimension2DManager.Dimension.AxisT && Dimension2DManager.VerticalAxis != Dimension2DManager.Dimension.AxisT)
            {
                Dimension2DManager.CurrentFrame++;
            }
        }
    }
}
