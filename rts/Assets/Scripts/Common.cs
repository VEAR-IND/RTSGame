using UnityEngine;
using System.Collections;

public class Common : MonoBehaviour {

	//float to bool
	public static bool FloatToBool(float Float)
	{
		if (Float < 0f) return false; else return true;
	}

	//unsign a Float
	public static float Unsigned(float Val) //Val - value
	{
		if (Val < 0f)Val *= -1;
		return Val;
	}


	//are the shift keys being held on ?
	public static bool ShiftKeysDown()
	{
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
			return true;
		else
			return false;
	}

}
