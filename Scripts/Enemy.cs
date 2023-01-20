using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player player;
    [SerializeField] protected int healthPoint;
    [SerializeField] protected int maxHealthPoint;
    public float damageCount;
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    private bool chasing;
    public float attackDistance = 1.1f;
    private bool collidingWithPlayer;
    Transform playerTransform;
    private Vector3 startingPosition;
    public float timeBtwAttack;
    public float startTimeBtwAttack;
    private Vector2 inputMove;
    private RaycastHit2D hit;
    [SerializeField] private float speed = 1.5f;
    protected Vector3 moveDelta;
    private BoxCollider2D boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); ;
        startingPosition = transform.position;
        player = FindObjectOfType<Player>();
        playerTransform = player.transform;
    }
    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
        Debug.Log("Taken " + damage + " damage!");
        Debug.Log("Health = " + healthPoint);
    }
    private void Update()
    {
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                chasing = true;
            }
            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
                if (Vector3.Distance(playerTransform.position, transform.position) <= attackDistance)
                {
                    Attack();
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
    }
    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * speed, input.y * speed, 0);
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Buildings", "Enemy"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Buildings", "Enemy"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
    private void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            player.TakeDamage(damageCount);
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
}