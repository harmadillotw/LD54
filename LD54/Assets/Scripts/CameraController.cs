using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public GameObject[] levels;
    public GameObject[] parallaxLevel1;
    public GameObject[] parallaxLevel2;
    public GameObject[] parallaxLevel3;
    public GameObject[] parallaxLevel4;
    public float choke;

    private Camera mainCamera;
    private Vector2 screenBounds;
    public Vector3 offset;

    //paralax test variables
    private float startpos, length;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        offset = transform.position - player.transform.position;
        foreach (GameObject obj in levels)
        {
            loadChildObjects(obj);
        }
        foreach (GameObject obj in parallaxLevel1)
        {
            loadChildObjects(obj);
        }
        foreach (GameObject obj in parallaxLevel2)
        {
            loadChildObjects(obj);
        }
        foreach (GameObject obj in parallaxLevel3)
        {
            loadChildObjects(obj);
        }
        foreach (GameObject obj in parallaxLevel4)
        {
            loadChildObjects(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;

        float parallaxFactor = 0.15f;
        foreach (GameObject obj in parallaxLevel4)
        {
            float distance = transform.position.x * parallaxFactor;
            Vector3 newM2Position = new Vector3(startpos + distance, obj.transform.position.y, obj.transform.position.z);
            obj.transform.position = newM2Position;
        }

        parallaxFactor = 0.3f;

        foreach (GameObject obj in parallaxLevel1)
        {
            float distance = transform.position.x * parallaxFactor;
            Vector3 newM2Position = new Vector3(startpos + distance, obj.transform.position.y, obj.transform.position.z);
            obj.transform.position = newM2Position;
        }

        parallaxFactor = 0.5f;
        foreach (GameObject obj in parallaxLevel2)
        {
            float distance = transform.position.x * parallaxFactor;
            Vector3 newM2Position = new Vector3(startpos + distance, obj.transform.position.y, obj.transform.position.z);
            obj.transform.position = newM2Position;
        }

        parallaxFactor = 0.8f;
        foreach (GameObject obj in parallaxLevel3)
        {
            float distance = transform.position.x * parallaxFactor;
            Vector3 newM2Position = new Vector3(startpos + distance, obj.transform.position.y, obj.transform.position.z);
            obj.transform.position = newM2Position;
        }
    }

    private void LateUpdate()
    {
        foreach (GameObject obj in levels)
        {
            repositionChildObjects(obj);
        }
        foreach (GameObject obj in parallaxLevel1)
        {
            repositionChildObjects(obj);
        }
        foreach (GameObject obj in parallaxLevel2)
        {
            repositionChildObjects(obj);
        }
        foreach (GameObject obj in parallaxLevel3)
        {
            repositionChildObjects(obj);
        }
        foreach (GameObject obj in parallaxLevel4)
        {
            repositionChildObjects(obj);
        }
    }

    private void loadChildObjects(GameObject obj)
    {
        Debug.Log(obj.name);
        float objectWidth = obj.GetComponentInChildren<SpriteRenderer>().bounds.size.x - choke;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 3 / objectWidth) + 2;
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());

    }

    private void repositionChildObjects(GameObject obj)
    {

        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {

            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;
            if (transform.position.x + (screenBounds.x * 2) > lastChild.transform.position.x + halfObjectWidth)
            {

                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);

            }
            else if (transform.position.x - (screenBounds.x * 2) < firstChild.transform.position.x - halfObjectWidth)
            {

                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }
}
