using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fizz6.LudumDare45
{
    public class Story : MonoBehaviour
    {
        private enum States
        {
            Name,
            Draw,
            Done
        }

        [SerializeField]
        private CharacterController0D characterController0D = null;

        [SerializeField]
        private GameObject background = null;

        [SerializeField]
        private GameObject drawingRootGameObject = null;

        [SerializeField]
        private RawImage drawingRawImage = null;

        private float drawingColor = 0.5f;
        private float drawingColorDelta = 0.1f;

        [SerializeField]
        private TextMeshProUGUI drawingColorTextMeshProUGUI = null;

        [SerializeField]
        private Button doneDrawingButton = null;

        [SerializeField]
        private float waitForTime = 1.0f;

        private States state = States.Name;

        private readonly string nothingName = "Nothing";

        private async void Awake()
        {
            Player.Texture = new Texture2D(32, 32);
            Color[] colors = new Color[Player.Texture.width * Player.Texture.height];
            colors.Select(color => Color.clear);
            Player.Texture.SetPixels(colors);
            Player.Texture.Apply();
            Player.Texture.wrapMode = TextureWrapMode.Clamp;
            Player.Texture.filterMode = FilterMode.Point;
            drawingRawImage.texture = Player.Texture;

            doneDrawingButton.onClick.AddListener(OnDoneDrawing);
            characterController0D.InputEvent += OnInput;

            await WriteToConsole(nothingName, "Hey... wait... what was your name again?");
        }

        private void OnDestroy()
        {
            doneDrawingButton.onClick.RemoveListener(OnDoneDrawing);
            characterController0D.InputEvent -= OnInput;
        }

        private async void OnInput(string value)
        {
            switch (state)
            {
                case States.Name:
                    Player.Name = value;
                    await PlayNameSequence();
                    break;
                case States.Draw:
                    break;
                default:
                    break;
            }

            state++;
        }

        private async void OnDoneDrawing()
        {
            await Exit();
        }

        private async Task WriteToConsole(string name, string value, double delay = 0.5)
        {
            characterController0D.IsInputEnabled = false;
            await characterController0D.WriteToConsole(name, value);
            await Task.Delay(TimeSpan.FromSeconds(delay));
            characterController0D.IsInputEnabled = true;
        }

        private async Task PlayNameSequence()
        {
            await WriteToConsole(Player.Name, $"{Player.Name}...");
            await WriteToConsole(nothingName, $"I'm just kidding {Player.Name}, of course I knew that!");
            await WriteToConsole(nothingName, "OooOOOoo, what's this?");
            await WriteToConsole(Player.Name, $"...", 1.0);
            await WriteToConsole(Player.Name, $"...", 2.0);
            await WriteToConsole(Player.Name, "Nothing...?");
            await WriteToConsole(Player.Name, "Nothing...?!", 0.5);
            await WriteToConsole(Player.Name, "NOTHING...?!", 0.2);
            await WriteToConsole(Player.Name, "NOOOOOTHIIING...?! Where'd you go?!", 2.0);
            await WriteToConsole(Player.Name, "...", 0.2);
            await WriteToConsole(Player.Name, "Wait... what is this?", 0.2);
            PlayDrawSequence();
        }

        private void PlayDrawSequence()
        {
            Cursor.visible = true;
            drawingRootGameObject.SetActive(true);
        }

        private void Update()
        {
            if (drawingRawImage == null)
            {
                return;
            }

            if (state == States.Draw)
            {
                if (Input.mouseScrollDelta.y > 0)
                {
                    drawingColor = Mathf.Clamp(drawingColor - drawingColorDelta, 0.0f, 1.0f);
                    drawingColorTextMeshProUGUI.text = $"{(int)(drawingColor * 100)}%";

                }
                else if (Input.mouseScrollDelta.y < 0)
                {
                    drawingColor = Mathf.Clamp(drawingColor + drawingColorDelta, 0.0f, 1.0f);
                    drawingColorTextMeshProUGUI.text = $"{(int)(drawingColor * 100)}%";
                }

                Vector2 mousePosition = Input.mousePosition;

                RectTransform drawingImageRectTransform = (RectTransform)drawingRawImage.transform;
                Rect drawingImageRect = RectTransformToScreenSpace(drawingImageRectTransform);

                Vector2 localPosition = new Vector2(mousePosition.x - drawingImageRect.x + drawingImageRect.width / 2.0f, mousePosition.y - drawingImageRect.y + drawingImageRect.height / 2.0f) / drawingImageRectTransform.lossyScale;
                Vector2Int roundedLocalPosition = new Vector2Int(Mathf.RoundToInt(localPosition.x), Mathf.RoundToInt(localPosition.y));

                if (roundedLocalPosition.x >= 0 && roundedLocalPosition.x < Player.Texture.width && roundedLocalPosition.y >= 0 && roundedLocalPosition.y < Player.Texture.height)
                {
                    if (Input.GetMouseButton(0))
                    {
                        Player.Texture.SetPixel(roundedLocalPosition.x, roundedLocalPosition.y, new Color(drawingColor, drawingColor, drawingColor));
                        Player.Texture.Apply();
                    }
                    else if (Input.GetMouseButton(1))
                    {
                        Player.Texture.SetPixel(roundedLocalPosition.x, roundedLocalPosition.y, Color.clear);
                        Player.Texture.Apply();
                    }
                }
            }
        }

        private async Task Exit()
        {
            for (int index = 0; index < background.transform.childCount; ++index)
            {
                Transform child = background.transform.GetChild(index);
                Destroy(child.gameObject);
            }

            Image image = background.GetComponent<Image>();
            for (int index = 0; index < 100; index++)
            {
                float alpha = 1.0f - index / 100.0f;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                await Task.Delay(TimeSpan.FromSeconds(0.01));
            }
        }

        public static Rect RectTransformToScreenSpace(RectTransform transform)
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            float x = transform.position.x + transform.anchoredPosition.x;
            float y = Screen.height - transform.position.y - transform.anchoredPosition.y;

            return new Rect(x, y, size.x, size.y);
        }
    }
}