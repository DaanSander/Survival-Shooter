using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour {

    //Settings
    public float maxAngle = 20.0f;
    public Enemy enemy;

    [HideInInspector]
    //Pathfinding
    public float health, noAttackedTime = 0f;
    private bool attacking = false;
    private bool playedDeathAudio = false;
    private Transform target;
    private UnityEngine.AI.NavMeshAgent navAgent;
    
    //Enemy components
    private EnemyHealthbar healthBar;
    private EnemyAudioManager audioManager;

    //Animations
    private Animator animator;
    private int attackHash, deathHash;

    //Attack
    private PlayerManager playerManager;
    private BoxCollider boxCollider;

    void Start() {
        //Initializing components
        target = GameObject.Find("Player").transform;

        //Enemy components
        healthBar = GetComponent<EnemyHealthbar>();
        audioManager = GetComponent<EnemyAudioManager>();

        //Player and targeting player components
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerManager = target.gameObject.GetComponent<PlayerManager>();

        //Other
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();

        //Initializing animator
        attackHash = Animator.StringToHash("attack");
        deathHash = Animator.StringToHash("die");

        //Setting variables
        health = enemy.maxHealth;
        navAgent.speed = enemy.speed;
        navAgent.updateRotation = false;
    }

    public void damage(float damage) {
        noAttackedTime = 0f;
        this.health -= damage;
    }

    void FixedUpdate() {
        Vector3 relative = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(relative.x, 0.0f, relative.z));

        if (!attacking)
            checkCollision();
        navAgent.SetDestination(target.position);
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Kan dit beter ?
        if (boxCollider == null)
            Gizmos.DrawWireCube(transform.GetComponent<Collider>().bounds.center, new Vector3(enemy.range, enemy.range, enemy.range));
        else
            Gizmos.DrawWireCube(boxCollider.bounds.center, new Vector3(enemy.range, enemy.range, enemy.range));
    }

    void Update() {
        if (health > 0) {
            noAttackedTime += Time.deltaTime;
            if (health < enemy.maxHealth && noAttackedTime >= enemy.regenPerSec && enemy.regens) {
                if (health + enemy.regenPerSec * Time.deltaTime > enemy.maxHealth) {
                    health = enemy.maxHealth;
                    return;
                }
                health += enemy.regenPerSec * Time.deltaTime;
            }
        }
        else {
            attacking = false;
            navAgent.speed = 0.0f;
            if (!playedDeathAudio) {
                gameObject.layer = LayerMask.NameToLayer("Not Lockonable");
                ScoreManager.score += enemy.points;
                audioManager.stopAudio();
                audioManager.playDeathAudio();
                playedDeathAudio = true;
            }
            if (!healthBar.destroyed())
                Destroy(healthBar.healthBar.gameObject);

            animator.SetTrigger(deathHash);
            Destroy(gameObject, 3   );
        }
    }
    

    bool playerInView() {
        return (Vector3.Angle(transform.forward, target.position - transform.position) <= maxAngle);
    }

    bool playerInRange() {
        return Vector3.Distance(transform.position, target.position) <= enemy.range;
    }

    public void endAnimation() {
        attacking = false;
    }

    void attack() {
        attacking = true;
        animator.SetTrigger(attackHash);
        audioManager.playAttackAudio();
    }

    public void calculateStats(int wave) {
        enemy.damageOutput += wave / 5.0f;
        enemy.maxHealth += Mathf.Pow((float)wave, 0.7f);
        enemy.points += (int) Mathf.Round(wave / 3);
    }

    public void checkCollision() {
        if (!playerInView()) return;
        if (!playerInRange()) return;
        if (!attacking)
            attack();
        else
            playerManager.damage(enemy.damageOutput);
    }

}
