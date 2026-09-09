using SQLInjectionDemo.Models;

namespace SQLInjectionDemo.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        var administratorRole = context.Roles.SingleOrDefault(role => role.RoleName == "Administrator");
        if (administratorRole is null)
        {
            administratorRole = new Role { RoleName = "Administrator" };
            context.Roles.Add(administratorRole);
        }

        var memberRole = context.Roles.SingleOrDefault(role => role.RoleName == "Member");
        if (memberRole is null)
        {
            memberRole = new Role { RoleName = "Member" };
            context.Roles.Add(memberRole);
        }

        context.SaveChanges();

        AddUserIfMissing(context, administratorRole.RoleID, "admin", "admin@example.local", "admin123", "Administrator demo account");
        AddUserIfMissing(context, memberRole.RoleID, "member", "member@example.local", "member123", "Member demo account");
        context.SaveChanges();
    }

    private static void AddUserIfMissing(
        AppDbContext context,
        int roleId,
        string userName,
        string email,
        string password,
        string bio)
    {
        if (context.Users.Any(user => user.UserName == userName))
        {
            return;
        }

        // Plain-text credentials are intentional in this local security demonstration.
        context.Users.Add(new User
        {
            RoleID = roleId,
            UserName = userName,
            EmailAdd = email,
            Password = password,
            Bio = bio
        });
    }
}
