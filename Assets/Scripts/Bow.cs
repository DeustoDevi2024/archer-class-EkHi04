using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Archer
{
    public class Bow : MonoBehaviour
    {
        [SerializeField] private InputActionReference fireInputReference;
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private float force;
        [SerializeField] private Transform handPosition;
        private Animator animator;

        private void Awake()
        {
            fireInputReference.action.performed += Action_performed;
            animator = GetComponent<Animator>();
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            StartCoroutine(Shoot());
        }

        private IEnumerator Shoot()
        {
            yield return new WaitForSeconds(0.3f);

            // Instanciar una flecha
            GameObject arrow = Instantiate(arrowPrefab, handPosition.position, Quaternion.identity);
            
            // Orientar la flecha hacia delante con respecto a la arquera
            arrow.transform.forward = transform.forward;

            // Aplicar una fuerza a la flecha para que salga disparada
            Rigidbody arrowRigidbody = arrow.GetComponent<Rigidbody>();
            if (arrowRigidbody != null)
            {
                arrowRigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("El prefab de flecha no tiene un Rigidbody adjunto.");
            }
        }
    }
}
