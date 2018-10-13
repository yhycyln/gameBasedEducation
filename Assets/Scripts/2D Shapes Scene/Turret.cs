using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	//Oyun kamerasını ekle
	public Camera gameCamera;
	public GameObject bulletPrefab;

	void Update()
	{
		Vector2 mousePosition = Input.mousePosition;
		Vector3 worldPosition = gameCamera.ScreenToWorldPoint(new Vector3 (
			mousePosition.x,
			mousePosition.y,
			this.transform.position.z - gameCamera.transform.position.z
		));

		//Açıları Düzenle
		this.transform.localEulerAngles = new Vector3(
			this.transform.localEulerAngles.x,
			this.transform.localEulerAngles.y,
			Mathf.Atan2( worldPosition.y - this.transform.position.y, worldPosition.x - this.transform.position.x  ) * Mathf.Rad2Deg 
		);

		if( Input.GetMouseButtonDown( 0 ) )
		{
			GameObject bulletObject = Instantiate( bulletPrefab );
			//bulletObject.transform.SetParent( this.parent );  //farklı bir şeyin parent'ı olsunlar
			bulletObject.transform.position = this.transform.position;

			Bullet bullet = bulletObject.GetComponent<Bullet> ();
			bullet.movementDirection = this.transform.right;
		}
	}
}
