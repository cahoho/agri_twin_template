using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InteractableUI : MonoBehaviour
{

    GameObject cameraObj;
    float minDistance = 1.8f;
    float maxDistance = 16.8f;
    float maxXAngle = 1f;
    float scaleFactorMutiplier = 15f;

    [Header("Panels")]
    public GameObject infoPanel;
    GameObject fatherObjUIs;
    List<GameObject> childObjList;
    void Awake()
    {
        cameraObj = GameObject.FindWithTag("MainCamera");
        fatherObjUIs = infoPanel.transform.parent.gameObject;
        childObjList = new List<GameObject>();
        foreach (Transform child in fatherObjUIs.transform)
        {
            if (child.gameObject != infoPanel)
            {
                childObjList.Add(child.gameObject);
                child.gameObject.SetActive(false);
                
            }
        }
        infoPanel.SetActive(false);

    }

    void Update()
    {

        float distance = Vector3.Distance(gameObject.transform.position, cameraObj.transform.position);
        if (distance >= minDistance && distance <= maxDistance)
        {
            float scaleFactor = distance / scaleFactorMutiplier;
            transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            //如果需要位置移动就取消注释：：
            //float targetY = defaultPosY - (int)(distance / 10);
            //float smoothY = Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * 5f);
            //transform.position = new Vector3(transform.position.x, smoothY, transform.position.z);
        }

        
    }
    private void LateUpdate()
    {
        Vector3 directionToTarget = cameraObj.transform.position - transform.position;
        Quaternion targetLookRotation = Quaternion.LookRotation(directionToTarget);

        Vector3 eulerAngles = targetLookRotation.eulerAngles;

        if (eulerAngles.x > 180)
        {
            eulerAngles.x -= 360;
        }
        eulerAngles.x = Mathf.Clamp(eulerAngles.x, -maxXAngle, maxXAngle);

        transform.rotation = Quaternion.Euler(eulerAngles);

        transform.Rotate(0, 180f, 0);
    }
    private void OnMouseDown()
    {

        if (!infoPanel.activeSelf) {
            foreach (GameObject childObj in childObjList)
            {
                if (childObj != null)
                {
                    childObj.SetActive(false);

                }
            }
        }
        infoPanel.SetActive(!infoPanel.activeSelf);

    }

}