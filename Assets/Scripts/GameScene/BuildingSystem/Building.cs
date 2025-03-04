using UnityEngine;

namespace GameScene.BuildingSystem
{
    public class Building : BuildingGridObject, IClickable
    {
        [field: SerializeField] public int[] ResourceCost { get; private set; }

        public void Click()
        {
            
        }
    }
}
