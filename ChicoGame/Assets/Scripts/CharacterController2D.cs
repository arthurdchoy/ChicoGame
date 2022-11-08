using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private bool m_FacingUp = false;
	private bool m_FacingDown = false;

	public enum dir { UP, DOWN, RIGHT, LEFT };

	private Vector3 m_Velocity = Vector3.zero;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void Move(float hmove, float vmove)
	{
		Vector3 targetVelocity;

		// Move the character by finding the target velocity
		if (hmove != 0 && vmove != 0)   // character go slower when going diagonally
		{
			targetVelocity = new Vector2(hmove * 6f, vmove * 6f);
		}
		else
		{
			targetVelocity = new Vector2(hmove * 10f, vmove * 10f);
		}
		// And then smoothing it out and applying it to the character
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		if (vmove > 0 && hmove == 0)
		{
			m_FacingUp = true;
			m_FacingDown = false;

		} else if (vmove < 0 && hmove == 0)
		{
			m_FacingUp = false;
			m_FacingDown = true;
		}
		else if (hmove != 0)
		{
			m_FacingUp = false;
			m_FacingDown = false;
		}

		// If the input is moving the player right and the player is facing left...
		if (hmove > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (hmove < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}

	}

	public dir GetFacing() 
	{
		if(m_FacingUp) return dir.UP;
		else if(m_FacingDown) return dir.DOWN;
		else if(m_FacingRight) return dir.RIGHT;
		return dir.LEFT;
	}
	/*public bool FacingUp()
    {
		return m_FacingUp;
    }
	public bool FacingDown()
	{
		return m_FacingDown;
	}
	public bool FacingRight()
	{
		return m_FacingRight && !m_FacingUp && !m_FacingDown;
	}
	public bool FacingLeft()
	{
		return !m_FacingRight && !m_FacingUp && !m_FacingDown;
	}*/


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

