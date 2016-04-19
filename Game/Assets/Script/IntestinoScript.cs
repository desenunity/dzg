using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntestinoScript : MonoBehaviour
{

    public float incremento = .0005f;

    float valor = 0;
    float inicio;

    public Image imagem;

    // Use this for initialization
    void Start()
    {
        inicio = Time.time;
    }

    private void NotificarPlayerTocarSirene()
    {
        /* GameObject player = GameObject.FindGameObjectWithTag("Player");

         if (player != null)
             player.GetComponent<PlayerScript>().TocarSireneDesespero();*/
    }

    // Update is called once per frame
    void Update()
    {
        /*float valor = Time.time - inicio;
        float incrementoDelta = incremento * Time.deltaTime;
		
        if (valor > .05f)
        {
            if ((imagem.fillAmount + incrementoDelta) < 1.0f)
            {
                imagem.fillAmount += incrementoDelta;
                inicio = Time.time;
            }
            else
            {
                inicio = Time.time;
                imagem.fillAmount = 0;
            }

            if (imagem.fillAmount >= .7f)
                NotificarPlayerTocarSirene();*/

    }
}

