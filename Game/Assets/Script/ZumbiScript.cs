using UnityEngine;
using System.Collections;

public class ZumbiScript : MonoBehaviour
{
    public bool JogarCatarroPlayer;

    public float Energia = 100;
    public GameObject[] EfeitoSangue1;

    private Animator animacao;
    GameObject player;
    NavMeshAgent agenteNav;
    public GameObject objExplosao;

    Renderer rend;
    Color corOriginal;
    Color novacor;

    float incrementoCor = 0;
    float tempoAlternarCor;
    bool alternarCor = false;

    public Texture2D TexturaZumbi;
    public Texture2D TexturaZumbiClose;
    public Texture2D TexturaZumbiBocaAberta;

    float tempoUltimoPisca;
    float tempoUltimoPiscaClose;
    float tempoEntrePisacadas;

    public GameObject CatarroObj;

    public AudioClip SomCatarro1;
    public AudioClip SomCatarro2;
    public AudioClip SomCatarro3;
    public AudioClip SomCatarro4;

    private AudioSource audioS;

    float timeUltimoCatarro = 0;

    float timeUltimoTiro = 0;

    // Use this for initialization
    void Start()
    {
        animacao = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();

        animacao.SetBool("Caminhando", false);
        player = GameObject.FindWithTag("Player");
        agenteNav = GetComponent<NavMeshAgent>();
        rend = this.transform.Find("ZombieBase_01/ZombieBase_mesh").gameObject.GetComponent<Renderer>();
        corOriginal = rend.material.GetColor("_Color");
        tempoAlternarCor = Time.time;
        tempoUltimoPisca = Time.time;
        CalculaTempoEntrePisacadas();        
    }

    // Update is called once per frame
    void Update()
    {
        //  animacao.SetBool("LevandoTiro", false);

        PersequirPlayer();
        

        if (!VerificarSeVomitaNoPlayer())
            Piscar();

        VerificaSeParaReagirTiro();
        AlternarCor();
    }

    private void CalculaTempoEntrePisacadas()
    {
        tempoEntrePisacadas = Random.Range(2.0f, 5.0f);
    }

    private void ExecutaSomCatarro()
    {
        int r = Random.Range(1, 5);

        switch (r)
        {
            case 1: audioS.PlayOneShot(SomCatarro1);
                break;
            case 2: audioS.PlayOneShot(SomCatarro2);
                break;
            case 3: audioS.PlayOneShot(SomCatarro3);
                break;
            case 4: audioS.PlayOneShot(SomCatarro4);
                break;
        }
    }

    private bool VerificarSeVomitaNoPlayer()
    {
        if ((JogarCatarroPlayer) && ((Time.time - timeUltimoCatarro) > 2))
        {
            rend.material.mainTexture = TexturaZumbiBocaAberta;

            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);

            GameObject cat = (GameObject)GameObject.Instantiate(CatarroObj, pos, Quaternion.identity);
            cat.transform.parent = this.gameObject.transform;

            JogarCatarroPlayer = false;

            ExecutaSomCatarro();

            timeUltimoCatarro = Time.time;
        }
        else
            rend.material.mainTexture = TexturaZumbi;

        return JogarCatarroPlayer;
    }

    private void Piscar()
    {
        if ((Time.time - tempoUltimoPisca) > tempoEntrePisacadas)
        {
            rend.material.mainTexture = TexturaZumbiClose;
            tempoUltimoPisca = Time.time;
            tempoUltimoPiscaClose = tempoUltimoPisca;
            CalculaTempoEntrePisacadas();
        }

        if ((rend.material.mainTexture == TexturaZumbiClose) &&
             ((Time.time - tempoUltimoPiscaClose) > .1f))
        {
            rend.material.mainTexture = TexturaZumbi;
        }

    }

    void PersequirPlayer()
    {
        if (this.Energia > 0)
        {
            RaycastHit hit;

            Vector3 posPlayer = player.transform.position;
            posPlayer.y += 1;

            if ((Physics.Raycast(this.transform.position, (posPlayer - this.transform.position).normalized, out hit)) &&
                (hit.collider.gameObject.tag == "Player"))
            {
                agenteNav.SetDestination(player.transform.position);

                if (hit.distance <= 5)
                    JogarCatarroPlayer = true;
            }

            if ((Mathf.Abs(agenteNav.velocity.x) + Mathf.Abs(agenteNav.velocity.z)) > 0.1f)
                animacao.SetBool("Caminhando", true);
            else
                animacao.SetBool("Caminhando", false);
        }
        else
            agenteNav.enabled = false;
    }

    private void AlternarCor()
    {
        if (incrementoCor > 0)
        {
            if ((Time.time - tempoAlternarCor) > .1f)
            {
                Color novacor = new Color(corOriginal.r + incrementoCor, corOriginal.g, corOriginal.b);

                if (alternarCor)
                    rend.material.SetColor("_Color", novacor);
                else
                    rend.material.SetColor("_Color", corOriginal);

                alternarCor = !alternarCor;

                tempoAlternarCor = Time.time;
            }
        }
        else
            rend.material.SetColor("_Color", corOriginal);
    }

    private void VerificaSeParaReagirTiro()
    {
        float ultimoTiro = Time.time - timeUltimoTiro;

//        Debug.Log(ultimoTiro);

        if (animacao.GetBool("LevandoTiro") && ultimoTiro > 1)
        {
            animacao.SetBool("LevandoTiro", false);
            incrementoCor = 0;
            Energia = 100;
        }
    }

    public void ReagirDisparo()
    {
        if (EfeitoSangue1.Length > 0)
        {
            GameObject efSangue = EfeitoSangue1[Random.Range(0, 2)];

            GameObject g = (GameObject)Instantiate(efSangue);
            g.transform.transform.parent = this.transform;
            g.transform.transform.position = new Vector3(this.transform.position.x + Random.Range(0, 1), this.transform.position.y + Random.Range(0, 3), this.transform.position.z + Random.Range(0, 0));

            GameObject g1 = (GameObject)Instantiate(efSangue);
            g1.transform.transform.parent = this.transform;
            g1.transform.transform.position = new Vector3(this.transform.position.x + Random.Range(0, 1), this.transform.position.y + Random.Range(0, 3), this.transform.position.z + Random.Range(0, .5f));

            this.Energia -= 10;

            incrementoCor += .1f;

            if (this.Energia <= 0)
            {
                animacao.SetBool("Caminhando", true);
                animacao.SetBool("Morrendo", true);

                GameObject ge = (GameObject)Instantiate(objExplosao);
                ge.transform.position = this.transform.position + new Vector3(0, 1, 0);

                GameObject.Destroy(this.gameObject);
            }
            animacao.SetBool("LevandoTiro", true);
            animacao.SetFloat("Percent", .6f);
            timeUltimoTiro = Time.time;
        }
    }
}
