using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fizz6.LudumDare45
{
    public class CharacterController0D : MonoBehaviour
    {
        [SerializeField]
        private GameObject textPrefab = null;

        [SerializeField]
        private VerticalLayoutGroup verticalLayoutGroup = null;

        [SerializeField]
        private TMP_InputField inputField = null;

        [SerializeField]
        private double delay = 0.05;

        public bool IsInputEnabled
        {
            get
            {
                return inputField.interactable;
            }

            set
            {
                if (inputField == null)
                {
                    return;
                }

                inputField.interactable = value;
                Focus();
            }
        }

        public event Action<string> InputEvent = null;

        private void Awake()
        {
            inputField.onValueChanged.AddListener(OnValueChanged);
            inputField.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnValueChanged(string value)
        {
            inputField.text = value.Substring(0, Mathf.Min(value.Length, 80));
        }

        private void OnEndEdit(string value)
        {
            if (value == null || value == string.Empty)
            {
                return;
            }

            inputField.text = string.Empty;
            InputEvent?.Invoke(value);
        }

        public async Task WriteToConsole(string name, string value)
        {
            GameObject textGameObject = Instantiate(textPrefab, verticalLayoutGroup.transform);
            TextMeshProUGUI textMeshProUGUI = textGameObject.GetComponentInChildren<TextMeshProUGUI>();
            textMeshProUGUI.text = $"{name}: ";
            await Display(textMeshProUGUI, value);
        }

        private async Task Display(TextMeshProUGUI textMeshProUGUI, string value)
        {
            foreach (char letter in value)
            {
                textMeshProUGUI.text += letter;
                await Task.Delay(TimeSpan.FromSeconds(delay));
            }
        }

        public void Focus()
        {
            if (inputField == null)
            {
                return;
            }

            inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
    }
}