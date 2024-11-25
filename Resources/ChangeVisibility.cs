/*
This snippet allows to set the visibility of an object, instantiated with the class TxComponent.
* 'Blank()' makes the object invisible 
* 'Display()' makes it visible.
*/

using System;
using System.IO;
using System.Windows.Forms;
using Tecnomatix.Engineering;

public class MainScript
{
    
    public static void MainWithOutput(ref StringWriter output)
    {
    	// Get the component by name
        TxComponent target_object = TxApplication.ActiveDocument.
		GetObjectsByName("Additional_part")[0] as TxComponent;
		
		// Set the visibiliy of the object
		//target_object.Blank();
		target_object.Display();
		
		// Refresh the display
		TxApplication.RefreshDisplay();
		
		// Display a message
        output.Write("The script worked fine!");      
    }
}
