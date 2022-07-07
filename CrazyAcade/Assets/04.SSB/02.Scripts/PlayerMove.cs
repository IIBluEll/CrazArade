using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;
    public float jumpPower = 10;
    public float rotSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Jump"))
        //{
        //    rb.AddForce(Vector3.up * Time.deltaTime);
        //}

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.y = 0; //y��ǥ�� ���ƹ�����


        //dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();

        rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
       
        
       // UpdateRotate();
    }

    private void UpdateRotate()
    {
        // ������� �Է¿� ���� ���� ��ȯ�� �Ѵ�.
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, rotSpeed * -180 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotSpeed * 180 * Time.deltaTime);
        }
    }
}
