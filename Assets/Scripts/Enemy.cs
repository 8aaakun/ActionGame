using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 7f;
    [SerializeField]
    private float jumpPower = 10f;
    private Animator animator;
    private Rigidbody2D rigid;
    private bool isGround = true;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        
        if(isGround){
            animator.SetInteger("AnimeState", 0);
        }

        if (inputX != 0)
        {
            Move(inputX);
        }

        if (Input.GetButtonDown("Jump") && isGround){
            Jump();
        }
        
        
        if(Input.GetKeyDown(KeyCode.Z)){
            Attack();
        }

    }

    void Move(float inputX)
    {
        if (inputX > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        Vector3 move = new Vector3(inputX * moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(move);
        if (isGround)
        {
            animator.SetInteger("AnimeState", 1);
        }
    }

    void Jump(){
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isGround = false;
        animator.SetInteger("AnimeState", 2);
    }
    
    void Attack(){
        animator.SetTrigger("Attack");
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Platform"){
            isGround = true;
        }
        else{
            isGround = false;
            //animator.SetInteger("AnimeState", 0);
        }
    }
}
