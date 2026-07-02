using UnityEngine;


public class CorazonUI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private bool estaActivo;

    public void ActivarCorazon()
    {
        estaActivo = true;
       
    }

    public void DesactivarCorazon()
    {
        animator.SetTrigger("des");
        estaActivo = false;
       
    }

    public bool EstaActivo() => estaActivo;
}