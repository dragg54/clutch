﻿using clutch_identity.Data;
using clutch_identity.Entities;
using clutch_identity.Requests;
using clutch_identity.Resources;
using System.Runtime.CompilerServices;

namespace clutch_identity.Extensions
{
    public static class UserExtension
    {
        public static Users ToAddUserRequest(this PostUserRequest request)
        {
            return new Users
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = UserManager.HashPassword(request.Password),
                Role = (Role)Enum.Parse(typeof(Role), request.Role),
                ActiveDate = DateTime.Now
            };
        }

        public static UserResource ToUserResource(this Users user)
        {
            return new UserResource
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role.ToString(),
                ActiveDate = user.ActiveDate
            };
        }

        public static List<UserResource> ToUserResources(this List<Users> users)
        {
            return users.Select(user => new UserResource
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role.ToString(),
                ActiveDate = user.ActiveDate
            }).ToList();
        }

        public static void ToAmendUserRequest(this PutUserRequest request, Users existingUser)
        {
                existingUser.FirstName = request.FirstName;
                existingUser.LastName = request.LastName;
                existingUser.Email = request.Email;
                existingUser.Role = (Role)Enum.Parse(typeof(Role), request.Role);
        }

        public static List<UserResource> ToUserResources(this IQueryable<Users> users)
        {
            return users.Select(user => new UserResource
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role.ToString(),
                ActiveDate = user.ActiveDate
            }).ToList();
        }
    }
}
