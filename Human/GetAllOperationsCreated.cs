/*
This snippet allows to save in a TxObjectList all the operations that have been created in the document. 
*/

using System;
using System.IO;
using System.Windows.Forms;
using Tecnomatix.Engineering;

public class MainScript
{
   
    public static void MainWithOutput(ref StringWriter output)
    {
    
    	// Initialize a variable taht contains the root of the operations
    	TxOperationRoot variable = TxApplication.ActiveDocument.OperationRoot;
    	
    	// Specify the type of operations that we want to get
    	TxTypeFilter filter = new TxTypeFilter(typeof(ITxOperation));
        filter.AddIncludedType(typeof(TxHumanTsbSimulationOperation));
    	filter.AddIncludedType(typeof(TxContinuousRoboticOperation));
    	
    	// Get the list of operations: do not use 'GetAllDescendants' because it also gives the points inside the operations
    	TxObjectList List = variable.GetDirectDescendants(filter);
    	
    	// Display the names to check if they are correct
    	for (int ii = 0; ii < List.Count; ii ++)
    	{
    		int new_idx = ii + 1;
    		output.Write("The opeeration number " + new_idx + " is called: " + List[ii].Name.ToString() + "\n");
    	}
 
    }
}
