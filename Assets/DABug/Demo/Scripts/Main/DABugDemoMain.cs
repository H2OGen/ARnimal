using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DArts;

namespace DArts {


public class DABugDemoMain : MonoBehaviour {

	public void goMess(string str_in) {

		System.DateTime date_time = System.DateTime.Now;
		string str_out = (str_in=="now")? "Now: " + date_time.ToString(): str_in;

		switch (str_in) {
		case "list_str":
			DABug.Log (buildListStr());
			break;
		case "dict_str_str":
			DABug.Log (buildDictStrStr());
			break;
		case "dict_str_obj":
			DABug.Log (buildDictStrObj());
			break;
		case "int":
			DABug.Log (BuildInt());
			break;
		case "float":
			DABug.Log (BuildFloat());
			break;
		case "long_txt":
			DABug.Log (buildLongTxt()); 
			break;
		case "block_txt":
			DABug.Log (buildBlockTxt());
			break;
		case "array_str":
			DABug.Log (buildArrayStr());
			break;
		case "array_int":
			DABug.Log (buildArrayInt());
			break;
		case "list_obj":
			DABug.Log (buildListObj());
			break;
		default:
			DABug.Log(str_out);
			break;
		}
	}


	// List of Objects ==========
	private List<object> buildListObj() {
		List<object> my_list = new List<object>();
		my_list.Add("Have a nice day!");
		my_list.Add(12345);
		my_list.Add(buildDictStrStr());
		my_list.Add(buildArrayStr());
		return my_list;
	}
	
	
	// Array of integers ==========
	public int[] buildArrayInt() {
		int[] my_array = new int[10];
		for (int i=0; i<=9; i++) my_array[i] = Random.Range(0,1000000);
		return my_array;
	}
	
	
	// Array of strings ==========
	public string[] buildArrayStr() {
		string[] my_array = {"Antelope", "Brown Bear", "Chimpanzee", "Dolphin", "Elephant", "Flamingo", "Giraffe"};
		return my_array;
	}
	
	
	// Integer ==========
	private int BuildInt() {
		int n = Random.Range(0,1000000);
		return n;
	}
	
	// Float ==========
	private float BuildFloat() {
		float n = Random.Range(0f,10000f);
		return n;
	}
	
	// List String ==========
	private List<string> buildListStr() {
		List<string> my_list = new List<string>();
		my_list.Add("Apple");
		my_list.Add("Orange");
		my_list.Add("Lemon");
		my_list.Add("Banana");
		my_list.Add("Peach");
		my_list.Add("Cherry");
		return my_list;
	}

	// Dictionary string string ==========
	private Dictionary<string, string> buildDictStrStr() {
		Dictionary<string, string> my_dict = new Dictionary<string, string>();
		my_dict.Add("Ag", "Silver");
		my_dict.Add("Au", "Gold");
		my_dict.Add("Ba", "Barium");
		my_dict.Add("H", "Hydrogen");
		my_dict.Add("O", "Oxygen");
		my_dict.Add("Pb", "Lead");
		my_dict.Add("U", "Uranium");
		my_dict.Add("Zn", "Zinc");
		return my_dict;
	}

	// Dictionary string object ==========
	private Dictionary<string, object> buildDictStrObj() {
		Dictionary<string, object> my_dict = new Dictionary<string, object>();
		
		Dictionary<string, string> my_record_0 = new Dictionary<string, string>();
		my_record_0.Add("Name","Dexter Campbell");
		my_record_0.Add("Addr","110 S. Wabash");
		my_record_0.Add("City","Chicago");
		my_record_0.Add("State","IL");
		my_record_0.Add("Zip","60603");
		Dictionary<string, string> my_record_1 = new Dictionary<string, string>();
		my_record_1.Add("Name","Rosa Lucas");
		my_record_1.Add("Addr","166 Central Ave.");
		my_record_1.Add("City","Charleston");
		my_record_1.Add("State","SC");
		my_record_1.Add("Zip","29406");
		Dictionary<string, string> my_record_2 = new Dictionary<string, string>();
		my_record_2.Add("Name","Molly Weber");
		my_record_2.Add("Addr","609 S. 5th Street");
		my_record_2.Add("City","Clarksburg");
		my_record_2.Add("State","WV");
		my_record_2.Add("Zip","26301");
		Dictionary<string, string> my_record_3 = new Dictionary<string, string>();
		my_record_3.Add("Name","Laurence Santos");
		my_record_3.Add("Addr","251 S Madison Ave");
		my_record_3.Add("City","Yuma");
		my_record_3.Add("State","AZ");
		my_record_3.Add("Zip","85364");

		my_dict.Add("2000", my_record_0);
		my_dict.Add("2001", my_record_1);
		my_dict.Add("2002", my_record_2);
		my_dict.Add("2003", my_record_3);

		return my_dict;
	}



	// Block of Solid Text ==========
	private string buildBlockTxt() {
		string str_1 = @"Line 001: 01234567890123456789012345678901234567890123456789012345678901234567890123456789
Line 002: 01234567890123456789012345678901234567890123456789012345678901234567890123456789
Line 003: 01234567890123456789012345678901234567890123456789012345678901234567890123456789
Line 004: 01234567890123456789012345678901234567890123456789012345678901234567890123456789
Line 005: 01234567890123456789012345678901234567890123456789012345678901234567890123456789
Line 006: 01234567890123456789012345678901234567890123456789012345678901234567890123456789
Line 007: 01234567890123456789012345678901234567890123456789012345678901234567890123456789
Line 008: 01234567890123456789012345678901234567890123456789012345678901234567890123456789
Line 009: 01234567890123456789012345678901234567890123456789012345678901234567890123456789
Line 010: 01234567890123456789012345678901234567890123456789012345678901234567890123456789";	
		return str_1;
	}




	// Loooong Text ==========
	private string buildLongTxt() {
		string str_1 = @"Long text begins here.

This product is meant for educational purposes only.

Any resemblance to real persons, living or dead is purely coincidental.
Void where prohibited.
Some assembly required.
List each check separately by bank number.
Batteries not included.

Contents may settle during shipment.
Use only as directed.
No other warranty expressed or implied.
Do not use while operating a motor vehicle or heavy equipment.
Postage will be paid by addressee.
Subject to CARB approval.

This is not an offer to sell securities.
Apply only to affected area.
May be too intense for some viewers.
Do not stamp.
Not rated by the Motion Picture Association of America.
Call for nutritional information.
Use other side for additional listings.

Printed on recycled paper.
For recreational use only.
Do not disturb.
All models over 18 years of age.
Prize not redeemable for cash value.
If condition persists, consult your physician.
No user-serviceable parts inside.
Freshest if eaten before date on carton.

To be used as a supplementary restraint system only.
Always fasten your safety belt.
Subject to change without notice.
Times approximate.
Simulated picture.
Do not staple or paper clip.
Price slightly higher east of Alaska.
No postage necessary if mailed in the United States.

Do not X-ray.
Breaking seal constitutes acceptance of agreement.
For off-road use only.
As seen on TV.
One size fits all.
Many suitcases look alike.
Contains a substantial amount of non-tobacco ingredients.
Colors may, in time, fade.

We have sent the forms which seem right for you.
Magnetic media, non-returnable if seal is broken.
Formatted to fit your screen.
Slippery when wet.
For office use only.
Not affiliated with the American Red Cross.
Drop in any mailbox.
Edited for television.

Keep cool, process promptly.
Post office will not deliver without postage.
List was current at time of printing.
Return to sender, no forwarding order on file, unable to forward.
Prolong exposure to vapors has caused cancer in laboratory animals.

Not responsible for direct, indirect, incidental or consequential damages resulting from any defect, error or failure to perform.
Keep away from children.
At participating locations only.
Not the Beatles.
Penalty for private use.
See label for sequence.

Substantial penalty for early withdrawal.
Do not write below this line.
Falling rock.
Lost ticket pays maximum rate.
Phenylketonurics: contains phenylalnine.
Your canceled check is your receipt.
Add toner.
Place stamp here.

Use only as directed; intentional misuse by deliberately concentrating and inhaling contents can be harmful or fatal.
Avoid contact with skin.
Road construction ahead.
Open other end.
Dealer participation may affect final price.

May not be present in all tap water.
Sanitized for your protection.
Be sure each item is properly endorsed.
Sign here without admitting guilt.
Slightly higher west of the Mississippi.
Park at your own risk.
Employees and their families and friends are not eligible.
Beware of dog.

Contestants have been briefed on some questions before the show.
Limited time offer, call now to ensure prompt delivery.
You must be present to win.
No passes accepted for this engagement.
No purchase necessary.
Processed at location stamped in code at top of
carton.
Shading within a garment may occur.
Keep away from fire or flames.
See Uniform Code of Military Justice.
Replace with same type.
Approved for veterans.
Booths for two or more.
Indicates a low-fat item.
Check here if tax deductible.
Some equipment shown is optional.

Price does not include taxes.
No Canadian coins.
Tax, tag, and title not included in advertised price.
Not recommended for children.
Prerecorded for this time zone.
Reproduction by mechanical or electronic means, including photocopying, is strictly prohibited.

No solicitors.
No alcohol, dogs or horses.
No anchovies unless otherwise specified.
Avoid spraying into eyes.
An 18% gratuity will be added for parties of 8 or more.";

		return str_1;
	}



}
}