using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GerenciadorEntestinoScript : MonoBehaviour {

    public GameObject ImageIntestinoDelgado;

    public GameObject ImageIntestinoGrosso;

    public Image ImageIntestinoGrossoMerda;

    private Vector3 scaleIntestinoDelgado;

    private Vector3 scaleIntestinoGrosso;

    private int fatorDelgado = 1;
    private int fatorGrosso = 1;

    private float velocidade = 3;

    public float incremento = 0.0005f;

    private float VelocidadePulsarIntestinoGrosso = 2;
    private float IntensidadePulsarIntestinoGrosso = 1.0f;

    float valor = 0;
    float inicio;

	// Use this for initialization
	void Start () {
        scaleIntestinoDelgado = new Vector3(1.02f, 1.02f, 0);
        scaleIntestinoGrosso = new Vector3(1, 1, 0);

        inicio = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        FazerIntestinoDelgadoPulsar();        
        FazerIntestinoGrossoPulsar();
        PreencherIntestinoMerda();        

        Globais.ValorPreenchimentoIntestinoGrossoMerda = ImageIntestinoGrossoMerda.fillAmount;
	}

    private void FazerIntestinoDelgadoPulsar()
    {
        if (fatorDelgado == 1)
            scaleIntestinoDelgado += new Vector3(0.01f, 0.01f, 0f) * Time.deltaTime * velocidade;
        else
            scaleIntestinoDelgado -= new Vector3(0.01f, 0.01f, 0f) * Time.deltaTime * velocidade;

        ImageIntestinoDelgado.transform.localScale = scaleIntestinoDelgado;

        if (((fatorDelgado == 1) && (scaleIntestinoDelgado.x >= 1.04)) || ((fatorDelgado == -1) && (scaleIntestinoDelgado.x <= 1.02f)))
            fatorDelgado *= -1;
    }

    private void AtualizaValoresIntestinoGrosso()
    {
        if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= 0 && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .1f)
        {
            VelocidadePulsarIntestinoGrosso = 4;
            IntensidadePulsarIntestinoGrosso = 1.01f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .1f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .2f)
        {
            VelocidadePulsarIntestinoGrosso = 6;
            IntensidadePulsarIntestinoGrosso = 1.02f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .2f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .3f)
        {
            VelocidadePulsarIntestinoGrosso = 8;
            IntensidadePulsarIntestinoGrosso = 1.03f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .3f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .4f)
        {
            VelocidadePulsarIntestinoGrosso = 12;
            IntensidadePulsarIntestinoGrosso = 1.04f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .4f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .5f)
        {
            VelocidadePulsarIntestinoGrosso = 14;
            IntensidadePulsarIntestinoGrosso = 1.05f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .5f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .6f)
        {
            VelocidadePulsarIntestinoGrosso = 16;
            IntensidadePulsarIntestinoGrosso = 1.052f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .6f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .7f)
        {
            VelocidadePulsarIntestinoGrosso = 18;
            IntensidadePulsarIntestinoGrosso = 1.054f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .7f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .8f)
        {
            VelocidadePulsarIntestinoGrosso = 20;
            IntensidadePulsarIntestinoGrosso = 1.056f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .8f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .9f)
        {
            IntensidadePulsarIntestinoGrosso = 1.058f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .9f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= 1.0f)
        {
            VelocidadePulsarIntestinoGrosso = 44;
            IntensidadePulsarIntestinoGrosso = 1.06f;
        }        
    }

    private void FazerIntestinoGrossoPulsar() 
    {
        AtualizaValoresIntestinoGrosso();

        if (fatorGrosso == 1)
            scaleIntestinoGrosso += new Vector3(0.01f, 0.01f, 0f) * Time.deltaTime * VelocidadePulsarIntestinoGrosso;
        else
            scaleIntestinoGrosso -= new Vector3(0.01f, 0.01f, 0f) * Time.deltaTime * VelocidadePulsarIntestinoGrosso;

        ImageIntestinoGrosso.transform.localScale = scaleIntestinoGrosso;
        ImageIntestinoGrossoMerda.transform.localScale = scaleIntestinoGrosso;

        if (((fatorGrosso == 1) && (scaleIntestinoGrosso.x >= IntensidadePulsarIntestinoGrosso)) || ((fatorGrosso == -1) && (scaleIntestinoGrosso.x <= 1)))
            fatorGrosso *= -1;
    }

    private void PreencherIntestinoMerda()
    {
       float valor = Time.time - inicio;
       float incrementoDelta = incremento * Time.deltaTime;

       if (valor > .05f)
       {
           if ((ImageIntestinoGrossoMerda.fillAmount + incrementoDelta) < 1.0f)
           {
               ImageIntestinoGrossoMerda.fillAmount += incrementoDelta;
               inicio = Time.time;
           }
           else
           {              
               Globais.TocarSireneDesespero = true;
           }
       }
    }

    public void DarMaisTempoIntestino()
    {
        ImageIntestinoGrossoMerda.fillAmount -= .1f;
    }
}
