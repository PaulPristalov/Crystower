using Assets.Scripts.Interfaces;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Assets.Scripts.Settings.constants;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Settings
{
    internal class SettingsUI : MonoBehaviour
    {
        private IFileManager _fileManager;

        [SerializeField] Assets.Scripts.Settings.Vault settingsVault;

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

        public void Init(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        private void Start()
        {
            saveButton.onClick.AddListener(SaveSettings);

            volumeSlider.value = settingsVault.volume;
            musicSlider.value = settingsVault.musicVolume;
            isVibration.isOn = settingsVault.isVibration;
        }

        private void SaveSettings()
        {
            settingsVault = new()
            {
                volume = volumeSlider.value,
                isVibration = isVibration.isOn,
                musicVolume = musicSlider.value
            };

            _fileManager.Save(FileNames.SETTINGS_NAME, settingsVault);

            _fileManager.Load(FileNames.SETTINGS_NAME, settingsVault);
        }
    }
}
