using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastController : MonoBehaviour
{
    [SerializeField]
    private float raycastDistance = 5.0f;

    [SerializeField]
    LayerMask layerMask;
    public TextMeshProUGUI interactionInfo;

    private void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastDistance, layerMask))
        {
            if(hit.collider.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactionInfo.gameObject.SetActive(true);
                interactionInfo.text = interactable.id;
                if (Input.GetMouseButtonDown(0)) 
                {
                    interactable.Interact();
                }
            }
       
        }
        else
        {
            interactionInfo.gameObject.SetActive(false);
        }
    }
}