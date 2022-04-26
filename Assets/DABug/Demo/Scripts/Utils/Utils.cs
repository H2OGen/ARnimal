using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.EventSystems;
using DArts;

namespace DArts {


public static class Utils {
	
	// Convert Hex value to Color ==========
	public static Color32 hex2Color(string hex_str) {
		byte r = 100;
		byte g = 100;
		byte b = 100;
		if (hex_str.Length==6) {
			r = byte.Parse(hex_str.Substring(0, 2), NumberStyles.HexNumber);
			g = byte.Parse(hex_str.Substring(2, 2), NumberStyles.HexNumber);
			b = byte.Parse(hex_str.Substring(4, 2), NumberStyles.HexNumber);
		}
		return new Color32(r, g, b, 1);
	}
	
}
}