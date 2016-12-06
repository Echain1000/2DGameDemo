using UnityEngine;
using System.Collections;

public class attackDetection : MonoBehaviour
{

	private Transform judge1;
	private Transform judge2;
	private Transform edge;
	private GameObject characterSelf;
	private Character2D character_;
	private Vector3 point1;
	private Vector3 point2;
	private ArrayList hitList=new ArrayList();

	public LayerMask m_hitTarget;
	public ParticleSystem particle;

	private void Awake ()
	{
		judge1 = transform.Find ("judge1");
		judge2 = transform.Find ("judge2");
		edge = transform.Find ("edge");
		character_ = GetComponentInParent<Character2D> ();
		characterSelf = gameObject.transform.parent.gameObject;
	}

	private void hitMessage (Character2D obj, int type)
	{
		if (type == 1) {
			if (!obj.hasBeenHitThisTime) {
				if (obj.attactState == 2) {
					obj.hasBeenHitThisTime = true;
					hitList.Add (obj);
					obj.SendMessage ("OnHitWeapon", null);
					particle.GetComponent<Transform>().position= edge.position;
					particle.Play ();
				}
			}
		} else if (type == 2) {
			if (!obj.hasBeenHitThisTime) {
				obj.SendMessage ("OnHitPlayer", null);
				obj.hasBeenHitThisTime = true;
				hitList.Add (obj);
			}
		}
	}

	private void hitRecover ()
	{
		if (hitList.Count == 0)
			return;
		else {
			for (int i = 0; i < hitList.Count; i++) {
				Character2D obj = hitList [i] as Character2D; //question is this obj automatically a pointer?
				obj.hasBeenHitThisTime = false;
				hitList.RemoveAt (i);
			}
		}
	}

	private void FixedUpdate ()
	{
		if (character_.attactState == 1) {
			hitRecover ();
			return;
		}
		point1 = judge1.position;
		point2 = judge2.position;
		Collider2D[] colliders = Physics2D.OverlapAreaAll (point1, point2, m_hitTarget);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders [i].gameObject != gameObject && colliders [i].gameObject != characterSelf) {
				switch (colliders [i].gameObject.tag) {
				case "weapon":
					{
						hitMessage (colliders [i].gameObject.GetComponentInParent<Character2D> (), 1);
						break;
					}
				case"Player":
					{
						hitMessage (colliders [i].gameObject.GetComponentInParent<Character2D> (), 2);
						break;
					}
				default:
					break;
				}
			}
		}
		
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
