using System.Collections;
using GameScene.LevelGeneration;
using GameScene.ResourceSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Architecture
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private int _seed = 0;
        
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private Generator _generator;
        [SerializeField] private VaultUI _ui;
    
        private void Start()
        {
            if (_seed == 0)
            {
                _seed = Random.Range(int.MinValue, int.MaxValue);
            }
            Random.InitState(_seed);
            StartCoroutine(Load());

            Vault vault = new();
            _ui.Initialize(vault);
        }

        private IEnumerator Load()
        {
            _levelGenerator.Generate();
            yield return null;
            yield return null;
            _generator.Generate();
            yield return null;
            yield return null;
        }
    }
}
