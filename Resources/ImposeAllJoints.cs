/*
This snippet allows to set all the joint values at the same time.
This script can be useful in the optimization of the screwing operation.
*/

using System;
using System.IO;
using System.Windows.Forms;
using Tecnomatix.Engineering;
using Tecnomatix.Engineering.Plc;
using System.Collections;

public class MainScript
{
	public static void Main(ref StringWriter output)
	{	
		
		// Save the robot (the index may change)  	
    	TxObjectList logicResources = TxApplication.ActiveDocument.GetObjectsByName("UR5e");
		ITxPlcLogicResource logicResource = logicResources[0] as ITxPlcLogicResource;		
		TxPlcLogicBehavior logicBehavior = logicResource.LogicBehavior;
		
		// store a specific joint of the robot		
		TxObjectList drivingJoints = (logicResource as ITxDevice).DrivingJoints;
        TxJoint j1 = drivingJoints[0] as TxJoint;
        TxJoint j2 = drivingJoints[1] as TxJoint;
        TxJoint j3 = drivingJoints[2] as TxJoint;
        TxJoint j4 = drivingJoints[3] as TxJoint;
        TxJoint j5 = drivingJoints[4] as TxJoint;
        TxJoint j6 = drivingJoints[5] as TxJoint;
		
		// Change the values at the same time       
        double Rot1 = -Math.PI / 2;
        double Rot2 = -Math.PI / 2;
        double Rot3 = -Math.PI / 2;
        double Rot4 = -Math.PI / 2;
        double Rot5 = Math.PI / 2;
        double Rot6 = 0;
        
        j1.CurrentValue = Rot1;
        j2.CurrentValue = Rot2;
        j3.CurrentValue = Rot3;
        j4.CurrentValue = Rot4;
        j5.CurrentValue = Rot5;
        j6.CurrentValue = Rot6;	
               
        // Refresh the display	
		TxApplication.RefreshDisplay();
		
		// Display all the joint values
		output.WriteLine("Joint 1: " + j1.CurrentValue.ToString() + " rad");
		output.WriteLine("Joint 2: " + j2.CurrentValue.ToString() + " rad");
		output.WriteLine("Joint 3: " + j3.CurrentValue.ToString() + " rad");
		output.WriteLine("Joint 4: " + j4.CurrentValue.ToString() + " rad");
		output.WriteLine("Joint 5: " + j5.CurrentValue.ToString() + " rad");
		output.WriteLine("Joint 6: " + j6.CurrentValue.ToString() + " rad");

	}
}
