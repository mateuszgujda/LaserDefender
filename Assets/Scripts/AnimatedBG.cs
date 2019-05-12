using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBG : MonoBehaviour
{


    Vector3 StartingPos;
    Material backgroundMaterial= null;
    [SerializeField]float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;       
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0,speed* Time.deltaTime);
        backgroundMaterial.mainTextureOffset += offset;
        if(backgroundMaterial.mainTextureOffset.y >=1)
        {
            backgroundMaterial.mainTextureOffset = new Vector2(backgroundMaterial.mainTextureOffset.x, 0f);
        }
    }
}
