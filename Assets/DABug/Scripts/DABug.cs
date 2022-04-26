// Dynamic Arts DABug v4.50: Debug Message Log Window 2016-12-01

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System;
using UnityEngine.SceneManagement;
using DArts;

namespace DArts {

// DABug v4.50 ==========
public class DABug : MonoBehaviour {

	[SerializeField]
	[Tooltip("Enable DABug window at startup")]
	private bool m_ShowAtStartup	= true;
	public bool ShowAtStartup		{ get { return m_ShowAtStartup; } set { m_ShowAtStartup = value; } }

	[SerializeField]
	[Tooltip("Enable the Hide (X) button")]
	private bool m_ShowHideButton	= true;
	public bool ShowHideButton		{ get { return m_ShowHideButton; } set { m_ShowHideButton = value; } }

	[SerializeField]
	[Tooltip("Do Not Destroy on Scene Change")]
	private bool m_UseInAllScenes	= true;
	public bool UseInAllScenes		{ get { return m_UseInAllScenes; } set { m_UseInAllScenes = value; } }

	public enum EPos{TopLeft, TopRight, BotLeft, BotRight};
	[SerializeField]
	[Tooltip("Position of DABug window at startup")]
	private EPos m_InitalPosition = EPos.BotLeft;
	public EPos InitialPosition		{ get { return m_InitalPosition; } set { m_InitalPosition = value; } }

	[SerializeField]
	[Tooltip("Text font size")]
	[Range(18,36)]
	private int m_FontSize = 24;
	public int FontSize { get { return m_FontSize; } set { m_FontSize = value; } }

	[SerializeField]
	[Tooltip("Total DABug window buffer size line count")]
	[Range(10,2000)]
	private int m_BufferSize = 80;
	public int BufferSize { get { return m_BufferSize; } set { m_BufferSize = value; } }
	
	[SerializeField]
	[Tooltip("Lines to display before switching to Turbo Mode")]
	[Range(30,120)]
	private int m_TurboLineCount = 80;
	public int TurboLineCount { get { return m_TurboLineCount; } set { m_TurboLineCount = value; } }
	
	private GameObject go_main;
	private GameObject pnl_shadow;
	private GameObject pnl_main;
	private GameObject pnl_control; 
	private GameObject btn_hide;
	private GameObject slider_h;
	private GameObject pnl_turbo;
	private GameObject pnl_logo;

	private List<string> types	= new List<string>();
	private static bool loaded	= false;
	private bool active			= false;
	private Text txt_box;
	private float off_x, off_y;
	private int buffer_size;
	private bool dragging = false;

	private System.Text.StringBuilder str_builder = new System.Text.StringBuilder();
	private bool turbo			= false;
	private int turbo_win_size	= 10;
	private int turbo_beg_line	= 0;
	private TurboHandle turbo_script;


	private List<string> lines	= new List<string>();
	private int max_line_len		= 100;


	// Event Delegate/Handler
	private delegate void myHandler (object obj1);
	private static event myHandler DABugMessEvent;

	
	// Public Interface: Send Debug Message to DABug Window ==========
	public static void Log(object obj1) {
		if (DABugMessEvent != null) DABugMessEvent(obj1);
	}		


	// Wake up! Singleton check ==========
	private void Awake() {
		if (loaded) {
			Destroy(this.gameObject);
		} else {
			if (m_UseInAllScenes) DontDestroyOnLoad(this.gameObject);
		}
	}


	// Level has Changed, build new Fly ==========
	private void mySceneChanged(UnityEngine.SceneManagement.Scene s1, UnityEngine.SceneManagement.Scene s2) {
		if (active) buildFly();
	}

	// Destroy
	private void OnDestroy() {
		DABugMessEvent -= addMess; // Message Listener
		SceneManager.activeSceneChanged -= mySceneChanged; // Scene Listener
		if (active) loaded = false;
	}

	// Start ==========
	private void Start() {
		loaded = true;
		active = true;
		buildFly();

		go_main		= transform.Find("GOMain").gameObject;
		txt_box		= transform.Find ("GOMain/Canvas/PnlMain/PnlMess/ScrollView/Viewport/Text").GetComponent<Text>();
		pnl_main	= this.gameObject.transform.Find("GOMain/Canvas/PnlMain").gameObject;
		pnl_shadow	= this.gameObject.transform.Find("GOMain/Canvas/PnlMain/PnlShadow").gameObject;
		pnl_control	= this.gameObject.transform.Find("GOMain/Canvas/PnlMain/PnlControl").gameObject;
		pnl_turbo	= this.gameObject.transform.Find("GOMain/Canvas/PnlMain/PnlTurbo").gameObject;
		pnl_logo	= this.gameObject.transform.Find("GOMain/Canvas/PnlMain/PnlLogo").gameObject;
		btn_hide	= this.gameObject.transform.Find("GOMain/Canvas/PnlMain/PnlControl/ImgHide").gameObject;
		slider_h	= this.gameObject.transform.Find("GOMain/Canvas/PnlMain/PnlMess/ScrollView/Scrollbar_H").gameObject;
		turbo_script= pnl_turbo.transform.Find("PnlTrack/BtnHandle").GetComponent<TurboHandle>();

		DABugMessEvent += addMess; // Message Listener
		SceneManager.activeSceneChanged += mySceneChanged; // Scene Listener

		setPosition(InitialPosition);
		pnl_control.SetActive(false);
		setTurbo(false);
		showPnlLogo(false);

		pnl_shadow.transform.SetAsFirstSibling();
		pnl_shadow.SetActive(false);

		btn_hide.SetActive(m_ShowHideButton);

		setFontSize(m_FontSize);

		this.gameObject.transform.SetAsLastSibling(); // Place on top
		go_main.SetActive(m_ShowAtStartup);

		types.Add("System.String");
		types.Add("System.Collections.Generic.List`1[System.String]");
		types.Add("System.Collections.Generic.Dictionary`2[System.String,System.String]");
		types.Add("System.Collections.Generic.Dictionary`2[System.String,System.Object]");
		types.Add("System.String[]");
		types.Add("System.Int32[]");
		types.Add("System.Collections.Generic.List`1[System.Object]");

	}


	// Add Message ==========
	private void addMess(object obj_mess) {

		string mess_str		= "";
		string mess_type	= obj_mess.GetType().ToString();
		int type_idx		= types.IndexOf(mess_type);

		switch (type_idx) {
		
		// String
		case 0:
			mess_str = obj_mess.ToString();
			if (!catchKeywords(mess_str)) add2Box(mess_str);
			break;
		
		// List<string>
		case 1:
			List<string> temp_list = obj_mess as List<string>;
			for (int i=0; i<temp_list.Count; i++) {
				add2Box (i.ToString() + ": " + temp_list[i]);
			}
			break;
		
		// Dictionary<string, string>
		case 2:
			Dictionary<string, string> temp_dict_str = obj_mess as Dictionary<string, string>;
			foreach(KeyValuePair<string, string> item in temp_dict_str) {
				add2Box(item.Key + ": " + item.Value);
			}
			break;
		
		// Dictionary<string, object>
		case 3:
			Dictionary<string, object> temp_dict_obj = obj_mess as Dictionary<string, object>;
			if (temp_dict_obj.ContainsKey("#DABug#")) {
				specialCommand(temp_dict_obj);
			} else {
				foreach(KeyValuePair<string, object> item in temp_dict_obj) {
					add2Box(item.Key + ": ");
					DABug.Log(item.Value);
				}
			}
			break;
		
		// string[] array
		case 4: 
			string[] temp_array_str = obj_mess as string[];
			for(int i=0; i<temp_array_str.Length; i++) {
				DABug.Log(i.ToString() + ": " + temp_array_str[i]);
			}
			break;
		
		// int[] array
		case 5: 
			int[] temp_array_int = obj_mess as int[];
			for(int i=0; i<temp_array_int.Length; i++) {
				DABug.Log(i.ToString() + ": " + temp_array_int[i].ToString());
			}
			break;
		
		// List<object>
		case 6:
			List<object> temp_list_obj = obj_mess as List<object>;
			for (int i=0; i<temp_list_obj.Count; i++) {
				add2Box (i.ToString() + ":");
				DABug.Log (temp_list_obj[i]);
			}
			break;
		
		// Type Not recognized yet
		default:
			add2Box(obj_mess.ToString());
			break;
		}

	}





	// Add string to Text Box (Rolling max length) if Visible ==========
	private void add2Box(string new_str) {
		int beg = 0;
		int qty = 0;

		if (go_main.activeSelf) {

			Regex.Replace(new_str, @"\r", "\n");
			string[] array1 = new_str.Split(new string[] { "\n" }, StringSplitOptions.None);

			foreach (string dirty_str in array1) {
				string s = dirty_str;
				if (s.Length>max_line_len) {
					while (s.Length> max_line_len) {
						lines.Add(s.Substring(0,max_line_len));
						s = s.Substring(max_line_len);
					}
					if (s.Length>0) lines.Add(s);
				} else {
					lines.Add(s);
				}
			}

			while (lines.Count>m_BufferSize) lines.RemoveAt(0);


			if (lines.Count > m_TurboLineCount) setTurbo(true);
			int win_siz = (turbo)? turbo_win_size: m_BufferSize;

			beg = Mathf.Max(0, lines.Count-win_siz); 
			qty = Mathf.Min(lines.Count - beg, win_siz);

			str_builder.Remove(0,str_builder.Length);
			for (int i = beg; i<beg + qty; i++) {
				str_builder.AppendLine(lines[i]);
			}

			txt_box.text = str_builder.ToString();
			
		}
		
		txt_box.transform.localPosition = new Vector2(10,0);

		if (turbo) {
			turbo_script.setHeight(lines.Count, m_BufferSize);
			turbo_script.setPos(1);
			turbo_beg_line = beg;

		}
	}
	
	



	// Catch Keywords ==========
	private bool catchKeywords(string str1) {
		bool answer = true;

		switch (str1) {
		case "#show#":
			go_main.SetActive(true);
			break;
		case "#hide#":
			go_main.SetActive(false);
			break;
		case "#flip#":
			go_main.SetActive(!go_main.activeSelf);
			break;
		case "#clear#":
			str_builder.Remove(0,str_builder.Length);
			txt_box.text = "";
			lines.Clear();
			setTurbo(false);
			break;
		case "#copy#":
			copyText();
			break;
		case "#bl#":
			setPosition(EPos.BotLeft);
			break;
		case "#br#":
			setPosition(EPos.BotRight);
			break;
		case "#tl#":
			setPosition(EPos.TopLeft);
			break;
		case "#tr#":
			setPosition(EPos.TopRight);
			break;
		default:
			answer = false;
			break;
		}

		return answer;
	}


	// Special Command ==========
		private void specialCommand(Dictionary<string, object> command) {

		switch(command["#DABug#"].ToString()) {
		case "turbo drag":
			turboDrag(command);
			break;
		default:
			DABug.Log("bad DABug command");
			break;
		}
	}


	// Enable/Disable Turbo Mode ==========
	private void setTurbo(bool new_state) {
		if (new_state) {
			if (!turbo) {	
				turbo = true;
				pnl_turbo.SetActive(true);
			}
		} else {
			turbo = false;
			pnl_turbo.SetActive(false);
		}
	}
	

	// Drag Turbo Handle ==========
	private void turboDrag(Dictionary<string, object> command) {

		float track_ht		= 0;
		float handle_ht		= 0;
		float handle_pos	= 0;
		bool ok				= true;

		try {
			float.TryParse(command["track_ht"].ToString(), out track_ht);
			float.TryParse(command["handle_ht"].ToString(), out handle_ht);
			float.TryParse(command["handle_pos"].ToString(), out handle_pos);
		} catch (System.Exception ex) {
			ok = false;
			DABug.Log("bad turbo drag command");
			DABug.Log(ex.Message);
		}


		if (ok) {

			float pct = 1 - (handle_pos / (track_ht - handle_ht));
			float beg_line = lines.Count * pct;

			int beg_page = (int) Math.Floor(beg_line / turbo_win_size);

			turbo_beg_line = beg_page * turbo_win_size;
			if (turbo_beg_line > lines.Count - turbo_win_size - 1) turbo_beg_line = lines.Count - turbo_win_size;

			str_builder.Remove(0,str_builder.Length);
			for (int i = turbo_beg_line; i<turbo_beg_line + turbo_win_size; i++) {
				str_builder.AppendLine(lines[i]);
			}
			
			txt_box.text = str_builder.ToString();
		}

	}


	// Click on Turbo Up/Down Page Button ==========
	public void movePage(int how_many) {

		turbo_beg_line += how_many * turbo_win_size;
		if (turbo_beg_line < 0) turbo_beg_line = 0;

		float pct = (float) turbo_beg_line / lines.Count;

		if (turbo_beg_line > lines.Count - turbo_win_size - 1) {
			turbo_beg_line = lines.Count - turbo_win_size; 
			pct = 1;	
		}

		turbo_script.setPos(pct);

		str_builder.Remove(0,str_builder.Length);
		for (int i = turbo_beg_line; i<turbo_beg_line + turbo_win_size; i++) {
			str_builder.AppendLine(lines[i]);
		}
		
		txt_box.text = str_builder.ToString();

	}


	// Create Fly Object ==========
	private void buildFly() {
		GameObject fly = Instantiate(Resources.Load("DAFly_DABug", typeof(GameObject))) as GameObject;
		fly.GetComponent<DAFly_DABug>().init(this.gameObject);
	}

	// Show Logo ==========
	public void showPnlLogo(bool show) {
		pnl_logo.SetActive(show);
		hiLogo(false);
	}

	// Logo Effect ==========
	public void hiLogo(bool show) {
		Transform tf = pnl_logo.transform.Find("BtnLogoVisit");
		float new_val = (show)? 1.5f: 1f;
		tf.localScale = new Vector3(new_val, new_val, new_val);
	}

	// Visit DA site ==========
	public void visitSite() {
		Application.OpenURL ("http://dynamicarts.com/"); 
	}


	//	Copy text to clipboard ==========
	private void copyText() {

		int beg = Mathf.Max(0, lines.Count-m_BufferSize); 
		int qty = Mathf.Min(lines.Count - beg, m_BufferSize);
		
		str_builder.Remove(0,str_builder.Length);
		for (int i = beg; i<beg + qty; i++) str_builder.AppendLine(lines[i]);

		TextEditor te = new TextEditor();
		te.text = str_builder.ToString();
		te.SelectAll();
		te.Copy();
	}


	//	Start Drag ==========
	public void begDrag() {
		dragging = true;
		off_x = Input.mousePosition.x - pnl_main.transform.position.x;
		off_y = Input.mousePosition.y - pnl_main.transform.position.y;
		pnl_shadow.SetActive(true);
	}


	//	Keep Dragging ==========
	public void keepDragging() {
		float x = Input.mousePosition.x - off_x;
		float y = Input.mousePosition.y - off_y;
		pnl_main.transform.position = new Vector2(x,y);
	}


	// Shadow ==========
	public void endDrag() {
		dragging = false;
		pnl_shadow.SetActive(false);
	}


	// Show/Hide Control Panel ==========
	public void showPnlControl(bool mode) {
		if (!dragging) {
			pnl_control.SetActive(mode);
			pnl_shadow.SetActive(false);
		}
	}

	// Set Horiz Position ==========
	public void setHorizHome() {

		float new_val = slider_h.GetComponent<Scrollbar>().value;
		if (new_val==0) {
			txt_box.transform.localPosition = new Vector2(10,txt_box.transform.localPosition.y);
		}
	}


	// Public Access for buttons ==========
	public void fire(string str_1) {
		DABug.Log (str_1);
	}


	// Set Position ==========
	private void setPosition(EPos new_pos) {
		RectTransform rt = pnl_main.GetComponent<RectTransform>();

		switch (new_pos) {
		case EPos.TopLeft:
			rt.anchorMin	= new Vector2(0,1);
			rt.anchorMax	= new Vector2(0,1);
			rt.pivot		= new Vector2(0,1);
			rt.anchoredPosition = new Vector2(0,-90);
			break;
		case EPos.TopRight:
			rt.anchorMin	= new Vector2(1,1);
			rt.anchorMax	= new Vector2(1,1);
			rt.pivot		= new Vector2(1,1);
			rt.anchoredPosition = new Vector2(0,-90);
			break;
		case EPos.BotLeft:
			rt.anchorMin	= new Vector2(0,0);
			rt.anchorMax	= new Vector2(0,0);
			rt.pivot		= new Vector2(0,0);
			rt.anchoredPosition = new Vector2(0,0);
			break;
		case EPos.BotRight:
			rt.anchorMin	= new Vector2(1,0);
			rt.anchorMax	= new Vector2(1,0);
			rt.pivot		= new Vector2(1,0);
			rt.anchoredPosition = new Vector2(0,0);
			break;
		default:
			break;
		}
	
	}



	// Set Initial Font Size ==========
	private void setFontSize(int new_size) {
		txt_box.fontSize = new_size;
		float height = pnl_turbo.GetComponent<RectTransform>().rect.height;
		turbo_win_size	= (int) (height * 1.1f/ new_size) - 1;
	}
 

}


}
