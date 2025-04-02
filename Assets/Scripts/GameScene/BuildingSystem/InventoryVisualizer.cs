using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BuildingSystem
{
    public class InventoryVisualizer : MonoBehaviour
    {
        [SerializeField] private BuildingPlacer _placer;
        [SerializeField] private BuildingVisual[] _visuals;

        private void Start()
        {
            UpdateVisual();
        }

        public void Inititialize()
        {
            
        }

        private void UpdateVisual()
        {
            for (int i = 0; i < _placer.Buildings.Length; i++)
            {
                _visuals[i].Image.sprite = _placer.Buildings[i].Sprite;
                _visuals[i].Text.text = _placer.Buildings[i].Name;
            }
        }

        [Serializable]
        public class BuildingVisual
        {
            [field: SerializeField] public Image Image;
            [field: SerializeField] public TMP_Text Text;
        }
    }
}
