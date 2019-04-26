using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Weapon, ILaserable
{
    [SerializeField]
    float maxLaserLengthDelay;
    [SerializeField]
    [Range(0, 100)]
    int lazerMaxLength;
    private LineRenderer line;
    private Vector3 boxCastSize;
    
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
    }
    public override void Use()
    {
        
        boxCastSize = new Vector3(0.2f, 0.2f, 0f);
        MenuAudioController.Instance.PlaySound("lasershot", false);
        StartCoroutine(shootLaserAnimation());
        
        /* Рисуем коробку
        Transform cube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        cube.position = muzzle.transform.position + muzzle.forward/2 * boxCastSize.z;
        cube.localScale = boxCastSize; //I believe this is right.
        cube.forward = muzzle.forward;
        Destroy(cube.GetComponent<BoxCollider>());
        */

    }

    IEnumerator shootLaserAnimation()
    {
        float t = 0;
        while (t < maxLaserLengthDelay)
        {
            boxCastSize = new Vector3(boxCastSize.x, boxCastSize.y, lazerMaxLength * (t / maxLaserLengthDelay));
            Vector3 center = muzzle.transform.position + muzzle.forward / 2 * boxCastSize.z;
            RaycastHit[] hits = Physics.BoxCastAll(center, boxCastSize/2, muzzle.forward, muzzle.rotation);
            if (hits != null)
            {
                foreach (RaycastHit hit in hits)
                {
                    IDestructable target = hit.transform.GetComponent<IDestructable>();
                    if (target != null)
                    {
                        target.ReceiveHit(damage);
                    }
                }
            }
            DrawLaser(line, muzzle.forward * boxCastSize.z + muzzle.transform.position);
            t += 0.02f; //TODO: Implement controllable laser effect time 
            yield return new WaitForSeconds(0.02f);
        }
        t = 0;
        while (t < maxLaserLengthDelay)
        {
            boxCastSize = new Vector3(boxCastSize.x, boxCastSize.y, lazerMaxLength);
            Vector3 center = muzzle.transform.position + muzzle.forward / 2 * boxCastSize.z;
            RaycastHit[] hits = Physics.BoxCastAll(center, boxCastSize / 2, muzzle.forward, muzzle.rotation);
            if (hits != null)
            {
                foreach (RaycastHit hit in hits)
                {
                    IDestructable target = hit.transform.GetComponent<IDestructable>();
                    if (target != null)
                    {
                        target.ReceiveHit(damage);
                    }
                }

            }
            DrawLaser(line, muzzle.forward * boxCastSize.z + muzzle.transform.position);
            t += 0.02f;
            yield return new WaitForSeconds(0.02f);
            
        }
        line.enabled = false;
        base.Use();
    }

    public void DrawLaser(LineRenderer line, Vector3 lastPoint)
    {
        line.SetPosition(0, muzzle.transform.position);
        line.SetPosition(1, lastPoint);
        line.enabled = true;
    }

}
