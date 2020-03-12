using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float amount = 0.1f;//hoeveel motion heeft het wapen
    public float maxAmount = 0.1f;//tot hoever kan het bewegen
    public float smoothAmount = 5;// hoe snel gaat de camera terug naar zijn originele positie
    public float startAmount;

    public float aimAmount = 0.01f;//amount word dit als je aimed

    private Vector3 initialPosition;

    public Weapon tommyGun;
    void Start ()
    {
        startAmount = amount;//zet start amount als referentie voor later
        tommyGun = GameObject.Find("Tommygun").GetComponent < Weapon>();
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (tommyGun.IsAiming == true)
        {
            amount = aimAmount;//als aim true is ga dan de motion omlaag doen zodat we daadwerkelijk kunnen aimen
        }
        else
        {
            amount = startAmount;// zet als we niet aimen onze motion speed terug naar het originele
        }
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}
