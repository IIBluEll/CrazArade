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
        // 사용자가 마우스 왼쪽 버튼을 누르면 총알공장에서 총알을
        //만들어 총구위치에 배치하고 싶다.

        //1.만약 사용자가 마우스 왼쪽 버튼을 누르면

        if (photonView.IsMine == true)
        {
            //Bomb();
        }
        else
        {
            //Bomb();

        }         
     
        //PhotonView.RPC("Bomb", RpcTarget.AllBuffered, go); //RPC 함수 호출
    }
    
    [PunRPC]
    void Bomb()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            if (SkinManager.instance.isEarth == true)
            {
                GameObject EarthBomb = ObjectPool.instance.GetDeactiveObject("EarthBomb");
                //불렛이 있다면
                //bullet !=null
                if (EarthBomb)
                {
                    EarthBomb.SetActive(true);
                    //총알 팩토리는 프리팹이므로 만들어지지 않은 프리팹이다
                    //그러므로 게임오브젝트로 받아서 만들어준다.

                    //3. 그 총알을 총구위치에 배치하고싶다.
                    //총알의 위치= 총구의 위치
                    EarthBomb.transform.position = bombPosiotion.transform.position;
                }


            }
            else if (SkinManager.instance.isSat == true)
            {
                GameObject SatBomb = ObjectPool.instance.GetDeactiveObject("SatBomb");


                //불렛이 있다면
                //bullet !=null
                if (SatBomb)
                {
                    SatBomb.SetActive(true);
                    //총알 팩토리는 프리팹이므로 만들어지지 않은 프리팹이다
                    //그러므로 게임오브젝트로 받아서 만들어준다.

                    //3. 그 총알을 총구위치에 배치하고싶다.
                    //총알의 위치= 총구의 위치
                    SatBomb.transform.position = bombPosiotion.transform.position;
                }


            }
            else if (SkinManager.instance.isMoon == true)
            {
                GameObject MoonBomb = ObjectPool.instance.GetDeactiveObject("MoonBomb");
                //불렛이 있다면
                //bullet !=null
                if (MoonBomb)
                {
                    MoonBomb.SetActive(true);
                    //총알 팩토리는 프리팹이므로 만들어지지 않은 프리팹이다
                    //그러므로 게임오브젝트로 받아서 만들어준다.

                    //3. 그 총알을 총구위치에 배치하고싶다.
                    //총알의 위치= 총구의 위치
                    MoonBomb.transform.position = bombPosiotion.transform.position;
                }


            }
            else if (SkinManager.instance.isMars == true)
            {
                GameObject MarsBomb = ObjectPool.instance.GetDeactiveObject("MarsBomb");
                //불렛이 있다면
                //bullet !=null
                if (MarsBomb)
                {
                    MarsBomb.SetActive(true);
                    //총알 팩토리는 프리팹이므로 만들어지지 않은 프리팹이다
                    //그러므로 게임오브젝트로 받아서 만들어준다.

                    //3. 그 총알을 총구위치에 배치하고싶다.
                    //총알의 위치= 총구의 위치
                    MarsBomb.transform.position = bombPosiotion.transform.position;
                }
            }

        //}


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    { 
    
    
    }


}
