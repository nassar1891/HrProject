namespace HrProject.ViewModels
{
	public class PermissionFormViewModel
	{
        public string? RoleId { get; set; }
        public string RoleName { get; set; }
        public List<CheckBoxViewModel> RoleClaims { get; set; }
    }
}
