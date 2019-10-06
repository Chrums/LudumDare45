using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fizz6.LudumDare45
{
    public static class Player
    {
        public static string Name = null;

        public static Texture2D Texture = null;

        private static Sprite sprite = null;

        public static Sprite Sprite
        {
            get
            {
                if (Texture == null)
                {
                    return null;
                }

                if (sprite == null)
                {
                    sprite = Sprite.Create(Texture, new Rect(Vector2.zero, new Vector2(Texture.width, Texture.height)), new Vector2(0.5f, 0.5f));
                }

                return sprite;
            }
        }
    }
}
