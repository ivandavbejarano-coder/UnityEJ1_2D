using UnityEngine;

public class ControladorPersonaje2D : MonoBehaviour
{
    public float Velocidad = 5.0f;
    public float FuerzaSalto = 8.0f;

    private Rigidbody2D _rb;
    private bool _tocandoSuelo;

    private void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 1. Movimiento horizontal
        float movimientoH = Input.GetAxis("Horizontal");

        // Uso de la propiedad linearVelocity 
        _rb.linearVelocity = new Vector2(movimientoH * Velocidad, _rb.linearVelocity.y);

        // Voltear el sprite cambiando la escala
        if (movimientoH > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movimientoH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // 2. Control del salto
        if (Input.GetButtonDown("Jump") && _tocandoSuelo)
        {
            _rb.AddForce(Vector2.up * FuerzaSalto, ForceMode2D.Impulse);
            _tocandoSuelo = false;
        }
    }

    // 3. Métodos de eventos 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            _tocandoSuelo = true;
        }
    }
}
