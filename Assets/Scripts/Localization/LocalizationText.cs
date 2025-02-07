using TMPro;
using UnityEngine;

namespace PixelFire.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizationText : MonoBehaviour
    {
        private TMP_Text _text;
        [SerializeField] private string _key;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            Localization.OnLanguageChanged += UpdateText;
            UpdateText();
        }

        private void OnDestroy()
        {
            Localization.OnLanguageChanged -= UpdateText;
        }

        private void UpdateText()
        {
            _text.text = Localization.Get(_key);
        }
    }
}
