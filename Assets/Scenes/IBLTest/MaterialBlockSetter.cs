using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBlockSetter : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;
    [Range(0f, 1f),SerializeField]
    private float ao = 1f;
    //MaterialPropertyBlock props;// = new MaterialPropertyBlock();

    // Start is called before the first frame update
    void Start()
    {
        //props = new MaterialPropertyBlock();
        //renderer.SetPropertyBlock(props);
    }

    // Update is called once per frame
    void Update()
    {
        var props = new MaterialPropertyBlock();
        props.SetFloat("_OcclusionStrength", ao);
        renderer.SetPropertyBlock(props);
    }
}
