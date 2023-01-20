using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image healthBar;
    public float healthPoint;
    public float damageCount;
    private Vector2 inputMove;
    private RaycastHit2D hit;
    [SerializeField] private float speed = 1.5f;
    public ContactFilter2D contactFilter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        healthPoint = 50.0f;
    }
    private void Update()
    {
        healthBar.fillAmount = healthPoint / 50.0f;
        Move();
        #region Обработка столкновений
        boxCollider.OverlapCollider(contactFilter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            hits[i] = null;
        }
        #endregion
    }
    protected void Move()
    {
        inputMove = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);

        if (inputMove.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        if (inputMove.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        // Axis Y
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, inputMove.y),
            Mathf.Abs(inputMove.y * Time.deltaTime), LayerMask.GetMask("Actor", "Buildings"));
        if (hit.collider == null)
        {
            transform.Translate(0, inputMove.y * Time.deltaTime, 0);
        }
        // Axis X
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(inputMove.x, 0),
            Mathf.Abs(inputMove.x * Time.deltaTime), LayerMask.GetMask("Actor", "Buildings"));
        if (hit.collider == null)
        {
            transform.Translate(inputMove.x * Time.deltaTime, 0, 0);
        }
    }
    public void TakeDamage(float damage)
    {
        healthPoint -= damage;
    }
}