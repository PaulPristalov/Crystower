using System;
using TMPro;
using UnityEngine;

namespace GameScene.ResourceSystem
{
    public class VaultUI : MonoBehaviour
    {
        [SerializeField] private ResourceObject[] _resources;
        private Vault _vault;

        public void Initialize(Vault vault)
        {
            _vault = vault ?? throw new ArgumentNullException();
            _vault.OnCountChanged += UpdateVisual;
            UpdateVisual();
        }

        public void OnDestroy()
        {
            if (_vault != null) _vault.OnCountChanged -= UpdateVisual;
        }

        private void UpdateVisual()
        {
            foreach (var resource in _resources)
            {
                resource.text.text = _vault.Get(resource.type).ToString();
            }
        }
    }

    [Serializable]
    public class ResourceObject
    {
        public TMP_Text text;
        public ResourceType type;
    }
}
