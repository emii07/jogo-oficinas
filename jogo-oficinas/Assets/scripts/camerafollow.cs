using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // O alvo que a câmera vai seguir (seu personagem)
    public Transform alvo;

    // A distância que a câmera vai manter do personagem
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    // O quão suave será o movimento (valores menores = mais suave)
    [Range(0.01f, 1f)]
    public float suavidade = 0.125f;

    void LateUpdate()
    {
        if (alvo != null)
        {
            // Posição desejada baseada na posição do alvo + o offset
            Vector3 posicaoDesejada = alvo.position + offset;
            
            // Interpola suavemente entre a posição atual da câmera e a desejada
            Vector3 posicaoSuavizada = Vector3.Lerp(transform.position, posicaoDesejada, suavidade);
            
            // Aplica a nova posição à câmera
            transform.position = posicaoSuavizada;
        }
    }
}