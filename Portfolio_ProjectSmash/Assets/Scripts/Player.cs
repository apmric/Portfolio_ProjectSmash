using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera followCamera;

    public float speed;

    float hAxis;
    float vAxis;

    bool fDown;

    Rigidbody rigid;
    Animator anim;

    Vector3 moveVec;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0 ,vAxis).normalized;

        this.transform.position += moveVec * speed * Time.deltaTime;
    }

    void Turn()
    {
        // 키보드에 의한 회전
        this.transform.LookAt(transform.position + moveVec);

        // 마우스에 의한 회전
        if (fDown)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - this.transform.position;
                nextVec.y = 0;
                this.transform.LookAt(this.transform.position + nextVec);
            }
        }
    }

    void FixedUpdate()
    {
        FreeznRotation();
    }

    void FreeznRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
}
