using UnityEngine;
using System.Collections;

public class EsferaColisorDisparoScript : MonoBehaviour
{

    public GameObject EfeitoTiro;

    AudioSource audio;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlanoDisparo")
        {
            GameObject obj = (GameObject)Instantiate(EfeitoTiro);
            obj.transform.parent = this.transform;

            obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

            other.gameObject.GetComponent<PlanoDisparoScript>().Disparar();
            audio.PlayOneShot(audio.clip, .5f);
        }
    }
}
