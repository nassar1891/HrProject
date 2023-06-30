namespace HrProject.Global
{
	public static class Permissions
	{
		public static List<string> GeneratePermissionList(string module)
		{
			return new List<string>()
			{
				$"Permission.{module}.Create",
				$"Permission.{module}.View",
				$"Permission.{module}.Edit",
				$"Permission.{module}.Delete"
			};
		}
	}
}
