using Assets.Scripts.Settings.constants;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Settings
{
    internal class SettingsUI : MonoBehaviour
    {
        private IFileManager _fileManager;

        private Assets.Scripts.Settings.Vault _settingsVault;

        [Space]
        [Header("UI elements")]
        [Tooltip("Toggle for vibration")]
        [SerializeField] private Toggle isVibration;


        [Tooltip("Slider for overall volume")]
        [SerializeField] private Slider volumeSlider;

        [Tooltip("Slider for music")]
        [SerializeField] private Slider musicSlider;

        //[Tooltip("Graphics")]
        //[SerializeField] Graphics graphics;

        [Tooltip("Language editing")]
        [SerializeField] private TMP_Dropdown languageEdit;

        [Tooltip("Button for save settings")]
        [SerializeField] private Button saveButton;

        public void Init(IFileManager fileManager, Assets.Scripts.Settings.Vault settingsVault)
        {
            _fileManager = fileManager;
            _settingsVault = settingsVault;
        }

        private void OnEnable()
        {
            print("Settings is init");  

            volumeSlider.value = _settingsVault.volume;
            musicSlider.value = _settingsVault.musicVolume;
            isVibration.isOn = _settingsVault.isVibration;
        }

        private void Start()
        {
            saveButton.onClick.AddListener(SaveSettings);
        }

        private void SaveSettings()
        {
            _settingsVault = new()
            {
                volume = volumeSlider.value,
                isVibration = isVibration.isOn,
                musicVolume = musicSlider.value
            };

            _fileManager.Save(FileNames.SETTINGS_NAME, _settingsVault);

            _settingsVault = _fileManager.Load<Assets.Scripts.Settings.Vault>(FileNames.SETTINGS_NAME);
        }
    }
}
