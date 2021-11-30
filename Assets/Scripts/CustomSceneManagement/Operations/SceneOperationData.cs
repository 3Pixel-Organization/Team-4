using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSceneManagement.Operations
{
	public enum SceneOperationBehavior
	{
		Unload,
		Reload,
		Load
	}

	public struct SceneOperationParms
	{
		public SceneDataSO scene;
		public SceneOperationBehavior operationBehavior;
	}
}
