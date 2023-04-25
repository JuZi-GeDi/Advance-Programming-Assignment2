using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform FirePoint;
    public float MaxDistance;
    public LayerMask LayerMask;
    public float Force = 100;
    public Transform GunPoint;
    public Camera Camera;
    public GameObject BulletPrefab;
    public float ReloadTimer = 0; // seconds
    public float ReloadTime = 1; // seconds4
    public GameObject Gun;
    public bool canShoot = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
        {
            FindGun();
        }
        Shoot();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.GetComponent<StarterAssetsInputs>().cursorLocked = !gameObject.GetComponent<StarterAssetsInputs>().cursorLocked;
            Cursor.visible = !gameObject.GetComponent<StarterAssetsInputs>().cursorLocked;
            Screen.lockCursor = gameObject.GetComponent<StarterAssetsInputs>().cursorLocked;
        }
    }

    void FindGun()
    {
        if (Vector3.Distance(gameObject.transform.position, Gun.transform.position) <= 1)
        {
            canShoot = true;
            Gun.SetActive(false);
        }
    }

    void Shoot()
    {
        if (canShoot)
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Vector3 endPoint;
            bool hit = Physics.Raycast(ray, out hitInfo, MaxDistance, LayerMask);
            if (hit)
                endPoint = hitInfo.point; // Raycast hits something
            else
                endPoint = FirePoint.position + ray.direction * MaxDistance; // Raycast hits nothing
            ReloadTimer -= Time.deltaTime;
            // Gun not ready to shoot yet
            if (ReloadTimer > 0)
                return;
            if (Input.GetMouseButton(0))
            {
                if (hit)
                {
                    FirePoint.LookAt(hitInfo.point);
                }
                else
                {
                    //Nothing was hit
                    FirePoint.LookAt(FirePoint.position + ray.direction * MaxDistance);
                }
                ReloadTimer = ReloadTime;
                Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            }
        }
    }
}
