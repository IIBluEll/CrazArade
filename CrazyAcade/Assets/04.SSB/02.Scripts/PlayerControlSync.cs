//네트워크 라이브러리 추가
using Photon.Pun;
using UnityEngine;


public class PlayerControlSync : MonoBehaviourPun, IPunObservable   //객체의 싱크를 맞추는 인터페이스 //MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;
    public float speed = 5f;
    //float rotSpeed = 180.0f;
    //float movSpeed = 3.0f;

    int spwanlimit = 1;

    public Vector3 setPos;
    public Quaternion setRot;

    //포톤뷰랑 폭탄 연동
    public PhotonView PV;
    public GameObject[] bombFactory;

    void Start()
    {
        //Vector2 initPos = Random.insideUnitCircle * 1.5f;
        //if (spwanlimit < 2)
        //{
        //    PhotonNetwork.Instantiate("Player", new Vector3(initPos.x, 1, initPos.y), Quaternion.identity);
        //    spwanlimit--;
        //}
        PV = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveRot();
    }

    private void MoveRot()
    {
        if (photonView.IsMine == true)
        {

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            anim.SetFloat("h", h);
            anim.SetFloat("v", v);


            Vector3 dir = Vector3.right * h + Vector3.forward * v;
            dir.y = 0; //y좌표값 막아버리기


            //dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;
            dir.Normalize();

            rb.MovePosition(transform.position + dir * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PhotonNetwork.Instantiate("EarthBomb", transform.position, Quaternion.identity);
               
           
            }
        }
        else //내 클라이언트의 내 객체 제어가 아닌경우 -> Remote (상대방 객체)
        {
            //기존 방식 - 프레임이 끊어지는 (지연) 발생
            //mud - mug online (패킷packit의 양을 늘린다) - 보정
            this.transform.position = Vector3.Lerp(this.transform.position, setPos, Time.deltaTime * 30f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, setRot, Time.deltaTime * 30f);
        }
    }
    //데이터가 차곡차곡 순차적으로 오고가고 함
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true) //내 객체의 값이나 행동이 이루어졌을 때(PhotoneView=mine)
        {
            //상대방이 보는 내 객체(Remote)에 값을 주어야겠지
            //보내는 함수 - stream.sendNext
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);

        }
        if (stream.IsReading)        // //상대방이 보는 내 객체(PhotoneView=Remote)일때
        {
            setPos = (Vector3)stream.ReceiveNext();   //맨 처음 position으로 넣었으니, posiotion 값
            setRot = (Quaternion)stream.ReceiveNext();   //rotation 값 넣었으니 rotation 값     
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Explosion")
        {
            anim.SetTrigger("IsDie");
        }
    }

}
