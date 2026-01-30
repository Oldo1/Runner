using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private GameObject _coinsRoad;

        public bool TryGetSegmentCoins(out IEnumerable<GameObject> coins)
        { 
            if (_coinsRoad != null)
            {
                coins = _coinsRoad.GetComponentsInChildren<Coin>(true).Select(x => x.gameObject);
                return true;
            }
            coins = null;
            return false;
        }
    }
}
