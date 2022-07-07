using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bomb : MonoBehaviourPunCallbacks
{

    public GameObject explosionPrefeb;
    public LayerMask levelMask;

    public int explong = 2;
    bool exploded = false;

    public PhotonView pv;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 0.5f);
    }

    void Explode()
    {
        Instantiate(explosionPrefeb, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosion(Vector3.forward));
        StartCoroutine(CreateExplosion(Vector3.right));
        StartCoroutine(CreateExplosion(Vector3.left));
        StartCoroutine(CreateExplosion(Vector3.back));

        GetComponent<MeshRenderer>().enabled = false;
        exploded = true;

        Invoke("DestroyBoomb", .3f);
        //PhotonNetwork.Destroy(explosionPrefeb);
    }

    [PunRPC]
    private void DestroyBoomb()
    {
        //PhotonNetwork.Destroy(explosionPrefeb);
        Destroy(this.gameObject);

    }


    IEnumerator CreateExplosion(Vector3 direction)
    {
        for (int i = 1; i < explong; i++)
        {
            RaycastHit hit;

            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit, i, levelMask);

            if (!hit.collider)
            {
                Instantiate(explosionPrefeb, transform.position + ((i * 0.7f) * direction), explosionPrefeb.transform.rotation);
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!exploded && other.CompareTag("Explosion"))
        {
            CancelInvoke("Explode");
            Explode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, .5f, 0), transform.forward, Color.red, 20);
    }
}
