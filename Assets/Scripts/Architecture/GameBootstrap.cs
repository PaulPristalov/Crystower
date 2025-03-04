using System.Collections;
using GameScene.BuildingSystem;
using GameScene.LevelGeneration;
using GameScene.ResourceSystem;
using UnityEngine;

namespace Architecture
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private int _seed = 0;
        
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private Generator _generator;
        [SerializeField] private VaultUI _ui;
        [SerializeField] private BuildingPlacer _buildingPlacer;
    
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
            _buildingPlacer.SetVault(vault);
        }

        private IEnumerator Load()
        {
            _levelGenerator.Generate();
            yield return null;
            _buildingPlacer.PlaceCrystal();
            yield return null;
            _generator.Generate();
        }
    }
}
