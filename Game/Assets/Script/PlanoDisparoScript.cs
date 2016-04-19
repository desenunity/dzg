using UnityEngine;
using System.Collections;

public class PlanoDisparoScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Disparar()
	{
		//GameObject.CreatePrimitive(PrimitiveType.Sphere);
		
		RaycastHit hit;
		
		if ((Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out hit)) &&
			(hit.collider.gameObject.tag == "Zumbi_Verde"))
			hit.collider.gameObject.GetComponent<ZumbiScript>().ReagirDisparo();
	}
}
