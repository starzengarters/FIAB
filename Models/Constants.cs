namespace FIAB.Models
{
	public sealed class Constants
	{
		public sealed class Roles
		{
			public static string User = "User";
			public static string Admin= "Admin";
			public static string UserOrAdmin = $"{User}, {Admin}";
		}

		public sealed class Messages
		{
			public static string RoleRequiredUser = $"You must have the {Roles.User} role to view this page.";
		}
	}

}