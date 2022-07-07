using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerBomb : MonoBehaviourPun
{
    public PhotonView PV;
    public GameObject[] bombFactory;
    public GameObject bombPosiotion;

    public Vector3 setPos;
    public Quaternion setRot;

    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.instance.CreateInstance("EarthBomb");
        ObjectPool.instance.CreateInstance("SatBomb");
        ObjectPool.instance.CreateInstance("MoonBomb");
        ObjectPool.instance.CreateInstance("MarsBomb");     
    }

    // Update is called once per frame
    void Update()
    {
        // ����ڰ� ���콺 ���� ��ư�� ������ �Ѿ˰��忡�� �Ѿ���
        //����� �ѱ���ġ�� ��ġ�ϰ� �ʹ�.

        //1.���� ����ڰ� ���콺 ���� ��ư�� ������

        if (photonView.IsMine == true)
        {
            //Bomb();
        }
        else
        {
            //Bomb();

        }         
     
        //PhotonView.RPC("Bomb", RpcTarget.AllBuffered, go); //RPC �Լ� ȣ��
    }
    
    [PunRPC]
    void Bomb()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            if (SkinManager.instance.isEarth == true)
            {
                GameObject EarthBomb = ObjectPool.instance.GetDeactiveObject("EarthBomb");
                //�ҷ��� �ִٸ�
                //bullet !=null
                if (EarthBomb)
                {
                    EarthBomb.SetActive(true);
                    //�Ѿ� ���丮�� �������̹Ƿ� ��������� ���� �������̴�
                    //�׷��Ƿ� ���ӿ�����Ʈ�� �޾Ƽ� ������ش�.

                    //3. �� �Ѿ��� �ѱ���ġ�� ��ġ�ϰ�ʹ�.
                    //�Ѿ��� ��ġ= �ѱ��� ��ġ
                    EarthBomb.transform.position = bombPosiotion.transform.position;
                }


            }
            else if (SkinManager.instance.isSat == true)
            {
                GameObject SatBomb = ObjectPool.instance.GetDeactiveObject("SatBomb");


                //�ҷ��� �ִٸ�
                //bullet !=null
                if (SatBomb)
                {
                    SatBomb.SetActive(true);
                    //�Ѿ� ���丮�� �������̹Ƿ� ��������� ���� �������̴�
                    //�׷��Ƿ� ���ӿ�����Ʈ�� �޾Ƽ� ������ش�.

                    //3. �� �Ѿ��� �ѱ���ġ�� ��ġ�ϰ�ʹ�.
                    //�Ѿ��� ��ġ= �ѱ��� ��ġ
                    SatBomb.transform.position = bombPosiotion.transform.position;
                }


            }
            else if (SkinManager.instance.isMoon == true)
            {
                GameObject MoonBomb = ObjectPool.instance.GetDeactiveObject("MoonBomb");
                //�ҷ��� �ִٸ�
                //bullet !=null
                if (MoonBomb)
                {
                    MoonBomb.SetActive(true);
                    //�Ѿ� ���丮�� �������̹Ƿ� ��������� ���� �������̴�
                    //�׷��Ƿ� ���ӿ�����Ʈ�� �޾Ƽ� ������ش�.

                    //3. �� �Ѿ��� �ѱ���ġ�� ��ġ�ϰ�ʹ�.
                    //�Ѿ��� ��ġ= �ѱ��� ��ġ
                    MoonBomb.transform.position = bombPosiotion.transform.position;
                }


            }
            else if (SkinManager.instance.isMars == true)
            {
                GameObject MarsBomb = ObjectPool.instance.GetDeactiveObject("MarsBomb");
                //�ҷ��� �ִٸ�
                //bullet !=null
                if (MarsBomb)
                {
                    MarsBomb.SetActive(true);
                    //�Ѿ� ���丮�� �������̹Ƿ� ��������� ���� �������̴�
                    //�׷��Ƿ� ���ӿ�����Ʈ�� �޾Ƽ� ������ش�.

                    //3. �� �Ѿ��� �ѱ���ġ�� ��ġ�ϰ�ʹ�.
                    //�Ѿ��� ��ġ= �ѱ��� ��ġ
                    MarsBomb.transform.position = bombPosiotion.transform.position;
                }
            }

        //}


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    { 
    
    
    }


}
