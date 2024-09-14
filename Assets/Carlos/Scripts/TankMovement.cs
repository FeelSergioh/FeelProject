using System.ComponentModel;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    
#region Fields
        
    [Range(1, 10)] public float speed = 10f;
    [Range(0, 10)] public float acceleration = 3f;
    [Range(1, 100)] public int reductionPercent = 60;
    [Range(90, 360)] public float turnSpeed = 180f;
    public Rigidbody2D rb2D;
    
    private float currentSpeed = 0f;
    
#endregion

#region Properties

    private float _reduction = -1f;
    public float Reduction 
    {
        get
        {
            if (_reduction == -1f)
            {
                _reduction = reductionPercent / 100f;
            }
            return _reduction;
        }
    }

#endregion

#region Unity Methods

    private void FixedUpdate()
    {
        Rotate();
        Movement();
    }
    
#endregion

#region Methods
    
    private void Rotate()
    {
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -turn);
    }

    private void Movement()
    {
        var rotationZ = transform.rotation.eulerAngles.z;
        rotationZ = Mathf.Abs(Mathf.Cos(rotationZ * Mathf.Deg2Rad));

        float reductionSpeed =  Reduction + (1 - Reduction) * rotationZ;

        float moveInput = Input.GetAxis("Vertical");
        float targetSpeed = moveInput * speed * reductionSpeed;

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        // Aplicar el movimiento usando Rigidbody2D
        rb2D.velocity = rb2D.transform.right * currentSpeed;

        // Ajustar la velocidad para evitar el movimiento no deseado
        rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, speed);
    }

#endregion

}
