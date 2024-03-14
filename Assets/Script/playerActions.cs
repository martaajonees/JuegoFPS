using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerActions : MonoBehaviour, IDamage
{
    public Transform postGun;
    public Transform cam;
    public GameObject bulletPrefab;
    public LayerMask ignoreLayer;
    RaycastHit hit;

    public int life = 20;

    public GameObject damageEffect;
    public float saveInterval = 0.5f;
    float saveTime;
    WaitForSeconds wait;

    void Start()
    {
        damageEffect.SetActive(false);
        saveTime = 0.0f;
        wait = new WaitForSeconds(0.2f);
    }

    private void Update()
    {
        Debug.DrawRay(cam.position, cam.forward *100f, Color.red);
        Debug.DrawRay(postGun.position, cam.forward *100f, Color.blue);

        if(Input.GetMouseButtonDown(0))
        {
            //Vector3 direction = cam.TransformDirection(new UnityEngine.Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 1));
            Vector3 direction = cam.TransformDirection(new Vector3(0,0,1));
            Debug.DrawRay(cam.position, direction * 100f, Color.green, 5f);

            //GameObject bulletobj = Instantiate(bulletPrefab);

            GameObject bulletobj = ObjectPollingManager.instance.GetBullet(true);

            bulletobj.transform.position = postGun.position;
            if(Physics.Raycast(cam.position, direction, out hit, Mathf.Infinity, ~ignoreLayer))
            {
                bulletobj.transform.LookAt(hit.point);
            }
            else
            {
                Vector3 dir = cam.position + direction*10f;
                bulletobj.transform.LookAt(dir);
            }
        }
        saveTime -= Time.deltaTime;
    }

    public bool DoDamage(int vld, bool isPlayer)
    {
        Debug.Log("Player recibe " + vld + " de daño " + isPlayer);
        if(isPlayer == true){
            return false;
        } else {
            if(saveTime <= 0){
                life -= vld;
                StartCoroutine(Effect());
            }
        }
        return true;
    }

    IEnumerator Effect()
    {
        saveTime = saveInterval;
        damageEffect.SetActive(true);
        yield return wait;
        damageEffect.SetActive(false);
        
    }
}
