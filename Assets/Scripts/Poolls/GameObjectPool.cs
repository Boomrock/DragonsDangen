using UnityEngine;

public class GameObjectPool : ObjectPool<GameObject>
{
    public GameObjectPool(int startCount, GameObject prefab) 
        : base(startCount, () => Preload(prefab), Get, Return) { }

    public static GameObject Preload(GameObject prefab) => Object.Instantiate(prefab);

    private static void Get(GameObject prefab) => prefab.SetActive(true);

    private static void Return(GameObject prefab) => prefab.SetActive(false);
}
