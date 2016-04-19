using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;


public class PlayerScript : MonoBehaviour
{
    private Animator animacao;
    private CharacterController controller;
  //  private GameObject GameObjectMaterial;

    public GameObject PeidoNormal;
    public GameObject PeidoSogra;
    public GameObject PeidoRapido;
    public GameObject SireneDesespero;

    public GameObject PeidoAlivio1;
    public GameObject PeidoAlivio2;
    public GameObject PeidoAlivio3;
    public GameObject PeidoAlivio4;

    private GameObject sireneDesesperoInstancia;

    private float frequenciaPeido = 15;

    private float velocidade = 4.5f;

    private bool peidando = false;

    private float malDaBarriga = 0;

    //dfsd
    private float ultimoPeido; //444   

    Fisheye fisheyeCamera;

    GameObject alivioPeido = null;

    bool subirTwirl;
    bool descerTwirl;
    bool inicioTwirl;

    GameObject spine;

    // Use this for initialization
    void Start()
    {
        animacao = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
      //  GameObjectMaterial = (GameObject)GameObject.FindWithTag("Material_Player");
        ultimoPeido = Time.time;
        fisheyeCamera = Camera.main.GetComponent<Fisheye>();

        subirTwirl = false;
        descerTwirl = false;
        inicioTwirl = false;

        spine = this.transform.Find("HeroBase_01/Base/Base Pelvis/Base Spine").gameObject;
        animacao.SetFloat("MalDaBarriga", 0);
    }

    // Update is called once per frame
    void Update()
    {
        AtualizarValores();
        LeMovimento();
        VerificarSePeidar();
        AplicaTwirl();
        MatarInstanciaAlivioPeido();
        TocarSireneDesespero();
        
    }

    private void MatarInstanciaAlivioPeido()
    {
      //  if (alivioPeido != null)
      //      GameObject.Destroy(alivioPeido.gameObject);
    }

    private void EscolherEfeitoAlivio()
    {
        int r = Random.Range(1, 5);

        switch (r) 
        {
            case 1 :
                alivioPeido = GameObject.Instantiate(PeidoAlivio1);
                break;
            case 2:
                alivioPeido = GameObject.Instantiate(PeidoAlivio2);
                break;
            case 3:
                alivioPeido = GameObject.Instantiate(PeidoAlivio3);
                break;
            case 4 :
                alivioPeido = GameObject.Instantiate(PeidoAlivio4);
                break;
        }
    }

    void LateUpdate()
    {
        Vector3 pos = Camera.main.transform.position;
        pos.x = this.transform.position.x;
        pos.z = this.transform.position.z - 7;
        Camera.main.transform.position = pos;
    }

    private void AplicaTwirl()
    {
        if (inicioTwirl)
        {
            fisheyeCamera.strengthX = .05f;
            fisheyeCamera.strengthY = .05f;
            inicioTwirl = false;
        }

        if (subirTwirl)
        {
            fisheyeCamera.strengthX = Mathf.Lerp(fisheyeCamera.strengthX, 0.4f, Time.deltaTime * 8.0f);
            fisheyeCamera.strengthY = Mathf.Lerp(fisheyeCamera.strengthY, 0.4f, Time.deltaTime * 8.0f);

            if (fisheyeCamera.strengthX >= 0.38f)
            {
                subirTwirl = false;
                descerTwirl = true;

                fisheyeCamera.strengthX = 0.4f;
                fisheyeCamera.strengthY = 0.4f;
            }
        }
        else if (descerTwirl)
        {
            fisheyeCamera.strengthX = Mathf.Lerp(fisheyeCamera.strengthX, 0.05f, Time.deltaTime * 8.0f);
            fisheyeCamera.strengthY = Mathf.Lerp(fisheyeCamera.strengthY, 0.05f, Time.deltaTime * 8.0f);

            if (fisheyeCamera.strengthX <= 0.053f)
            {
                subirTwirl = false;
                descerTwirl = false;
                peidando = false;

                fisheyeCamera.strengthX = 0.05f;
                fisheyeCamera.strengthY = 0.05f;

                EscolherEfeitoAlivio();
                animacao.SetBool("Peidando", false);
            }
        }
    }

    private void LeMovimento()
    {
        float horValue = Input.GetAxis("Horizontal");
        float vertValue = Input.GetAxis("Vertical");

        if ((horValue != 0) || (vertValue != 0))
        {
            Vector3 input = new Vector3(horValue, 0, vertValue);
            transform.rotation = Quaternion.LookRotation(input);

            Vector3 forcaMov = Vector3.forward * velocidade;

            forcaMov.y = 0;

            controller.Move(transform.TransformDirection(forcaMov) * Time.deltaTime);
            animacao.SetFloat("Velocidade", 1);
        }
        else
            animacao.SetFloat("Velocidade", 0);

        animacao.SetBool("Atirando",  Input.GetKey(KeyCode.T));
    }

    void VerificarSePeidar()
    {
        if ((!peidando) && ((Time.time - ultimoPeido) > frequenciaPeido))
        {
            Peidar();
            ultimoPeido = Time.time;
        }
    }

    void AtualizarValores()
    {
        if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= 0 && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .1f)
        {
            frequenciaPeido = 12;
            velocidade = 4.5f;
            malDaBarriga = 0;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .1f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .2f)
        {
            frequenciaPeido = 11;
            velocidade = 4.3f;
            malDaBarriga = .1f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .2f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .3f)
        {
            frequenciaPeido = 10;
            velocidade = 4.1f;
            malDaBarriga = .2f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .3f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .4f)
        {
            frequenciaPeido = 9;
            velocidade = 4.0f;
            malDaBarriga = .2f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .4f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .5f)
        {
            frequenciaPeido = 8;
            velocidade = 3.5f;
            malDaBarriga = .3f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .5f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .6f)
        {
            frequenciaPeido = 7;
            velocidade = 3.5f;
            malDaBarriga = .3f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .6f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .7f)
        {
            frequenciaPeido = 6;
            velocidade = 3.0f;
            malDaBarriga = .3f;
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .7f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .8f)
        {
            frequenciaPeido = 5;
            velocidade = 2.5f;
            malDaBarriga = .35f;
            //animacao.SetFloat("MalDaBarriga", 0.4f);
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .8f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= .9f)
        {
            frequenciaPeido = 4;
            velocidade = 2.0f;
            malDaBarriga = .35f;
            //animacao.SetFloat("MalDaBarriga", 0.45f);
        }
        else if (Globais.ValorPreenchimentoIntestinoGrossoMerda >= .9f && Globais.ValorPreenchimentoIntestinoGrossoMerda <= 1.0f)
        {
            frequenciaPeido = 3;
            velocidade = 1.5f;
            malDaBarriga = .35f;
            //animacao.SetFloat("MalDaBarriga", 0.5f);
        }
    }
    
    void Peidar()
    {
        peidando = true;
        inicioTwirl = true;
        subirTwirl = true;
        descerTwirl = false;

        animacao.SetBool("Peidando", true);
        
        animacao.SetFloat("MalDaBarriga", malDaBarriga);       

        GameObject peido =
            (GameObject)Instantiate(
                SorteiaPeido(),
                new Vector3(this.transform.position.x, .7f, this.transform.position.z),
                this.transform.rotation);
        
        //peido.transform.parent = this.transform;

    }

    GameObject SorteiaPeido()
    {
        int p = Random.Range(1, 15);

//        Debug.Log(p);

        if (p >= 1 && p <= 5)
            return PeidoNormal;
        else if (p >= 6 && p <= 10)
            return PeidoSogra;
        else
            return PeidoRapido;
    }

    private void TocarSireneDesespero()
    {
        if ((Globais.TocarSireneDesespero) && (sireneDesesperoInstancia == null))
        {
            sireneDesesperoInstancia = (GameObject)Instantiate(SireneDesespero, this.transform.position, this.transform.rotation);
            sireneDesesperoInstancia.transform.parent = this.transform;
        }
    }

    public void PararSireneDesespero()
    {
        GameObject.Destroy(sireneDesesperoInstancia);
    }

    void Atirar()
    {

    }
}
