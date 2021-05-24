using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviourPun
{ 
    public static EnemyAI instance;
    [HideInInspector]public NavMeshAgent agent;
    [SerializeField] private GameObject[] destination;
    private Rigidbody rb; int i = 0;
    [HideInInspector] public TMP_Text EnemyNameText;
    [SerializeField] private Material mat;
    [SerializeField] private SkinnedMeshRenderer render;
    public bool canMove;  
    Color tempcolor;

    void Start()
    {
        mat = render.materials[0];

        canMove = true;
        if (instance == null)
        {
            instance = this;
        }

        EnemyNameText.text = "FallDown#" + Random.Range(0000, 9999);
        destination = GameObject.FindGameObjectsWithTag("Destination");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        tempcolor  = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
    }
   
    private void Update()
    {
        mat.color = tempcolor;
        mat.SetColor("_EmissionColor", tempcolor);
        if (canMove && GameManager.instace.CountDownOver)
        {

            GetComponent<Animator>().Play("run");

            if (GameManager.instace.firstPlayer) agent.isStopped = true;
            agent.SetDestination(destination[i].transform.position);

            if (transform.position.z >= destination[i].transform.position.z)
            {
                i++;
                if (i >= destination.Length)
                {
                    agent.isStopped = true;
                    canMove = false;
                }                
            }

            //if (this.transform.position.y <= -15f)
            //{
            //    Destroy(this.gameObject);
            //    PhotonNetwork.Instantiate(GameManager.instace.EnemyPrefab.name, GameManager.instace.InitialRespawnpos.transform.position, Quaternion.identity, 0);
            //    GetComponent<Animator>().Play("run");
            //}
        }
    }
   

 
}
