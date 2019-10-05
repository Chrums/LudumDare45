using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectState
{
    public float x;
    public float y;
    public float z;
    public float t;
    public float dx;
    public float dy;
    public float dz;
    public float dt;
}

public class StateHistory : MonoBehaviour
{
    const int maxFrames = 60 * 60 * 3; // 3 minutes
    List<Dictionary<GameObject, ObjectState>> states = new List<Dictionary<GameObject, ObjectState>>(maxFrames);

    int frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxFrames; i++)
        {
            states.Add(new Dictionary<GameObject, ObjectState>());
        }
    }

    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            setFrame(frame - 2);
        }
    }

    void setFrame(int _frame)
    {
        frame = _frame;
        var recordedObjects = GameObject.FindGameObjectsWithTag("History");
        foreach (var obj in recordedObjects)
        {
            obj.SetActive(false);
        }
        foreach (var objPair in states[frame])
        {
            objPair.Key.transform.SetPositionAndRotation(
                new Vector3(objPair.Value.x, objPair.Value.y, objPair.Value.z),
                objPair.Key.transform.rotation); //TODO: rotation
            objPair.Key.SetActive(true);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var recordedObjects = GameObject.FindGameObjectsWithTag("History");
        foreach (var obj in recordedObjects)
        {
            ObjectState objectState = new ObjectState();
            objectState.x = obj.transform.position.x;
            objectState.y = obj.transform.position.y;
            objectState.z = obj.transform.position.z;
            objectState.t = frame;

            objectState.dx = obj.transform.position.x; // TODO: Do this
            objectState.y = obj.transform.position.y;
            objectState.z = obj.transform.position.z;
            objectState.t = frame;

            states[frame % maxFrames][obj] = objectState;
        }
        frame++;
    }
}
