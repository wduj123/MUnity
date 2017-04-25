// ========================================================
// 描 述：PlayerControl.cs 
// 作 者：郑贤春 
// 时 间：2017/02/25 15:43:20 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;

namespace Matchman.Project
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector]
        public bool facingRight = true;
        [HideInInspector]
        public bool jump = false;
        public float jumpForce = 365f;

        public bool grounded = false;

        public float moveForce = 365f;
        public float maxSpeed = 5f;


        void Awake()
        {
        }


        // Update is called once per frame
        void Update()
        {
            Vector3 castStart = transform.position + Vector3.up;
            Vector3 castEnd = transform.position - Vector3.up;
            grounded = Physics2D.Linecast(castStart, castEnd);//LayerMask.LayerToName("Ground"));
            Debug.DrawLine(castStart, castEnd, Color.red, 1f);
            if (Input.GetButtonDown("Jump") && grounded)
            {
                jump = true;
            }
        }

        void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");//获取水平输入
                                                  //Debug.Log(Mathf.Abs(h));

            //行进
            if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
            }
            //转身
            if (h > 0 && !facingRight)
            {
                Flip();
            }
            else if (h < 0 && facingRight)
            {
                Flip();
            }
            //速度
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            //跳跃
            if (jump)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
                jump = false;
            }
        }

        void Flip()
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}

