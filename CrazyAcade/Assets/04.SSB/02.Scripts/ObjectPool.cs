using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static  ObjectPool instance;
    private void Awake()
    {
        instance = this;
    }

    public int maxCount = 20;

    //전체목록-key를 가지고 2차원적인 목록을 만든다.
    //키는 string , 만들어지는 리스트는 값으로 한다/
    public Dictionary<string, List<GameObject>> dic;
    //비활성화 목록
    //List<GameObject> list;
    public Dictionary<string, List<GameObject>> deActiveDic;

    public void CreateInstance(string prefabName)
    {
        //폴더이름과 똑같은 클래스 : Resources

        string key = prefabName;
        //dic이 생성되어있고, dicㅔ 이미 key가 포함되어있다면
        if (dic != null && dic.ContainsKey(key))
        {
            //함수를 바로 종료하고 싶다.
            return;
        }

        GameObject factory = Resources.Load<GameObject>(prefabName);
        //초기화
        if (dic == null)
        {
            dic = new Dictionary<string, List<GameObject>>();
            deActiveDic = new Dictionary<string, List<GameObject>>();
        }

        //불렛을 넣는 리스트 만들기
        dic.Add(key, new List<GameObject>());
        deActiveDic.Add(key, new List<GameObject>());


        for (int i = 0; i < maxCount; i++)
        {

            GameObject go = Instantiate(factory);
            go.name = key;

            //아래와 두줄은 같은 뜻이다.
            //go.name=go.name.Replace("(Clone)", "");
            //go.name = key;

            go.SetActive(false);
            //주소값만 넣어놓는것
            dic[key].Add(go);
            deActiveDic[key].Add(go);

        }

    }

    //List에 들어있는 보이는 것만 넣어 놓고 안보이는 것은 빼 놓고 비활성화 해놓는다.
    internal GameObject GetDeactiveObject(string key)
    {
        //만약 비활성화 된 것이 없다면
        if (deActiveDic[key].Count == 0)
        {
            //비활성 목록이 비어있다면 새로 만들어 반환한다.
            GameObject factory = Resources.Load<GameObject>(key);
            GameObject gogo = Instantiate(factory);
            gogo.name = key;

            //아래와 두줄은 같은 뜻이다.
            //go.name=go.namekey.Replace("(Clone)", "");
            //go.name = key;

            gogo.SetActive(false);
            //주소값만 넣어놓는것
            dic[key].Add(gogo);
            //deActiveDic[key].Add(gogo);
            //null 반환
            return gogo;
        }
        //비활성 목록의 첫번째 항목을 반환한다. 이 때 목록에서 삭제한다.
        GameObject go = deActiveDic[key][0];
        deActiveDic[key].RemoveAt(0);
        return go;
    }

    //List에 들어있는 게임오브젝트중에 비활성화 된것이 있으면 그것을 반환하고 싶다.
    //for문 검색을 하면 너무 오래걸러진다.
    internal GameObject GetDeactiveObjectOld(string key)
    {
        for (int i = 0; i < maxCount; i++)
        {
            //만약 list[i]이 비활성화 되어있다면
            if (dic[key][i].activeSelf == false)
            {
                //반환하고 싶다.
                return dic[key][i];
            }

        }
        return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
