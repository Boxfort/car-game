using UnityEngine;
using System.Collections;

public class EventManagerScript : MonoBehaviour {

    public delegate void OnCollectableGet();
    public delegate void OnPlayerDestoyed();
    public static event OnCollectableGet OnCollectableGetMethods;
    public static event OnPlayerDestoyed OnPlayerDestroyedMethods;

	public static void IncrementCollectable()
    {
        OnCollectableGetMethods();
    }

    public static void EndGame()
    {
        OnPlayerDestroyedMethods();
    }
}
