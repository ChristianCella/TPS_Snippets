/*
This snippet allows to set the color of an object aftwer referencing it by name.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Tecnomatix.Engineering;
using Tecnomatix.Engineering.Utilities;

public class MainScript
{
	public static void Main(ref StringWriter output)
	{

		// Save the obejct after addressing it by name	
		ITxObject selected_obj = TxApplication.ActiveDocument.GetObjectsByName("Cube_02")[0];

		// Set the color       
		(selected_obj as ITxDisplayableObject).Color = TxColor.TxColorLightBlue; // Try also TxColorWhite, for example
		TxApplication.RefreshDisplay();

	}

}