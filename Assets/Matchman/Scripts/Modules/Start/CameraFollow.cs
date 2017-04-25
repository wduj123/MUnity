// ========================================================
// 描 述：CameraFollow.cs 
// 作 者：郑贤春 
// 时 间：2017/02/28 09:21:47 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    public float xMargin = 1f;
    [SerializeField]
    public float yMargin = 1f;
    [SerializeField]
    public float xSmooth = 8f;
    [SerializeField]
    public float ySmooth = 8f;
    [SerializeField]
    private Transform maxXAndYTarget;
    [SerializeField]
    private Transform minXAndYTarget;
    [SerializeField]
    private Vector2 maxXAndY;
    [SerializeField]
    private Vector2 minXAndY;

    [SerializeField]
	private Transform player;

	//
	void Awake()
    {
        maxXAndY = maxXAndYTarget.transform.position;
        minXAndY = minXAndYTarget.transform.position;
	}

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //
    bool CheckXMargin(){
		return Mathf.Abs (transform.position.x - player.position.x) > xMargin;
	}
	bool CheckYMargin(){
		return Mathf.Abs (transform.position.y - player.position.y) > yMargin;
	}

	void FixedUpdate(){
		TrackPlayer();
	}

	void TrackPlayer(){
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		if (CheckXMargin()) {
			targetX = Mathf.Lerp (transform.position.x, player.position.x, xSmooth * Time.deltaTime);
		}
		if (CheckYMargin()) {
			targetY = Mathf.Lerp (transform.position.y, player.position.y, ySmooth * Time.deltaTime);
		}
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
		transform.position = new Vector3 (targetX, targetY, transform.position.z);
		//Debug.Log (player.position.y);
	}

	
}
