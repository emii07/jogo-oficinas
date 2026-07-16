using UnityEngine;
using System.Collections;

public class TelaSplash : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);

        GameManager.Instancia.CarregarCena("MenuPrincipal");
    }
}