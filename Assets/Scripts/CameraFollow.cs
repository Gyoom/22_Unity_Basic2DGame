
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float timeOffset; // decalage en seconde entre le debut du mouvement de player et le suivi de la cam (optionnel)
    [SerializeField] private Vector3 posOffset; // Z donne du recul, Y de la hauteur, X decale à gauche/droite
    [SerializeField] private Vector3 velocity; // recupère la valeur pour affichage ds Menu Unity
    
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
        // SmootDamp() permet de deplacer un objet vers un position à une certaine vitesse
    }
}
