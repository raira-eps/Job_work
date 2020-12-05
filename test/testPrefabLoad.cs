using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPrefabLoad : MonoBehaviour
{
    [SerializeField]
    GameObject zero;
    private void Awake()
    {
        var prefab = Resources.Load("Prefabs/TransitionLCanvas");
        Instantiate(prefab,zero.transform.position, Quaternion.identity);
    }
}
