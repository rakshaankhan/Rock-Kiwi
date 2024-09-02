using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    //Basic properties and component refs...
    public GameObject anchor;
    public GameObject spawner;
    public float health = 20F;
    public float moveSpeed = 3F;
    public float collisionDmg = 5F;
    private bool awayFromAnchor = true;
    private Rigidbody2D rb;
    
    //Parameters describing the minion's attack properties...
    [SerializeField] private GameObject projectile;
    public AttackPattern firingPattern = AttackPattern.FixedAlternating;
    public AttackRotation firingRotation = AttackRotation.Clockwise;
    public float attackPeriod = 2F;
    [SerializeField] private float attackTimer = 0;
    public int burstSize = 3;
    public float burstDensity = .2F;
    [SerializeField] private float burstTimer = 0;
    [SerializeField] private int shotNum = 0;
    [SerializeField] private Transform launchPoint;

    public void Start(){
        rb = GetComponent<Rigidbody2D>();
        if(projectile == null || launchPoint == null){
            Debug.Log("Minion Controller: projectile spawn parameters null -- component disabled...");
            this.gameObject.SetActive(false);
            return;
        }
        if(this.transform.position != anchor.transform.position){
            StartCoroutine(moveToAnchor());
        }

        burstTimer = burstDensity;
        return;
    }

    public void Update(){
        if(attackTimer >= attackPeriod){
            attackTimer = 0;
            StartCoroutine(Attack());
        }
        else{
            attackTimer += Time.deltaTime;
        }
    }

    public IEnumerator Attack(){
        
        for(shotNum = 0; shotNum < burstSize;){
            if(burstTimer >= burstDensity){
                Instantiate(projectile, this.transform.position, this.transform.rotation);
                burstTimer = 0;
                shotNum++;
            }
            else{burstTimer += Time.deltaTime;}
            yield return null;
        }

        burstTimer = burstDensity;
        attackTimer = 0;
        yield break;
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<PlayerHealth>().TakehealthDamage((int)collisionDmg);
        }
        return;
    }

    public void takeDamage(float dmg){
        health -= dmg;
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }
    
    private IEnumerator moveToAnchor(){
        //Describes the movement taken by the minion as it transitions
        //from its spawn point to its anchor point...
        Vector3 direction = Vector3.Normalize(anchor.transform.position - this.transform.position);
        rb.velocity = (Vector2)(direction * moveSpeed);
        while(true){
            if((this.transform.position - anchor.transform.position).magnitude <= .2F){
                this.transform.position = anchor.transform.position;
                rb.velocity = Vector2.zero;
                yield break;
            }
            yield return null;
        }
    }

    private void handInResignation(){
        //Self-destruct method that communicates death to the spawner...
        
    }
}

public enum AttackPattern{
    Fixed,
    FixedAlternating,
    Rotating
}

public enum AttackRotation{
    Clockwise,
    CounterClockwise
}
