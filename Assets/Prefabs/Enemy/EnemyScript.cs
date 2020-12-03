using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //public Transform player;
    public LayerMask aggroLayerMask; // MAKE SURE TO ASSIGN TO PLAYER
    private Collider[] withinAggroColliders;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private Player playerObject;
    [SerializeField]
    private float MovementSpeed;
    private HealthSystem health;
    [SerializeField]
    private float aggroRadious;
    [SerializeField]
    private float EnemyDistanceRun = 4.0f;
    private bool lit;
    private Vector3 newPos;
    private Rigidbody rb;
    
    public bool seePlayer{set; get;}
    private bool runFromPlayer = false;

    [SerializeField]
    int damageAmount = 5;

    // Start is called before the first frame update
    private void Start(){
        newPos = this.transform.position;
        //this.transform.position.y = 2;
        rb = GetComponent<Rigidbody>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        health = new HealthSystem(100);
        playerObject = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
    }
    private void FixedUpdate(){
        withinAggroColliders = Physics.OverlapSphere(transform.position, aggroRadious, aggroLayerMask );
        if(withinAggroColliders.Length > 0 && !runFromPlayer){
            //Debug.Log("Found Player");
            ChasePlayer(withinAggroColliders[0].GetComponent<Player>());
        }
    }

    public void TakeDamage(int amount){
        runFromPlayer = true;
        health.Damage(amount);
        checkIsDead();
        RunFromPlayer(playerObject); //MAKE RUN THE OPPOSITE DIRECTION
        //Debug.Log("Taking damage" + health.GetHealthPercentage());
        
    }
    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "player"){
            Vector3 dir = other.contacts[0].point - transform.position;
            playerObject.GetComponent<Rigidbody>().AddForce(-dir.normalized * 50, ForceMode.Impulse);
            rb.AddForce(dir*50, ForceMode.Impulse);
            playerObject.TakeDamage(damageAmount);
        }
    }

    private void ChasePlayer(Player player){
        
        navAgent.SetDestination(playerObject.transform.position);
        //navAgent.Warp(player.transform.position);
        //Debug.Log("Moving toward player");
    }
    private void RunFromPlayer(Player player){
        
        //Debug.Log("Running from player");
         float distance = Vector3.Distance(transform.position, playerObject.transform.position);
         if(distance < EnemyDistanceRun){ 
             
            Vector3 dirToPlayer = transform.position - playerObject.transform.position;
            newPos = transform.position + dirToPlayer;
            navAgent.SetDestination(newPos);            
         }
         
    }

    public void checkIsDead(){
        if(health.GetHealthPercentage() <= 0){
            Destroy(this.gameObject); //make sure collider does not stay
            //PLAY DESTROY ANIMATION HERE
            //Debug.Log("Destroyed");
        }
    }

    
}