using UnityEngine;
public class script1 : MonoBehaviour
{
    public Camera t;
    void Start()
    {
       
    }
    void Update()
    {
         var renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = t.activeTexture;
    }
}