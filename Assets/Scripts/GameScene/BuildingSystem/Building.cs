using UnityEngine;

namespace GameScene.BuildingSystem
{
    public class Building : BuildingGridObject, IClickable
    {
        public int health = 0;

        public void Click()
        {
            health++;
        }
    }
}
