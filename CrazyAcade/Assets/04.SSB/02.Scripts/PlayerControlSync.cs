//��Ʈ��ũ ���̺귯�� �߰�
using Photon.Pun;
using UnityEngine;


public class PlayerControlSync : MonoBehaviourPun, IPunObservable   //��ü�� ��ũ�� ���ߴ� �������̽� //MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;
    public float speed = 5f;
    //float rotSpeed = 180.0f;
    //float movSpeed = 3.0f;

    int spwanlimit = 1;

    public Vector3 setPos;
    public Quaternion setRot;

    //������ ��ź ����
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
            dir.y = 0; //y��ǥ�� ���ƹ�����


            //dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;
            dir.Normalize();

            rb.MovePosition(transform.position + dir * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PhotonNetwork.Instantiate("EarthBomb", transform.position, Quaternion.identity);
               
           
            }
        }
        else //�� Ŭ���̾�Ʈ�� �� ��ü ��� �ƴѰ�� -> Remote (���� ��ü)
        {
            //���� ��� - �������� �������� (����) �߻�
            //mud - mug online (��Ŷpackit�� ���� �ø���) - ����
            this.transform.position = Vector3.Lerp(this.transform.position, setPos, Time.deltaTime * 30f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, setRot, Time.deltaTime * 30f);
        }
    }
    //�����Ͱ� �������� ���������� ������ ��
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true) //�� ��ü�� ���̳� �ൿ�� �̷������ ��(PhotoneView=mine)
        {
            //������ ���� �� ��ü(Remote)�� ���� �־�߰���
            //������ �Լ� - stream.sendNext
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);

        }
        if (stream.IsReading)        // //������ ���� �� ��ü(PhotoneView=Remote)�϶�
        {
            setPos = (Vector3)stream.ReceiveNext();   //�� ó�� position���� �־�����, posiotion ��
            setRot = (Quaternion)stream.ReceiveNext();   //rotation �� �־����� rotation ��     
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
