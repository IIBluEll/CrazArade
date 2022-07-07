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

    //��ü���-key�� ������ 2�������� ����� �����.
    //Ű�� string , ��������� ����Ʈ�� ������ �Ѵ�/
    public Dictionary<string, List<GameObject>> dic;
    //��Ȱ��ȭ ���
    //List<GameObject> list;
    public Dictionary<string, List<GameObject>> deActiveDic;

    public void CreateInstance(string prefabName)
    {
        //�����̸��� �Ȱ��� Ŭ���� : Resources

        string key = prefabName;
        //dic�� �����Ǿ��ְ�, dic�� �̹� key�� ���ԵǾ��ִٸ�
        if (dic != null && dic.ContainsKey(key))
        {
            //�Լ��� �ٷ� �����ϰ� �ʹ�.
            return;
        }

        GameObject factory = Resources.Load<GameObject>(prefabName);
        //�ʱ�ȭ
        if (dic == null)
        {
            dic = new Dictionary<string, List<GameObject>>();
            deActiveDic = new Dictionary<string, List<GameObject>>();
        }

        //�ҷ��� �ִ� ����Ʈ �����
        dic.Add(key, new List<GameObject>());
        deActiveDic.Add(key, new List<GameObject>());


        for (int i = 0; i < maxCount; i++)
        {

            GameObject go = Instantiate(factory);
            go.name = key;

            //�Ʒ��� ������ ���� ���̴�.
            //go.name=go.name.Replace("(Clone)", "");
            //go.name = key;

            go.SetActive(false);
            //�ּҰ��� �־���°�
            dic[key].Add(go);
            deActiveDic[key].Add(go);

        }

    }

    //List�� ����ִ� ���̴� �͸� �־� ���� �Ⱥ��̴� ���� �� ���� ��Ȱ��ȭ �س��´�.
    internal GameObject GetDeactiveObject(string key)
    {
        //���� ��Ȱ��ȭ �� ���� ���ٸ�
        if (deActiveDic[key].Count == 0)
        {
            //��Ȱ�� ����� ����ִٸ� ���� ����� ��ȯ�Ѵ�.
            GameObject factory = Resources.Load<GameObject>(key);
            GameObject gogo = Instantiate(factory);
            gogo.name = key;

            //�Ʒ��� ������ ���� ���̴�.
            //go.name=go.namekey.Replace("(Clone)", "");
            //go.name = key;

            gogo.SetActive(false);
            //�ּҰ��� �־���°�
            dic[key].Add(gogo);
            //deActiveDic[key].Add(gogo);
            //null ��ȯ
            return gogo;
        }
        //��Ȱ�� ����� ù��° �׸��� ��ȯ�Ѵ�. �� �� ��Ͽ��� �����Ѵ�.
        GameObject go = deActiveDic[key][0];
        deActiveDic[key].RemoveAt(0);
        return go;
    }

    //List�� ����ִ� ���ӿ�����Ʈ�߿� ��Ȱ��ȭ �Ȱ��� ������ �װ��� ��ȯ�ϰ� �ʹ�.
    //for�� �˻��� �ϸ� �ʹ� �����ɷ�����.
    internal GameObject GetDeactiveObjectOld(string key)
    {
        for (int i = 0; i < maxCount; i++)
        {
            //���� list[i]�� ��Ȱ��ȭ �Ǿ��ִٸ�
            if (dic[key][i].activeSelf == false)
            {
                //��ȯ�ϰ� �ʹ�.
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
