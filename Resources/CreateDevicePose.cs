/*
This snippet allows to create a new pose for a device in the Tecnomatix Plant Simulation software. It was used to create the poses
for the robot base on te Camozzi line, but with some small adaptatioons it can be used to create poses also for
the fingers of the grippers.
*/
 
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tecnomatix.Engineering;
using Tecnomatix.Engineering.Olp;
using Tecnomatix.Engineering.Plc;
 
public class MainScript
{
    public static void Main(ref StringWriter output)
    {
        
        // Define parameters
        double robot_base_position = 1300; // mm
        string device_name = "Line";
        string pose_name = "BasePose";

        // Get the device by name
		TxObjectList selectedObjects = TxApplication.ActiveSelection.GetItems();
        selectedObjects = TxApplication.ActiveDocument.GetObjectsByName(device_name);
        TxDevice line_device = selectedObjects[0] as TxDevice;

        // Create a new pose
        TxPoseData openposeData = new TxPoseData();
        ArrayList openarraylist = new ArrayList();
        openarraylist.Add(robot_base_position);
        openposeData.JointValues = openarraylist;
        TxPoseCreationData NewPose = new TxPoseCreationData(pose_name, openposeData);
        TxPose new_base_pose = line_device.CreatePose(NewPose);


    }	
}
