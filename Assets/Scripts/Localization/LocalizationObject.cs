using UnityEngine;

namespace PixelFire.Localization
{
    public class LocalizationObject : MonoBehaviour
    {
        [SerializeField] private Language _language = Language.ru;

        private void Start()
        {
            Localization.OnLanguageChanged += UpdateEnable;
            UpdateEnable();
        }

        private void OnDestroy()
        {
            Localization.OnLanguageChanged -= UpdateEnable;
        }

        private void UpdateEnable()
        {
            gameObject.SetActive(_language == Localization.Language);
        }
    }
}
