using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.EventSystems;


public class LinesDrawer : MonoBehaviour {
	public float currTime;
	public GameObject ObjectNull;
	public static LinesDrawer ins;
	public GameObject Cube;
	public int maxPoint = 100;
	private void Awake() {
		ins = this;
	}
	public GameObject linePrefab;
	public LayerMask cantDrawOverLayer;
	public LayerMask cantDrawOverLayer1;
	public LayerMask cantDrawOverLayer2;
	int cantDrawOverLayerIndex;
	int cantDrawOverLayerIndex1;
	int cantDrawOverLayerIndex2;

	[Space ( 30f )]
	public Gradient lineColor;
	public float linePointsMinDistance;
	public float lineWidth;

	public Line currentLine;

	Camera cam;
	public bool isDestroy = false;
	private LineRenderer lineRend;
	private EdgeCollider2D edgeCollider2D;


	void Start ( ) {
		cam = Camera.main;
		cantDrawOverLayerIndex = LayerMask.NameToLayer ( "CantDrawOver" );
		lineRend = GetComponent<LineRenderer>();
		//cantDrawOverLayerIndex = LayerMask.NameToLayer ( "Platform" );
		//Debug.Log(cantDrawOverLayerIndex);
		edgeCollider2D = GetComponent<EdgeCollider2D>();
	}
	public bool isBtnDown = false;
	public bool isCanDraw = true;
	public Vector2 firstMousePosition;
	void Update ( ) {
		
	
		//   if (EventSystem.current == null || EventSystem.current.IsPointerOverGameObject(1))    // is the touch on the GUI
        //     {
        //         // GUI Action
        //         return;
                
        //     }
		// else{
			
		if(isCanDraw){
			Vector2 mousePosition = cam.ScreenToWorldPoint ( Input.mousePosition );
			firstMousePosition = mousePosition;
			RaycastHit2D hit = Physics2D.CircleCast ( mousePosition, lineWidth / 20f, Vector2.zero, .01f, cantDrawOverLayer );
			if (Input.GetMouseButtonDown(0) && !hit ){
			//  mousePosition = cam.ScreenToWorldPoint ( Input.mousePosition );
			// 	transform.position = mousePosition;
			BeginDraw();
			isBtnDown = true;
			
		}
		if (currentLine != null)
		{
			Draw();
		}
		if (Input.GetMouseButtonUp(0) && isBtnDown){
			//Line.ins.Destroy();
			EndDraw();
			isCanDraw = false;
			GameManager.ins.StartAttack();
			//Target.ins.StartGame();

			Line.ins.CreateNewLine();
			Cube.transform.position = new Vector3(transform.position.x, transform.position.y, Cube.transform.position.z);
			Cube.GetComponent<MeshFilter>().mesh = Line.ins.lineBakeMesh;
			//EdgeCollider2D edge = Cube.AddComponent<EdgeCollider2D>();
			//Debug.Log(currentLine.GetComponent<EdgeCollider2D>().points.Count());
			//edge.points = currentLine.GetComponent<EdgeCollider2D>().points;
			//edge.edgeRadius = 0.03f;
			GameObject objectNull = Instantiate(ObjectNull, Cube.GetComponent<MeshFilter>().mesh.bounds.center, Quaternion.identity);
			Line.ins.ChildOfObjectNull(objectNull);
			//Cube.transform.parent = objectNull.transform;
			//currentLine.gameObject.transform.parent = objectNull.transform;
			objectNull.GetComponent<Rigidbody2D>().gravityScale = .8f;
			// if(edge.points.Count() == 2){
			// 	Destroy(Cube.gameObject);
			// }
			Destroy(Cube.gameObject);
			Target.ins.StartGame();


			// Start Count down
			currTime = Time.time;
			

			// Delete Line Touch Platform
			DeleteLineTouchPlatform();
		}
		}
		//}
	}

	// Begin Draw ----------------------------------------------
	void BeginDraw ( ) {
		//currentLine = Instantiate ( linePrefab, this.transform.position, Quaternion.identity ).GetComponent<Line>();
		//currentLine = Instantiate ( linePrefab, new Vector2(0, 5f), Quaternion.identity ).GetComponent<Line>();
		currentLine = Instantiate ( linePrefab, this.transform ).GetComponent<Line>();

		//Set line properties
		//currentLine.UsePhysics ( false );
		currentLine.SetLineColor ( lineColor );
		currentLine.SetPointsMinDistance ( linePointsMinDistance );
		currentLine.SetLineWidth ( lineWidth );
	}
	// Draw ----------------------------------------------------
	Vector2 initMousePosition;
	private bool isBool = true;
	private bool isNewPoint = true;
	private Vector2 newPos;
	public bool isTouchPlatform = false;
	RaycastHit2D hitPlatform;
	public bool boolObj = false;
	void Draw ( ) {
		
		Vector2 mousePosition = cam.ScreenToWorldPoint ( Input.mousePosition );

		//Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw( )
		RaycastHit2D hit = Physics2D.CircleCast ( mousePosition, lineWidth / 20f, Vector2.zero, .01f, cantDrawOverLayer );
		if ( hit  ){
				boolObj = true;
				//EndDraw ( );
				Debug.Log("cham thanh");
		}
		if(boolObj){
			if(isBool){
					initMousePosition = currentLine.GetLastPoint();
					//Debug.LogWarning(currentLine.GetLastPoint());
					lineRend.SetPosition(0, initMousePosition);
					isBool = false;	
				}
				
				if(isNewPoint && mousePosition != initMousePosition){
					newPos = mousePosition;
					lineRend.SetPosition(1, newPos );
					//Debug.Log("cham thanh");
					isNewPoint = false;
				}
				if(newPos != mousePosition){
					isNewPoint = true;
				}
		}
		 hitPlatform = Physics2D.Raycast(initMousePosition, (newPos - initMousePosition).normalized, (newPos - initMousePosition).magnitude, cantDrawOverLayer);
		if(hitPlatform && boolObj){
						isTouchPlatform = true;
					}
		else{
						isTouchPlatform = false;
					}
		// else if(hit1){
		// 	EndDraw();
		// }hit2)
		// }
		 if(!hit  && currentLine.points.Count() < maxPoint && !isTouchPlatform) 
		{
			
			currentLine.AddPoint ( mousePosition );
			// Update do dai cua day
			UIManager.ins.PointUpdate(maxPoint - currentLine.points.Count(), maxPoint);
			// Xoa day ko ve dc
			isBool = true;
			//lineRend.SetPosition(1, initMousePosition);
		}
		// else{
		// 	EndDraw();
		// }
	}
	// End Draw ------------------------------------------------
	void EndDraw ( ) {
		if ( currentLine != null && !isDestroy ) {
			if ( currentLine.pointsCount < 2 ) {
				//If line has one point
				Destroy ( currentLine.gameObject );
			} 
			else {
				
				//Add the line to "CantDrawOver" layer
				currentLine.gameObject.layer = cantDrawOverLayerIndex;
				//Activate Physics on the line
				//currentLine.UsePhysics ( true );
				currentLine = null;

		
			}
		}
	}
	public void DeleteLineTouchPlatform(){
		lineRend.positionCount = 0;
	}
	
}
