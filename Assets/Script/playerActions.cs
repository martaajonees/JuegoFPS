using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class playerActions : MonoBehaviour, IDamage
{
    public Transform postGun;
    public Transform cam;
    public GameObject bulletPrefab;
    public LayerMask ignoreLayer;
    RaycastHit hit;

    public int life = 20;
    public int ammo = 10;

    public GameObject damageEffect;
    public float saveInterval = 0.5f;
    float saveTime;
    WaitForSeconds wait;

    void Start()
    {
        damageEffect.SetActive(false);
        saveTime = 0.0f;
        CanvasController.instance.AddTextHp(life);
        CanvasController.instance.AddTextAmmo(ammo);
        wait = new WaitForSeconds(0.2f);
    }

    private void Update()
    {
        Debug.DrawRay(cam.position, cam.forward *100f, Color.red);
        Debug.DrawRay(postGun.position, cam.forward *100f, Color.blue);

        if(ammo > 0 && Input.GetMouseButtonDown(0))
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
            ammo--;
            CanvasController.instance.AddTextAmmo(ammo);
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
                CanvasController.instance.AddTextHp(life);
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

        if(life <= 0)
        {
            GameManager.instance.FinGame(false);
        }
        
    }
}
