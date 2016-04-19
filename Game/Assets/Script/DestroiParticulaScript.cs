using UnityEngine;
using System.Collections;

public class DestroiParticulaScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GameObject.Destroy(this.gameObject, GetComponent<ParticleSystem>().duration);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
