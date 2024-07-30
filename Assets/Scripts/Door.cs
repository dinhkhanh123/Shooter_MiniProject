using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float moveSpeed ; 
    [SerializeField] private float doorMoveDistance ;
    [SerializeField] private int indexDoor;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpening = false;
    private bool isClosing = false;



    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition - new Vector3(0, doorMoveDistance, 0);
    }

    public void Open()
    {
        if (Reference.levelManager.roomCurrent == (indexDoor - 1) && Reference.levelManager.currentEnemyAlive == 0)
        {
            if (!isOpening && !isClosing) 
            {
                Reference.levelManager.OpenDoor();
                StartCoroutine(MoveDoorDown());
            }
        }
    }

    public void Close()
    {
        if (!isClosing && !isOpening) 
        {
            StartCoroutine(MoveDoorUp());
        }
    }


    private IEnumerator MoveDoorDown()
    {
        isOpening = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition; 
        isOpening = false;
    }

    private IEnumerator MoveDoorUp()
    {
        isClosing = true;

        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = initialPosition;
        isClosing = false;
    }


}
