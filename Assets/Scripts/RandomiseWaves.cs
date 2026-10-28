using System; using UnityEngine;
using Random = UnityEngine.Random;


public class RandomiseWaves : MonoBehaviour
{
    [SerializeField] private Material _material;

    private void Awake()
    {   
        _material = GetComponent<Renderer>().material;
    }

    void Start()
    {
        _material.SetVector("Wave Direction", RandomV4());
        // Unity.Mathematics.Random random = new Unity.Mathematics.Random();

        // float f1 = Random.value;
        // float f2 = Random.value;
        // _material.SetFloat("Random Seed A", f1);
        // _material.SetFloat("Random Seed B", f2);
    }

    private Vector4 RandomV4()
    {
        float f1 = Random.value;
        float f2 = Random.value;
        float f3 = Random.value;
        return new Vector4(f1, f2, f3, 0);
    }
    
    // Update is called once per frame
    void Update()
    {
        float f1 = Random.value;
        _material.SetFloat("Wave Height", f1);
        // _material.SetVector("Wave Direction", RandomV4());
        
        // Unity.Mathematics.Random random = new Unity.Mathematics.Random();
        // float f1 = random.NextFloat();
        // float f1 = Random.value*10;
        // float f2 = Random.value*10;
        // _material.SetFloat("Random Seed A", f1);
        // _material.SetFloat("Random Seed B", f2);
        // _material.SetFloat("Wave Height", f1);
        //
        // Debug.Log(f2 + " : "+_material.GetFloat("Random Seed B"));
        // float f2 = random.NextFloat();
    }
}
