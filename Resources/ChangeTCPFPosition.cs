// Copyright 2019 Siemens Industry Software Ltd.
using System;
using System.IO;
using System.Windows.Forms;
using Tecnomatix.Engineering;

public class MainScript
{
    

    public static void MainWithOutput(ref StringWriter output)
    {	
    	// Save the robot instance (the index may change) 
    	TxObjectList selectedObjects = TxApplication.ActiveSelection.GetItems();
		selectedObjects = TxApplication.ActiveDocument.GetObjectsByName("UR5e");
    	TxRobot rob = selectedObjects[0] as TxRobot;
    	
    	
    	// Save the frame instance  (write "TOOLFRAME" to restire the initial position)
    	TxObjectList selectedObjects1 = TxApplication.ActiveSelection.GetItems();
		selectedObjects1 = TxApplication.ActiveDocument.GetObjectsByName("fr1");
    	TxFrame fram = selectedObjects1[0] as TxFrame;
    	
    	// Impose the new position to TCPF
    	rob.TCPF.AbsoluteLocation = fram.AbsoluteLocation;
    	
        output.Write("Hello World!");
    }
}
