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

        bool isFucked = false;

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

            if (Input.GetButtonDown("Fire1"))
            {
                Dimension2DManager.SetDimensions(Dimension2DManager.VerticalAxis, Dimension2DManager.HorizontalAxis);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                if (!isFucked)
                {
                    Dimension2DManager.SetDimensions(Dimension2DManager.Dimension.AxisX, Dimension2DManager.Dimension.AxisT);
                    isFucked = true;
                }
                else
                {
                    Dimension2DManager.SetDimensions(Dimension2DManager.Dimension.AxisX, Dimension2DManager.Dimension.AxisY);
                    isFucked = false;
                }
            }
        }
    }
}
