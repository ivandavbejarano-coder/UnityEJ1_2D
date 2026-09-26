using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorPersonaje2D : MonoBehaviour
{
    // Propiedades públicas / expuestas en el Inspector (PascalCase)
    public float Velocidad = 5.0f;
    public float FuerzaSalto = 8.0f;

    // Campos privados de la clase (camelCase con guion bajo)
    private Rigidbody2D _rb;
    private bool _tocandoSuelo;
    private Vector3 _escalaOriginal; // Guarda el tamaño original que definiste en el Inspector

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        // Guardamos la escala que el personaje tiene al iniciar el juego
        _escalaOriginal = transform.localScale;
    }

    private void Update()
    {
        // 1. Movimiento horizontal con el nuevo Input System
        float movimientoH = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                movimientoH = 1f;
            }
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                movimientoH = -1f;
            }
        }

        // Aplicación del movimiento físico usando linearVelocity
        _rb.linearVelocity = new Vector2(movimientoH * Velocidad, _rb.linearVelocity.y);

        // Voltear el sprite multiplicando la escala original por la dirección (1 o -1)
        if (movimientoH > 0)
        {
            transform.localScale = new Vector3(_escalaOriginal.x, _escalaOriginal.y, _escalaOriginal.z);
        }
        else if (movimientoH < 0)
        {
            transform.localScale = new Vector3(-_escalaOriginal.x, _escalaOriginal.y, _escalaOriginal.z);
        }

        // 2. Control del salto adaptado al nuevo sistema
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && _tocandoSuelo)
        {
            _rb.AddForce(Vector2.up * FuerzaSalto, ForceMode2D.Impulse);
            _tocandoSuelo = false;
        }
    }

    // 3. Detección de colisiones para reestablecer el salto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            _tocandoSuelo = true;
        }
    }
}
