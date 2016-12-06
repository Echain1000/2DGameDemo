using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (Character2D))]

public class EnemyControl2D : MonoBehaviour {

	private Character2D m_Character;
	private bool m_Jump;
	private bool m_Attack;

	private void Awake()
	{
		m_Character = GetComponent<Character2D>();
	}

	private void FixedUpdate()
	{
		// Read the inputs.
		//bool crouch = Input.GetKey(KeyCode.LeftControl);
		bool crouch=false;
		//float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float h = 0f;
		// Pass all parameters to the character control script.
		m_Character.Move(h, crouch, m_Jump,m_Attack);
		m_Jump = false;
		m_Attack = true;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
