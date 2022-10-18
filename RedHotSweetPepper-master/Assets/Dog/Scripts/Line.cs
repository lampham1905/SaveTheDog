//using System.Numerics;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
public class Line : MonoBehaviour {
	public static Line ins;
	private void Awake() {
		ins = this;
	}
	
	//Create a new Line
	public Mesh lineBakeMesh;
	//public MeshFilter mesh;
	

	public void CreateNewLine(){
		lineBakeMesh = new Mesh();
		lineRenderer.BakeMesh(lineBakeMesh, true);
		//Debug.Log("Bake");
	}
	public void ChildOfObjectNull(GameObject objectNull){
		this.gameObject.transform.parent = objectNull.transform;
	}

	public LineRenderer lineRenderer;
	public EdgeCollider2D edgeCollider;
	

	[HideInInspector] public List<Vector2> points = new List<Vector2> ( );
	[HideInInspector] public int pointsCount = 0;

	//The minimum distance between line's points.
	float pointsMinDistance = 0.1f;

	//Circle collider added to each line's point
	float circleColliderRadius;

	private void Start() {
		 

		//  lineRenderer.BakeMesh(lineBakedMesh, true);
		// mesh.mesh = lineBakedMesh;
	}
	public void AddPoint ( Vector2 newPoint ) {
		//If distance between last point and new point is less than pointsMinDistance do nothing (return)
		if (pointsCount >= 1 && Vector2.Distance ( newPoint, GetLastPoint ( ) ) < pointsMinDistance )
			return;
		points.Add ( newPoint );
		pointsCount++;
		//Add Circle Collider to the Point
		CircleCollider2D circleCollider = this.gameObject.AddComponent <CircleCollider2D> ( );
		circleCollider.offset = newPoint;
		circleCollider.radius = circleColliderRadius;
		Destroy(circleCollider);
		//Line Renderer
		lineRenderer.positionCount = pointsCount;
		lineRenderer.SetPosition ( pointsCount - 1, newPoint );

		//Edge Collider
		//Edge colliders accept only 2 points or more (we can't create an edge with one point :D )
		if ( pointsCount > 1 )
			edgeCollider.points = points.ToArray ( );
	}

	public Vector2 GetLastPoint ( ) {
		return (Vector2 )lineRenderer.GetPosition ( pointsCount - 1 );
	}

	// public void UsePhysics ( bool usePhysics ) {
	// 	// isKinematic = true  means that this rigidbody is not affected by Unity's physics engine
	// 	rigidBody.isKinematic = !usePhysics;
	// }

	public void SetLineColor ( Gradient colorGradient ) {
		lineRenderer.colorGradient = colorGradient;
	}
	public void SetPointsMinDistance ( float distance ) {
		pointsMinDistance = distance;
	}
	public void SetLineWidth ( float width ) {
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;
		circleColliderRadius = width / 2f;
		//edgeCollider.edgeRadius = circleColliderRadius;
		edgeCollider.edgeRadius = .03f;
	}
	public void Destroy(){
		LinesDrawer.ins.isDestroy = true;
		Destroy(this.gameObject);
	}
	public Vector2[] Edge2D(){
		Vector2[] vectorEdge = GetComponent<EdgeCollider2D>().points;
		return vectorEdge;
	}
	public void DetroySelf(){
		this.gameObject.SetActive(false);
	}
	
}