using System;
using TMPro;
using UnityEngine;

namespace PixelFire.Localization
{
    public class LocalizationObjectController : MonoBehaviour
    {
        [Serializable]
        public class TextObject
        {
            public TMP_Text Text { get; private set; }
            public string Key { get; private set; }

            public TextObject(TMP_Text text, string key)
            {
                Text = text;
                Key = key;
            }
        }

        [SerializeField] private TextObject[] _objects;

        private void Start()
        {
            Localization.OnLanguageChanged += UpdateText;
            UpdateText();
        }

        private void OnDestroy()
        {
            Localization.OnLanguageChanged -= UpdateText;
        }

        private void UpdateText()
        {
            foreach (var obj in _objects)
            {
                obj.Text.text = Localization.Get(obj.Key);
            }
        }
    }
}
