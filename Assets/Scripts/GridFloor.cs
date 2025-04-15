//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Serialization;

//public class GridFloor : MonoBehaviour
//{
//    private Material originMaterial;
//    private Renderer renderer;

//    [NonSerialized] public GameObject CurrentCharacter;

//    // Start is called before the first frame update
//    void Awake()
//    {
//        renderer = GetComponent<Renderer>();
//        originMaterial = renderer.material;
//    }

//    private void OnMouseDown()
//    {
//        PlayerController.Instance.OnMouseDownFromFloor(this);
//    }

//    private void OnMouseEnter()
//    {
//        renderer.material = MaterialManager.Instance.outlineMaterial;
//    }

//    private void OnMouseExit()
//    {
//        renderer.material = originMaterial;
//    }
//}
