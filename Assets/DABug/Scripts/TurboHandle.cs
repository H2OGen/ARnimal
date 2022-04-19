// DABug Handle in Turbo Mode ==========

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DArts;

namespace DArts {
	
public class TurboHandle : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler {

	private float parent_height = 0;
	private float off_y = 0;
	private float new_y = 0;
	private float my_height = 0;
	private RectTransform my_rect;
	private RectTransform parent_rect;
	private Dictionary<string, object> drag_command = new Dictionary<string, object>();


	void Start() {
		my_rect		= GetComponent<RectTransform>();
		parent_rect	= transform.parent.GetComponent<RectTransform>();
		parent_height = parent_rect.rect.height;
		drag_command["#DABug#"]		= "turbo drag";
	}


	// Set Height using line count and tot count ==========
	public void setHeight(float qty_lines, float tot_lines) {

		float pct_full = qty_lines / tot_lines;

		my_height = Mathf.Clamp((1-pct_full) * parent_height, 40, parent_height); 

		//Debug.Log("setHtPos q:" + qty_lines + " t:" + tot_lines + " %:" + pct_full + " h:" + my_height);
		//Debug.Log("%pos:" + pct_position);

		if (my_rect!=null) my_rect.sizeDelta = new Vector2(0, my_height);

	}


	// Set Position using % ==========
	public void setPos(float pct_position) {

		float new_y = Mathf.Clamp((1-pct_position) * (parent_height - my_height), 0, parent_height - my_height);

		//Debug.Log("setpos pct:" + pct_position + " ph:" + parent_height + " my:" + my_height + " new_y:" + new_y);

		transform.localPosition = new Vector2(0, new_y);
	}



	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		Transform ref_rect = gameObject.transform;
		Vector2 localPoint;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(ref_rect.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);

		off_y = localPoint.y;

		parent_height	= parent_rect.rect.height;
		my_height		= GetComponent<RectTransform>().rect.height;

		//Debug.Log("down, parent_ht:" + parent_height + " my_height:" + my_height);


	}

	#endregion



	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData) 	{

		Transform ref_rect = gameObject.transform.parent;
		Vector2 localPoint;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(ref_rect.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);

		new_y = Mathf.Clamp(localPoint.y - off_y, 0, parent_height - my_height -1);
		transform.localPosition = new Vector2(0, new_y);

		drag_command["track_ht"]	= parent_height;
		drag_command["handle_ht"]	= my_height;
		drag_command["handle_pos"]	= new_y;
		DABug.Log(drag_command);

	}
	#endregion


	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData) 	{
		//DABug.Log("end drag");
	}

	#endregion

}
}